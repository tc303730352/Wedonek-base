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
    <el-card style="margin-top: 10px;">
      <div slot="header">
        <span>内容配置</span>
        <el-button style="float: right;" type="success" @click="addTab">新增导航Tab</el-button>
      </div>
      <el-tabs v-model="tabValue" type="border-card" :closable="true" @tab-remove="removeTab">
        <el-tab-pane
          v-for="item in tabs"
          :key="item.id"
          :label="item.label"
          :name="item.id"
        >
          <tabEdit :ref="item.id" :page-id="pageId" :use-range="useRange" :module-id="id" :is-load="item.id == tabValue" :config="item" />
        </el-tab-pane>
      </el-tabs>
    </el-card>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :loading="loading"
        type="success"
        @click="save"
      >保存</el-button>
      <el-button @click="reset">取消</el-button>
    </el-row>
  </el-form>
</template>

<script>
import * as moduleApi from '@/shop/api/pageModule'
import tabEdit from './tabEdit.vue'
import { EnumDic } from '@/shop/config/shopConfig'
import moment from 'moment'
export default {
  components: {
    tabEdit
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
      tabValue: null,
      loading: false,
      id: null,
      placeType: '001',
      tabs: [],
      tabId: 1,
      visible: false,
      moduleConfig: {
        ModuleTitle: null
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
    removeTab(tab) {
      const index = this.tabs.findIndex(a => a.id === tab)
      if (index !== -1) {
        this.tabs.splice(index, 1)
      }
    },
    addTab() {
      const id = 'tab_' + this.tabId
      this.tabs.push({
        label: 'tab' + this.tabId,
        id: id
      })
      this.tabValue = id
      this.tabId = this.tabId + 1
    },
    async reset() {
      this.loading = false
      this.id = this.config.mid.toString()
      const res = await moduleApi.Get(this.id)
      this.moduleConfig.ModuleTitle = res.ModuleTitle
      if (res.ModuleConfig && res.ModuleConfig.Tabs) {
        let max = 0
        this.tabs = res.ModuleConfig.Tabs.map(c => {
          c.id = c.TabKey
          c.label = c.TabName
          const id = parseInt(c.TabKey.substring(4))
          if (id > max) {
            max = id
          }
          return c
        })
        this.tabId = max + 1
        this.tabValue = this.tabs[0].id
      } else {
        this.tabs = []
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
      const tabs = []
      const fileId = []
      for (let i = 0; i < this.tabs.length; i++) {
        const c = this.tabs[i]
        const item = await this.$refs[c.id][0].save()
        if (item == null) {
          return
        }
        tabs.push(item)
      }
      const set = {
        ModuleTitle: this.moduleConfig.ModuleTitle,
        FileId: fileId,
        ModuleConfig: {
          ModuleName: 'skuTabs',
          Tabs: tabs
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
