<template>
  <el-form :label-width="labelWidth" class="formContent">
    <el-row :gutter="24">
      <draggable v-model="list" class="draggable" :sort="true" :group="listControl" @end="endSort" @add="addControl">
        <transition-group class="draggable-group">
          <el-col v-for="item in controlList" :key="item.Id" :span="item.Span == null ? 12 : item.Span">
            <el-form-item :label="item.Title+item.Id">
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
      type: String,
      default: null
    }
  },
  data() {
    return {
      list: [],
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
      this.list = this.table.controls
    },
    async addControl(e) {
      const index = e.newIndex
      const t = this.list[index]
      const add = {
        FormId: this.formId,
        TableId: this.table.Id,
        ColSpan: 24 / this.table.ColSpan,
        ControlId: t.Id,
        ColTitle: t.Name,
        Name: pinyin.getCamelChars(t.Name) + (index + 1),
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
    endSort(e) {
      const arr = this.controlList
      const t = arr[e.newIndex]
      arr[e.newIndex] = arr[e.oldIndex]
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
