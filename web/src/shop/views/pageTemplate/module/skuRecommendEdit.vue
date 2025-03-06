<template>
  <el-form
    ref="editModule"
    :model="moduleConfig"
    label-width="120px"
    :rules="rules"
  >
    <el-card>
      <span slot="header">基本信息</span>
      <el-form-item label="标题" prop="ModuleTitle">
        <el-input
          v-model="moduleConfig.ModuleTitle"
          maxlength="50"
          placeholder="标题"
        />
      </el-form-item>
      <el-form-item label="是否隐藏标题" prop="IsHideTitle">
        <el-switch
          v-model="moduleConfig.IsHideTitle"
          active-text="隐藏标题"
          inactive-text="显示标题"
          maxlength="50"
        />
      </el-form-item>
      <el-form-item label="标题图片" prop="TitleImg">
        <imageUpBtn
          v-model="moduleConfig.TitleFileId"
          file-key="PageModuleImg"
          :multiple="false"
          tag="title"
          :file-uri.sync="moduleConfig.TitleImg"
          :link-biz-pk="id"
        />
      </el-form-item>
    </el-card>
    <el-card style="margin-top: 10px;">
      <span slot="header">内容配置</span>
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
    </el-card>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :loading="loading"
        type="success"
        @click="save"
      >保存</el-button>
      <el-button @click="reset">取消</el-button>
    </el-row>
    <moduleGoodsEdit :page-id="pageId" :module-id="id" :visible="spuVisible" @close="spuVisible=false" />
  </el-form>
</template>

<script>
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
import * as moduleApi from '@/shop/api/pageModule'
import moduleGoodsEdit from './moduleGoodsEdit.vue'
import categorySelect from '@/shop/components/category/categorySelect'
import { EnumDic } from '@/shop/config/shopConfig'
import moment from 'moment'
export default {
  components: {
    imageUpBtn,
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
    config: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      loading: false,
      id: null,
      placeType: '001',
      spuVisible: false,
      moduleConfig: {
        ModuleTitle: null,
        IsHideTitle: false,
        TitleImg: null,
        TitleFileId: null,
        ChioseType: 3
      },
      rules: {
        ModuleTitle: [{
          required: true,
          message: '标题不能为空！',
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
      this.id = this.config.mid
      const res = await moduleApi.Get(this.id)
      let conf = {
        ModuleTitle: res.ModuleTitle,
        IsHideTitle: false,
        TitleImg: null,
        TitleFileId: null,
        ChioseType: 3,
        OrderType: null,
        TopNum: null,
        Level: null,
        CategoryId: null,
        GroupName: null,
        GroupId: null
      }
      if (res.ModuleConfig && res.ModuleConfig.Recommend) {
        const config = res.ModuleConfig.Recommend
        conf.IsHideTitle = config.IsHideTitle
        if (config.SkuConfig) {
          conf = Object.assign(conf, config.SkuConfig)
        }
      }
      this.moduleConfig = conf
    },
    save() {
      const that = this
      this.$refs['editModule'].validate((valid) => {
        if (valid) {
          that.saveConfig()
        } else {
          return false
        }
      })
    },
    async saveConfig() {
      const set = {
        ModuleTitle: this.moduleConfig.ModuleTitle,
        FileId: this.moduleConfig.TitleFileId,
        ModuleConfig: {
          ModuleName: 'skuRecommend',
          Recommend: {
            IsHideTitle: this.moduleConfig.IsHideTitle,
            TitleImg: this.moduleConfig.TitleImg && this.moduleConfig.TitleImg.length > 0 ? this.moduleConfig.TitleImg[0] : null,
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
      }
      console.log(set)
      await moduleApi.Set(this.id, set)
      set.Id = this.id
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('save', set)
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
<style >

.el-input__suffix {
    right: 0px !important;
  }
</style>
