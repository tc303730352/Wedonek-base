<template>
  <el-card>
    <div slot="header">
      <span>公司列表</span>
    </div>
    <div class="app-container">
      <div style="text-align: right; padding-bottom: 10px;">
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
            :value="e.row.Status"
            :inactive-value="2"
            :active-value="1"
            @change="(value) => statusChange(e.row, value)"
          />
        </template>
        <template slot="leaver" slot-scope="e">
          <el-button v-if="e.row.LeaverId" type="text" @click="showEmp(e.row.LeaverId)">{{ e.row.Leaver }}</el-button>
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
            v-if="e.row.Status == 0"
            size="mini"
            icon="el-icon-delete"
            type="danger"
            circle
            @click="drop(e.row)"
          />
          <el-button
            icon="el-icon-edit"
            size="mini"
            type="primary"
            circle
            @click="edit(e.row)"
          />
        </template>
      </w-table>
    </div>
    <editCompany :id="id" :visible="visible" :companys="companys" @close="close" />
  </el-card>
</template>

<script>
import * as companyApi from '@/api/base/company'
import { HrEnumDic, hrCompanyType } from '@/config/publicDic'
import editCompany from './components/editCompany.vue'
import moment from 'moment'
export default {
  components: {
    editCompany
  },
  data() {
    return {
      HrEnumDic,
      hrCompanyType,
      empId: null,
      visible: false,
      id: null,
      title: '公司列表',
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
          title: '管理员',
          align: 'center',
          slotName: 'leaver'
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
      companys: []
    }
  },
  mounted() {
    this.load()
  },
  methods: {
    moment,
    add() {
      this.id = null
      this.visible = true
    },
    edit(row) {
      this.id = row.Id
      this.visible = true
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
