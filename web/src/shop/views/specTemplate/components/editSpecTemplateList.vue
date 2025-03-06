<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="900px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-row style="margin-bottom: 5px">
      <el-button
        style="float: right"
        type="success"
        @click="add()"
      >添加规格</el-button>
    </el-row>
    <w-table :data="specList" :columns="columns" :is-paging="false" row-key="Id">
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          v-model="e.row.IsEnable"
          @change="setIsEnabe(e.row)"
        />
      </template>
      <template slot="Sort" slot-scope="e">
        <el-input-number
          v-model="e.row.Sort"
          :min="1"
          :max="maxSort"
          placeholder="排序位"
          class="el-input"
          @change="SetSort(e.row)"
        />
      </template>
      <template slot="SpecsIcon" slot-scope="e">
        <img
          v-if="e.row.SpecsIcon != null && e.row.SpecsIcon != ''"
          :src="e.row.SpecsIcon"
          width="30px"
        >
      </template>
      <template slot="Spec" slot-scope="e">
        <el-tag
          v-for="(item, index) in e.row.Spec"
          :key="index"
          style="margin-right: 5px"
        >{{ item }}</el-tag>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          v-if="e.row.IsEnable == false"
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="edit(e.row.Id)"
        />
        <el-button
          v-if="e.row.IsEnable == false"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <editSpec
      :id="id"
      :visible="visibleEdit"
      :template-id="templateId"
      @close="closeEdit"
    />
  </el-dialog>
</template>

<script>
import * as specApi from '@/shop/api/specTemplate'
import editSpec from './editSpecTemplate.vue'
export default {
  components: {
    editSpec
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    templateId: {
      type: String,
      default: null
    },
    groupName: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑规格',
      isRefresh: false,
      visibleEdit: false,
      maxSort: 0,
      id: null,
      specList: [],
      columns: [
        {
          key: 'SpecName',
          title: '规格名',
          align: 'left'
        },
        {
          key: 'SpecsIcon',
          title: '规格图标',
          align: 'center',
          width: 100,
          slotName: 'SpecsIcon'
        },
        {
          key: 'Sort',
          title: '排序位',
          align: 'left',
          slotName: 'Sort',
          width: 150
        },
        {
          key: 'IsEnable',
          title: '是否启用',
          align: 'center',
          slotName: 'IsEnable',
          width: 130
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          width: 120,
          slotName: 'action'
        }
      ]
    }
  },
  computed: {},
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
        this.isRefresh = true
        this.reset()
      }
    },
    edit(id) {
      this.id = id
      this.visibleEdit = true
    },
    add() {
      this.id = null
      this.visibleEdit = true
    },
    async reset() {
      this.title = '编辑(' + this.groupName + ')规格'
      this.specList = await specApi.Gets(this.templateId)
      if (this.specList.length == 0) {
        this.maxSort = 1
      } else {
        this.maxSort = this.specList.length
      }
    },
    closeForm() {
      this.$emit('close', this.isRefresh)
    },
    drop(row) {
      const title = '确认删除该规格 ' + row.SpecName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row)
      })
    },
    async submitDrop(row) {
      await specApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.isRefresh = true
      const index = this.specList.findIndex((c) => c.Id == row.Id)
      this.specList.splice(index, 1)
    },
    async setIsEnabe(row) {
      await specApi.SetIsEnable(row.Id, row.IsEnable)
    },
    async SetSort(row) {
      const res = await specApi.SetSort(row.Id, row.Sort)
      this.specList.forEach((c) => {
        if (res[c.Id] != null) {
          c.Sort = res[c.Id]
        }
      })
      this.specList.OrderBy('Sort')
    }
  }
}
</script>
