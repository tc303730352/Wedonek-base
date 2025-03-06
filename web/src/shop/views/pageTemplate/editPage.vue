<template>
  <div>
    <el-card :style="{ minHeight: height }">
      <span slot="header">编辑模版</span>
      <div class="mainBody">
        <div class="left">
          <el-card>
            <span slot="header">模块列表</span>
            <el-collapse v-model="active">
              <el-collapse-item title="商品类" name="goods">
                <draggable
                  v-model="goodsModule"
                  :group="goods"
                  animation="300"
                  :move="moveCheck"
                  @start="onStart"
                  @end="onEnd"
                >
                  <transition-group>
                    <div
                      v-for="item in goodsModule"
                      :key="item.ModuleKey"
                      :span="12"
                      class="item"
                    >
                      <div class="imgDiv">
                        <img :src="item.ModuleIcon">
                      </div>
                      <div class="title">{{ item.ModuleName }}</div>
                    </div>
                  </transition-group>
                </draggable>
              </el-collapse-item>
              <el-collapse-item title="图文类" name="advert">
                <draggable
                  v-model="advertModule"
                  :group="goods"
                  animation="300"
                  :move="moveCheck"
                  @start="onStart"
                  @end="onEnd"
                >
                  <transition-group>
                    <div
                      v-for="item in advertModule"
                      :key="item.ModuleKey"
                      :span="12"
                      class="item"
                    >
                      <div class="imgDiv">
                        <img :src="item.ModuleIcon">
                      </div>
                      <div class="title">{{ item.ModuleName }}</div>
                    </div>
                  </transition-group>
                </draggable>
              </el-collapse-item>
              <el-collapse-item title="营销类" name="marketing">
                <draggable
                  v-model="marketingModule"
                  :group="goods"
                  animation="300"
                  :move="moveCheck"
                  @start="onStart"
                  @end="onEnd"
                >
                  <transition-group>
                    <div
                      v-for="item in marketingModule"
                      :key="item.ModuleKey"
                      :span="12"
                      class="item"
                    >
                      <div class="imgDiv">
                        <img :src="item.ModuleIcon">
                      </div>
                      <div class="title">{{ item.ModuleName }}</div>
                    </div>
                  </transition-group>
                </draggable>
              </el-collapse-item>
            </el-collapse>
          </el-card>
        </div>
        <div class="body">
          <div class="main">
            <div class="header">
              <el-input style="width: 60%" placeholder="商品名">
                <i slot="append" class="el-icon-search" />
              </el-input>
              <div class="icon">
                <i class="el-icon-more" />
                <i class="el-icon-circle-close" />
              </div>
            </div>
            <div class="content">
              <draggable
                v-model="modules"
                :group="template"
                animation="300"
                @start="onStart"
                @end="onEnd"
                @add="onAdd"
              >
                <transition-group class="moduleList">
                  <div v-for="(item, index) in modules" :key="index">
                    <component
                      :is="item.key"
                      :name="item.name"
                      :value="item"
                      :example-img="exampleImg"
                      @chiose="chiose"
                      @click="clickModule"
                      @remove="remove"
                    />
                  </div>
                </transition-group>
              </draggable>
            </div>
          </div>
        </div>
      </div>
    </el-card>
    <el-drawer
      :title="title"
      :visible.sync="visible"
      :modal="false"
      :wrapper-closable="false"
      :show-close="true"
      size="650px"
      :append-to-body="true"
    >
      <component
        :is="config.editKey"
        v-if="config"
        :name="config.editKey"
        :is-load="visible"
        :use-range="range"
        :page-id="pageId"
        :config="config"
        @save="saveConfig"
      />
    </el-drawer>
  </div>
</template>

<script>
import Vue from 'vue'
import { Gets } from '@/shop/api/pageModuleList'
import * as moduleApi from '@/shop/api/pageModule'
import { GetExampleImg } from '@/shop/api/advertPlace'
import draggable from 'vuedraggable'
export default {
  components: {
    draggable
  },
  data() {
    return {
      loading: false,
      isAdd: false,
      active: ['goods', 'advert', 'marketing'],
      height: '500px',
      title: null,
      upClick: null,
      range: null,
      pageId: null,
      config: null,
      exampleImg: {},
      visible: false,
      drag: false,
      id: 1,
      goods: {
        name: 'page',
        pull: 'clone',
        put: false
      },
      template: {
        name: 'page',
        pull: false,
        put: true
      },
      loadModules: [],
      modules: [],
      goodsModule: [],
      advertModule: [],
      marketingModule: []
    }
  },
  computed: {},
  mounted() {
    this.height =
      document.getElementsByClassName('app-main')[0].offsetHeight - 100 + 'px'
    this.pageId = this.$route.params.id
    this.range = parseInt(this.$route.params.range)
    this.load()
    this.loadPageModule()
    this.loadModule()
  },
  methods: {
    async load() {
      const res = await GetExampleImg(['001', '002', '003', '004', '005', '006'])
      this.exampleImg = res
    },
    saveConfig(e) {
      this.visible = false
      const item = this.modules.find(a => a.mid === e.Id)
      if (item != null) {
        item.config = e.ModuleConfig
      }
    },
    // 开始拖拽事件
    onStart() {
      this.drag = true
    },
    // 拖拽结束事件
    onEnd(e) {
      this.drag = false
      if (e.newIndex !== e.oldIndex && this.isAdd === false) {
        this.saveSort()
      }
    },
    async loadPageModule() {
      const datas = await moduleApi.Gets(this.pageId)
      if (datas.length > 0) {
        this.modules = datas.map((c) => {
          return {
            mid: c.Id,
            name: c.Id,
            editKey: c.ModuleKey + 'Edit',
            title: c.ModuleTitle,
            key: c.ModuleKey,
            isChiose: false,
            config: c.ModuleConfig
          }
        })
      } else {
        this.modules = []
      }
    },
    async loadModule() {
      this.loadModules = await Gets(this.range)
      if (this.loadModules.length > 0) {
        this.loadModules.forEach((c) => {
          Vue.component(c.ModuleKey, function(resolve, reject) {
            require([
              '@/shop/views/pageTemplate/moduleView/' +
                c.ModuleKey +
                'View.vue'
            ], resolve)
          })
          const key = c.ModuleKey + 'Edit'
          Vue.component(key, function(resolve, reject) {
            require([
              '@/shop/views/pageTemplate/module/' + key + '.vue'
            ], resolve)
          })
        })
        this.goodsModule = this.loadModules.filter((c) => c.ModuleType === 0)
        this.advertModule = this.loadModules.filter((c) => c.ModuleType === 1)
        this.marketingModule = this.loadModules.filter((c) => c.ModuleType === 2)
      }
    },
    remove(e) {
      const index = this.modules.findIndex((a) => a.mid === e.mid)
      if (index === -1) {
        return
      }
      const title = '确认删除该模块: ' + e.title + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(e, index)
      })
    },
    async submitDrop(e, index) {
      this.loading = true
      try {
        await moduleApi.Delete(e.mid)
        if (e.mid === this.upClick) {
          this.upClick = null
          this.visible = false
        }
        this.modules.splice(index, 1)
        this.loading = false
      } catch {
        this.loading = false
      }
    },
    clickModule(e) {
      if (this.visible) {
        return
      }
      this.visible = true
    },
    chiose(e) {
      if (e.isChiose && this.upClick === e.mid) {
        return
      } else if (this.loading) {
        return
      }
      this.loading = true
      e.isChiose = true
      if (this.upClick != null && this.upClick !== e.mid) {
        const item = this.modules.find((a) => a.mid === this.upClick)
        item.isChiose = false
      }
      this.upClick = e.mid
      this.modules = this.modules.concat()
      this.loading = false
      this.title = e.title
      this.config = e
      this.visible = true
    },
    async onAdd(e) {
      this.isAdd = true
      const index = e.newIndex
      const item = this.modules[index]
      const id = await moduleApi.Add({
        TemplateId: this.pageId,
        ModuleId: item.Id,
        ModuleKey: item.ModuleKey,
        ModuleTitle: item.ModuleName,
        Sort: index + 1
      })
      const data = {
        mid: id,
        editKey: item.ModuleKey + 'Edit',
        key: item.ModuleKey,
        title: item.ModuleName,
        isChiose: false,
        config: null
      }
      this.modules[index] = data
      this.chiose(data)
      this.saveSort()
      this.isAdd = false
    },
    moveCheck(e, original) {
      return e.to.getAttribute('class') === 'moduleList'
    },
    async saveSort() {
      if (this.modules.length === 0) {
        return
      }
      let sort = 0
      await moduleApi.SetSort(
        this.modules.map((c) => {
          sort = sort + 1
          return {
            Id: c.mid,
            Sort: sort
          }
        })
      )
    }
  }
}
</script>

<style scoped>
.el-drawer__wrapper {
  z-index: 1 !important;
}
.mainBody {
  width: 100%;
  min-height: 100%;
  position: relative;
}

.left {
  width: 300px;
  position: absolute;
  top: 0;
  left: 0;
  z-index: 2;
}
.item {
  padding: 5px;
  cursor: pointer;
  float: left;
  width: 50%;
}
.item .imgDiv {
  width: 100%;
  text-align: center;
  height: 108px;
  line-height: 108px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.item .imgDiv img {
  max-height: 108px;
  max-width: 108px;
}
.item .title {
  text-align: center;
  font-size: 14px;
}
.moduleList {
  min-height: 780px;
  display: block;
}
.body {
  width: 100%;
  min-height: 100%;
  text-align: center;
  background-color: #f2f2f2;
}

.main {
  width: 375px;
  min-height: 800px;
  display: inline-block;
  border-radius: 5px;
  border-width: 1px;
  border-style: solid;
  border-color: #d7d7d7;
  margin-top: 20px;
  margin-bottom: 20px;
  position: relative;
  z-index: 1;
}
.main .content {
  width: 100%;
  background-color: #f2f2f2;
  min-height: 760px;
}

.main .header {
  width: 100%;
  height: 40px;
  text-align: left;
  line-height: 40px;
  border-bottom-width: 1px;
  border-bottom-color: #d7d7d7;
  border-bottom-style: solid;
}

.main .header .icon {
  float: right;
  height: 30px;
  font-size: 22px;
  line-height: 30px;
  font-weight: 700;
  padding-left: 8px;
  border: 1px solid;
  border-radius: 20px;
  border-color: #909399;
  padding-right: 1px;
  margin-top: 5px;
  margin-right: 5px;
}
.main main .header .icon i {
  padding-right: 3px;
}
</style>
