<template>
  <el-form :label-width="labelWidth+'px'" class="formContent">
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
import draggable from 'vuedraggable'
export default {
  components: {
    draggable
  },
  props: {
    controls: {
      type: Array,
      default: null
    },
    labelWidth: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      list: [],
      controlList: [],
      id: 1,
      isRefresh: 0,
      listControl: {
        name: 'control',
        pull: false,
        put: true
      }
    }
  },
  watch: {
    controls: {
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
      this.list = this.controls
    },
    addControl(e) {
      const index = e.newIndex
      const t = Object.assign({}, this.list[index])
      t.Id = this.id
      this.id = this.id + 1
      this.list[index] = t
      this.controlList.push(t)
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
