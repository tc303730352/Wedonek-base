<template>
  <el-card>
    <div slot="header">
      <span>公司列表</span>
    </div>
    <div class="app-container">
      <div v-if="checkPower('hr.company.add')" style="text-align: right; padding-bottom: 10px;">
        <el-button
          type="success"
          @click="add"
        >新增公司</el-button>
      </div>
      <w-table
        :data="companys"
        :columns="columns"
        :is-paging="false"
        row-key="Id"
      >
        <template slot="Status" slot-scope="e">
          <el-switch
            :disabled="checkPower('hr.company.set') == false"
            :value="e.row.Status"
            :inactive-value="2"
            :active-value="1"
            @change="(value) => statusChange(e.row, value)"
          />
        </template>
        <template slot="leaver" slot-scope="e">
          <span v-if="e.row.LeaverId">{{ e.row.Leaver }}</span>
          <el-button
            v-if="checkPower('hr.company.set')"
            class="el-icon-edit"
            type="text"
            style="margin-left: 5px; cursor: pointer"
            @click="showEmp(e.row)"
          />
        </template>
        <template slot="admin" slot-scope="e">
          <span v-if="e.row.AdminId">{{ e.row.Admin }}</span>
          <el-button
            v-if="checkPower('hr.company.set')"
            class="el-icon-edit"
            type="text"
            style="margin-left: 5px; cursor: pointer"
            @click="showAdmin(e.row)"
          />
        </template>
        <template slot="CompanyType" slot-scope="e">
          {{ hrCompanyType[e.row.CompanyType].text }}
        </template>
        <template slot="AddTime" slot-scope="e">
          {{ moment(e.row.AddTime).format('YYYY-MM-DD') }}
        </template>
        <template slot="Contacts" slot-scope="e">
          <span>{{ e.row.Contacts }}</span>
          <span v-if="e.row.Telephone">{{ ' ' + e.row.Telephone }}</span>
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            v-if="e.row.Status == 0 && checkPower('hr.company.delete')"
            size="mini"
            icon="el-icon-delete"
            type="danger"
            circle
            @click="drop(e.row)"
          />
          <template v-if="checkPower('hr.company.set')">
            <el-button
              icon="el-icon-setting"
              size="mini"
              type="info"
              circle
              @click="editPower(e.row)"
            />
            <el-button
              icon="el-icon-edit"
              size="mini"
              type="primary"
              circle
              @click="edit(e.row)"
            />
          </template>
        </template>
      </w-table>
    </div>
    <editCompany :id="id" :visible="visible" :companys="companys" @close="close" />
    <empChoice
      :visible="empVisible"
      :emp-id="empId"
      :title="empTitle"
      :company-id="id"
      :is-sub-company="isSubCompany"
      @save="saveEmp"
      @close="empVisible=false"
    />
    <editCompanyPower :visible="powerVisible" :company-id="id" @close="powerVisible=false" />
  </el-card>
</template>

<script>
import * as companyApi from '@/api/base/company'
import { HrEnumDic, hrCompanyType } from '@/config/publicDic'
import editCompany from './components/editCompany.vue'
import editCompanyPower from './components/editCompanyPower.vue'
import empChoice from '@/components/emp/empChoice.vue'
import moment from 'moment'
export default {
  components: {
    editCompany,
    empChoice,
    editCompanyPower
  },
  data() {
    return {
      HrEnumDic,
      hrCompanyType,
      empId: null,
      visible: false,
      empVisible: false,
      powerVisible: false,
      isSubCompany: false,
      op: null,
      id: null,
      curRow: null,
      title: '公司列表',
      empTitle: '',
      columns: [
        {
          key: 'FullName',
          title: '公司全称',
          align: 'left'
        },
        {
          key: 'ShortName',
          title: '简称',
          align: 'left'
        },
        {
          key: 'CompanyType',
          title: '公司类型',
          slotName: 'CompanyType'
        },
        {
          key: 'Address',
          title: '地址',
          align: 'left'
        },
        {
          key: 'Contacts',
          title: '联系人',
          align: 'center',
          slotName: 'Contacts'
        },
        {
          key: 'Leaver',
          title: '负责人',
          align: 'center',
          slotName: 'leaver'
        },
        {
          key: 'Admin',
          title: '管理员',
          align: 'center',
          slotName: 'admin'
        },
        {
          key: 'Status',
          title: '状态',
          slotName: 'Status'
        },
        {
          key: 'AddTime',
          title: '添加时间',
          slotName: 'AddTime'
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          slotName: 'action'
        }
      ],
      queryParam: {
        ParentId: null,
        Status: null
      },
      companys: [],
      rolePower: ['hr.company.add', 'hr.company.set', 'hr.company.delete']
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  mounted() {
    this.initPower()
    this.load()
  },
  methods: {
    moment,
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
    showEmp(row) {
      this.curRow = row
      this.isSubCompany = false
      this.op = 'leaver'
      this.id = row.Id
      this.empId = row.LeaverId == null ? [] : [row.LeaverId]
      this.empTitle = '选择' + row.FullName + '公司负责人'
      this.empVisible = true
    },
    showAdmin(row) {
      this.curRow = row
      this.op = 'admin'
      this.isSubCompany = true
      this.id = this.comId
      this.empId = row.AdminId == null ? [] : [row.AdminId]
      this.empTitle = '选择' + row.FullName + '公司管理员'
      this.empVisible = true
    },
    saveEmp(e) {
      this.empVisible = false
      if (this.op === 'leaver') {
        this.setLeader(e)
      } else {
        this.setAdmin(e)
      }
    },
    async setAdmin(e) {
      const leader = e.user.length === 0 ? null : e.user[0]
      if (leader == null) {
        await companyApi.SetAdminId(this.curRow.Id, null)
      } else {
        await companyApi.SetAdminId(this.curRow.Id, leader.EmpId)
      }
      this.load()
    },
    async setLeader(e) {
      const leader = e.user.length === 0 ? null : e.user[0]
      if (leader == null) {
        await companyApi.SetLeaverId(this.id, null)
      } else {
        await companyApi.SetLeaverId(this.id, leader.EmpId)
      }
      this.load()
    },
    add() {
      this.id = null
      this.visible = true
    },
    edit(row) {
      this.id = row.Id
      this.visible = true
    },
    editPower(row) {
      this.id = row.Id
      this.powerVisible = true
    },
    async statusChange(row, status) {
      if (status === 1) {
        await companyApi.Enable(row.Id)
      } else {
        await companyApi.Stop(row.Id)
      }
      row.Status = status
    },
    drop(row) {
      const title =
        '确认删除公司 ' + ((row.ShortName == null || row.ShortName === '') ? row.FullName : row.ShortName) + '?'
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
      await companyApi.Delete(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    async load() {
      const list = await companyApi.GetTree(this.queryParam)
      this.companys = list
    }
  }
}
</script>
