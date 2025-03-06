<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="900px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="关键字"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="字典项状态">
          <enumItem
            v-model="queryParam.Status"
            :dic-key="HrEnumDic.dicItemStatus"
            placeholder="字典项状态"
            :multiple="true"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="success" @click="addItem">添加字典项</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table :data="items" :columns="columns" :is-paging="false">
      <template slot="DicStatus" slot-scope="e">
        <el-switch
          :value="e.row.DicStatus"
          :inactive-value="2"
          :active-value="1"
          @change="(value) => setStatus(e.row, value)"
        />
      </template>
      <template slot="Sort" slot-scope="e">
        <el-button
          v-if="e.row.Sort != 1"
          icon="el-icon-caret-top"
          size="mini"
          style="float: left"
          circle
          @click="upItem(e.row)"
        />
        <el-button
          v-if="e.row.Sort != maxSort"
          size="mini"
          style="float: right"
          icon="el-icon-caret-bottom"
          circle
          @click="downItem(e.row, e.index)"
        />
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="warning"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="editItem(e.row)"
        />
        <el-button
          v-if="e.row.DicStatus == 0"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="deleteItem(e.row)"
        />
      </template>
    </w-table>
    <editDicItem :id="id" :visible="visibleEdit" :dic-id="dicId" @close="closeEdit" />
  </el-dialog>
</template>

<script>
import * as dicItemApi from '@/api/base/dictItem'
import { HrEnumDic, DicItemStatus } from '@/config/publicDic'
import editDicItem from './components/editDicItem.vue'
export default {
  name: 'EditDic',
  components: {
    editDicItem
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    dicName: {
      type: String,
      default: null
    },
    dicId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrEnumDic,
      DicItemStatus,
      items: [],
      visibleEdit: false,
      loading: false,
      maxSort: 1,
      id: null,
      title: '字典',
      columns: [
        {
          sortby: 'DicText',
          key: 'DicText',
          title: '字典文本',
          align: 'center',
          minWidth: 150,
          sortable: 'custom'
        },
        {
          key: 'DicValue',
          title: '字典值',
          align: 'center',
          minWidth: 200
        },
        {
          sortby: 'DicStatus',
          key: 'DicStatus',
          title: '状态',
          align: 'center',
          slotName: 'DicStatus',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'Sort',
          key: 'Sort',
          title: '排序',
          align: 'center',
          slotName: 'Sort',
          minWidth: 100,
          sortable: 'custom'
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
      queryParam: {
        DicId: null,
        QueryKey: null,
        Status: null
      }
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    closeEdit(isRefresh) {
      this.visibleEdit = false
      if (isRefresh) {
        this.reset()
      }
    },
    editItem(row) {
      this.id = row.Id
      this.parent = null
      this.visibleEdit = true
    },
    async reset() {
      this.title = '字典' + this.dicName + '明细'
      this.queryParam.DicId = this.dicId
      this.queryParam.QueryKey = null
      this.queryParam.Status = null
      this.load()
    },
    upItem(row) {
      const to = this.items[row.index - 1]
      this.moveLoad(row, to)
    },
    async moveLoad(form, to) {
      await dicItemApi.Move(form.Id, to.Id)
      const sort = form.Sort
      const index = form.index
      form.index = to.index
      form.Sort = to.Sort
      to.Sort = sort
      to.index = index
      this.items.OrderBy('Sort')
    },
    downItem(row) {
      const to = this.items[row.index + 1]
      this.moveLoad(row, to)
    },
    addItem(row) {
      this.id = null
      this.parent = row
      this.visibleEdit = true
    },
    deleteItem(row) {
      const title = '确认删除字典项 ' + row.DicText + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row.Id)
      })
    },
    async setStatus(row, status) {
      if (status == 1) {
        await dicItemApi.Enable(row.Id)
      } else {
        await dicItemApi.Stop(row.Id)
      }
      row.DicStatus = status
    },
    async submitDrop(id) {
      await dicItemApi.DeleteItem(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    async load() {
      this.loading = true
      this.items = await dicItemApi.GetItems(this.queryParam)
      let index = 0
      this.items.forEach(c => {
        c.index = index
        index = index + 1
      })
      this.maxSort = this.items.Max('Sort')
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
