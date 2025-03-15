<template>
  <el-card>
    <div slot="header">
      <span>公司列表</span>
    </div>
    <div class="app-container">
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="公司状态">
          <enumItem
            v-model="queryParam.Status"
            :dic-key="HrEnumDic.companyStatus"
            placeholder="公司状态"
            :multiple="true"
            class-name="filter-item"
            @change="load"
            @load="initDict"
          />
        </el-form-item>
        <el-form-item>
          <el-button
            type="success"
            @click="addCompany"
          >新增公司</el-button>
        </el-form-item>
      </el-form>
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
        <template slot="CompanyType" slot-scope="e">
          {{ hrCompanyType[e.row.CompanyType].text }}
        </template>
        <template slot="Contacts" slot-scope="e">
          <span>{{ e.row.Contacts }}</span>
          <span v-if="e.row.Telephone">{{ e.row.Telephone }}</span>
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            icon="el-icon-plus"
            size="mini"
            circle
            @click="addDept(e.row.Id)"
          />
          <el-button
            v-if="e.row.Status == 0"
            size="mini"
            icon="el-icon-delete"
            type="danger"
            circle
            @click="deleteDept(e.row)"
          />
          <el-button
            icon="el-icon-edit"
            size="mini"
            type="primary"
            circle
            @click="editDept(e.row)"
          />
        </template>
      </w-table>
    </div>
  </el-card>
</template>

<script>
import * as companyApi from '@/api/base/company'
import { HrEnumDic, hrCompanyType } from '@/config/publicDic'
export default {
  components: {
  },
  data() {
    return {
      HrEnumDic,
      hrCompanyType,
      depts: [],
      empId: null,
      status: {},
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
          key: 'LeaverName',
          title: '管理员',
          align: 'center'
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
    async load() {
      const list = await companyApi.GetTree(this.queryParam)
      this.companys = list
    },
    initDict(dict) {
      dict.forEach(e => {
        this.status[e.Value] = e.Text
      })
    }
  }
}
</script>
