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
    <el-card style="margin-top: 20px;">
      <div slot="header">
        <span>内容配置</span>
      </div>
      <el-form-item label="图片" prop="ImgSrc">
        <imageUpBtn
          v-model="moduleConfig.FileId"
          file-key="PageModuleImg"
          :link-biz-pk="id"
          :file-uri.sync="moduleConfig.ImgSrc"
          @change="save"
        />
      </el-form-item>
      <el-form-item label="当前创建热区">
        <span style="color: #43AF2B;">{{ hostNum }}</span>
      </el-form-item>
      <el-button type="primary" style="width: 90%; margin-left: 5%;" @click="editHost">编辑热区</el-button>
    </el-card>
    <imgWall :visible="visible" :zones="zones" :page-id="pageId" :module-id="id" :img-src="imgSrc" @save="saveWall" @close="visible=false" />
  </el-form>
</template>

<script>
import * as moduleApi from '@/shop/api/pageModule'
import { EnumDic } from '@/shop/config/shopConfig'
import imageUpBtn from '@/components/fileUp/imageUpBtn'
import imgWall from './imgWall.vue'
export default {
  components: {
    imageUpBtn,
    imgWall
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
      zones: [],
      imgSrc: null,
      loading: false,
      id: null,
      hostNum: 0,
      moduleConfig: {
        ModuleTitle: null,
        ImgSrc: null
      },
      rules: {
        ModuleTitle: [{
          required: true,
          message: '标题不能为空！',
          trigger: 'blur'
        }],
        ImgSrc: [{
          required: true,
          message: '图片链接不能为空！',
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
    saveWall(items) {
      this.zones = items
      this.visible = false
      this.hostNum = this.zones.length
      this.save()
    },
    editHost() {
      this.imgSrc = null
      if (this.moduleConfig.ImgSrc && this.moduleConfig.ImgSrc.length !== 0) {
        this.imgSrc = this.moduleConfig.ImgSrc[0]
      }
      this.visible = true
    },
    async reset() {
      this.loading = false
      this.id = this.config.mid
      const res = await moduleApi.Get(this.id)
      this.moduleConfig.ModuleTitle = res.ModuleTitle
      if (res.ModuleConfig && res.ModuleConfig.ImgWall) {
        this.zones = res.ModuleConfig.ImgWall.HostConfig
        this.hostNum = this.zones.length
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
          ModuleName: 'imgWall',
          ImgWall: {
            ImgSrc: this.moduleConfig.ImgSrc && this.moduleConfig.ImgSrc.length > 0 ? this.moduleConfig.ImgSrc[0] : null,
            HostConfig: this.zones
          }
        },
        FileId: this.moduleConfig.FileId
      }
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
.el-divider {
  width: 3px;
  background-color: #1890ff;
}
</style>
  <style >

  .el-input__suffix {
      right: 0px !important;
    }
  </style>
