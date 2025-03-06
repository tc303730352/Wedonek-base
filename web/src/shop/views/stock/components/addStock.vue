<template>
  <el-dialog
    :visible="visible"
    width="500px"
    :title="title"
    :modal-append-to-body="false"
    :before-close="handleClose"
  >
    <el-row :gutter="24" class="row">
      <el-col :span="6" style="text-align: right">当前库存：</el-col>
      <el-col :span="18">
        {{ curStock }}
      </el-col>
    </el-row>
    <el-row :gutter="24" class="row">
      <el-col :span="6" style="text-align: right">补存库存数：</el-col>
      <el-col :span="18">
        <el-input-number
          v-model="stockNum"
          :min="0"
          :step="1"
          placeholder="补存库存数"
        />
      </el-col>
    </el-row>
    <div slot="footer">
      <el-button
        type="danger"
        :loading="loading"
        @click="submit"
      >补存库存</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { GetStockNum, Add } from '@/shop/api/goodsStock'
export default {
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    skuName: {
      type: String,
      default: null
    },
    skuId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '补存库存',
      curStock: 0,
      loading: false,
      stockNum: 0
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
    handleClose() {
      this.$emit('close', false)
    },
    reset() {
      this.title = '商品（' + this.skuName + ')补存库存'
      this.load()
    },
    async load() {
      this.curStock = await GetStockNum(this.skuId)
    },
    async add() {
      try {
        const isAdd = await Add({
          SkuId: this.skuId,
          Stock: this.stockNum
        })
        if (isAdd) {
          this.$message({
            type: 'success',
            message: '补充库存成功!'
          })
          this.$emit('close', true)
        } else {
          this.$message({
            type: 'warning',
            message: '补充库存失败，当前库存已变更!'
          })
          this.load()
        }
      } catch {}
    },
    submit() {
      if (this.stockNum == 0) {
        this.$message({
          type: 'error',
          message: '补存库存数必须大于零!'
        })
        return
      }
      const title = '确认补存：' + this.skuName + '的库存?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.add()
      })
    }
  }
}
</script>
<style lang="scss" scoped>
.row {
  height: 30px;
  line-height: 30px;
  margin-bottom: 10px;
}
</style>
