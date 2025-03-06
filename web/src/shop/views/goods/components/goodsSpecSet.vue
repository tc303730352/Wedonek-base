<template>
  <el-card>
    <div slot="header">
      <span>商品规格配置</span>
    </div>
    <w-table :data="items" :columns="columns" :is-paging="false" row-key="Id">
      <template slot="Icon" slot-scope="e">
        <img
          v-if="e.row.Icon != null && e.row.Icon != ''"
          :src="e.row.Icon"
          width="40px"
        >
      </template>
      <template slot="GroupName" slot-scope="e">
        <span>{{ e.row.GroupName }}</span>
        <div class="groupEdit">
          <el-button
            v-if="e.row.GroupSort != minGroupSort"
            icon="el-icon-caret-top"
            size="mini"
            circle
            @click="setGSort(e.row, 'up')"
          />
          <el-button
            v-if="e.row.GroupSort != maxGroupSort"
            size="mini"
            icon="el-icon-caret-bottom"
            circle
            @click="setGSort(e.row, 'down')"
          />
          <el-button
            v-if="e.row.TemplateId == null"
            size="mini"
            type="primary"
            title="编辑"
            icon="el-icon-edit"
            circle
            @click="editSpecGroup(e.row)"
          />
          <el-button
            size="mini"
            type="danger"
            title="删除"
            icon="el-icon-delete"
            circle
            @click="dropGroup(e.row)"
          />
        </div>
      </template>
      <template slot="Sort" slot-scope="e">
        <el-button
          v-if="e.row.type == 1 && e.row.Sort != minSort[e.row.GroupId]"
          icon="el-icon-caret-top"
          size="mini"
          style="float: left"
          circle
          @click="setSort(e.row, 'up')"
        />
        <el-button
          v-if="e.row.type == 1 && e.row.Sort != maxSort[e.row.GroupId]"
          size="mini"
          style="float: right"
          icon="el-icon-caret-bottom"
          circle
          @click="setSort(e.row, 'down')"
        />
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="添加规格项"
          icon="el-icon-plus"
          circle
          @click="add(e.row)"
        />
        <el-button
          v-if="e.row.type == 1"
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
        <el-button
          v-if="e.row.type == 1"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :loading="loading"
        size="mini"
        type="primary"
        @click="save"
      >保存并下一步</el-button>
      <el-button :loading="loading" @click="reset">重置</el-button>
      <el-button :loading="loading" @click="prev">上一步</el-button>
    </el-row>
    <goodsSpecEdit
      :visible="visibleEdit"
      :goods-id="goodsId"
      :spec="editSpec"
      @close="visibleEdit = false"
      @save="saveSpec"
    />
    <goodsSpecGroupEdit
      :visible="visibleGroup"
      :goods-id="goodsId"
      :group="editGroup"
      @close="visibleGroup = false"
      @save="saveGroup"
    />
  </el-card>
</template>

<script>
import {
  Sync,
  DeleteSpec,
  SetSpecSort,
  DeleteGroup,
  SetGroupSort
} from '@/shop/api/goodsSpec'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
import goodsSpecEdit from './goodsSpecEdit.vue'
import goodsSpecGroupEdit from './goodsSpecGroupEdit.vue'
export default {
  components: {
    imageUpBtn,
    goodsSpecEdit,
    goodsSpecGroupEdit
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    goodsId: {
      type: String,
      default: null
    },
    categoryId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      loading: false,
      items: [],
      visibleEdit: false,
      visibleGroup: false,
      maxSort: {},
      editGroup: {
        Id: null,
        GroupName: null
      },
      minSort: {},
      minGroupSort: 0,
      maxGroupSort: 0,
      editSpec: {
        Id: null,
        SpecName: null,
        SpecId: null,
        GroupId: null
      },
      columns: [
        {
          key: 'GroupName',
          title: '规格组名',
          align: 'center',
          resizable: false,
          isMerge: true,
          slotName: 'GroupName',
          width: 255,
          renderHeader: this.createHeader
        },
        {
          key: 'SpecName',
          title: '规格名',
          align: 'center',
          resizable: false,
          width: 255
        },
        {
          key: 'Icon',
          title: '图标',
          align: 'center',
          resizable: false,
          slotName: 'Icon',
          width: 200
        },
        {
          key: 'Sort',
          title: '排序',
          align: 'center',
          resizable: false,
          slotName: 'Sort',
          width: 100
        },
        {
          key: 'Action',
          title: '操作规格',
          resizable: false,
          align: 'left',
          slotName: 'action'
        }
      ]
    }
  },
  computed: {},
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    createHeader(h, e) {
      const list = [<span>{e.column.label}</span>]
      list.push(
        <el-button
          style='float:right;'
          size='small'
          type='primary'
          title='添加组'
          icon='el-icon-plus'
          vOn:click={() => this.addGroup()}
          circle
        ></el-button>
      )
      return <div>{list}</div>
    },
    editSpecGroup(row) {
      this.editGroup.Id = row.GroupId
      this.editGroup.GroupName = row.GroupName
      this.visibleGroup = true
    },
    addGroup() {
      this.editGroup.Id = null
      this.editGroup.GroupName = null
      this.visibleGroup = true
    },
    dropGroup(row) {
      const title = '确认删除该规格组：' + row.GroupName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row.GroupId)
      })
    },
    async submitDrop(id) {
      await DeleteGroup(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      const index = this.source.findIndex((c) => c.Id == id)
      this.source.splice(index, 1)
      this.items = this.formatRow(this.source)
    },
    drop(row) {
      const title =
        '确认删除该规格：' + row.GroupName + '-' + row.SpecName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.remove(row)
      })
    },
    save() {
      if (this.source.length == 0) {
        this.$message({
          type: 'error',
          message: '商品规格不能为空!'
        })
        return
      }
      this.$emit('next')
    },
    async setSort(row, type) {
      const sort = type == 'up' ? row.Sort - 1 : row.Sort + 1
      if (
        sort < this.minSort[row.GroupId] ||
        sort > this.maxSort[row.GroupId]
      ) {
        return
      }
      const sortDic = await SetSpecSort(row.Id, sort)
      if (sortDic) {
        const item = this.source.find((c) => c.Id == row.GroupId)
        for (const c in sortDic) {
          const t = item.Spec.find((a) => a.Id == c)
          t.Sort = sortDic[c]
        }
        item.Spec.OrderBy('Sort')
        this.$nextTick(function() {
          this.items = this.formatRow(this.source)
        })
      }
    },
    async setGSort(row, type) {
      const sort = type == 'up' ? row.GroupSort - 1 : row.GroupSort + 1
      if (sort < this.minGroupSort || sort > this.maxGroupSort) {
        return
      }
      const sortDic = await SetGroupSort(row.GroupId, sort)
      if (sortDic) {
        for (const c in sortDic) {
          const t = this.source.find((a) => a.Id == c)
          t.Sort = sortDic[c]
        }
        this.source.OrderBy('Sort')
        this.$nextTick(function() {
          this.items = this.formatRow(this.source)
        })
      }
    },
    async remove(row) {
      await DeleteSpec(row.Id)
      const data = this.source.find((c) => c.Id == row.GroupId)
      if (data == null) {
        return
      }
      const index = data.Spec.findIndex((c) => c.Id == row.Id)
      if (index != -1) {
        data.Spec.splice(index, 1)
        this.items = this.formatRow(this.source)
      }
    },
    add(row) {
      this.editSpec.Id = null
      this.editSpec.SpecName = null
      this.editSpec.GroupId = row.GroupId
      this.visibleEdit = true
    },
    edit(row) {
      this.editSpec.Id = row.Id
      this.editSpec.SpecName = row.SpecName
      this.editSpec.GroupId = row.GroupId
      this.visibleEdit = true
    },
    formatRow(list) {
      const rows = []
      let maxG = 0
      let minG = 999
      list.forEach((a) => {
        if (a.Sort > maxG) {
          maxG = a.Sort
        }
        if (a.Sort < minG) {
          minG = a.Sort
        }
        if (a.Spec != null && a.Spec.length > 0) {
          const index = rows.length
          let max = 0
          let min = 999
          a.Spec.forEach((c) => {
            rows.push({
              Id: c.Id,
              type: 1,
              GroupId: a.Id,
              GroupName: a.GroupName,
              SpecName: c.SpecName,
              Sort: c.Sort,
              GroupSort: a.Sort,
              Icon: c.SpecIcon,
              rowSpan: {
                GroupName: 0
              }
            })
            if (c.Sort > max) {
              max = c.Sort
            }
            if (c.Sort < min) {
              min = c.Sort
            }
          })
          this.maxSort[a.Id] = max
          this.minSort[a.Id] = min
          rows[index].rowSpan['GroupName'] = a.Spec.length
        } else {
          this.maxSort[a.Id] = 0
          this.minSort[a.Id] = 0
          rows.push({
            Id: a.Id,
            type: 0,
            GroupId: a.Id,
            GroupName: a.GroupName,
            GroupSort: a.Sort,
            rowSpan: {
              GroupName: 1
            }
          })
        }
      })
      this.maxGroupSort = maxG
      this.minGroupSort = minG
      return rows
    },
    prev() {
      this.$emit('prev')
    },
    async reset() {
      this.loading = true
      const list = await Sync(this.goodsId, this.categoryId)
      if (list == null) {
        this.source = []
        this.items = []
      } else {
        this.source = list
        this.items = this.formatRow(list)
      }
      this.loading = false
    },
    saveGroup(e) {
      this.visibleGroup = false
      if (e.type == 'set' && e.isUpdate == false) {
        return
      } else if (e.type == 'set') {
        this.setSpecGroup(e.data)
      } else {
        this.addSpecGroup(e.data)
      }
    },
    addSpecGroup(data) {
      this.loading = true
      this.maxGroupSort = this.maxGroupSort + 1
      data.Sort = this.maxGroupSort
      this.source.push(data)
      this.items = this.formatRow(this.source)
      this.loading = false
    },
    setSpecGroup(row) {
      this.loading = true
      const item = this.source.find((c) => c.Id == row.Id)
      item.GroupName = row.GroupName
      this.items = this.formatRow(this.source)
      this.loading = false
    },
    saveSpec(e) {
      this.visibleEdit = false
      if (e.type == 'set' && e.isUpdate == false) {
        return
      } else if (e.type == 'set') {
        this.setSpec(e.data)
      } else {
        this.addSpec(e.data)
      }
    },
    addSpec(row) {
      this.loading = true
      const item = this.source.find((c) => c.Id == row.GroupId)
      if (item.Spec == null) {
        item.Spec = [row]
      } else {
        item.Spec.push(row)
      }
      this.items = this.formatRow(this.source)
      this.loading = false
    },
    setSpec(row) {
      this.loading = true
      const item = this.source.find((c) => c.Id == row.GroupId)
      const index = item.Spec.findIndex((c) => c.Id == row.Id)
      const spec = item.Spec[index]
      spec.SpecIcon = row.SpecIcon
      spec.SpecName = row.SpecName
      this.items = this.formatRow(this.source)
      this.loading = false
    }
  }
}
</script>
<style type="less" scoped>
.groupEdit {
  position: absolute;
  bottom: 0;
  width: 100%;
  height: 30px;
  text-align: right;
  left: 0;
  padding-right: 5px;
  line-height: 30px;
  .el-button {
    padding: 3px !important;
  }
}
</style>
