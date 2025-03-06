<template>
  <el-card>
    <div slot="header">
      <span>字典列表</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="字典名">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="字典名"
            @change="loadDic"
          />
        </el-form-item>
        <el-form-item label="字典状态">
          <enumItem
            v-model="queryParam.Status"
            :dic-key="HrEnumDic.dicStatus"
            placeholder="字典状态"
            :multiple="true"
            @change="loadDic"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="success" @click="addDic">添加字典</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="dicts"
      :columns="columns"
      :is-paging="true"
      :paging="paging"
      @load="loadDic"
    >
      <template slot="Status" slot-scope="e">
        <el-switch
          :value="e.row.Status"
          :inactive-value="2"
          :active-value="1"
          @change="(value) => statusChange(e.row, value)"
        />
      </template>
      <template slot="IsSysDic" slot-scope="e">
        <span v-if="e.row.IsSysDic">是</span>
        <span v-else>否</span>
      </template>
      <template slot="IsTreeDic" slot-scope="e">
        <span v-if="e.row.IsTreeDic">是</span>
        <span v-else>否</span>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="字典项"
          icon="el-icon-receiving"
          circle
          @click="show(e.row)"
        />
        <el-button
          v-if="e.row.IsSysDic == false"
          size="mini"
          type="warning"
          title="编辑字典"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
        <el-button
          v-if="e.row.IsSysDic == false && e.row.Status == 0"
          size="mini"
          type="danger"
          title="删除字典"
          icon="el-icon-delete"
          circle
          @click="dropDic(e.row)"
        />
      </template>
    </w-table>
    <dicEdit :id="dicId" :visible="visable" @close="close" />
    <treeDicItem
      :dic-id="dicId"
      :visible="treeVisable"
      :dic-name="dicName"
      @close="closeTree"
    />
    <dictItem
      :dic-id="dicId"
      :visible="itemVisable"
      :dic-name="dicName"
      @close="closeItem"
    />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as dictApi from '@/api/base/dict'
import { HrEnumDic } from '@/config/publicDic'
import dicEdit from './components/editDic.vue'
import treeDicItem from './treeDicItem.vue'
import dictItem from './dicItem.vue'
export default {
  components: {
    dicEdit,
    treeDicItem,
    dictItem
  },
  data() {
    return {
      HrEnumDic,
      dicts: [],
      dicId: null,
      treeVisable: false,
      visable: false,
      itemVisable: false,
      dicName: null,
      queryParam: {
        QueryKey: null,
        Status: [],
        IsSysDic: null,
        IsTreeDic: null
      },
      columns: [
        {
          sortby: 'DicName',
          key: 'DicName',
          title: '字典名',
          align: 'center',
          minWidth: 150,
          sortable: 'custom'
        },
        {
          key: 'Remark',
          title: '备注',
          align: 'center',
          minWidth: 200
        },
        {
          sortby: 'Status',
          key: 'Status',
          title: '状态',
          align: 'center',
          slotName: 'Status',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'IsSysDic',
          key: 'IsSysDic',
          title: '是否为系统字典',
          align: 'center',
          slotName: 'IsSysDic',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'IsTreeDic',
          key: 'IsTreeDic',
          title: '是否是树形字典',
          align: 'center',
          minWidth: 100,
          slotName: 'IsTreeDic',
          sortable: 'custom'
        },
        {
          key: 'ItemNum',
          title: '字典项数',
          align: 'right',
          minWidth: 100
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          fixed: 'right',
          width: '140px',
          slotName: 'action'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  mounted() {
    this.loadDic()
  },
  methods: {
    moment,
    addDic() {
      this.dicId = null
      this.visable = true
    },
    edit(row) {
      this.dicId = row.Id
      this.visable = true
    },
    show(row) {
      this.dicId = row.Id
      this.dicName = row.DicName
      if (row.IsTreeDic) {
        this.treeVisable = true
      } else {
        this.itemVisable = true
      }
    },
    closeTree(isRefresh) {
      this.treeVisable = false
      if (isRefresh) {
        this.reset()
      }
    },
    closeItem(isRefresh) {
      this.itemVisable = false
      if (isRefresh) {
        this.reset()
      }
    },
    close(isRefresh) {
      this.visable = false
      if (isRefresh) {
        this.reset()
      }
    },
    async statusChange(row, value) {
      if (value == 1) {
        await dictApi.Enable(row.Id)
      } else {
        await dictApi.Stop(row.Id)
      }
      this.$message({
        type: 'success',
        message: '设置成功!'
      })
      row.Status = value
    },
    dropDic(row) {
      const title = '确认删除字典 ' + row.DicName + '?'
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
      await dictApi.DeleteDic(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.loadDic()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.Status = []
      this.queryParam.IsSysDic = null
      this.queryParam.IsTreeDic = null
      this.paging.Index = 1
      this.loadDic()
    },
    async loadDic() {
      const res = await dictApi.query(this.queryParam, this.paging)
      this.dicts = res.List
      this.paging.Total = res.Count
    }
  }
}
</script>
