
<template>
  <el-card body-style="padding:20px">
    <div slot="header">
      <span>商品规格</span>
      <el-row style="float: right">
        <el-button type="success" :loading="loading" @click="preview">发布前预览</el-button>
        <el-button :loading="loading" @click="prev">返回上一步</el-button>
      </el-row>
    </div>
    <el-tabs v-model="activeName" type="card">
      <el-tab-pane v-for="item in skuItems" :key="item.id" :name="item.id">
        <template slot="label">
          <el-switch
            v-model="item.isEnable"
            @change="stateChange(item)"
          />
          {{ item.title }}
        </template>
        <el-alert
          title="商品规格信息"
          :type="item.isEnable ? 'success' : 'info'"
          style="margin-top: 10px; margin-bottom: 10px"
          :description="item.name.join(',')"
        />
        <goodsSku
          v-if="item.isEnable"
          :goods-id="goodsId"
          :spec="{
            SpecKey: item.id,
            Name: item.name,
            SpecId: item.ids,
          }"
          :category-id="categoryId"
          :is-load="activeName == item.id"
          @save="saveSku"
        />
      </el-tab-pane>
    </el-tabs>
  </el-card>
</template>

<script>
import { GetSpecSku, SetSkuState } from '@/shop/api/goodsSpec'
import goodsSku from './goodsSku.vue'
export default {
  components: {
    goodsSku
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
      activeName: null,
      skuItems: [],
      skuSpec: {}
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
    preview() {
      this.$router.push({ name: 'goodsDetailed', params: { id: this.goodsId }})
    },
    save() {
      const list = this.skuItems.filter((c) => c.skuId == null && c.isEnable)
      if (list.length == 0) {
        this.$emit('next')
      } else {
        this.$message({
          type: 'error',
          message: '还有商品未填写信息!'
        })
      }
    },
    prev() {
      this.$emit('prev')
    },
    async stateChange(item) {
      await SetSkuState({
        GoodsId: this.goodsId,
        SpecKey: item.id,
        IsEnable: item.isEnable
      })
    },
    saveSku(e) {
      if (e.isEdit == false) {
        return
      }
      const item = this.skuItems.find((c) => c.id == specKey)
      item.skuId = skuId
      item.title =
        item.name.length > 3
          ? item.name.slice(0, 3).join(',') + '等(已填写)'
          : item.name.join(',') + '(已填写)'
    },
    async reset() {
      this.loading = true
      const list = await GetSpecSku(this.goodsId)
      if (list.length == 0) {
        this.prev()
        return
      }
      this.skuItems = list.map((c) => {
        const title =
          c.Name.length > 3
            ? c.Name.slice(0, 3).join(',') + '等'
            : c.Name.join(',')
        return {
          id: c.SpecKey,
          title: title + '(' + (c.SkuId == null ? '未填写' : '已填写') + ')',
          name: c.Name,
          ids: c.SpecId,
          isEnable: c.IsEnable,
          skuId: c.skuId
        }
      })
      this.activeName = list[0].SpecKey
      this.loading = false
    }
  }
}
</script>
  <style type="less" scoped>
</style>
