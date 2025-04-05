<template>
  <div>
    <el-card class="table">
      <div slot="header" class="header">
        <div class="title">
          <el-divider direction="vertical" />
          {{ table.Title }}
        </div>
        <div class="opBtn">
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="edit" />
          <el-button
            type="danger"
            size="mini"
            icon="el-icon-delete"
            @click="drop"
          />
        </div>
      </div>
      <formContent :form-id="formId" :table="table" :label-width="table.labelWidth" />
    </el-card>
    <editFormTable :id="table.Id" :visible.sync="visible" @close="close" />
  </div>
</template>

<script>
import formContent from './formContent.vue'
import * as tableApi from '@/customForm/api/table'
import editFormTable from './editFormTable.vue'
export default {
  components: {
    formContent,
    editFormTable
  },
  props: {
    table: {
      type: Object,
      default: null
    },
    formId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      visible: false
    }
  },
  mounted() {},
  methods: {
    close(isSet, table) {
      if (isSet) {
        this.table.Title = table.Title
        this.table.IsHidden = table.IsHidden
        this.table.ColNum = table.ColNum
        this.table.LabelWidth = table.LabelWidth
      }
    },
    edit() {
      this.visible = true
    },
    drop() {
      const title = '确认删除表单 ' + this.table.Title + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop()
      })
    },
    async submitDrop() {
      await tableApi.Delete(this.table.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.$emit('remove', this.table.Id)
    }
  }
}
</script>

<style scoped>
.table {
  margin-bottom: 10px;
}
.table .header {
  text-align: left;
}
.table .header .opBtn {
  float: right;
}
.table .header .title {
  font-size: 1.1rem;
  display: inline-block;
}
.table .header .title .el-divider--vertical {
  width: 4px;
  margin: 0;
  height: 1.2em;
  background-color: #0095ff;
}
.table .formContent {
  width: 100%;
}
</style>
