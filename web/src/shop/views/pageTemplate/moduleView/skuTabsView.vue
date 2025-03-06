<template>
  <div :class="item.class">
    <div class="leftTitle">
      {{ item.title }}
    </div>
    <div class="rightBtn">
      <el-button
        type="text"
        :loading="loading"
        style="color: red"
        icon="el-icon-setting"
      />
      <el-button type="text" :loading="loading" icon="el-icon-document-copy" />
      <el-button
        type="text"
        :loading="loading"
        icon="el-icon-delete"
        @click="remove"
      />
    </div>
    <div class="coverImg" @click="chiose">
      <img v-if="item.isload == false" src="/pageTemplate/image/skuCover.png">
      <div v-else class="tabs">
        <el-tabs v-model="activeName" @tab-click="tabChiose">
          <el-tab-pane
            v-for="tab in item.tabs"
            :key="tab.TabKey"
            :name="tab.TabKey"
          >
            <span slot="label" class="tabLabel">{{ tab.TabName }}</span>
            <div v-for="(spu, index) in spuList[tab.TabKey]" :key="index" class="spu">
              <div class="img">
                <el-image :src="spu.MainImg" width="100%" />
              </div>
              <p class="name">{{ spu.GoodsName }}</p>
              <p class="price">
                ¥<em class="int">{{ spu.UnitPrice }}</em>.{{ spu.Point }}
                <el-button icon="el-icon-shopping-cart-1" size="small" type="danger" circle />
              </p>
            </div>
            <p v-if="spuList[tab.TabKey] && tab.SkuConfig.TopNum == spuList[tab.TabKey].length" class="move">查看更多....</p>
            <div style="clear: both" />
          </el-tab-pane>
        </el-tabs>
      </div>
    </div>
  </div>
</template>

<script>
import * as moduleApi from '@/shop/api/moduleGoods'
import * as goodsApi from '@/shop/api/goods'
export default {
  components: {},
  props: {
    value: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      loading: false,
      id: null,
      isLoad: false,
      activeName: null,
      item: {
        class: 'module',
        isload: false,
        title: null,
        tabs: null
      },
      spuList: {}
    }
  },
  watch: {
    value: {
      handler(val) {
        if (val && val.mid != null) {
          this.reset()
        }
      },
      immediate: true,
      deep: true
    }
  },
  methods: {
    reset() {
      this.id = this.value.mid
      this.item.class = this.value.isChiose ? 'module active' : 'module'
      this.item.title = this.value.title
      if (this.value.config == null || this.value.config.Tabs.length !== 0) {
        this.item.tabs = this.value.config.Tabs
        this.item.isload = true
        const tab = this.item.tabs[0]
        this.load(tab)
      } else {
        this.item.isload = false
      }
    },
    tabChiose(e) {
      this.activeName = null
      const tab = this.item.tabs.find(c => c.TabKey === e.name)
      this.load(tab)
    },
    load(tab) {
      const config = tab.SkuConfig
      if (config.ChioseType === 0) {
        this.loadModule(tab, config)
      } else {
        this.loadGoods(tab, config)
      }
    },
    async loadModule(tab, config) {
      const list = await moduleApi.GetTops({
        ModuleId: this.id,
        Tag: tab.TabKey,
        Top: config.TopNum
      })
      this.initGoods(list)
      this.spuList[tab.TabKey] = list
      this.$nextTick(() => {
        this.activeName = tab.TabKey
      })
    },
    async loadGoods(tab, config) {
      const param = {
        CategoryId: config.CategoryId,
        Lvl: config.Level,
        Top: config.TopNum
      }
      if (config.OrderType === 0) {
        param.SortName = 'Id'
        param.IsDesc = true
      } else if (config.OrderType === 1) {
        param.SortName = 'SaleNum'
        param.IsDesc = true
      } else if (config.OrderType === 2) {
        param.SortName = 'Price'
        param.IsDesc = false
      }
      const list = await goodsApi.GetTops(param)
      this.initGoods(list)
      this.spuList[tab.TabKey] = list
      this.$nextTick(() => {
        this.activeName = tab.TabKey
      })
    },
    initGoods(list) {
      list.forEach((c) => {
        const str = c.Price.toString()
        const index = str.lastIndexOf('.')
        if (index === -1) {
          c.UnitPrice = str
          c.Point = '00'
        } else {
          c.UnitPrice = str.substring(0, index)
          c.Point = str.substring(index + 1)
          if (c.Point.length === 1) {
            c.Point = c.Point + '0'
          }
        }
      })
    },
    chiose() {
      if (this.value.isChiose) {
        this.$emit('click', this.value)
        return
      }
      this.loading = true
      this.$emit('chiose', this.value)
      this.loading = false
    },
    remove() {
      this.loading = true
      this.$emit('remove', this.value)
      this.loading = false
    }
  }
}
</script>

  <style scoped>
.active {
  border-color: #29f;
  border-width: 0.15rem;
  border-style: solid;
}

.active .leftTitle {
  background-color: hsla(0, 0%, 43%, 0.85) !important;
  color: #fff !important;
}

.module {
  width: 100%;
  min-height: 195px;
}

.module .coverImg {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}

.module .leftTitle {
  width: 28px;
  position: absolute;
  left: -50px;
  z-index: 1;
  margin-top: 0px;
  background-color: #fff;
  text-align: center;
  padding-top: 10px;
  padding-bottom: 10px;
  border-radius: 10px;
  color: gray;
  font-size: 14px;
  padding-left: 5px;
  padding-right: 5px;
  border: 1px solid #e1e1e1;
}

.module .rightBtn {
  position: absolute;
  left: 375px;
  z-index: 1;
  margin-top: 0px;
  background-color: #fff;
  text-align: center;
  padding: 10px;
  border-radius: 10px;
  border: 1px solid #e1e1e1;
  width: 50px;
}

.module .rightBtn .el-button {
  margin: 0px;
  padding: 5px;
  font-size: 16px;
}
.module .tabs {
  width: 100%;
}
.module .tabLabel {
  padding-left: 5px !important;
  padding-right: 5px !important;
}
.module .coverImg img {
  width: 100%;
}
.module .move {
  margin: 0;
  text-align: center;
  padding: 10px;
  font-size: 16px;
  font-weight: 500;
  margin-top: 5px;
}
.module .spu {
  border-radius: 12px;
  background-color: #fff;
  width: 48%;
  float: left;
  margin: 1%;
}
.module .spu .img {
  width: 100%;
}
.module .spu .name {
  font-size: 16px;
  color: #434343;
  font-weight: 400;
  font-family: "-apple-system, Helvetica, sans-serif";
  margin: 0;
  text-align: left;
  line-height: 20px;
  max-height: 40px;
  overflow: hidden;
}
.module .spu .price {
  font-style: normal;
  font-family: JDZH-Regular, sans-serif;
  font-size: 16px;
  line-height: 20px;
  color: #ff4142;
  text-align: left;
  padding-left: 5px;
}

.module .spu .price .el-button{
 float: right;
 margin-right: 5px;
}
.module .spu .price .int {
  font-size: 22px;
  font-style: normal;
}
</style>
