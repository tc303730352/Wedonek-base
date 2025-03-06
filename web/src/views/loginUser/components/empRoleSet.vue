<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-table
      ref="roleTable"
      :data="roles"
      row-key="Id"
      style="width: 100%"
      @selection-change="selectChange"
    >
      <el-table-column type="selection" width="55" />
      <el-table-column
        prop="RoleName"
        align="center"
        label="角色名"
        min-width="150"
      />
      <el-table-column
        prop="Remark"
        align="center"
        label="备注"
        min-width="200"
      />
      <el-table-column
        prop="IsAdmin"
        label="是否是管理员"
        align="center"
        min-width="200"
      >
        <template slot-scope="scope">
          <span v-if="scope.row.IsAdmin">管理员</span>
        </template>
      </el-table-column>
    </el-table>
    <p v-if="roleId.length == 0" style="color: #f56c6c; text-align: center">
      至少选择一个角色!
    </p>
    <el-row slot="footer" :gutter="24" style="text-align: center">
      <el-button
        :disabled="roleId.length == 0"
        type="primary"
        @click="save"
      >保存</el-button>
      <el-button @click="closeForm">取消</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import moment from 'moment'
import { getSelect } from '@/api/role/role'
import * as empRoleApi from '@/api/role/empRole'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    title: {
      type: String,
      default: '设定人员角色'
    },
    empId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      roles: [],
      roleId: [],
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
  methods: {
    moment,
    async load() {
      this.roles = await getSelect()
      this.loadEmpRole()
    },
    async save() {
      if (this.roleId.length == 0) {
        return
      }
      await empRoleApi.set(this.empId, this.roleId)
      this.$message({
        type: 'success',
        message: '设置成功!'
      })
      this.$emit('close', true)
    },
    selectChange(rows) {
      this.roleId = rows.map((c) => c.Id)
    },
    async loadEmpRole() {
      this.roleId = await empRoleApi.get(this.empId)
      if (this.roleId.length != 0) {
        this.roles.forEach((row) => {
          if (this.roleId.includes(row.Id)) {
            this.$refs.roleTable.toggleRowSelection(row)
          }
        })
      }
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
