<template>
  <el-dialog
    title="选择展示的商品"
    :visible="visible"
    width="800"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <el-row>
      <el-col :span="12">
        <el-button
          :disabled="selectKeys.length == 0"
          @click="batchDrop"
        >批量撤出</el-button>
      </el-col>
      <el-col :span="12">
        <el-form style="float: right" :inline="true" :model="queryParam">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="商品名\SPU编号"
              @change="search"
            />
          </el-form-item>
          <el-form-item>
            <el-button @click="resetSearch">重置</el-button>
            <el-button type="success" @click="chiose">选择商品</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <w-table
      :data="dataList"
      :is-paging="false"
      :columns="columns"
      max-height="600px"
      :is-select="true"
      :is-multiple="true"
      :select-keys="selectKeys"
      row-key="Id"
      @selected="
        (e) => {
          selectKeys = e.keys;
        }
      "
    >
      <template slot="Sort" slot-scope="e">
        <el-input-number v-model="e.row.Sort" :min="1" :max="source.length" style="width: 120px;" @change="setSort(e.row)" />
      </template>
      <template slot="IsVirtual" slot-scope="e">
        {{ e.row.IsVirtual ? '是': '否' }}
      </template>
      <template slot="GoodsName" slot-scope="e">
        <div class="goods">
          <div class="img">
            <el-image
              shape="square"
              :src="e.row.MainImg"
              width="80px"
            />
          </div>
          <div class="content">
            <div class="item">
              <span
                :style="{ color: GoodsStatus[e.row.Status].color }"
              >({{ GoodsStatus[e.row.Status].text }})</span>
              {{ e.row.GoodsName }}
            </div>
            <div class="spuNo">SPU：{{ e.row.GoodsSpu }}</div>
          </div>
        </div>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="danger"
          title="撤出"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <chioseSpu
      :visible="spuVisible"
      :is-multiple="true"
      :spu-id="goodsId"
      @close="spuVisible = false"
      @save="saveSpu"
    />
  </el-dialog>
</template>

<script>
import { GoodsStatus } from '@/shop/config/shopConfig'
import * as goodsApi from '@/shop/api/moduleGoods'
import chioseSpu from '@/shop/components/goods/chioseSpu.vue'
export default {
  components: {
    chioseSpu
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    pageId: {
      type: String,
      default: null
    },
    moduleId: {
      type: String,
      default: null
    },
    tag: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      GoodsStatus,
      spuVisible: false,
      dataList: [],
      source: null,
      goodsId: null,
      isRefresh: false,
      selectKeys: [],
      goods: {
        name: 'goods',
        pull: true,
        put: false
      },
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          minWidth: 300,
          slotName: 'GoodsName'
        },
        {
          key: 'Category',
          title: '所属类目',
          align: 'left',
          minWidth: 120
        },
        {
          key: 'SkuNum',
          title: 'Sku数量',
          align: 'right',
          minWidth: 120
        },
        {
          key: 'IsVirtual',
          title: '虚拟物品',
          align: 'center',
          slotName: 'IsVirtual',
          minWidth: 120
        },
        {
          key: 'Price',
          title: '价格',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'Sort',
          title: '排序位',
          align: 'left',
          slotName: 'Sort',
          width: 150
        },
        {
          key: 'action',
          title: '操作',
          align: 'center',
          slotName: 'action',
          width: 100
        }
      ],
      queryParam: {
        QueryKey: null
      }
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.resetAll()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    async setSort(row) {
      const res = await goodsApi.SetSort(row.Id, row.Sort)
      for (const i in res) {
        if (i != null) {
          const item = this.source.find(a => a.Id === parseInt(i))
          if (item) {
            item.Sort = res[i]
          }
        }
      }
      this.source.OrderBy('Sort')
      this.resetSearch()
    },
    async drop(row) {
      await goodsApi.Delete(row.Id)
      const index = this.source.findIndex((c) => c.Id === row.Id)
      if (index !== -1) {
        this.source.splice(index, 1)
        this.isRefresh = true
        this.goodsId = this.source.filter(c => c !== row.GoodsId)
        this.resetSearch()
      }
    },
    search() {
      if (this.queryParam.QueryKey == null || this.queryParam.QueryKey === '') {
        this.resetSearch()
        return
      }
      this.selectKeys = []
      this.dataList = this.source.filter(
        (c) =>
          c.SkuName.includes(this.queryParam.QueryKey) ||
            c.GoodsSku.includes(this.queryParam.QueryKey)
      )
    },
    async batchDrop() {
      if (this.selectKeys == null || this.selectKeys.length === 0) {
        return
      }
      await goodsApi.BatchDelete(this.selectKeys)
      this.isRefresh = true
      this.load()
    },
    handleClose() {
      this.$emit('close', this.isRefresh, this.source)
    },
    async load() {
      this.source = await goodsApi.Gets(this.moduleId, this.tag)
      this.goodsId = this.source.map(c => c.GoodsId)
      this.dataList = this.source
    },
    chiose() {
      this.spuVisible = true
    },
    async saveSpu(e) {
      this.spuVisible = false
      this.source = await goodsApi.Sync({
        TemplateId: this.pageId,
        TModuleId: this.moduleId,
        TagKey: this.tag,
        GoodsId: e.spus.map((c) => {
          return c.Id
        })
      })
      this.goodsId = this.source.map(c => c.GoodsId)
      this.isRefresh = false
      this.resetSearch()
      this.$emit('change', this.source)
    },
    resetAll() {
      this.selectKeys = []
      this.queryParam.QueryKey = null
      this.load()
    },
    resetSearch() {
      this.selectKeys = []
      this.queryParam.QueryKey = null
      this.dataList = this.source
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
.goods image {
  width: 100%;
}
.goods .content {
  display: flow-root;
  text-align: left;
}
.goods .content .item {
  margin: 0;
  padding-left: 5px;
  font-size: 16px;
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
