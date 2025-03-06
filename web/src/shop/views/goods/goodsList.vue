<template>
  <leftRightSplit :left-span="4">
    <categoryTree
      slot="left"
      :is-chiose-end="false"
      :is-auto-chiose="false"
      @change="change"
    />
    <el-card slot="right">
      <div slot="header">
        <span>{{ title }}</span>
      </div>
      <el-row>
        <el-form :inline="true" :model="queryParam">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="商品名\SPU"
              @change="load"
            />
          </el-form-item>
          <el-form-item label="状态">
            <el-select
              v-model="queryParam.Status"
              placeholder="商品状态"
              :multiple="true"
              @change="load"
            >
              <el-option
                v-for="item in GoodsStatus"
                :key="item.value"
                :value="item.value"
                :label="item.text"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button @click="reset">重置</el-button>
            <el-button type="success" @click="add">添加商品</el-button>
          </el-form-item>
        </el-form>
      </el-row>
      <w-table
        :data="dataList"
        :is-paging="true"
        :columns="columns"
        :paging="paging"
        row-key="Id"
        @load="load"
      >
        <template slot="Status" slot-scope="e">
          <span :style="{ color: GoodsStatus[e.row.Status].color }">
            {{ GoodsStatus[e.row.Status].text }}
          </span>
        </template>
        <template slot="IsVirtual" slot-scope="e">
          {{ e.row.IsVirtual ? "是" : "否" }}
        </template>
        <template slot="PublicTime" slot-scope="e">
          <span v-if="e.row.Status != 0">{{
            moment(e.row.PublicTime).format("YYYY-MM-DD HH:mm")
          }}</span>
        </template>
        <template slot="AddTime" slot-scope="e">
          {{ moment(e.row.AddTime).format("YYYY-MM-DD HH:mm") }}
        </template>
        <template slot="OffShelfTime" slot-scope="e">
          <span v-if="e.row.OffShelfTime == 3">{{
            moment(e.row.OffShelfTime).format("YYYY-MM-DD HH:mm")
          }}</span>
        </template>
        <template slot="GoodsName" slot-scope="e">
          <div class="goods">
            <div v-if="e.row.MainImg" class="img">
              <el-image :src="e.row.MainImg" class="cover" />
            </div>
            <div class="content">
              <div class="item" :title="e.row.GoodsName" @click="show(e.row)">
                {{
                  e.row.GoodsName
                }}
              </div>
              <div class="spuNo">SPU：{{ e.row.GoodsSpu }}</div>
            </div>
          </div>
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            size="mini"
            type="primary"
            title="详细"
            icon="el-icon-view"
            circle
            @click="show(e.row)"
          />
          <el-button
            v-if="e.row.Status == 2"
            size="mini"
            type="warning"
            title="下架"
            icon="el-icon-download"
            circle
            @click="offshelf(e.row)"
          />
          <el-button
            v-if="e.row.Status == 0 || e.row.Status == 3"
            size="mini"
            type="primary"
            title="编辑"
            icon="el-icon-edit"
            circle
            @click="edit(e.row)"
          />
          <el-button
            v-if="e.row.Status == 0"
            size="mini"
            type="danger"
            title="删除"
            icon="el-icon-delete"
            circle
            @click="drop(e.row)"
          />
        </template>
      </w-table>
    </el-card>
  </leftRightSplit>
</template>

<script>
import moment from 'moment'
import * as goodsApi from '../../api/goods'
import categoryTree from '../../components/category/categoryTree.vue'
import { GoodsStatus } from '@/shop/config/shopConfig'
export default {
  components: {
    categoryTree
  },
  data() {
    return {
      dataList: [],
      GoodsStatus,
      title: null,
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          slotName: 'GoodsName',
          width: 300,
          fixed: 'left'
        },
        {
          key: 'CategoryName',
          title: '所属类目',
          align: 'center',
          minWidth: 130
        },
        {
          key: 'SkuNum',
          title: '拥有的SKU数量',
          align: 'right',
          minWidth: 100
        },
        {
          key: 'CommentNum',
          title: '评论数',
          align: 'right',
          minWidth: 100
        },
        {
          key: 'Status',
          title: '状态',
          align: 'center',
          slotName: 'Status',
          minWidth: 100
        },
        {
          key: 'IsVirtual',
          title: '是否为虚拟物品',
          slotName: 'IsVirtual',
          align: 'center',
          minWidth: 114
        },
        {
          key: 'PublicTime',
          title: '发布时间',
          align: 'center',
          slotName: 'PublicTime',
          minWidth: 120
        },
        {
          key: 'OffShelfTime',
          title: '下架时间',
          align: 'center',
          slotName: 'OffShelfTime',
          minWidth: 120
        },
        {
          key: 'AddTime',
          title: '添加时间',
          align: 'center',
          slotName: 'AddTime',
          minWidth: 100
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          minWidth: 150,
          fixed: 'right',
          slotName: 'action'
        }
      ],
      queryParam: {
        QueryKey: null,
        CategoryId: null,
        Lvl: null
      },
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  computed: {},
  mounted() {
    this.title = '所有商品列表'
    this.load()
  },
  methods: {
    moment,
    change(category) {
      this.title = category.Name + '-商品列表'
      this.queryParam.CategoryId = category.Id
      this.queryParam.Lvl = category.Lvl
      this.load()
    },
    offshelf(row) {
      const title = '确认下架该商品：' + row.GoodsName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitOffshelf(row)
      })
    },
    async submitOffshelf(row) {
      await goodsApi.Offshelf(row.Id)
      this.$message({
        type: 'success',
        message: '下架成功!'
      })
      this.load()
    },
    drop(row) {
      const title = '确认删除该商品：' + row.GoodsName + '?'
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
      await goodsApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    async load() {
      const res = await goodsApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.dataList = res.List
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.CategoryId = null
      this.queryParam.Status = null
      this.queryParam.Lvl = null
      this.paging.Index = 1
      this.load()
    },
    add() {
      this.$router.push({ name: 'goodsAdd' })
    },
    show(row) {
      this.$router.push({ name: 'goodsDetailed', params: { id: row.Id }})
    },
    edit(row) {
      this.$router.push({
        name: 'goodsEdit',
        params: { id: row.Id, cid: row.CategoryId }
      })
    }
  }
}
</script>

<style  scoped>
.goods {
  width: 100%;
  height: 80px;
}
.goods .img {
  width: 80px;
  float: left;
  height: 80px;
  text-align: center;
}
.goods .cover {
  width: 100%;
  max-height: 80px;
}
.goods .content {
  display: flow-root;
  text-align: left;
}
.goods .content .item {
  margin: 0;
  padding-left: 5px;
  color: #1890ff;
  font-size: 16px;
  cursor: pointer;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: break-spaces;
  line-height: 20px;
  max-height: 40px;
}

.goods .content .spuNo {
  padding-left: 10px;
  margin: 0;
  color: #82848a;
  font-size: 12px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}
.goods .content i {
  font-weight: 600;
}
</style>
