<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="关键字"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="业务类型">
          <dictItem
            v-model="queryParam.BusType"
            dic-id="6"
            placeholder="业务类型"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button
            v-if="checkPower('hr.operate.menu.add')"
            type="success"
            @click="addMenu"
          >添加权限</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="dataList"
      :columns="columns"
      :is-paging="true"
      row-key="Id"
      :paging="paging"
      @load="load"
    >
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          v-model="e.row.IsEnable"
          :disabled="checkPower('hr.operate.menu.set') == false"
          active-text="启用"
          inactive-text="停用"
          @change="setState(e.row)"
        />
      </template>
      <template slot="BusType" slot-scope="e">
        {{ busType[e.row.BusType] }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          v-if="!e.row.IsEnable && checkPower('hr.operate.menu.delete')"
          size="mini"
          type="danger"
          title="删除操作菜单"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <addOpMenu :visible="visible" @close="close" />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as opMenuApi from '@/api/opMenu'
import { GetItemName } from '@/api/base/dictItem'
import addOpMenu from './addOpMenu'
export default {
  components: {
    addOpMenu
  },
  data() {
    return {
      dataList: [],
      visible: false,
      id: null,
      columns: [
        {
          key: 'Title',
          title: '权限功能说明',
          align: 'center',
          width: 200
        },
        {
          key: 'BusType',
          title: '业务类型',
          align: 'center',
          slotName: 'BusType',
          width: 150
        },
        {
          key: 'IsEnable',
          title: '是否启用',
          align: 'center',
          slotName: 'IsEnable',
          width: 150
        },
        {
          key: 'RoutePath',
          title: '路由路径',
          align: 'left',
          minWidth: 300
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
      rolePower: [
        'hr.operate.menu.set',
        'hr.operate.menu.add',
        'hr.operate.menu.delete'
      ],
      queryParam: {
        QueryKey: null,
        BusType: null
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
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    async setState(row) {
      await opMenuApi.SetIsEnable(row.Id, row.IsEnable)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
    },
    addMenu() {
      this.visible = true
    },
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
    drop(row) {
      const title = '确认删除权限 ' + row.Title + '?'
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
      await opMenuApi.Delete(id)
      this.load()
    },
    async loadItem() {
      this.busType = await GetItemName('6')
    },
    async load() {
      const res = await opMenuApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.dataList = res.List
      } else {
        this.dataList = []
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
