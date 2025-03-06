<template>
  <el-form
    ref="editTab"
    :model="moduleConfig"
    label-width="120px"
    :rules="rules"
  >
    <el-form-item label="导航名称" prop="TabName">
      <el-input
        v-model="moduleConfig.TabName"
        maxlength="50"
        placeholder="导航名称"
      />
    </el-form-item>
    <el-form-item label="推荐方式" prop="ChioseType">
      <el-radio-group
        v-model="moduleConfig.ChioseType"
        @change="chioseChange"
      >
        <el-radio-button :label="0">手动输入</el-radio-button>
        <el-radio-button :label="1">商品分组</el-radio-button>
        <el-radio-button :label="2">商品分类</el-radio-button>
        <el-radio-button :label="3">全部商品</el-radio-button>
      </el-radio-group>
    </el-form-item>
    <el-form-item v-if="moduleConfig.ChioseType == 0" label="选择商品">
      <el-button type="primary" icon="el-icon-goods" @click="chioseSpu" />
    </el-form-item>
    <el-form-item v-else-if="moduleConfig.ChioseType == 2" label="商品类目" prop="CategoryId">
      <categorySelect
        v-model="moduleConfig.CategoryId"
        :is-chiose-end="true"
        placeholder="商品类目"
        :lvl.sync="moduleConfig.Level"
      />
    </el-form-item>
    <el-form-item v-if="moduleConfig.ChioseType !== 0" label="排序方式" prop="OrderType">
      <enumItem
        v-model="moduleConfig.OrderType"
        :dic-key="EnumDic.pageSkuOrderType"
        placeholder="排序方式"
        :multiple="false"
        sys-head="shop"
      />
    </el-form-item>
    <el-form-item label="展示数量" prop="TopNum">
      <el-input-number
        v-model="moduleConfig.TopNum"
        :min="2"
        :max="20"
        placeholder="展示数量"
      />
    </el-form-item>
    <moduleGoodsEdit :page-id="pageId" :module-id="moduleId" :tag="tag" :visible="spuVisible" @close="spuVisible=false" />
  </el-form>
</template>

<script>
import moduleGoodsEdit from './moduleGoodsEdit.vue'
import categorySelect from '@/shop/components/category/categorySelect'
import { EnumDic } from '@/shop/config/shopConfig'
import moment from 'moment'
export default {
  components: {
    categorySelect,
    moduleGoodsEdit
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    useRange: {
      type: Number,
      default: null
    },
    pageId: {
      type: String,
      default: null
    },
    moduleId: {
      type: String,
      default: null
    },
    config: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      loading: false,
      spuVisible: false,
      tag: null,
      moduleConfig: {
        IsHideTitle: false,
        ChioseType: 3
      },
      rules: {
        TabName: [{
          required: true,
          message: '导航名称不能为空！',
          trigger: 'blur'
        }]
      }
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
    moment,
    chioseSpu() {
      this.spuVisible = true
    },
    chioseChange() {
      this.moduleConfig.CategoryId = null
      this.moduleConfig.Level = null
      this.moduleConfig.OrderType = null
      this.moduleConfig.GroupName = null
      this.moduleConfig.GroupId = null
    },
    async reset() {
      this.loading = false
      this.tag = this.config.id
      let conf = {
        TabName: null,
        IsHideTitle: false,
        ChioseType: 3,
        OrderType: null,
        TopNum: null,
        Level: null,
        CategoryId: null,
        GroupName: null,
        GroupId: null,
        IsMore: true,
        IsSingle: false
      }
      if (this.config.TabName != null) {
        conf.TabName = this.config.TabName
        conf.IsHideTitle = this.config.IsHideTitle
        if (this.config.SkuConfig) {
          conf = Object.assign(conf, this.config.SkuConfig)
        }
      }
      this.moduleConfig = conf
    },
    async save() {
      if (await this.$refs['editTab'].validate()) {
        return {
          TabKey: this.tag,
          TabName: this.moduleConfig.TabName,
          SkuConfig: {
            ChioseType: this.moduleConfig.ChioseType,
            GroupId: this.moduleConfig.GroupId,
            GroupName: this.moduleConfig.GroupName,
            CategoryId: this.moduleConfig.CategoryId,
            Level: this.moduleConfig.Level,
            TopNum: this.moduleConfig.TopNum,
            OrderType: this.moduleConfig.OrderType,
            IsMore: this.moduleConfig.IsMore
          }
        }
      }
      return null
    }
  }
}
</script>
    <style scoped>
  .title {
    color: #333;
    font-size: 12px;
    font-weight: bolder;
    line-height: 30px;
    height: 30px;
  }
  .el-input__suffix {
    right: 0px !important;
  }
  .advertImg {
    padding: 10px;
  }
  .advertImg .el-row {
    line-height: 30px;
  }
  .advertImg .el-button {
    padding: 0;
    margin: 0;
    font-size: 18px;
  }
  .addBtn {
    width: 90%;
    margin-left: 5%;
    height: 40px;
  }
  h3 {
    margin-bottom: 20px;
  }
  </style>
