<template>
  <div>
    <el-card>
      <div slot="header">
        <span>选择促销商品</span>
      </div>
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
                placeholder="商品名\SKU"
                @change="search"
              />
            </el-form-item>
            <el-form-item>
              <el-button @click="resetSearch">重置</el-button>
              <el-button type="success" @click="chiose">选择促销商品</el-button>
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
        <template slot="GoodsName" slot-scope="e">
          <span
            :style="{ color: GoodsStatus[e.row.GoodsStatus].color }"
          >({{ GoodsStatus[e.row.GoodsStatus].text }})</span>
          <span>{{ e.row.GoodsName }}</span>
        </template>
        <template slot="SpecsShow" slot-scope="e">
          <span style="white-space: pre">{{ e.row.SpecsShow }}</span>
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
              <div class="item">
                <span
                  v-if="e.row.SkuIsEnable"
                  style="color: #43af2b"
                >(已启用)</span>
                <span v-else style="color: #e4393c">(已停用)</span>
                {{ e.row.SkuName }}
              </div>
              <div class="skuNo">编号：{{ e.row.GoodsSku }}</div>
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
      <el-row style="text-align: center; margin-top: 20px">
        <el-button type="warning" @click="loadSku">重置</el-button>
        <el-button @click="prevStep">返回上一步</el-button>
      </el-row>
    </el-card>
    <chioseSku
      :visible="visible"
      :sku-id="skuId"
      :is-multiple="true"
      @save="saveSku"
      @close="visible = false"
    />
  </div>
</template>

<script>
import { GoodsStatus } from '@/shop/config/shopConfig'
import * as couponApi from '@/shop/api/couponGoods'
import chioseSku from '@/shop/components/goods/chioseSku.vue'
export default {
  components: {
    chioseSku
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      GoodsStatus,
      loading: false,
      skuList: [],
      dataList: [],
      selectKeys: [],
      visible: false,
      skuId: null,
      queryParam: {
        QueryKey: null
      },
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          minWidth: 140,
          slotName: 'GoodsName'
        },
        {
          key: 'sku',
          title: '规格名',
          align: 'left',
          slotName: 'sku',
          minWidth: 300
        },
        {
          key: 'SpecsShow',
          title: '规格说明',
          align: 'left',
          slotName: 'SpecsShow',
          minWidth: 150
        },
        {
          key: 'Price',
          title: '原价格',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'action',
          title: '操作',
          align: 'center',
          slotName: 'action',
          minWidth: 100
        }
      ]
    }
  },
  computed: {},
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.loadSku()
        }
      },
      immediate: true
    }
  },
  methods: {
    async batchDrop() {
      if (this.selectKeys == null || this.selectKeys.length == 0) {
        return
      }
      await couponApi.BatchDelete(this.id, this.selectKeys)
      this.loadSku()
    },
    prevStep() {
      this.$emit('prev')
    },
    async drop(row) {
      await couponApi.Delete(row.Id)
      const index = this.skuList.findIndex((c) => c.Id == row.Id)
      if (index !== -1) {
        this.skuList.splice(index, 1)
        this.reset(this.skuList)
      }
    },
    chiose() {
      this.visible = true
    },
    nextStep() {
      this.$emit('next')
    },
    resetSearch() {
      this.reset(this.skuList)
    },
    reset(list) {
      this.skuId = list.map((c) => c.SkuId)
      this.queryParam.QueryKey = null
      this.dataList = list
      this.selectKeys = []
    },
    async saveSku(e) {
      this.visible = false
      this.skuId = e.keys
      this.skuList = await couponApi.Set(
        this.id,
        e.skus.map((c) => {
          return {
            GoodsId: c.GoodsId,
            SkuId: c.SkuId
          }
        })
      )
      this.reset(this.skuList)
    },
    search() {
      if (this.queryParam.QueryKey == null || this.queryParam.QueryKey == '') {
        this.reset(this.skuList)
        return
      }
      this.selectKeys = []
      this.dataList = this.skuList.filter(
        (c) =>
          c.SkuName.includes(this.queryParam.QueryKey) ||
            c.GoodsSku.includes(this.queryParam.QueryKey)
      )
    },
    async loadSku() {
      const res = await couponApi.Gets(this.id)
      this.skuList = res
      this.reset(res)
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
