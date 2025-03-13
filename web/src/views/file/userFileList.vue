<template>
  <div>
    <leftRightSplit :left-span="4">
      <el-card slot="left">
        <div slot="header">
          <span>用户文件目录</span>
        </div>

      </el-card>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
        </div>
        <el-row>
          <el-form :inline="true" :model="queryParam">
            <el-form-item label="关键字">
              <el-input
                v-model="queryParam.QueryKey"
                placeholder="人员名\编号\手机号"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item label="岗位">
              <treeDicItem
                v-model="queryParam.Post"
                :dic-id="HrItemDic.post"
                :is-multiple="true"
                placeholder="人员岗位"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item label="职务">
              <dictItem
                v-model="queryParam.Title"
                :multiple="true"
                :dic-id="HrItemDic.title"
                placeholder="职务"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item label="员工类型">
              <enumItem
                v-model="queryParam.UserType"
                :dic-key="HrEnumDic.hrUserType"
                placeholder="员工类型"
                :multiple="true"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item>
              <el-checkbox
                v-model="queryParam.IsNoOpen"
                label="未开通账号"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item>
              <el-button type="success" @click="addEmp">添加员工</el-button>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <p v-if="chioseEmp.length > 0" :gutter="24">
          <span style="margin-right: 10px">已选择:{{ chioseEmp.length }}</span>
          <el-button
            type="primary"
            size="mini"
            @click="openAccount"
          >开通账号</el-button>
        </p>
        <w-table
          :data="fileList"
          :columns="columns"
          :is-paging="true"
          row-key="Id"
          :paging="paging"
          @load="loadFile"
        />
      </el-card>
    </leftRightSplit>
  </div>
</template>

<script>
import moment from 'moment'
export default {
  name: 'UserFile',
  components: {
  },
  data() {
    return {
      fileList: [],
      title: '用户文件列表',
      columns: [
        {
          key: 'EmpNo',
          title: '人员信息',
          align: 'left',
          width: 250,
          fixed: 'left',
          slotName: 'empBase',
          sortable: 'custom'
        },
        {
          sortby: 'DeptId',
          key: 'Dept',
          title: '单位部门',
          align: 'center',
          minWidth: 120,
          sortable: 'custom'
        },
        {
          sortby: 'PostCode',
          key: 'Post',
          title: '岗位',
          align: 'center',
          sortable: 'custom'
        },
        {
          key: 'DeptTitle',
          title: '职务',
          align: 'center',
          slotName: 'empTitle',
          minWidth: 150
        },
        {
          key: 'Status',
          title: '用户状态',
          align: 'center',
          slotName: 'status',
          minWidth: 120,
          sortable: 'custom'
        },
        {
          key: 'Phone',
          title: '手机号',
          align: 'center',
          width: 125,
          sortable: 'custom'
        },
        {
          key: 'Birthday',
          title: '生日',
          align: 'center',
          slotName: 'birthday',
          sortable: 'custom',
          minWidth: 100
        },
        {
          key: 'UserType',
          title: '人员类型',
          align: 'center',
          sortable: 'custom',
          slotName: 'UserType',
          minWidth: 110
        },
        {
          key: 'RegTime',
          title: '注册日期',
          align: 'center',
          slotName: 'regTime',
          sortable: 'custom',
          minWidth: 110
        },
        {
          key: 'IsOpenAccount',
          title: '是否开通账号',
          align: 'center',
          slotName: 'isOpenAccount',
          sortable: 'custom',
          fixed: 'right',
          minWidth: 130
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          fixed: 'right',
          width: '100px',
          slotName: 'action'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'EmpId',
        IsDesc: false,
        Total: 0
      },
      visible: false,
      empId: null,
      queryParam: {
        CompanyId: null,
        QueryKey: null,
        IsEntry: true,
        DeptId: null,
        UnitId: null,
        Status: null,
        UserType: null,
        Post: null,
        Title: null
      }
    }
  },
  mounted() {},
  methods: {
    moment,
    editEmpTitle(empId) {
      this.empId = empId
      this.visible = true
    },
    addEmp() {
      this.$router.push({ name: 'addEmp' })
    },
    checkIsSelect(row) {
      return (
        row.IsOpenAccount === false && row.Status === 1 && row.DeptId !== 0
      )
    },
    async setStatus(row, status) {
      await empApi.setStatus(row.EmpId, status)
      row.Status = status
      if (status !== 1 && this.chioseEmp.length > 0) {
        this.chioseEmp = this.chioseEmp.filter((c) => c !== row.EmpId)
      }
    },
    async openAccount() {
      if (this.chioseEmp.length === 0) {
        return
      }
      await open(this.queryParam.CompanyId, this.chioseEmp)
      this.$message({
        type: 'success',
        message: '开通成功!'
      })
      this.chioseEmp = []
      this.loadEmp()
    },
    selected(e) {
      this.chioseEmp = e.keys
    },
    async loadEmp() {
      const res = await empApi.queryEmp(this.queryParam, this.paging)
      if (res.List) {
        this.emps = res.List
      } else {
        this.emps = []
      }
      this.paging.Total = res.Count
    },
    dropEmp(emp) {
      const title = '确认删除员工 ' + emp.EmpName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(emp.EmpId)
      })
    },
    async submitDrop(id) {
      await empApi.deleteEmp(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.chioseEmp = []
      this.loadEmp()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.Title = null
      this.queryParam.IsEntry = false
      this.queryParam.Status = null
      this.queryParam.UserType = null
      this.queryParam.Post = null
      this.chioseEmp = []
      this.paging.Index = 1
      this.loadEmp()
    },
    editEmp(id) {
      this.$router.push({
        name: 'editEmp',
        params: {
          id: id
        }
      })
    },
    chioseDept(e) {
      this.queryParam.CompanyId = e.companyId
      if (e.value.length == 0) {
        this.queryParam.UnitId = null
        this.queryParam.DeptId = null
        this.title = e.comName + '公司-人员列表'
      } else {
        const dept = e.value[0]
        if (dept.IsUnit) {
          this.title = dept.DeptName + '单位-人员列表'
          this.queryParam.UnitId = dept.DeptId
          this.queryParam.DeptId = null
        } else {
          this.title = dept.DeptName + '部门-人员列表'
          this.queryParam.UnitId = null
          this.queryParam.DeptId = [dept.DeptId]
        }
      }
      this.loadEmp()
    }
  }
}
</script>
