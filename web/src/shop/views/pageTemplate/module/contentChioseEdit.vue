<template>
  <el-dialog
    title="选择跳转地址"
    :visible="visible"
    width="900px"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <el-tabs v-model="active" type="card">
      <el-tab-pane label="选择商品" name="goods">
        <el-form
          ref="editGoods"
          :model="goodsConfig"
          label-width="120px"
        >
          <el-form-item label="筛选方式" prop="ChioseType">
            <el-radio-group
              v-model="goodsConfig.ChioseType"
              @change="resetGoods"
            >
              <el-radio-button :label="0">手动选择</el-radio-button>
              <el-radio-button :label="1">商品分组</el-radio-button>
              <el-radio-button :label="2">商品分类</el-radio-button>
              <el-radio-button :label="3">全部商品</el-radio-button>
            </el-radio-group>
          </el-form-item>
          <el-form-item v-if="goodsConfig.ChioseType == 0" label="选择商品">
            <el-input v-model="title" :readonly="true" placeholder="选择商品">
              <el-button slot="append" type="primary" icon="el-icon-goods" @click="chioseSpu" />
            </el-input>
          </el-form-item>
          <el-form-item v-else-if="goodsConfig.ChioseType == 2" label="商品类目" prop="CategoryId">
            <categorySelect
              v-model="goodsConfig.CategoryId"
              :is-chiose-end="true"
              placeholder="商品类目"
              :lvl.sync="goodsConfig.Level"
            />
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-tab-pane label="选择活动" name="activity">
        <el-form
          ref="editActivity"
          :model="activityConfig"
          label-width="120px"
        >
          <el-form-item label="选择活动">
            <el-input v-model="title" :readonly="true" placeholder="促销活动">
              <el-button slot="append" type="primary" icon="el-icon-goods" @click="chioseAct" />
            </el-input>
          </el-form-item>
        </el-form>
      </el-tab-pane>
      <el-tab-pane label="选择页面" name="third">角色管理</el-tab-pane>
    </el-tabs>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :loading="loading"
        type="success"
        @click="save"
      >保存</el-button>
      <el-button @click="handleClose">取消</el-button>
    </el-row>
    <moduleGoodsEdit :page-id="pageId" :module-id="moduleId" :visible="spuVisible" :tag="tag" @change="skuChange" @close="closeSku" />
    <chioseActivity :visible="actVisable" :activity-id="actId" @save="saveActivity" @close="actVisable=false" />
  </el-dialog>
</template>

<script>
import categorySelect from '@/shop/components/category/categorySelect'
import moduleGoodsEdit from './moduleGoodsEdit.vue'
import chioseActivity from '@/shop/components/activity/chioseActivity.vue'
export default {
  components: {
    categorySelect,
    moduleGoodsEdit,
    chioseActivity
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
    itemId: {
      type: Number,
      default: null
    },
    config: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      goodsConfig: {
        ChioseType: 0
      },
      activityConfig: {
        ActivityId: null,
        ActivityTitle: null
      },
      title: null,
      tag: null,
      linkConfig: {},
      loading: false,
      actVisable: false,
      spuVisible: false,
      actId: null,
      active: null
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
    closeSku(isRefresh, skus) {
      this.spuVisible = false
      if (isRefresh) {
        this.title = '共选择了(' + skus.length + ')个商品'
      }
    },
    chioseAct() {
      this.actId = null
      if (this.activityConfig.ActivityId !== null) {
        this.actId = [this.activityConfig.ActivityId]
      }
      this.actVisable = true
    },
    skuChange(skus) {
      this.title = '共选择了(' + skus.length + ')个商品'
    },
    saveActivity(e) {
      this.actVisable = false
      if (e.keys == null || e.keys.length === 0) {
        this.activityConfig.ActivityId = null
        this.title = null
      } else {
        this.activityConfig.ActivityId = e.keys[0]
        this.title = e.activity[0].ActivityTitle
      }
    },
    reset() {
      this.tag = this.itemId.toString()
      if (this.config == null) {
        this.resetGoods()
        this.active = 'goods'
      } else {
        this.title = this.config.Title
        if (this.config.JumpType === 0) {
          const sku = this.config.Sku
          this.activityConfig = {
            ActivityId: null
          }
          this.goodsConfig.ChioseType = sku.ChioseType
          this.goodsConfig.CategoryId = sku.CategoryId
          this.goodsConfig.Level = sku.Level
          this.goodsConfig.GroupName = sku.GroupName
          this.goodsConfig.GroupId = sku.GroupId
          this.active = 'goods'
        } else if (this.config.JumpType === 1) {
          this.resetGoods()
          this.activityConfig = this.config.Activity
          this.active = 'activity'
        } else if (this.config.JumpType === 2) {
          this.resetGoods()
          this.activityConfig = null
          this.linkConfig = this.config.Link
          this.active = 'page'
        }
      }
    },
    chioseSpu() {
      this.spuVisible = true
    },
    save() {
      const data = {
        Title: this.title
      }
      if (this.active === 'goods') {
        data.JumpType = 0
        const sku = {
          ChioseType: this.goodsConfig.ChioseType
        }
        if (this.goodsConfig.ChioseType === 1) {
          sku.GroupId = this.config.GroupId
          sku.GroupName = this.config.GroupName
        } else if (this.goodsConfig.ChioseType === 2) {
          sku.CategoryId = this.goodsConfig.CategoryId
          sku.Level = this.goodsConfig.Level
        }
        data.Sku = sku
      } else if (this.active === 'activity') {
        data.JumpType = 1
        data.Activity = this.activityConfig
      } else if (this.active === 'page') {
        data.JumpType = 2
        data.Link = this.linkConfig
      }
      this.$emit('save', this.itemId, data)
    },
    resetGoods() {
      this.goodsConfig.CategoryId = null
      this.goodsConfig.Level = null
      this.goodsConfig.GroupName = null
      this.goodsConfig.GroupId = null
    },
    handleClose() {
      this.$emit('close')
    }
  }
}
</script>
<style scoped>
.label {
  color: #1890ff;
  font-size: 14px;
  font-weight: 400;
}
</style>
