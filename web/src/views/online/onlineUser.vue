<template>
  <el-card>
    <div slot="header">
      <span>在线用户列表</span>
    </div>
    <w-table
      :data="users"
      :columns="columns"
      :is-paging="true"
      row-key="AccreditId"
      :paging="paging"
    >
      <template slot="isOnline" slot-scope="e">
        {{ e.row.IsOnline ? '是' : '否' }}
      </template>
      <template slot="loginTime" slot-scope="e">
        {{ moment(e.row.LoginTime).format('YYYY-MM-DD HH:mm:ss') }}
      </template>
      <template slot="expire" slot-scope="e">
        <span v-if="e.row.Expire"> {{ moment(e.row.Expire).format('YYYY-MM-DD HH:mm:ss') }}</span>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          v-if="checkPower('hr.user.kickout')"
          size="mini"
          type="warning"
          title="踢出用户"
          circle
          @click="kickOut(e.row)"
        >
          <svg-icon icon-class="link" />
        </el-button>
      </template>
    </w-table>
  </el-card>
</template>

<script>
import moment from 'moment'
import * as onlineUserApi from '@/api/emp/onlineUser'
export default {
  components: {
  },
  data() {
    return {
      users: [],
      rolePower: ['hr.user.kickout'],
      columns: [
        {
          key: 'AccreditId',
          title: '授权码',
          align: 'left',
          width: 150
        },
        {
          key: 'UserName',
          title: '用户名',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'DeptName',
          title: 'DeptName',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'LoginIp',
          title: '登陆IP',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'Browser',
          title: '浏览器',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'SysName',
          title: '系统名',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'IsOnline',
          title: '是否在线',
          align: 'center',
          minWidth: 100,
          slotName: 'isOnline'
        },
        {
          key: 'LoginTime',
          title: '登陆时间',
          align: 'center',
          slotName: 'loginTime',
          minWidth: 150
        },
        {
          key: 'Expire',
          title: '过期时间',
          align: 'center',
          slotName: 'expire',
          minWidth: 150
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
        SortName: 'AccreditId',
        IsDesc: false,
        Total: 0
      }
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
    async kickOut(row) {
      const title =
        '确认踢出用户 ' + row.UserName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitKickout(row.AccreditId)
      })
    },
    async submitKickout(accreditId) {
      await onlineUserApi.KickOut(accreditId)
    },
    async load() {
      const res = await onlineUserApi.query(this.paging)
      if (res.List) {
        this.users = res.List
      } else {
        this.users = []
      }
      this.paging.Total = res.Count
    },
    reset() {
      this.paging.Index = 1
      this.load()
    }
  }
}
</script>
