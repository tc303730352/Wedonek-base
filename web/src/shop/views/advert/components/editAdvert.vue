<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="advertEdit"
      :model="advert"
      label-width="120px"
      :rules="rules"
    >
      <el-form-item label="广告标题" prop="Title">
        <el-input
          v-model="advert.Title"
          maxlength="100"
          placeholder="广告标题"
        />
      </el-form-item>
      <el-form-item label="投放时段" prop="PutInDate">
        <el-date-picker
          v-model="advert.PutInDate"
          type="datetimerange"
          format="yyyy-MM-dd HH:mm"
          :picker-options="pickerOptions"
          range-separator="至"
          start-placeholder="开始时间"
          end-placeholder="结束时间"
        />
      </el-form-item>
      <el-form-item label="投放类型" prop="RelationType">
        <enumItem
          v-model="advert.RelationType"
          :dic-key="EnumDic.relationType"
          placeholder="投放类型"
          :multiple="false"
          sys-head="shop"
        />
      </el-form-item>
      <el-form-item
        v-if="advert.RelationType == 0"
        label="选择商品"
        prop="RelationId"
      >
        <el-input
          :value="relationTitle"
          maxlength="100"
          placeholder="投放的商品"
          :readonly="true"
        >
          <el-button
            slot="append"
            icon="el-icon-circle-plus-outline"
            @click="chioseSku"
          />
        </el-input>
      </el-form-item>
      <el-form-item label="排序位" prop="Sort">
        <el-input-number v-model="advert.Sort" :min="1" placeholder="排序位" />
      </el-form-item>
      <el-form-item label="广告图片">
        <imageUpBtn
          file-key="AdvertImg"
          :link-biz-pk="id"
          :img-set="imgSet"
          @change="fileChange"
        />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
    <chioseSku
      :visible="skuVisible"
      :sku-id="rid"
      @save="saveSku"
      @close="skuVisible = false"
    />
  </el-dialog>
</template>

<script>
import moment from 'moment'
import { GetConfig } from '@/shop/api/advertPlace'
import * as advertApi from '@/shop/api/advert'
import { EnumDic } from '@/shop/config/shopConfig'
import imageUpBtn from '@/components/fileUp/imageUpBtn'
import chioseSku from '@/shop/components/goods/chioseSku.vue'
export default {
  components: {
    imageUpBtn,
    chioseSku
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    placeId: {
      type: String,
      default: null
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      title: '编辑广告位',
      loading: false,
      skuVisible: null,
      relationTitle: null,
      rid: null,
      skuName: null,
      advert: {},
      imgSet: null,
      today: moment().startOf('date'),
      pickerOptions: {
        disabledDate: function(time) {
          return time < moment().startOf('date')
        }
      },
      rules: {
        Title: [
          {
            required: true,
            message: '广告标题不能为空！',
            trigger: 'blur'
          }
        ],
        RelationType: [
          {
            required: true,
            message: '投放类型不能为空!',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.loadPlace()
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    chioseSku() {
      this.skuVisible = true
    },
    saveSku(e) {
      this.skuVisible = false
      this.advert.RelationId = e.keys[0]
      const sku = e.skus[0]
      this.relationTitle = sku.SkuName
    },
    async loadPlace() {
      const place = await GetConfig(this.placeId)
      if (place.ImgSet != null) {
        this.imgSet = place.ImgSet
        this.imgSet.Show = place.ImgShow
      }
    },
    async reset() {
      this.skuName = null
      this.rid = null
      if (this.id == null) {
        this.title = '新增广告'
        this.advert = {
          RelationType: 0,
          RelationId: null,
          Title: null,
          PutInDate: [],
          Sort: 1
        }
      } else {
        this.title = '编辑广告'
        const res = await advertApi.Get(this.id)
        this.advert = {
          RelationType: res.RelationType,
          RelationId: res.RelationId,
          Title: res.Title,
          ImgSrc: res.ImgSrc,
          Sort: res.Sort,
          PutInDate: []
        }
        this.relationTitle = res.RelationTitle
        if (res.RelationType == 0 && res.RelationId != 0) {
          this.rid = [res.RelationId]
        }
        if (res.PutInBegin != null && res.PutInEnd != null) {
          this.advert.PutInDate = [new Date(res.PutInBegin), new Date(res.PutInEnd)]
        } else {
          this.advert.PutInDate = []
        }
      }
      this.loading = false
    },
    fileChange(fileId, files) {
      if (files.length == 0) {
        this.advert.FileId = null
        this.advert.ImgSrc = null
      } else {
        this.advert.ImgSrc = files[0].FileUri
        this.advert.FileId = fileId[0]
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['advertEdit'].validate((valid) => {
        if (valid) {
          if (that.id == null) {
            that.add()
          } else {
            that.set()
          }
        } else {
          return false
        }
      })
    },
    format() {
      const data = {
        PlaceId: this.placeId,
        Title: this.advert.Title,
        ImgSrc: this.advert.ImgSrc,
        FileId: this.advert.FileId,
        RelationType: this.advert.RelationType,
        RelationId: this.advert.RelationId,
        Sort: this.advert.Sort
      }
      if (this.advert.PutInDate != null && this.advert.PutInDate.length > 0) {
        data.PutInBegin = this.advert.PutInDate[0]
        data.PutInEnd = this.advert.PutInDate[1]
      }
      return data
    },
    async add() {
      this.loading = true
      const data = this.format()
      await advertApi.Add(data)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const data = this.format()
      const isSet = await advertApi.Set(this.id, data)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
