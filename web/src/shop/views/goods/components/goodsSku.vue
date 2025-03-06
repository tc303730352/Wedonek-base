
<template>
  <el-form ref="skuEdit" :model="sku" label-width="150px" :rules="rules">
    <el-card>
      <div slot="header">
        <span>基本信息</span>
        <el-row style="float: right; margin-right: 10px">
          <el-button
            :loading="loading"
            size="mini"
            type="primary"
            @click="save"
          >保存</el-button>
          <el-button :loading="loading" @click="reset">重置</el-button>
        </el-row>
      </div>
      <el-form-item label="SKU编号" prop="GoodsSku">
        <el-input
          v-model="sku.GoodsSku"
          :maxlength="64"
          placeholder="SPU编号"
        />
      </el-form-item>
      <el-form-item label="商品名" prop="SkuName">
        <el-input v-model="sku.SkuName" :maxlength="50" placeholder="商品名" />
      </el-form-item>
      <el-form-item label="价格" prop="Price">
        <el-input-number v-model="sku.Price" placeholder="价格" :min="0" />
      </el-form-item>
      <el-form-item label="重量" prop="Weight">
        <el-input-number v-model="sku.Weight" placeholder="重量" :min="0" />
      </el-form-item>
      <el-form-item label="体积" prop="Volume">
        <el-input-number v-model="sku.Volume" placeholder="体积" :min="0" />
      </el-form-item>
      <el-form-item label="简要说明" prop="Show">
        <el-input v-model="sku.Show" placeholder="简要说明" :maxlength="100" />
      </el-form-item>
      <el-form-item label="封面图" prop="SkuImg">
        <imageUpBtn
          v-if="isInit"
          v-model="sku.CoverFileId"
          file-key="goodsSkuCover"
          :multiple="false"
          :file-uri.sync="sku.SkuImg"
          :link-biz-pk="sku.SkuId"
        />
      </el-form-item>
      <el-form-item label="商品图片" prop="ImgList">
        <imageUpBtn
          v-if="isInit"
          v-model="sku.FileId"
          file-key="goodsSkuImg"
          :up-filelimit="10"
          :multiple="true"
          :is-order-by="true"
          :file-uri.sync="sku.ImgList"
          :link-biz-pk="sku.SkuId"
        />
      </el-form-item>
      <el-form-item label="商品介绍" prop="ImgShow">
        <imageUpBtn
          v-if="isInit"
          v-model="sku.ImgShowFileId"
          file-key="goodsImgShow"
          :up-filelimit="20"
          :multiple="true"
          :is-order-by="true"
          :file-uri.sync="sku.ImgShow"
          :link-biz-pk="sku.SkuId"
        />
      </el-form-item>
    </el-card>
    <goodsAttr ref="skuAttr" :attrs="attrs" />
  </el-form>
</template>

<script>
import * as skuApi from '@/shop/api/goodsSku'
import { GetAttrTrees } from '@/shop/api/attrTemplate'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
import goodsAttr from '../../goodsAttr/goodsAttr.vue'
export default {
  components: {
    imageUpBtn,
    goodsAttr
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    spec: {
      type: Object,
      default: () => {
        return {
          SpecKey: null,
          Name: null,
          SpecId: null
        }
      }
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
      skuSpec: {},
      sku: {},
      isInit: false,
      specKey: null,
      rules: {
        GoodsSku: [
          {
            required: true,
            message: 'SKU编号不能为空！',
            trigger: 'blur'
          },
          {
            message: 'SKU编号长度应在2-64位之间！',
            trigger: 'blur',
            min: 2
          }
        ],
        Price: [
          {
            required: true,
            message: '售价不能为空！',
            trigger: 'blur'
          }
        ],
        SkuName: [
          {
            required: true,
            message: '商品名不能为空',
            trigger: 'blur'
          },
          {
            message: '商品名长度应在2-50位之间！',
            trigger: 'blur',
            min: 2
          }
        ]
      },
      attrs: [],
      columns: [
        {
          key: 'GroupName',
          title: '规格组名',
          align: 'center',
          resizable: false,
          isMerge: true,
          slotName: 'GroupName',
          width: 255,
          renderHeader: this.createHeader
        },
        {
          key: 'SpecName',
          title: '规格名',
          align: 'center',
          resizable: false,
          width: 255
        },
        {
          key: 'Icon',
          title: '图标',
          align: 'center',
          resizable: false,
          slotName: 'Icon',
          width: 200
        },
        {
          key: 'Sort',
          title: '排序',
          align: 'center',
          resizable: false,
          slotName: 'Sort',
          width: 100
        },
        {
          key: 'Action',
          title: '操作规格',
          resizable: false,
          align: 'left',
          slotName: 'action'
        }
      ]
    }
  },
  computed: {},
  watch: {
    isLoad: {
      handler(val) {
        if (val && this.specKey != this.spec.SpecKey) {
          this.specKey = this.spec.SpecKey
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    save() {
      const that = this
      this.$refs['skuEdit'].validate((valid) => {
        if (valid) {
          if (that.sku.SkuId == null) {
            that.addSku()
          } else {
            that.setSku()
          }
        } else {
          return false
        }
      })
    },
    format(data) {
      delete data.ImgShowFileId
      if (data.FileId == null) {
        [
          data.FileId = []
        ]
      }
      if (this.sku.ImgShowFileId != null && this.sku.ImgShowFileId.length > 0) {
        data.FileId = data.FileId.concat(this.sku.ImgShowFileId)
      }
      delete data.CoverFileId
      delete data.SkuImg
      if (this.sku.CoverFileId != null && this.sku.CoverFileId.length > 0) {
        data.FileId = data.FileId.concat(this.sku.CoverFileId)
        data.SkuImg = this.sku.SkuImg[0]
      }
      delete data.PcFileId
      if (this.sku.PcFileId != null && this.sku.PcFileId.length > 0) {
        data.FileId = data.FileId.concat(this.sku.PcFileId)
      }
      data.Attrs = this.$refs.skuAttr.getValue()
    },
    async setSku() {
      this.loading = true
      const data = Object.assign({}, this.sku)
      this.format(data)
      try {
        await skuApi.Set(this.sku.SkuId, data)
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        this.$emit('save', {
          isEdit: true,
          skuId: this.sku.SkuId,
          specKey: this.spec.SpecKey
        })
      } catch {}
      this.loading = false
    },
    async addSku() {
      this.loading = true
      const add = Object.assign({}, this.sku)
      add.GoodsId = this.goodsId
      add.SpecId = this.spec.SpecId
      this.format(add)
      delete add.SkuId
      try {
        const skuId = await skuApi.Add(add)
        this.sku.SkuId = skuId
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        this.$emit('save', {
          isEdit: false,
          skuId: skuId,
          specKey: this.spec.SpecKey
        })
      } catch {}
      this.loading = false
    },
    async reset() {
      this.loading = true
      const sku = await skuApi.Get(this.goodsId, this.spec.SpecKey)
      if (sku != null) {
        sku.SpecName = this.spec.Name.join('\n')
        if (sku.SkuImg == null) {
          sku.SkuImg = []
        } else {
          sku.SkuImg = [sku.SkuImg]
        }
        this.sku = sku
        this.attrs = sku.Attrs
      } else {
        this.sku.SkuId = null
        this.sku.SpecName = this.spec.Name.join('\n')
        this.loadAttrs()
      }
      this.isInit = true
      this.loading = false
    },
    async loadAttrs() {
      const res = await GetAttrTrees(this.categoryId)
      if (res == null || res.length == 0) {
        return
      }
      this.attrs = res
    }
  }
}
</script>
    <style type="less" scoped>
</style>
