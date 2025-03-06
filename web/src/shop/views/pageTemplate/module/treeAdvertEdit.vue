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
    </el-card>
    <el-card>
      <div slot="header">
        <span>内容配置</span>
      </div>
      <el-form-item label="左广告位" prop="LeftPlaceId">
        <el-input
          v-model="moduleConfig.LeftPlaceTitle"
          :readonly="true"
          placeholder="选择广告位"
        >
          <el-button slot="suffix" icon="el-icon-picture" @click="chiosePlace('004')" />
        </el-input>
      </el-form-item>
      <el-form-item label="右上广告位" prop="TopPlaceId">
        <el-input
          v-model="moduleConfig.TopPlaceTitle"
          :readonly="true"
          placeholder="选择广告位"
        >
          <el-button slot="suffix" icon="el-icon-picture" @click="chiosePlace('005')" />
        </el-input>
      </el-form-item>
      <el-form-item label="右下广告位" prop="BottomPlaceId">
        <el-input
          v-model="moduleConfig.BottomPlaceTitle"
          :readonly="true"
          placeholder="选择广告位"
        >
          <el-button slot="suffix" icon="el-icon-picture" @click="chiosePlace('006')" />
        </el-input>
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
    <chioseAdvertPlace :visible="visible" :is-multiple="false" :place-id="placeId" :use-range="useRange" :place-type="placeType" @close="visible=false" @save="savePlace" />
  </el-form>
</template>

<script>
import * as moduleApi from '@/shop/api/pageModule'
import { EnumDic } from '@/shop/config/shopConfig'
import chioseAdvertPlace from '@/shop/components/advertPlace/chioseAdvertPlace.vue'
export default {
  components: {
    chioseAdvertPlace
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
      visible: null,
      loading: false,
      id: null,
      placeType: '004',
      placeId: null,
      moduleConfig: {
        ModuleTitle: null,
        LeftPlaceTitle: null,
        LeftPlaceId: null,
        TopPlaceId: null,
        BottomPlaceId: null,
        TopPlaceTitle: null,
        BottomPlaceTitle: null
      },
      rules: {
        ModuleTitle: [{
          required: true,
          message: '标题不能为空！',
          trigger: 'blur'
        }],
        LeftPlaceId: [{
          required: true,
          message: '左广告位不能为空！',
          trigger: 'blur'
        }],
        TopPlaceId: [{
          required: true,
          message: '右上广告位不能为空！',
          trigger: 'blur'
        }],
        BottomPlaceId: [{
          required: true,
          message: '右下广告位不能为空！',
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
    chiosePlace(placeType) {
      this.placeType = placeType
      this.placeId = null
      if (placeType === '004' && this.moduleConfig.LeftPlaceId !== null) {
        this.placeId = [this.moduleConfig.LeftPlaceId]
      } else if (placeType === '005' && this.moduleConfig.TopPlaceId !== null) {
        this.placeId = [this.moduleConfig.TopPlaceId]
      } else if (placeType === '006' && this.moduleConfig.BottomPlaceId !== null) {
        this.placeId = [this.moduleConfig.BottomPlaceId]
      }
      this.visible = true
    },
    savePlace(e) {
      this.visible = false
      let title = null
      let id = null
      if (e.keys && e.keys.length > 0) {
        title = e.place[0].PlaceTitle
        id = e.keys[0]
      }
      if (this.placeType === '004') {
        this.moduleConfig.LeftPlaceId = id
        this.moduleConfig.LeftPlaceTitle = title
      } else if (this.placeType === '005') {
        this.moduleConfig.TopPlaceId = id
        this.moduleConfig.TopPlaceTitle = title
      } else if (this.placeType === '006') {
        this.moduleConfig.BottomPlaceId = id
        this.moduleConfig.BottomPlaceTitle = title
      }
    },
    async reset() {
      this.loading = false
      this.id = this.config.mid
      const res = await moduleApi.Get(this.id)
      this.moduleConfig.ModuleTitle = res.ModuleTitle
      if (res.ModuleConfig && res.ModuleConfig.TreeAdvert) {
        const place = res.ModuleConfig.TreeAdvert
        this.moduleConfig.LeftPlaceTitle = place.Left.PlaceTitle
        this.moduleConfig.LeftPlaceId = place.Left.PlaceId
        this.moduleConfig.TopPlaceId = place.Top.PlaceId
        this.moduleConfig.BottomPlaceId = place.Bottom.PlaceId
        this.moduleConfig.TopPlaceTitle = place.Top.PlaceTitle
        this.moduleConfig.BottomPlaceTitle = place.Bottom.PlaceTitle
      }
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
        ModuleConfig: {
          ModuleName: 'treeAdvert',
          TreeAdvert: {
            Top: {
              PlaceId: this.moduleConfig.TopPlaceId,
              PlaceTitle: this.moduleConfig.TopPlaceTitle
            },
            Left: {
              PlaceId: this.moduleConfig.LeftPlaceId,
              PlaceTitle: this.moduleConfig.LeftPlaceTitle
            },
            Bottom: {
              PlaceId: this.moduleConfig.BottomPlaceId,
              PlaceTitle: this.moduleConfig.BottomPlaceTitle
            }
          }
        }
      }
      await moduleApi.Set(this.id, set)
      set.Id = this.id
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('save', this.set)
    }
  }
}
</script>

<style >

.el-input__suffix {
    right: 0px !important;
  }
</style>
