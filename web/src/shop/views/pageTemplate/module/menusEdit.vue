<template>
  <el-form
    ref="editModule"
    :model="moduleConfig"
    label-width="120px"
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
    <el-card style="margin-top: 10px">
      <div slot="header" style="height: 40px; line-height: 40px">
        <span>菜单列表</span>
        <el-button
          style="float: right; margin-right: 5px"
          type="success"
          @click="addMenu"
        >新增菜单</el-button>
      </div>
      <div class="table">
        <p v-if="error" style="text-align: left;color: red;">{{ error }}</p>
        <el-row class="head">
          <el-col :span="8">图标</el-col>
          <el-col :span="6">标题</el-col>
          <el-col :span="8">跳转地址</el-col>
          <el-col :span="2">操作</el-col>
        </el-row>
        <draggable
          v-model="dataList"
          class="rows"
          :group="menus"
          animation="300"
        >
          <transition-group>
            <el-row v-for="item in dataList" :key="item.Id" class="row">
              <el-col :span="8" class="icon">
                <imageUpBtn
                  v-model="item.FileId"
                  file-key="PageMenuIcon"
                  :multiple="false"
                  :tag="item.Id.toString()"
                  :is-show-tip="false"
                  :img-set="{MaxWidth: 200,MaxHeight: 200,MinWidth: 150, MinHeight:150,Show: '图片高宽比为1:1,高宽在150-200之间。'}"
                  :file-uri.sync="item.Icon"
                  :link-biz-pk="id"
                  @error="upError"
                >
                  <template slot="trigger">
                    <i class="el-icon-plus uploader-icon" />
                  </template>
                  <template slot="fileList" slot-scope="e">
                    <img
                      :src="e.files[0].FileUri"
                      :alt="e.files[0].FileName"
                      class="menuIcon"
                    >
                  </template>
                </imageUpBtn>
              </el-col>
              <el-col :span="6">
                <el-input v-model="item.Title" placeholder="菜单名" />
              </el-col>
              <el-col :span="8" style="text-align: right;">
                <span v-if="item.Content" class="contentTitle" :title="item.Content.Title">{{ item.Content.Title }}</span>
                <el-button
                  size="mini"
                  type="primary"
                  class="editPage"
                  title="设置跳转地址"
                  icon="el-icon-edit"
                  circle
                  @click="chiose(item)"
                />
              </el-col>
              <el-col :span="2" style="text-align: center;">
                <el-button
                  size="mini"
                  type="danger"
                  title="删除"
                  icon="el-icon-delete"
                  circle
                  @click="drop(item)"
                />
              </el-col>
            </el-row>
          </transition-group>
        </draggable>
      </div>
    </el-card>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :loading="loading"
        type="success"
        @click="save"
      >保存</el-button>
      <el-button @click="reset">重置</el-button>
    </el-row>
    <contentEdit :visible="visible" :module-id="id" :page-id="pageId" :item-id="itemId" :config="content" @save="saveContent" @close="visible=false" />
  </el-form>
</template>

<script>
import draggable from 'vuedraggable'
import * as moduleApi from '@/shop/api/pageModule'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
import contentEdit from './contentChioseEdit.vue'
export default {
  components: {
    imageUpBtn,
    draggable,
    contentEdit
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
      error: null,
      content: {
        JumpType: 0,
        Sku: null,
        Activity: null,
        Link: null,
        Title: null
      },
      itemId: null,
      menuId: 1,
      dataList: [],
      moduleConfig: {
        ModuleTitle: null
      },
      menus: {
        name: 'menus',
        put: false,
        pull: false
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
    drop(row) {
      const index = this.dataList.findIndex((a) => a.Id === row.Id)
      if (index !== -1) {
        this.dataList.splice(index, 1)
      }
    },
    upError(error) {
      this.error = error
    },
    addMenu() {
      const data = {
        Id: this.menuId,
        Icon: null,
        Title: null,
        FileId: null,
        IsSave: false,
        Content: null
      }
      this.menuId = this.menuId + 1
      this.dataList.push(data)
    },
    chiose(item) {
      this.itemId = item.Id
      this.content = item.Content
      this.visible = true
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
      this.menuId = 0
      if (res.ModuleConfig && res.ModuleConfig.Menus) {
        const menus = res.ModuleConfig.Menus
        menus.forEach((c) => {
          if (c.Id > this.menuId) {
            this.menuId = c.Id
          }
        })
        this.dataList = menus
        this.menuId = this.menuId + 1
      } else {
        this.menuId = 1
      }
      this.moduleConfig = conf
    },
    save() {
      const that = this
      this.$refs['editModule'].validate((valid) => {
        if (valid) {
          that.saveMenu()
        } else {
          return false
        }
      })
    },
    async saveContent(id, config) {
      this.visible = false
      const item = this.dataList.find(c => c.Id === id)
      if (item != null) {
        item.Content = config
        await this.saveConfig()
      }
    },
    async saveMenu() {
      const data = await this.saveConfig()
      this.$emit('save', data)
    },
    async saveConfig() {
      const fileId = []
      const set = {
        ModuleTitle: this.moduleConfig.ModuleTitle,
        ModuleConfig: {
          ModuleName: 'menus',
          Menus: this.dataList.map((c) => {
            if (c.FileId != null && c.FileId.length > 0) {
              fileId.push(c.FileId[0])
            }
            return {
              Id: c.Id,
              Icon: c.Icon != null && Array.isArray(c.Icon) ? c.Icon[0] : c.Icon,
              Title: c.Title,
              Content: c.Content
            }
          })
        }
      }
      set.FileId = fileId
      await moduleApi.Set(this.id, set)
      set.Id = this.id
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      return set
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
.uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 60px;
  height: 60px;
  line-height: 60px;
  border-radius: 100px;
  text-align: center;
  border: 1px;
  border-color: #8c939d;
  border-style: dashed;
}
.menuIcon {
  width: 60px;
  height: 60px;
  border-radius: 100px;
}
</style>
  <style >
.el-input__suffix {
  right: 0px !important;
}
.table {
  width: 100%;
}
.table .head {
  height: 20px;
  line-height: 20px;
  font-size: 14px;
  text-align: center;
  font-weight: 700;
}
.table .rows {
  width: 100%;
  margin-top: 10px;
}
.table .row .el-col{
  height: 70px;
  line-height: 70px;
  padding-left: 2px;
  padding-right: 2px;
}
.table .row {
  padding: 5px;
  margin-bottom: 5px;
  text-align: left;
}
.table .row .icon {
  text-align: center;
}
.table .row .contentTitle {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 150px;
  text-align: left;
  float: left;
  display: block;
}
</style>
