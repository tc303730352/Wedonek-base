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
          <el-button v-if="checkPower('hr.dic.add')" type="success" @click="addItem">添加字典项</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <el-table
      v-loading="loading"
      :data="trees"
      style="width: 100%"
      row-key="Id"
      default-expand-all
      :tree-props="{ children: 'Children' }"
    >
      <el-table-column prop="DicText" label="字典文本" sortable min-width="150" />
      <el-table-column prop="DicValue" label="字典值" sortable min-width="150" />
      <el-table-column prop="DicStatus" label="状态" min-width="120">
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.DicStatus"
            :disabled="checkPower('hr.dic.set') == false"
            :active-value="1"
            :inactive-value="2"
            @change="(val) => setStatus(scope.row, val)"
          />
        </template>
      </el-table-column>
      <el-table-column
        prop="Sort"
        label="排序"
        sortable
        width="100"
      >
        <template slot-scope="scope">
          <span v-if="!checkPower('hr.dic.set')">{{ scope.row.Sort }}</span>
          <template v-else>
            <el-button
              v-if="scope.row.Sort != 1"
              icon="el-icon-caret-top"
              size="mini"
              style="float: left"
              circle
              @click="upItem(scope.row)"
            />
            <el-button
              v-if="scope.row.Sort != scope.row.MaxSort"
              size="mini"
              style="float: right"
              icon="el-icon-caret-bottom"
              circle
              @click="downItem(scope.row)"
            />
          </template>
        </template>
      </el-table-column>
      <el-table-column prop="Action" label="操作" sortable width="150">
        <template slot-scope="scope">
          <el-button
            v-if="checkPower('hr.dic.add')"
            icon="el-icon-plus"
            size="mini"
            circle
            @click="addItem(scope.row)"
          />
          <el-button
            v-if="scope.row.DicStatus == 0 && checkPower('hr.dic.delete')"
            size="mini"
            icon="el-icon-delete"
            type="danger"
            circle
            @click="deleteItem(scope.row)"
          />
          <el-button
            v-if="checkPower('hr.dic.set')"
            icon="el-icon-edit"
            size="mini"
            type="primary"
            circle
            @click="editItem(scope.row)"
          />
        </template>
      </el-table-column>
    </el-table>
    <editTreeItem
      :id="id"
      :visible="visibleEdit"
      :dic-id="dicId"
      :parent="parent"
      @close="closeEdit"
    />
  </el-dialog>
</template>

<script>
import * as dicApi from '@/api/base/treeDicItem'
import { HrEnumDic, DicItemStatus } from '@/config/publicDic'
import editTreeItem from './components/editTreeItem.vue'
export default {
  name: 'EditDic',
  components: {
    editTreeItem
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
      trees: [],
      parent: null,
      visibleEdit: false,
      loading: false,
      id: null,
      title: '字典',
      queryParam: {
        DicId: null,
        QueryKey: null,
        Status: null
      },
      rolePower: ['hr.dic.add', 'hr.dic.set', 'hr.dic.delete']
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
  mounted() {
    this.initPower()
  },
  methods: {
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
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
    async upItem(row) {
      await dicApi.Move(row.Id, row.UpId)
      this.load()
    },
    async downItem(row) {
      await dicApi.Move(row.Id, row.DownId)
      this.load()
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
      if (status === 1) {
        await dicApi.Enable(row.Id)
      } else {
        await dicApi.Stop(row.Id)
      }
      row.DicStatus = status
    },
    async submitDrop(id) {
      await dicApi.DeleteItem(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    format(rows) {
      const index = rows.length - 1
      let i = 0
      rows.forEach((a) => {
        if (a.Children && a.Children.length !== 0) {
          this.format(a.Children)
        }
        a.MaxSort = rows[index].Sort
        if (a.MaxSort !== a.Sort) {
          a.DownId = rows[i + 1].Id
        } else if (i !== 0) {
          a.UpId = rows[i - 1].Id
        }
        i = i + 1
      })
    },
    async load() {
      this.loading = true
      let tree = await dicApi.getTree(this.queryParam)
      if (tree == null) {
        tree = []
      }
      const index = tree.length - 1
      let i = 0
      tree.forEach((a) => {
        if (a.Children && a.Children.length !== 0) {
          this.format(a.Children)
        }
        a.MaxSort = tree[index].Sort
        if (a.MaxSort !== a.Sort) {
          a.DownId = tree[i + 1].Id
        } else if (i !== 0) {
          a.UpId = tree[i - 1].Id
        }
        i = i + 1
      })
      this.trees = tree
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
