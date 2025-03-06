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
      <span slot="header">内容配置</span>
      <el-form-item label="广告位" prop="PlaceId">
        <el-input
          v-model="moduleConfig.PlaceTitle"
          :readonly="true"
          placeholder="选择广告位"
        >
          <el-button slot="suffix" icon="el-icon-picture" @click="chiosePlace" />
        </el-input>
      </el-form-item>
    </el-card>
    <chioseAdvertPlace :visible="visible" :is-multiple="false" :place-id="placeId" :use-range="useRange" place-type="003" @close="visible=false" @save="savePlace" />
  </el-form>
</template>

<script>
import * as moduleApi from '@/shop/api/pageModule'
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
      loading: false,
      id: null,
      visible: false,
      placeId: null,
      moduleConfig: {
        ModuleTitle: null,
        PlaceId: null,
        PlaceTitle: null
      },
      rules: {
        ModuleTitle: [{
          required: true,
          message: '标题不能为空！',
          trigger: 'blur'
        }],
        PlaceId: [{
          required: true,
          message: '请选择广告位!',
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
    chiosePlace() {
      this.placeId = null
      if (this.moduleConfig.PlaceId != null) {
        this.placeId = [this.moduleConfig.PlaceId]
      }
      this.visible = true
    },
    savePlace(e) {
      this.visible = false
      if (e.keys && e.keys.length > 0) {
        this.moduleConfig.PlaceTitle = e.place[0].PlaceTitle
        this.moduleConfig.PlaceId = e.keys[0]
      } else {
        this.moduleConfig.PlaceTitle = null
        this.moduleConfig.PlaceId = null
      }
      this.save()
    },
    async reset() {
      this.loading = false
      this.id = this.config.mid
      const res = await moduleApi.Get(this.id)
      const conf = {
        ModuleTitle: res.ModuleTitle,
        PlaceId: null,
        PlaceTitle: null
      }
      if (res.ModuleConfig && res.ModuleConfig.ImgAdvert) {
        const config = res.ModuleConfig.ImgAdvert
        conf.PlaceId = config.PlaceId
        conf.PlaceTitle = config.PlaceTitle
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
          ModuleName: 'imgAdvert',
          ImgAdvert: {
            PlaceId: this.moduleConfig.PlaceId,
            PlaceTitle: this.moduleConfig.PlaceTitle
          }
        }
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
