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
      <template slot="Name" slot-scope="e">
        <p style="margin: 0; line-height: 26px;">
          <icon
            v-if="e.row.Icon"
            style="font-size: 24px; margin-right: 5px"
            :icon="e.row.Icon"
          />{{ e.row.Name }}
        </p>
      </template>
      <template slot="ControlType" slot-scope="e">
        {{ getEnumName(controlType, e.row.ControlType) }}
      </template>
      <template slot="Status" slot-scope="e">
        <el-switch
          :value="e.row.Status == 1"
          active-text="启用"
          :inactive-text="e.row.Status == 0 ? '起草' : '停用'"
          @change="(val)=> setState(e.row, val)"
        />
      </template>
      <template slot="IsBaseControl" slot-scope="e">
        {{ e.row.IsBaseControl ? '基础组件': '扩展组件' }}
      </template>
      <template slot="EditControl" slot-scope="e">
        {{ e.row.EditControl }}
      </template>
      <template slot="ShowControl" slot-scope="e">
        {{ e.row.ShowControl }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="编辑控件信息"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
      </template>
    </w-table>
    <editControl :id="id" :visible="visible" @close="close" />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as controlApi from '@/customForm/api/control'
import {
  ControlStatus,
  EnumDic,
  DictItemDic
} from '@/customForm/config/formConfig'
import editControl from './editControl.vue'
export default {
  components: {
    editControl
  },
  data() {
    return {
      EnumDic,
      DictItemDic,
      ControlStatus,
      controlType: [],
      visible: false,
      id: null,
      controls: [],
      columns: [
        {
          key: 'Name',
          title: '控件Icon名称',
          align: 'center',
          slotName: 'Name',
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
          key: 'IsBaseControl',
          title: '是否为基础组件',
          align: 'center',
          slotName: 'IsBaseControl',
          width: 130
        },
        {
          key: 'MinWidth',
          title: '控件宽度(px)',
          align: 'right',
          minWidth: 120
        },
        {
          key: 'EditControl',
          title: '编辑控件路径',
          align: 'center',
          slotName: 'EditControl',
          minWidth: 150
        },
        {
          key: 'ShowControl',
          title: '显示控件路径',
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
      },
      dictItems: {}
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    moment,
    edit(row) {
      this.id = row.Id
      this.visible = true
    },
    async setState(row, isEnable) {
      if (isEnable) {
        await controlApi.Enable(row.Id)
      } else {
        await controlApi.Disable(row.Id)
      }
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      // eslint-disable-next-line require-atomic-updates
      row.Status = isEnable ? 1 : 2
    },
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    show(row) {
      this.id = row.Id
      this.visible = true
    },
    getEnumName(list, key) {
      if (list == null || list.length === 0) {
        return null
      }
      const item = list.find((a) => a.Value === key)
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
