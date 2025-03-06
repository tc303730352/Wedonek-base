<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
    </div>
    <el-steps
      style="margin-bottom: 20px"
      :active="active"
      finish-status="success"
      @click="chioseStep"
    >
      <el-step title="商品信息" @click.native="chioseStep(0)" />
      <el-step title="商品规格" @click.native="chioseStep(1)" />
      <el-step title="规格详细" @click.native="chioseStep(2)" />
    </el-steps>
    <goodsDatum
      v-if="active == 0"
      :id="goodsId"
      :is-load="active == 0"
      @next="saveGoods"
    />
    <goodsSpecSet
      v-else-if="active == 1"
      :is-load="active == 1"
      :category-id="categoryId"
      :goods-id="goodsId"
      @next="nextStep"
      @prev="prevStep"
    />
    <goodsSku
      v-else-if="active == 2"
      :is-load="active == 2"
      :category-id="categoryId"
      :goods-id="goodsId"
      @next="nextStep"
      @prev="prevStep"
    />
  </el-card>
</template>

<script>
import { SyncSku } from '../../api/goodsSpec'
import goodsDatum from './components/goodsDatum.vue'
import goodsSpecSet from './components/goodsSpecSet.vue'
import goodsSku from './components/goodsSkuSet.vue'
export default {
  components: {
    goodsDatum,
    goodsSpecSet,
    goodsSku
  },
  data() {
    return {
      title: '新增商品',
      loading: false,
      active: -1,
      goodsId: null,
      categoryId: null
    }
  },
  computed: {},
  mounted() {
    const id = this.$route.params.id
    const cid = this.$route.params.cid
    if (id == null) {
      this.title = '新增商品'
    } else {
      this.title = '编辑商品'
      this.goodsId = parseInt(id)
      this.categoryId = parseInt(cid)
    }
    this.active = 0
  },
  methods: {
    saveGoods(data) {
      this.goodsId = data.Id
      this.categoryId = data.CategoryId
      this.nextStep()
    },
    chioseStep(index) {
      if (index == this.active) {
        return
      } else if (index > 0 && this.goodsId == null) {
        return
      } else if (index == 2) {
        this.sync()
      } else {
        this.active = index
      }
    },
    prevStep() {
      this.active = this.active - 1
    },
    nextStep() {
      const step = this.active + 1
      if (step == 2) {
        this.sync()
      } else {
        this.active = step
      }
    },
    async sync() {
      await SyncSku(this.goodsId)
      this.active = 2
    }
  }
}
</script>
