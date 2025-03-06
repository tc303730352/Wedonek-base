<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="advertPlaceEdit"
      :model="advertPlace"
      label-width="120px"
      :rules="rules"
    >
      <el-form-item label="广告位标题" prop="PlaceTitle">
        <el-input
          v-model="advertPlace.PlaceTitle"
          maxlength="50"
          placeholder="广告位标题"
        />
      </el-form-item>
      <el-form-item label="分类" prop="PlaceType">
        <dictItem
          v-model="advertPlace.PlaceType"
          :dic-id="DictItemDic.placeType"
          placeholder="分类"
          @change="placeTypeChange"
        />
      </el-form-item>
      <el-form-item label="使用范围" prop="UseRange">
        <enumItem
          v-model="advertPlace.UseRange"
          :disabled="id != null"
          :dic-key="EnumDic.pageUseRange"
          placeholder="使用范围"
          :multiple="false"
          sys-head="shop"
        />
      </el-form-item>
      <el-form-item label="示例图片" prop="ExampleImg">
        <imageUpBtn
          v-model="advertPlace.FileId"
          file-key="AdvertPlace"
          :multiple="false"
          :file-uri.sync="advertPlace.ExampleImg"
          :link-biz-pk="id"
        />
      </el-form-item>
      <el-form-item label="广告图片数" prop="ImgNum">
        <el-input-number v-model="advertPlace.ImgNum" :disabled="advertPlace.PlaceType !=='002'" :min="1" :max="10" />
      </el-form-item>
      <el-card>
        <span slot="header">图片宽高配置</span>
        <el-form-item label="宽高比例" style="margin-top: 10px">
          <el-input
            v-model="imgSet.WidthRatio"
            type="number"
            style="width: 45%"
            placeholder="宽"
          />
          <span
            style="padding-left: 10px; padding-right: 10px; font-weight: 700"
          >:</span>
          <el-input
            v-model="imgSet.HeightRatio"
            type="number"
            style="width: 45%"
            placeholder="高"
          />
        </el-form-item>
        <el-form-item label="最小宽高">
          <el-input
            v-model="imgSet.MinWidth"
            style="width: 45%"
            type="number"
            placeholder="宽"
          />
          <span
            style="padding-left: 10px; padding-right: 10px; font-weight: 700"
          >-</span>
          <el-input
            v-model="imgSet.MinHeight"
            style="width: 45%"
            type="number"
            placeholder="高"
          />
        </el-form-item>
        <el-form-item label="最大宽高">
          <el-input
            v-model="imgSet.MaxWidth"
            style="width: 45%"
            type="number"
            placeholder="宽"
          />
          <span
            style="padding-left: 10px; padding-right: 10px; font-weight: 700"
          >-</span>
          <el-input
            v-model="imgSet.MaxHeight"
            style="width: 45%"
            type="number"
            placeholder="高"
          />
        </el-form-item>
      </el-card>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import * as placeApi from '@/shop/api/advertPlace'
import imageUpBtn from '@/components/fileUp/imageUpBtn'
import { EnumDic, DictItemDic } from '@/shop/config/shopConfig'
export default {
  components: {
    imageUpBtn
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      DictItemDic,
      EnumDic,
      title: '编辑广告位',
      advertPlace: {},
      loading: false,
      imgSet: {
        WidthRatio: null,
        HeightRatio: null,
        MinWidth: null,
        MinHeight: null,
        MaxWidtht: null,
        MaxHeight: null
      },
      rules: {
        PlaceKey: [
          {
            required: true,
            message: '广告位Key不能为空！',
            trigger: 'blur'
          }
        ],
        PlaceTitle: [
          {
            required: true,
            message: '广告位说明不能为空！',
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
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    async reset() {
      if (this.id == null) {
        this.title = '新增广告位'
        this.advertPlace = {
          PlaceTitle: null,
          ImgNum: 1,
          UseRange: 1,
          PlaceType: '001',
          ExampleImg: null,
          FileId: null
        }
        this.imgSet = {
          WidthRatio: null,
          HeightRatio: null,
          MinWidth: null,
          MinHeight: null,
          MaxWidtht: null,
          MaxHeight: null
        }
      } else {
        const res = await placeApi.Get(this.id)
        this.advertPlace = {
          PlaceTitle: res.PlaceTitle,
          ImgNum: res.ImgNum,
          UseRange: res.UseRange,
          PlaceType: res.PlaceType,
          ExampleImg: null,
          FileId: null
        }
        this.title = '编辑广告位'
        let imgSet = res.ImgSet
        if (imgSet != null && imgSet.Ratio != null) {
          imgSet.WidthRatio = imgSet.Ratio[0]
          imgSet.HeightRatio = imgSet.Ratio[1]
        } else if (imgSet == null) {
          imgSet = {
            WidthRatio: null,
            HeightRatio: null,
            MinWidth: null,
            MinHeight: null,
            MaxWidtht: null,
            MaxHeight: null
          }
        }
        this.imgSet = imgSet
      }
      this.loading = false
    },
    placeTypeChange() {
      if (this.advertPlace.PlaceType !== '002') {
        this.advertPlace.ImgNum = 1
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['advertPlaceEdit'].validate((valid) => {
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
    async add() {
      this.loading = true
      const data = this.format()
      await placeApi.Add(data)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    fromatImgSet() {
      const imgSet = Object.assign({}, this.imgSet)
      if (this.imgSet.WidthRatio && this.imgSet.HeightRatio) {
        imgSet.Ratio = [this.imgSet.WidthRatio, this.imgSet.HeightRatio]
      }
      return imgSet
    },
    format() {
      const data = {
        PlaceKey: this.advertPlace.PlaceKey,
        PlaceTitle: this.advertPlace.PlaceTitle,
        ImgNum: this.advertPlace.ImgNum
      }
      data.ImgSet = this.fromatImgSet()
      if (this.advertPlace.FileId && this.advertPlace.FileId.length > 0) {
        data.FileId = this.advertPlace.FileId[0]
        data.ExampleImg = this.advertPlace.ExampleImg[0]
      }
      return data
    },
    async set() {
      this.loading = true
      const data = this.format()
      const isSet = await placeApi.Set(this.id, data)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
