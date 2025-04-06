<template>
  <el-form :label-width="labelWidth+'px'" :rules="rules" class="formContent">
    <el-row :gutter="24"   v-loading="loading">
      <draggable v-model="conList" class="draggable" :sort="true" :group="listControl" @end="endSort" @add="addControl">
        <transition-group class="draggable-group">
          <el-col v-for="item in controlList" :key="item.ColId" :span="item.ColSpan">
            <el-form-item :label="item.ColTitle" :prop="item.ColId">
              <formControl :control="item" />
              <div class="opBtn">
                <el-button type="text" icon="el-icon-edit" @click="edit(item)" />
                <el-button type="text" icon="el-icon-delete" @click="remove(item)" />
                <el-button v-if="item.ColSpan != minSpan" type="text" icon="el-icon-s-fold" @click="setSpan(item, false)" />
                <el-button v-if="item.ColSpan != maxSpan" type="text" icon="el-icon-s-unfold" @click="setSpan(item, true)" />
              </div>
            </el-form-item>
          </el-col>
        </transition-group>
      </draggable>
    </el-row>
    <editColumn :id="id" :visible.sync="visible" :table-type="table.TableType" @close="close" />
  </el-form>
</template>

<script>
import moment from 'moment'
import { pinyin } from 'pinyin-pro'
import * as columnApi from '@/customForm/api/column'
import draggable from 'vuedraggable'
import editColumn from './editColumn.vue'
import formControl from './formControl.vue'
export default {
  components: {
    draggable,
    editColumn,
    formControl
  },
  props: {
    formId: {
      type: String,
      default: null
    },
    labelWidth: {
      type: Number,
      default: 120
    },
    table: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      conList: [],
      controlList: [],
      maxSpan: 24,
      minSpan: 6,
      visible: false,
      id: null,
      loading: false,
      rules: [],
      listControl: {
        name: 'control',
        pull: false,
        put: true
      }
    }
  },
  watch: {
    table: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    },
    'table.ColNum': {
      handler(val) {
        if (val) {
          this.initColNum()
        }
      },
      immediate: false
    }
  },
  mounted() {
  },
  methods: {
    moment,
    edit(item) {
      this.id = item.ColId
      this.visible = true
    },
    close(isSet, data) {
      if (!isSet) {
        return
      }
      const col = this.controlList.find(a => a.ColId === this.id)
      col.ColName = data.ColName
      col.ControlId = data.ControlId
      col.ColTitle = data.ColTitle
      col.ColType = data.ColType
      col.Description = data.Description
      col.MaxLen = data.MaxLen
      col.IsNotNull = data.IsNotNull
      col.DefaultVal = data.DefaultVal
      col.EditControl = data.EditControl
      col.ShowControl = data.ShowControl
      col.ControlSet = data.ControlSet
      this.initRules()
    },
    initRules() {
      const rules = {}
      this.controlList.forEach(c => {
        if (c.IsNotNull === true) {
          rules[c.ColId] = [{
            required: true,
            message: c.ColTitle + '不能为空！',
            trigger: 'blur'
          }]
        }
      })
      this.rules = rules
    },
    async setSpan(item, isAdd) {
      if (isAdd) {
        item.ColSpan = parseInt(item.ColSpan) + this.minSpan
      } else {
        item.ColSpan = parseInt(item.ColSpan) - this.minSpan
      }
      await columnApi.SetSpan(item.ColId, item.ColSpan)
    },
    async initColNum() {
      this.minSpan = 24 / this.table.ColNum
      if (this.controlList.length === 0) {
        return
      }
      const span = []
      this.controlList.forEach(c => {
        const t = 24 / this.table.ColNum
        if (t !== c.ColSpan) {
          c.ColSpan = t
          span.push({
            Id: c.ColId,
            Value: t
          })
        }
      })
      if (span.length > 0) {
        await columnApi.SaveSpan(span)
      }
    },
    reset() {
      this.minSpan = 24 / this.table.ColNum
      if (this.table.Columns != null) {
        this.controlList = this.table.Columns
        this.conList = this.table.Columns.map(c => {
          return {
            ColId: c.ColId
          }
        })
        this.initRules()
      } else {
        this.controlList = []
        this.conList = []
        this.rules = {}
      }
    },
    async remove(item) {
      this.loading = true
      await columnApi.Delete(item.ColId)
      this.drop(item.ColId)
      this.loading = false
    },
    drop(id) {
      const index = this.controlList.findIndex(a => a.ColId === id)
      if (index === -1) {
        return
      }
      this.controlList.splice(index, 1)
      this.conList.splice(index, 1)
    },
    createName(title) {
      const name = pinyin(title, { pattern: 'first', toneType: 'none', separator: '' })
      let t = name
      let i = 1
      do {
        if (this.controlList.findIndex(c => c.ColName === t) === -1) {
          return t
        }
        t = name + i
        i = i + 1
      // eslint-disable-next-line no-constant-condition
      } while (true)
    },
    async addControl(e) {
      this.loading = true
      const index = e.newIndex
      const t = this.conList[index]
      if (t == null) {
        return
      }
      const add = {
        FormId: this.formId,
        TableId: this.table.Id,
        ColSpan: 24 / this.table.ColNum,
        ControlId: t.Id,
        ColTitle: t.Name,
        ColName: this.createName(t.Name),
        ColType: t.ControlType,
        Width: t.Width,
        Sort: index,
        EditControl: t.EditControl,
        ShowControl: t.ShowControl
      }
      const id = await columnApi.Add(add)
      add.ColId = id
      this.controlList.push(add)
      this.loading = false
    },
    async endSort(e) {
      if (this.controlList.length <= 1 || e.newIndex === e.oldIndex) {
        return
      }
      const data = []
      const arr = this.controlList
      const t = arr[e.newIndex]
      const old = arr[e.oldIndex]
      data.push({
        Id: t.ColId,
        Value: e.oldIndex
      })
      data.push({
        Id: old.ColId,
        Value: e.newIndex
      })
      await columnApi.SetSort(data)
      arr[e.oldIndex].Sort = e.newIndex
      arr[e.newIndex] = old
      arr[e.oldIndex] = t
      this.controlList = [].concat(arr)
    }
  }
}
</script>
<style scoped>
.formContent .opBtn {
  position: absolute;
  right: 0;
}
.el-col {
  cursor: pointer;
}
.formContent .el-form-item {
  height: 96px;
}
.formContent .el-form-item  .opBtn .el-button {
  font-size: 16px;
}
</style>
