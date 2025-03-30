<template>
  <div>
    <leftRightSplit :left-span="4">
      <el-card slot="left">
        <div slot="header">
          <span>所在公司部门</span>
          <el-checkbox
            v-model="isShowChildren"
            style="float: right"
          >显示子公司</el-checkbox>
        </div>
        <unitDeptTree
          ref="deptTree"
          :is-sub-company="isShowChildren"
          @load="chioseDept"
          @change="chioseDept"
        />
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
              <el-switch
                v-model="queryParam.IsEntry"
                active-text="显示入职员工"
                inactive-text="显示本公司员工"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                v-if="checkPower('hr.emp.add')"
                type="success"
                @click="addEmp"
              >添加员工</el-button>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <p
          v-if="chioseEmp.length > 0 && checkPower('hr.emp.open.account')"
          :gutter="24"
        >
          <span style="margin-right: 10px">已选择:{{ chioseEmp.length }}</span>
          <el-button
            type="primary"
            size="mini"
            @click="openAccount"
          >开通账号</el-button>
        </p>
        <w-table
          :data="emps"
          :columns="columns"
          :is-paging="true"
          :is-select="checkPower('hr.emp.open.account')"
          row-key="EmpId"
          :select-keys="chioseEmp"
          :is-multiple="true"
          :check-is-select="checkIsSelect"
          :paging="paging"
          @selected="selected"
          @load="loadEmp"
        >
          <template slot="empBase" slot-scope="e">
            <div class="userInfo">
              <div class="head">
                <img
                  v-if="e.row.UserHead == null || e.row.UserHead == ''"
                  src="/image/defhead.png"
                  width="50"
                  height="50"
                >
                <el-avatar
                  v-else
                  shape="square"
                  :size="50"
                  :src="e.row.UserHead | imageUri"
                />
              </div>
              <div class="content">
                <div class="item">
                  <i
                    v-if="e.row.Sex == 1"
                    style="color: #409eff"
                    class="el-icon-male"
                  />
                  <i
                    v-else-if="e.row.Sex == 2"
                    style="color: #f56c6c"
                    class="el-icon-female"
                  />
                  <el-link @click="showEmp(e.row)">{{ e.row.EmpName }}</el-link>
                </div>
                <div class="empNo">{{ e.row.EmpNo }}</div>
              </div>
            </div>
          </template>
          <template slot="status" slot-scope="e">
            <el-switch
              :disabled="!checkPower('hr.emp.set')"
              :value="e.row.Status"
              :inactive-value="2"
              :active-value="1"
              @change="(val) => setStatus(e.row, val)"
            />
          </template>
          <template slot="UserType" slot-scope="e">
            {{ HrUserType[e.row.UserType].text }}
          </template>
          <template slot="empTitle" slot-scope="e">
            <template v-if="e.row.DeptTitle != null && e.row.DeptTitle != ''">
              <span>{{ e.row.DeptTitle }}</span>
              <el-tooltip
                v-if="e.row.Title && e.row.Title.length > 0"
                style="margin-left: 5px"
                effect="dark"
                placement="bottom"
              >
                <div slot="content">
                  <p v-for="(t, index) in e.row.Title" :key="index">
                    {{ t.Show }}
                  </p>
                </div>
                <i class="el-icon-more" />
              </el-tooltip>
            </template>
            <template v-else-if="e.row.Title && e.row.Title.length > 0">
              {{ e.row.Title[0].Show }}
              <el-tooltip
                v-if="e.row.Title.length > 1"
                effect="dark"
                placement="bottom"
              >
                <div slot="content">
                  <p v-for="(t, index) in e.row.Title" :key="index">
                    {{ t.Show }}
                  </p>
                </div>
                <i class="el-icon-more" />
              </el-tooltip>
            </template>
          </template>
          <template slot="birthday" slot-scope="e">
            {{
              e.row.Birthday
                ? moment(e.row.Birthday).format("YYYY-MM-DD")
                : null
            }}
          </template>
          <template slot="regTime" slot-scope="e">
            {{ moment(e.row.RegTime).format("YYYY-MM-DD") }}
          </template>
          <template slot="isOpenAccount" slot-scope="e">
            <el-tag v-if="e.row.IsOpenAccount == false">未开通</el-tag>
            <el-tag v-else type="success">已开通</el-tag>
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              v-if="comId == e.row.CompanyId && checkPower('hr.emp.set')"
              size="mini"
              type="primary"
              title="编辑人员资料"
              icon="el-icon-edit"
              circle
              @click="editEmp(e.row.EmpId)"
            />
            <el-button
              v-if="checkPower('hr.emp.title.set')"
              size="mini"
              type="default"
              title="编辑人员职务"
              icon="el-icon-postcard"
              circle
              @click="editEmpTitle(e.row.EmpId)"
            />
            <el-button
              v-if="e.row.Status == 1 && checkPower('hr.emp.entry')"
              size="mini"
              type="default"
              title="人员调任"
              icon="el-icon-share"
              circle
              @click="empEntry(e.row)"
            />
            <el-button
              v-if="
                e.row.Status == 0 &&
                  comId == e.row.CompanyId &&
                  checkPower('hr.emp.delete')
              "
              size="mini"
              type="danger"
              title="删除员工"
              icon="el-icon-delete"
              circle
              @click="dropEmp(e.row)"
            />
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <empTitleEdit
      :emp-id="empId"
      :visible="visible"
      @cancel="(isRefresh) => {
        visible = false
        if(isRefresh) {
          loadEmp()
        }
      }"
    />
    <editEmpEntry
      :emp="emp"
      :company-id="queryParam.CompanyId"
      :visible="entryVisible"
      @close="(isRefresh) => {
        entryVisible = false
        if(isRefresh) {
          loadEmp()
        }
      }"
    />
  </div>
</template>

<script>
import moment from 'moment'
import * as empApi from '@/api/emp/emp'
import { open } from '@/api/emp/loginUser'
import empTitleEdit from '../empTitle/empTitleList.vue'
import {
  HrEnumDic,
  HrItemDic,
  HrUserType,
  HrEmpStatus
} from '@/config/publicDic'
import unitDeptTree from '@/components/unit/unitDeptTree.vue'
import editEmpEntry from './components/editEmpEntry.vue'
export default {
  components: {
    unitDeptTree,
    empTitleEdit,
    editEmpEntry
  },
  data() {
    return {
      HrEnumDic,
      HrItemDic,
      HrUserType,
      HrEmpStatus,
      entryVisible: false,
      emp: {
        CompanyId: null,
        EmpId: null,
        EmpName: null
      },
      emps: [],
      chioseEmp: [],
      status: [],
      rolePower: [
        'hr.emp.open.account',
        'hr.emp.add',
        'hr.emp.set',
        'hr.emp.delete',
        'hr.emp.title.set',
        'hr.emp.entry'
      ],
      isShowChildren: false,
      title: '员工列表',
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
          width: '150px',
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
        IsEntry: false,
        DeptId: null,
        UnitId: null,
        Status: null,
        UserType: null,
        Post: null,
        Title: null
      }
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  mounted() {
    this.initPower()
  },
  methods: {
    moment,
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
    empEntry(emp) {
      this.emp.CompanyId = this.queryParam.CompanyId
      this.emp.EmpId = emp.EmpId
      this.emp.EmpName = emp.EmpName
      this.entryVisible = true
    },
    editEmpTitle(empId) {
      this.empId = empId
      this.visible = true
    },
    showEmp(row) {
      this.$router.push({ name: 'showEmp', params: { id: row.EmpId }})
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
      if (this.queryParam.IsNoOpen === false) {
        this.queryParam.IsNoOpen = null
      }
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
      if (e.value.length === 0) {
        this.queryParam.UnitId = null
        this.queryParam.DeptId = null
        this.title = e.comName[e.companyId] + '公司-人员列表'
      } else {
        const dept = e.value[0]
        if (dept.type === 'unit') {
          this.title = dept.Name + '单位-人员列表'
          this.queryParam.UnitId = dept.Id
          this.queryParam.DeptId = null
        } else {
          this.title = dept.Name + '部门-人员列表'
          this.queryParam.UnitId = null
          this.queryParam.DeptId = [dept.Id]
        }
      }
      this.loadEmp()
    }
  }
}
</script>
<style  scoped>
.userInfo {
  width: 250px;
  height: 60px;
}
.userInfo .head {
  width: 50px;
  float: left;
  height: 60px;
  text-align: center;
}
.userInfo .content {
  width: 180px;
  float: left;
  text-align: left;
}
.userInfo .content .item {
  text-indent: 5px;
  margin: 0;
  font-size: 16px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}

.userInfo .content .empNo {
  padding-left: 10px;
  margin: 0;
  color: #82848a;
  font-size: 12px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}
.userInfo .content i {
  font-weight: 600;
}
</style>
