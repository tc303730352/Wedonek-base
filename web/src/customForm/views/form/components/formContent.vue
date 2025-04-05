<template>
  <el-form :label-width="labelWidth+'px'" class="formContent">
    <el-row :gutter="24">
      <draggable v-model="conList" class="draggable" :sort="true" :group="listControl" @end="endSort" @add="addControl">
        <transition-group class="draggable-group">
          <el-col v-for="item in controlList" :key="item.ColId" :span="item.ColSpan">
            <el-form-item :label="item.ColTitle">
              <el-input />
              <div class="opBtn">
                <el-button type="text" icon="el-icon-edit" />
                <el-button type="text" icon="el-icon-delete" @click="remove(item)" />
                <el-button v-if="item.ColSpan != minSpan" type="text" icon="el-icon-s-fold" @click="setSpan(item, false)" />
                <el-button v-if="item.ColSpan != maxSpan" type="text" icon="el-icon-s-unfold" @click="setSpan(item, true)" />
              </div>
            </el-form-item>
          </el-col>
        </transition-group>
      </draggable>
    </el-row>
  </el-form>
</template>

<script>
import moment from 'moment'
import { pinyin } from 'pinyin-pro'
import * as columnApi from '@/customForm/api/column'
import draggable from 'vuedraggable'
export default {
  components: {
    draggable
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
            ColId: c.Id
          }
        })
      } else {
        this.controlList = []
        this.conList = []
      }
    },
    async remove(item) {
      await columnApi.Delete(item.ColId)
      this.drop(item.ColId)
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
      let name = pinyin(title, { pattern: 'first', toneType: 'none', separator: '' })
      let i = 1
      do {
        if (this.controlList.findIndex(c => c.Name === name) === -1) {
          return name
        }
        name = name + i
        i = i + 1
      // eslint-disable-next-line no-constant-condition
      } while (true)
    },
    async addControl(e) {
      const index = e.newIndex
      const t = this.conList[index]
      const add = {
        FormId: this.formId,
        TableId: this.table.Id,
        ColSpan: 24 / this.table.ColNum,
        ControlId: t.Id,
        ColTitle: t.Name,
        ColName: this.createName(t.Name),
        ColType: t.ColType,
        Width: t.Width,
        Sort: index,
        EditControl: t.EditControl,
        ShowControl: t.ShowControl
      }
      const id = await columnApi.Add(add)
      t.ColId = id
      add.ColId = id
      this.controlList.push(add)
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
  float: right;
}
.el-col {
  cursor: pointer;
}
.formContent .el-form-item  .opBtn .el-button {
  font-size: 16px;
}
</style>
