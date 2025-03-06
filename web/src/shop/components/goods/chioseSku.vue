<template>
  <el-dialog
    title="选择商品"
    :visible="visible"
    width="800"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
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
                placeholder="商品名\SKU"
                @change="load"
              />
            </el-form-item>
            <el-form-item>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <w-table
          :data="dataList"
          :is-paging="true"
          :columns="columns"
          :paging="paging"
          :is-select="true"
          :is-multiple="isMultiple"
          :select-keys="selectKeys"
          row-key="SkuId"
          @selected="selectEvent"
          @load="load"
        >
          <template slot="SpecsShow" slot-scope="e">
            <span :title="e.row.SpecsShow" style="white-space: pre">{{
              e.row.SpecsShow
            }}</span>
          </template>
          <template slot="sku" slot-scope="e">
            <div class="sku">
              <div class="img">
                <el-avatar
                  shape="square"
                  :size="80"
                  :src="e.row.SkuImg"
                />
              </div>
              <div class="content">
                <div class="item" :title="e.row.SkuName">
                  {{ e.row.SkuName }}
                </div>
                <div class="skuNo">编号：{{ e.row.GoodsSku }}</div>
              </div>
            </div>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <el-row :gutter="24">
      <el-col
        :span="2"
        style="text-align: right; line-height: 30px"
      >已选:</el-col>
      <el-col :span="20" style="line-height: 30px">
        <el-tag
          v-for="sku in selectSku"
          :key="sku.SkuId"
          :disable-transitions="true"
          style="margin-bottom: 5px; margin-right: 5px"
          closable
          @close="dropSku(sku)"
        >
          {{ sku.SkuName }}
        </el-tag>
      </el-col>
    </el-row>
    <el-row style="text-align: center">
      <el-button type="success" @click="save">确认选择</el-button>
      <el-button type="default" @click="handleClose">关闭</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import moment from 'moment'
import * as skuApi from '@/shop/api/goodsSku'
import categoryTree from '../../components/category/categoryTree.vue'
export default {
  components: {
    categoryTree
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    skuId: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      dataList: [],
      selectKeys: [],
      selectSku: [],
      skuCache: {},
      title: '选择规格商品',
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          minWidth: 140,
          fixed: 'left'
        },
        {
          key: 'sku',
          title: '规格名',
          align: 'left',
          slotName: 'sku',
          minWidth: 300,
          fixed: 'left'
        },
        {
          key: 'SpecsShow',
          title: '规格说明',
          align: 'left',
          slotName: 'SpecsShow',
          minWidth: 150
        },
        {
          key: 'CategoryName',
          title: '所属类目',
          align: 'center',
          minWidth: 120
        },
        {
          key: 'Price',
          title: '价格',
          align: 'center',
          minWidth: 100
        }
      ],
      queryParam: {
        QueryKey: null,
        CategoryId: null,
        Lvl: null
      },
      paging: {
        Size: 5,
        Index: 1,
        SortName: 'SkuId',
        IsDesc: false,
        Total: 0
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
    moment,
    change(category) {
      this.title = category.Name + '-商品列表'
      this.queryParam.CategoryId = category.Id
      this.queryParam.Lvl = category.Lvl
      this.load()
    },
    handleClose() {
      this.$emit('close')
    },
    dropSku(sku) {
      this.selectSku = this.selectSku.filter((c) => c.SkuId !== sku.SkuId)
      this.selectKeys = this.selectKeys.filter((c) => c !== sku.SkuId)
    },
    selectEvent(e) {
      this.selectKeys = e.keys
      e.list.forEach((c) => {
        this.skuCache[c.SkuId] = c
      })
      this.selectSku = e.keys.map((c) => this.skuCache[c])
    },
    async load() {
      const res = await skuApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.dataList = res.List
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
    },
    save() {
      if (this.selectKeys.length == 0) {
        this.$message({
          message: '请选择商品!',
          type: 'error'
        })
        return
      }
      this.$emit('save', {
        keys: this.selectKeys,
        skus: this.selectSku
      })
    },
    async loadSku() {
      const skus = await skuApi.Gets(this.selectKeys)
      if (skus && skus.length > 0) {
        skus.forEach((c) => {
          this.skuCache[c.SkuId] = c
        })
      }
      this.selectSku = skus
    },
    resetAll() {
      if (this.skuId == null || this.skuId.length == 0) {
        this.selectKeys = []
        this.selectSku = []
      } else {
        this.selectKeys = this.skuId
        this.loadSku()
      }
      this.reset()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.CategoryId = null
      this.queryParam.Lvl = null
      this.paging.Index = 1
      this.load()
    }
  }
}
</script>

<style  scoped>
.sku {
  width: 100%;
  height: 80px;
}
.sku .img {
  width: 80px;
  float: left;
  height: 80px;
  text-align: center;
}
.sku image {
  width: 100%;
}
.sku .content {
  display: flow-root;
  text-align: left;
}
.sku .content .item {
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

.sku .content .skuNo {
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
.sku .content i {
  font-weight: 600;
}
</style>
