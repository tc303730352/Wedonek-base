<template>
  <el-dialog
    :title="title"
    :visible="visible"
    class="empTitleDialog"
    width="1000px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <div v-if="isPower" style="text-align: right; padding-bottom: 10px">
      <el-button
        type="primary"
        size="mini"
        @click="addTitle"
      >添加职务</el-button>
    </div>
    <el-table :data="titles" style="width: 100%">
      <el-table-column type="index" label="序号" width="50" />
      <el-table-column
        prop="CompanyName"
        align="center"
        label="公司名"
        min-width="200"
      />
      <el-table-column
        prop="UnitName"
        align="center"
        label="单位"
        min-width="200"
      />
      <el-table-column
        prop="DeptName"
        align="center"
        label="部门"
        min-width="200"
      />
      <el-table-column
        prop="Title"
        label="职务"
        align="center"
        min-width="200"
      />
      <el-table-column prop="action" label="操作">
        <template slot-scope="scope">
          <el-button
            v-if="isPower"
            size="mini"
            type="danger"
            icon="el-icon-delete"
            circle
            @click="dropTitle(scope.row)"
          />
        </template>
      </el-table-column>
    </el-table>
    <addEmpTitle :emp-id="empId" :visible="addVisible" @close="closeAdd" />
  </el-dialog>
</template>

<script>
import moment from 'moment'
import * as empTitleApi from '@/api/emp/empTitle'
import addEmpTitle from './components/addEmpTitle.vue'
export default {
  components: {
    addEmpTitle
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    empId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      titles: [],
      title: '人员职务',
      isPower: false,
      isRefresh: false,
      addVisible: false
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.load()
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.initPower()
  },
  methods: {
    moment,
    async initPower() {
      this.isPower = await this.$checkPower(['hr.emp.title.set']).length === 1
    },
    addTitle() {
      this.addVisible = true
    },
    dropTitle(row) {
      const title = '确认删除职务 ' + row.CompanyName + '-' + row.Title + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row.Id)
      })
    },
    async submitDrop(id) {
      await empTitleApi.deleteTitle(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.isRefresh = true
      this.titles = this.titles.filter((c) => c.Id !== id)
    },
    closeAdd(isRefresh) {
      this.addVisible = false
      if (isRefresh) {
        this.isRefresh = true
        this.load()
      }
    },
    closeForm() {
      this.$emit('cancel', this.isRefresh)
    },
    async load() {
      this.titles = await empTitleApi.gets(this.empId, this.comId)
    }
  }
}
</script>
<style>
.empTitleDialog .el-dialog__body {
  padding-top: 10px !important;
}
</style>
