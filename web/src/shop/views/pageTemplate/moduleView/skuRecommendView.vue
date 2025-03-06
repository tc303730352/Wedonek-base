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
        @click="chiose"
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
      <img v-if="isLoad == false" src="/pageTemplate/image/skuCover.png">
      <div v-else class="recommend">
        <template v-if="item.isHidden == false">
          <el-image
            v-if="item.titleImg"
            :src="item.titleImg"
            style="width: 100%"
          />
          <p v-else class="title">{{ item.title }}</p>
        </template>
        <div class="content">
          <div v-for="(spu, index) in goods" :key="index" class="spu">
            <div class="img">
              <el-image :src="spu.MainImg" width="100%" />
            </div>
            <p class="name">{{ spu.GoodsName }}</p>
            <p class="price">
              ¥<em class="int">{{ spu.UnitPrice }}</em>.{{ spu.Point }}
              <el-button icon="el-icon-shopping-cart-1" size="small" type="danger" circle />
            </p>
          </div>
          <div style="clear: both" />
        </div>
        <p v-if="item.topNum == goods.length" class="move">查看更多....</p>
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
      item: {
        class: 'module',
        title: null,
        cover: null,
        topNum: null,
        titleImg: null,
        isHidden: false
      },
      goods: []
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
      if (this.value.config && this.value.config.Recommend) {
        const config = this.value.config.Recommend
        this.item.titleImg = config.TitleImg
        this.item.isHidden = config.IsHideTitle
        this.item.topNum = config.SkuConfig.TopNum
        this.isLoad = true
        this.load(config.SkuConfig)
      } else {
        this.item.cover = '/pageTemplate/image/skuCover.png'
        this.isLoad = false
      }
    },
    load(config) {
      if (config.ChioseType === 0) {
        this.loadModule()
      } else {
        this.loadGoods(config)
      }
    },
    async loadModule() {
      const list = await moduleApi.GetTops({
        ModuleId: this.id,
        Top: this.item.topNum
      })
      this.initGoods(list)
    },
    async loadGoods(config) {
      const param = {
        CategoryId: config.CategoryId,
        Lvl: config.Level,
        Top: this.item.topNum
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
      this.goods = list
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

.module .coverImg img {
  width: 100%;
}

.recommend {
  width: 100%;
  background: #fff;
  border-radius: 10px;
  min-height: 195px;
}
.recommend .content {
  background-color: #f2f2f2;
}
.recommend .title {
  line-height: 30px;
  height: 30px;
  text-align: left;
  padding-left: 10px;
  margin: 0;
  font-size: 16px;
}
.recommend .move {
  margin: 0;
  text-align: center;
  padding: 10px;
  font-size: 16px;
  font-weight: 500;
  margin-top: 5px;
}
.recommend .spu {
  border-radius: 12px;
  background-color: #fff;
  width: 48%;
  float: left;
  margin: 1%;
}
.recommend .spu .img {
  width: 100%;
}
.recommend .spu .name {
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
.recommend .spu .price {
  font-style: normal;
  font-family: JDZH-Regular, sans-serif;
  font-size: 16px;
  line-height: 20px;
  color: #ff4142;
  text-align: left;
  padding-left: 5px;
}

.recommend .spu .price .el-button{
 float: right;
 margin-right: 5px;
}
.recommend .spu .price .int {
  font-size: 22px;
  font-style: normal;
}
</style>
