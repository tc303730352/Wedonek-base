<template>
  <el-card>
    <div slot="header">
      <span>用户登陆日志</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="登陆名/登陆IP"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="是否成功">
          <el-select v-model="queryParam.IsSuccess" clearable placeholder="是否成功" @change="load">
            <el-option label="全部" :value="null" />
            <el-option label="成功" :value="true" />
            <el-option label="失败" :value="false" />
          </el-select>
        </el-form-item>
        <el-form-item label="登陆时段">
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
        <span v-if="!e.row.IsSuccess">{{ e.row.FailShow == null ? e.row.ErrorCode : e.row.FailShow }} </span>
      </template>
      <template slot="IsSuccess" slot-scope="e">
        <span :style="{color: e.row.IsSuccess ? '#43AF2B' :'#999'}">{{ e.row.IsSuccess ? '成功' : '失败' }}</span>
      </template>
      <template slot="LoginTime" slot-scope="e">
        {{ moment(e.row.LoginTime).format("YYYY-MM-DD HH:mm:ss") }}
      </template>
    </w-table>
  </el-card>
</template>

<script>
import moment from 'moment'
import * as onlineLogApi from '@/api/onlineLog/onlineLog'
export default {
  components: {
  },
  data() {
    return {
      logs: [],
      columns: [
        {
          key: 'LoginName',
          title: '登陆名',
          align: 'center',
          width: 150,
          sortby: 'LoginName',
          sortable: 'custom'
        },
        {
          sortby: 'LoginIp',
          key: 'LoginIp',
          title: '登陆IP',
          align: 'center',
          minWidth: 140,
          sortable: 'custom'
        },
        {
          key: 'Address',
          title: '登陆地址',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'Browser',
          title: '浏览器',
          align: 'center'
        },
        {
          key: 'SysName',
          title: '系统名',
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
          key: 'LoginTime',
          title: '登陆时间',
          align: 'center',
          slotName: 'LoginTime',
          minWidth: 110
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      },
      queryParam: {
        QueryKey: null,
        IsSuccess: null,
        Begin: null,
        End: null
      }
    }
  },
  computed: {
  },
  mounted() {
    this.load()
  },
  methods: {
    moment,
    async load() {
      const res = await onlineLogApi.query(this.queryParam, this.paging)
      if (res.List) {
        this.logs = res.List
      } else {
        this.logs = []
      }
      this.paging.Total = res.Count
      console.log(this.paging)
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

