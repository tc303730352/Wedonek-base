<template>
  <el-card>
    <div slot="header">
      <span>表单控件管理</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="控件名称">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="控件名称"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="控件类型">
          <enumItem
            v-model="queryParam.ControlType"
            :dic-key="EnumDic.ControlType"
            sys-head="form"
            placeholder="控件类型"
            :enum-items.sync="controlType"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="控件状态">
          <enumItem
            v-model="queryParam.Status"
            :dic-key="EnumDic.ControlStatus"
            sys-head="form"
            placeholder="控件状态"
            :multiple="true"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="controls"
      :columns="columns"
      :is-paging="true"
      row-key="Id"
      :paging="paging"
      @load="load"
    >
      <template slot="ControlType" slot-scope="e">
        {{ getEnumName(controlType, e.row.ControlType) }}
      </template>
      <template slot="Status" slot-scope="e">
        {{ ControlStatus[e.row.Status].text }}
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
  </el-card>
</template>

<script>
import moment from 'moment'
import * as controlApi from '@/customForm/api/control'
import { GetItemName } from '@/api/base/dictItem'
import { ControlStatus, EnumDic, DictItemDic } from '@/customForm/config/formConfig'
export default {
  components: {
  },
  data() {
    return {
      EnumDic,
      ControlStatus,
      controlType: [],
      controls: [],
      columns: [
        {
          key: 'Name',
          title: '控件名称',
          align: 'center',
          width: 200
        },
        {
          key: 'Description',
          title: '说明',
          align: 'left',
          width: 200
        },
        {
          key: 'ControlType',
          title: '控件类型',
          align: 'center',
          slotName: 'ControlType',
          width: 100
        },
        {
          key: 'MinWidth',
          title: '控件最小宽度',
          align: 'right',
          minWidth: 120
        },
        {
          key: 'EditControl',
          title: '编辑控件名',
          align: 'center',
          slotName: 'EditControl',
          minWidth: 150
        },
        {
          key: 'ShowControl',
          title: '显示控件名',
          slotName: 'ShowControl',
          align: 'center'
        },
        {
          key: 'Status',
          title: '状态',
          align: 'center',
          slotName: 'Status',
          minWidth: 120
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
        ControlType: null,
        Status: null
      }
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    moment,
    show(row) {
      this.id = row.Id
      this.visible = true
    },
    getEnumName(list, key) {
      if (list == null || list.length === 0) {
        return null
      }
      const item = list.find(a => a.Value === key)
      return item == null ? null : item.Text
    },
    async load() {
      const res = await controlApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.controls = res.List
      } else {
        this.controls = []
      }
      this.paging.Total = res.Count
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.ControlType = null
      this.queryParam.Status = null
      this.paging.Index = 1
      this.load()
    }
  }
}
</script>
