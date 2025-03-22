<template>
  <el-card>
    <div slot="header">
      <span>角色列表</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="角色名">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="角色名"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="success" @click="addRole">添加角色</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="roles"
      :columns="columns"
      :is-paging="true"
      :paging="paging"
      @load="load"
    >
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          :value="e.row.IsEnable"
          :disabled="e.row.IsDefRole"
          :inactive-value="false"
          :active-value="true"
          @change="(val) => setIsEnable(e.row, val)"
        />
      </template>
      <template slot="IsAdmin" slot-scope="e">
        <el-switch
          :value="e.row.IsAdmin"
          :inactive-value="false"
          :active-value="true"
          @change="(val) => setIsAdmin(e.row, val)"
        />
      </template>
      <template slot="EmpNum" slot-scope="e">
        <el-button type="text" @click="showEmp(e.row)">{{ e.row.EmpNum }}</el-button>
      </template>
      <template slot="IsDefRole" slot-scope="e">
        <el-switch
          :value="e.row.IsDefRole"
          :disabled="e.row.IsEnable == false"
          :inactive-value="false"
          :active-value="true"
          @change="(val) => setIsDefRole(e.row, val)"
        />
      </template>
      <template slot="AddTime" slot-scope="e">
        {{ moment(e.row.AddTime).format("YYYY-MM-DD HH:mm") }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="显示"
          icon="el-icon-view"
          circle
          @click="showView(e.row)"
        />
        <el-button
          v-if="e.row.IsEnable == false"
          size="mini"
          type="primary"
          title="编辑角色"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
        <el-button
          v-if="e.row.IsEnable == false"
          size="mini"
          type="danger"
          title="删除角色"
          icon="el-icon-delete"
          circle
          @click="dropRole(e.row)"
        />
      </template>
    </w-table>
    <editRole :visible="visable" :role-id="roleId" @close="close" />
    <roleView :visible="viewVisable" :role-id="roleId" @close="viewVisable=false" />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as roleApi from '@/api/role/role'
import editRole from './components/editRole.vue'
import roleView from './components/roleView.vue'
export default {
  components: {
    editRole,
    roleView
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  data() {
    return {
      roles: [],
      queryParam: {
        QueryKey: null
      },
      visable: false,
      viewVisable: false,
      roleId: null,
      columns: [
        {
          sortby: 'RoleName',
          key: 'RoleName',
          title: '角色名',
          align: 'center',
          minWidth: 150,
          sortable: 'custom'
        },
        {
          sortby: 'Remark',
          key: 'Remark',
          title: '备注',
          align: 'center',
          minWidth: 200,
          sortable: 'custom'
        },
        {
          sortby: 'IsDefRole',
          key: 'IsDefRole',
          title: '默认角色',
          align: 'center',
          slotName: 'IsDefRole',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'IsAdmin',
          key: 'IsAdmin',
          title: '是否为管理员',
          align: 'center',
          slotName: 'IsAdmin',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'IsEnable',
          key: 'IsEnable',
          title: '是否启用',
          align: 'center',
          minWidth: 100,
          slotName: 'IsEnable',
          sortable: 'custom'
        },
        {
          key: 'EmpNum',
          title: '员工数',
          align: 'right',
          minWidth: 100,
          slotName: 'EmpNum'
        },
        {
          key: 'AddTime',
          title: '添加时间',
          align: 'center',
          minWidth: 120,
          slotName: 'AddTime'
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          fixed: 'right',
          width: '150px',
          slotName: 'action'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  mounted() {
    this.load()
  },
  methods: {
    moment,
    showEmp(row) {
      this.$router.push({ name: 'user', query: { roleId: row.Id }})
    },
    addRole() {
      this.roleId = null
      this.visable = true
    },
    edit(row) {
      this.roleId = row.Id
      this.visable = true
    },
    close(isRefresh) {
      this.visable = false
      if (isRefresh) {
        this.reset()
      }
    },
    showView(row) {
      this.roleId = row.Id
      this.viewVisable = true
    },
    async setIsEnable(row, isEnable) {
      await roleApi.setIsEnable(row.Id, isEnable)
      row.IsEnable = isEnable
    },
    async setIsAdmin(row, isAdmin) {
      await roleApi.setIsAdmin(row.Id, isAdmin)
      row.IsAdmin = isAdmin
    },
    async setIsDefRole(row, isDef) {
      if (!isDef) {
        this.$message({
          type: 'error',
          message: '必须存在一个默认角色!'
        })
        return
      }
      await roleApi.setIsDefRole(row.Id)
      const def = this.roles.find((c) => c.IsDefRole)
      def.IsDefRole = def
    },
    dropRole(row) {
      const title = '确认删除角色 ' + row.RoleName + '?'
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
      await roleApi.deleteRole(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.paging.Index = 1
      this.load()
    },
    async load() {
      this.queryParam.CompanyId = this.comId
      const res = await roleApi.query(this.queryParam, this.paging)
      this.roles = res.List
      this.paging.Total = res.Count
    }
  }
}
</script>
