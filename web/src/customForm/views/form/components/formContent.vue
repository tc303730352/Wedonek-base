<template>
  <el-form :label-width="labelWidth" class="formContent">
    <el-row :gutter="24">
      <draggable v-model="conList" class="draggable" :sort="true" :group="listControl" @end="endSort" @add="addControl">
        <transition-group class="draggable-group">
          <el-col v-for="item in controlList" :key="item.Id" :span="item.ColSpan">
            <el-form-item :label="item.ColTitle">
              <el-input />
              <div class="opBtn">
                <el-button type="text" icon="el-icon-edit" />
                <el-button type="text" icon="el-icon-delete" />
                <el-button type="text" icon="el-icon-s-unfold" />
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
    table: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      conList: [],
      controlList: [],
      labelWidth: '80px',
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
    }
  },
  mounted() {
  },
  methods: {
    moment,
    reset() {
      if (this.table.Columns != null) {
        this.conList = this.table.Columns
      } else {
        this.conList = []
      }
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
      t.Id = id
      add.Id = id
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
        Id: t.Id,
        Sort: e.oldIndex
      })
      data.push({
        Id: old.Id,
        Sort: e.newIndex
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
