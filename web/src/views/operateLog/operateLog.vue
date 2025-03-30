<template>
  <el-card>
    <div slot="header">
      <span>用户操作日志</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="模块标题">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="模块标题"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="是否成功">
          <el-select
            v-model="queryParam.IsSuccess"
            clearable
            placeholder="是否成功"
            @change="load"
          >
            <el-option label="全部" :value="null" />
            <el-option label="成功" :value="true" />
            <el-option label="失败" :value="false" />
          </el-select>
        </el-form-item>
        <el-form-item label="业务类型">
          <dictItem
            v-model="queryParam.BusType"
            dic-id="6"
            placeholder="业务类型"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="操作时段">
          <el-date-picker
            v-model="queryParam.Begin"
            type="date"
            placeholder="开始日期"
            @change="load"
          />
          <span style="padding: 5px">至</span>
          <el-date-picker
            v-model="queryParam.End"
            type="date"
            placeholder="结束日期"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="logs"
      :columns="columns"
      :is-paging="true"
      row-key="Id"
      :paging="paging"
      @load="load"
    >
      <template slot="failShow" slot-scope="e">
        <span
          v-if="!e.row.IsSuccess"
        >{{ e.row.FailShow == null ? e.row.ErrorCode : e.row.FailShow }}
        </span>
      </template>
      <template slot="Duration" slot-scope="e">
        {{ e.row.Duration + " 毫秒" }}
      </template>
      <template slot="UserType" slot-scope="e">
        {{ HrLoginUserType[e.row.UserType].text }}
      </template>
      <template slot="BusType" slot-scope="e">
        {{ busType[e.row.BusType] }}
      </template>
      <template slot="IsSuccess" slot-scope="e">
        <span :style="{ color: e.row.IsSuccess ? '#43AF2B' : '#999' }">{{
          e.row.IsSuccess ? "成功" : "失败"
        }}</span>
      </template>
      <template slot="AddTime" slot-scope="e">
        {{ moment(e.row.AddTime).format("YYYY-MM-DD HH:mm:ss") }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="显示信息"
          icon="el-icon-view"
          circle
          @click="show(e.row)"
        />
      </template>
    </w-table>
    <operateView
      :id="id"
      :visible.sync="visible"
    />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as opLogApi from '@/api/operateLog'
import { GetItemName } from '@/api/base/dictItem'
import { HrLoginUserType } from '@/config/publicDic'
import operateView from './operateView.vue'
export default {
  components: {
    operateView
  },
  data() {
    return {
      HrLoginUserType,
      logs: [],
      visible: false,
      id: null,
      columns: [
        {
          key: 'Title',
          title: '模块标题',
          align: 'center',
          width: 200
        },
        {
          key: 'EmpName',
          title: '人员名',
          align: 'center',
          width: 200,
          sortby: 'EmpId',
          sortable: 'custom'
        },
        {
          key: 'DeptName',
          title: '部门名',
          align: 'center',
          width: 200
        },
        {
          key: 'BusType',
          title: '业务类型',
          align: 'center',
          slotName: 'BusType',
          minWidth: 150
        },
        {
          key: 'UserType',
          title: '用户类型',
          align: 'center',
          slotName: 'UserType',
          minWidth: 150
        },
        {
          key: 'Ip',
          title: 'Ip地址',
          align: 'center'
        },
        {
          key: 'Address',
          title: '地址',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'IsSuccess',
          title: '是否成功',
          align: 'center',
          slotName: 'IsSuccess',
          minWidth: 120
        },
        {
          key: 'FailShow',
          title: '失败原因',
          align: 'center',
          slotName: 'failShow',
          width: 200
        },
        {
          key: 'Duration',
          title: '耗时',
          align: 'center',
          slotName: 'Duration',
          minWidth: 120
        },
        {
          key: 'AddTime',
          title: '登陆时间',
          align: 'center',
          slotName: 'AddTime',
          minWidth: 110
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          width: '100px',
          slotName: 'action'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      },
      busType: {},
      queryParam: {
        QueryKey: null,
        IsSuccess: null,
        Begin: null,
        End: null
      }
    }
  },
  computed: {},
  mounted() {
    this.loadItem()
    this.load()
  },
  methods: {
    moment,
    show(row) {
      this.id = row.Id
      this.visible = true
    },
    async loadItem() {
      this.busType = await GetItemName('6')
    },
    async load() {
      const res = await opLogApi.query(this.queryParam, this.paging)
      if (res.List) {
        this.logs = res.List
      } else {
        this.logs = []
      }
      this.paging.Total = res.Count
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.IsSuccess = null
      this.queryParam.Begin = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    }
  }
}
</script>
