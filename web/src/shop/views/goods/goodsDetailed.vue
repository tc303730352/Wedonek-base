<template>
  <el-card>
    <div slot="header">
      <span>基本信息</span>
      <div
        v-if="
          goods.Status == GoodsStatus[0].value ||
            goods.Status == GoodsStatus[3].value
        "
        style="float: right"
      >
        <el-button type="primary" @click="edit">编辑</el-button>
        <el-button
          type="success"
          icon="el-icon-upload2"
          @click="beginPublic"
        >发布</el-button>
      </div>
      <div
        v-if="goods.Status == GoodsStatus[2].value"
        style="float: right"
      >
        <el-button
          type="warning"
          icon="el-icon-download"
          @click="offshelf()"
        >下架</el-button>
      </div>
    </div>
    <div>
      <div v-if="goods.MainImg" style="float: left; width: 120px;">
        <p style="text-align: center;font-weight: 700;font-size: 14px;margin: 0;">封面图</p>
        <el-image :src="goods.MainImg" style="width:100%" />
      </div>
      <div style="float: left;">
        <el-form :inline="true" label-width="150px">
          <el-form-item label="SPU">
            {{ goods.GoodsSpu }}
          </el-form-item>
          <el-form-item label="商品名">
            {{ goods.GoodsName }}
          </el-form-item>
          <el-form-item label="物流费用方案">
            {{ goods.LogisticsName }}
          </el-form-item>
          <el-form-item label="是否为虚拟商品">
            {{ goods.IsVirtual ? "是" : "否" }}
          </el-form-item>
          <el-form-item label="后台类目">
            <el-breadcrumb separator="/">
              <el-breadcrumb-item
                v-for="(name, index) in goods.CategoryName"
                :key="index"
              >{{ name }}</el-breadcrumb-item>
            </el-breadcrumb>
          </el-form-item>
          <el-form-item label="商品状态">
            <span :style="{ color: GoodsStatus[goods.Status].color }">{{
              GoodsStatus[goods.Status].text
            }}</span>
          </el-form-item>
        </el-form>
      </div>
      <div style="clear: both;" />
    </div>
    <el-tabs v-model="activeName" style="margin-top: 20px" type="card">
      <el-tab-pane name="pc" label="PC端预览">
        <pcPreview
          :source="goods"
          :is-load="activeName == 'pc'"
        />
      </el-tab-pane>
      <el-tab-pane label="小程序预览" name="mini">
        <miniPreview
          :source="goods"
          :is-load="activeName == 'mini'"
        />
      </el-tab-pane>
    </el-tabs>
    <goodsPublic
      :visible="visible"
      :goods-id="goods.Id"
      :goods-name="goods.GoodsName"
      @close="close"
    />
  </el-card>
</template>

<script>
import pcPreview from './components/pcPreview'
import miniPreview from './components/miniPreview.vue'
import { GetDetailed, Offshelf } from '@/shop/api/goods'
import goodsPublic from './components/goodsPublic.vue'
import { GoodsStatus } from '@/shop/config/shopConfig'
export default {
  components: {
    pcPreview,
    miniPreview,
    goodsPublic
  },
  data() {
    return {
      GoodsStatus,
      activeName: null,
      loading: false,
      visible: false,
      goodsId: null,
      visiblePrice: false,
      goods: {
        Id: null,
        Status: 0
      }
    }
  },
  computed: {},
  mounted() {
    this.goodsId = this.$route.params.id
    if (this.goodsId) {
      this.reset()
    } else {
      this.$router.push({ name: 'goodsList' })
    }
  },
  methods: {
    close(isPublic) {
      this.visible = false
      if (isPublic) {
        this.goods.Status = 1
      }
    },
    offshelf() {
      const title = '确认下架该商品：' + this.goods.GoodsName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitOffshelf()
      })
    },
    async submitOffshelf() {
      await Offshelf(this.goodsId)
      this.$message({
        type: 'success',
        message: '下架成功!'
      })
      this.goods.Status = 3
    },
    beginPublic() {
      this.visible = true
    },
    edit() {
      this.$router.push({
        name: 'goodsEdit',
        params: { id: this.goodsId, cid: this.goods.CategoryId }
      })
    },
    async reset() {
      const data = await GetDetailed(this.goodsId)
      if (data == null) {
        this.$router.push({ name: 'goodsList' })
        return
      }
      this.goods = data
      this.activeName = 'pc'
    }
  }
}
</script>
<style lang="scss" scoped>
.el-form--inline .el-form-item {
  width: 32%;
}
.el-form-item .el-breadcrumb {
  line-height: inherit;
}
</style>

<style lang="css">
.goodsTabs {
  background-color: #fff;
}
.goodsTabs .el-tabs__header .el-tabs__item {
  height: 38px;
  line-height: 38px;
  font-size: 14px;
  color: #666;
  border-style: none;
  width: 106px;
}

.goodsTabs .el-tabs__header .el-tabs__item:hover {
  color: #e4393c;
}
.goodsTabs .el-tabs__header {
  border-bottom: 1px solid #e4393c;
}
.goodsTabs .is-active {
  background-color: #e4393c;
  color: #fff !important;
  cursor: default;
}
</style>
