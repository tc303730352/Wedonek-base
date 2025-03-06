<template>
  <el-dialog
    :visible="visible"
    width="900px"
    :title="title"
    :modal-append-to-body="false"
    :before-close="handleClose"
  >
    <w-table :data="dataList" :is-paging="false" :columns="columns">
      <template slot="Stock" slot-scope="e">
        <el-input-number
          v-model="e.row.Stock"
          class="el-input"
          :min="1"
          :max="1000000"
          :step="1"
          placeholder="库存量"
        />
      </template>
    </w-table>
    <div slot="footer">
      <el-button type="danger" :loading="loading" @click="submitPublic">确认发布</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { GetStocks, Public } from '@/shop/api/goods'
export default {
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    goodsId: {
      type: String,
      default: null
    },
    goodsName: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      dataList: [],
      title: '商品发布',
      loading: false,
      columns: [
        {
          key: 'GoodsSku',
          title: 'SKU编号',
          align: 'left',
          width: 200
        },
        {
          key: 'SkuName',
          title: '规格名',
          align: 'left',
          width: 250
        },
        {
          key: 'SpecShow',
          title: '规格说明',
          align: 'center',
          minWidth: 250
        },
        {
          key: 'Stock',
          title: '库存',
          slotName: 'Stock',
          align: 'center',
          minWidth: 150
        }
      ]
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.load()
        }
      },
      immediate: true
    }
  },
  methods: {
    handleClose() {
      this.$emit('close', false)
    },
    async load() {
      const res = await GetStocks(this.goodsId)
      this.title = '商品（' + this.goodsName + ')发布'
      this.dataList = res
    },
    submitPublic() {
      if (this.dataList.length == 0) {
        this.$message({
          type: 'error',
          message: '无可出售的商品!'
        })
        return
      }
      const title = '确认发布商品：' + this.goodsName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.publicGoods()
      })
    },
    async publicGoods() {
      const data = {}
      this.dataList.forEach((c) => {
        data[c.SkuId] = c.Stock
      })
      this.loading = true
      try {
        await Public(this.goodsId, data)
        this.$message({
          type: 'success',
          message: '发布成功!'
        })
        this.$emit('close', true)
      } catch {}
      this.loading = false
    }
  }
}
</script>
