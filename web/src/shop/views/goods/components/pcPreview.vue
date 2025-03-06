<template>
  <div class="pcMain">
    <div class="content">
      <div class="category">
        <el-breadcrumb separator=">">
          <el-breadcrumb-item v-for="(name, index) in category" :key="index">{{
            name
          }}</el-breadcrumb-item>
        </el-breadcrumb>
      </div>
      <div class="header">
        <div class="imgList">
          <imageCover :images="sku.ImgList"></imageCover>
        </div>
        <div class="title">
          <p class="skuName">{{ sku.SkuName }}</p>
          <p class="skuShow">{{ sku.Show }}</p>
          <el-row :gutter="24" class="row">
            <el-col class="label" :span="4">售价</el-col>
            <el-col class="skuPrice" :span="20"
              >￥ <span>{{ sku.Price }}</span></el-col
            >
          </el-row>
          <el-row :gutter="24" class="row">
            <el-col class="label" :span="4">促销</el-col>
            <el-col class="text" :span="20">暂无促销信息</el-col>
          </el-row>
          <el-row :gutter="24" class="row">
            <el-col class="label" :span="4">配送</el-col>
            <el-col class="text" :span="16">
              <areaSelect v-model="areaId" placeholder="选择目的地" />
            </el-col>
            <el-col :span="4">
              <el-button type="primary" @click="logisticsPrice">计算物流费用</el-button>
            </el-col>
          </el-row>
          <el-divider></el-divider>
          <el-row
            :gutter="24"
            class="row"
            v-for="item in goods.SpecGroup"
            :key="item.Id"
          >
            <el-col class="label" :span="4">选择{{ item.GroupName }}</el-col>
            <el-col class="spec" :span="20">
              <div
                :class="getItemClss(item, spec)"
                v-for="spec in item.Spec"
                :key="spec.Id"
                @click="chioseSku(item, spec)"
              >
                <img v-if="spec.SpecIcon" :src="spec.SpecIcon" />
                <span>{{ spec.SpecName }}</span>
              </div>
            </el-col>
          </el-row>
          <el-divider></el-divider>
          <div class="buy">
            <el-input-number
              :min="1"
              :max="99"
              v-model="buyNum"
              :step="1"
              class="bigNumber"
              placeholder="购买数量"
            ></el-input-number>
            <el-button size="medium" type="danger">加入购物车</el-button>
          </div>
        </div>
        <div style="clear: both"></div>
      </div>
      <div class="body">
        <div class="left"></div>
        <div class="right">
          <el-tabs v-model="activeTab" class="goodsTabs" type="card">
            <el-tab-pane label="商品介绍" name="show">
              <div class="attrShow">
                <p class="brand">品牌：{{ goods.BrandName }}</p>
                <ul class="attrs">
                  <li :title="goods.GoodsName">
                    商品名称：{{ goods.GoodsName }}
                  </li>
                  <li :title="sku.GoodsSku">商品编号：{{ sku.GoodsSku }}</li>
                  <li
                    :title="attr.Value"
                    v-for="(attr, index) in mainAttr"
                    :key="index"
                  >
                    {{ attr.Name }}：{{ attr.Value }}
                  </li>
                </ul>
                <div class="more" v-if="sku.Attrs.length > 0">
                  <el-button type="text" @click="moreAttr"
                    >更多参数>></el-button
                  >
                </div>
              </div>
              <div class="imageShow">
                <el-image
                  :src="src"
                  :lazy="true"
                  v-for="(src, index) in sku.ImgShow"
                  :key="index"
                ></el-image>
              </div>
            </el-tab-pane>
            <el-tab-pane label="规格与包装" name="attr">
              <div class="attrTable">
                <el-row
                  :gutter="24"
                  v-for="(attr, index) in attrs"
                  :key="index"
                >
                  <el-col :span="4" class="title">{{ attr.Name }}</el-col>
                  <el-col :span="20" v-if="attr.IsDir" class="dir">
                    <el-row v-for="(item, k) in attr.Attrs" :key="k">
                      <el-col :span="4" class="title">{{ item.Name }}</el-col>
                      <el-col :span="20" class="value">{{ item.Value }}</el-col>
                    </el-row>
                  </el-col>
                  <el-col :span="20" v-else>{{ attr.Value }}</el-col>
                </el-row>
              </div>
            </el-tab-pane>
            <el-tab-pane label="售后保障" name="server"></el-tab-pane>
          </el-tabs>
          <div class="serverShow">
            <div class="head">
              <h3>售后保障</h3>
            </div>
            <div class="show">
              <div v-for="(item, index) in goods.ServerShow" :key="index">
                <strong>{{ item.Title }}</strong
                ><br />
                <p v-if="item.ImgSrc == null">{{ item.Description }}</p>
                <template v-else>
                  <el-image
                    :src="src"
                    :lazy="true"
                    v-for="(src, index) in item.ImgSrc"
                    :key="index"
                  ></el-image>
                </template>
              </div>
            </div>
          </div>
        </div>
        <div style="clear: both"></div>
      </div>
    </div>
    <calaulate
      :visible="visiblePrice"
      :templateId="goods.LogisticsId"
      :param="{
        Weight: sku.Weight,
        Volume: sku.Volume,
        TotalPrice: buyNum * sku.Price,
        AreaId: areaId
       }"
      @close="visiblePrice = false"
    ></calaulate>
  </div>
</template>

<script>
import imageCover from "@/shop/components/img/imageCover.vue";
import areaSelect from "@/components/area/areaSelect.vue";
import calaulate from "@/shop/views/logistics/components/calculatePrice";
export default {
  computed: {},
  props: {
    source: {
      type: Object,
      default: null,
    },
    isLoad: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    isLoad: {
      handler(val) {
        if (val && this.source != null) {
          this.reset();
        }
      },
      immediate: true,
    },
  },
  components: {
    imageCover,
    areaSelect,
    calaulate
  },
  data() {
    return {
      category: [],
      chiose: {},
      isInit: false,
      visiblePrice: false,
      goods: {},
      buyNum: 1,
      areaId:0,
      skuState: {},
      groupId: [],
      attrs: [],
      mainAttr: [],
      activeTab: "show",
      sku: {
        ImgList: [],
        ImgShow: [],
        SkuName: null,
        Price: null,
        Show: null,
        Attrs: [],
      },
    };
  },
  methods: {
    moreAttr() {
      this.activeTab = "attr";
    },
    logisticsPrice(){
      this.visiblePrice = true
    },
    getItemClss(item, spec) {
      let cls = "item";
      if (spec.SpecIcon != null) {
        cls = cls + " itemPic";
      }
      const state = this.getChooseState(item.Id, spec.Id);
      if (!state.IsEnable) {
        const key = this.getChioseKey(spec.Id);
        if (key == null) {
          cls = cls + " disabled";
        } else {
          cls = cls + ' noActive';
        }
      } else if (this.chiose[item.Id] == spec.Id) {
        cls = cls + " active";
      }
      return cls;
    },
    getChooseState(gid, specId) {
      if (this.chiose == null) {
        return {
          IsEnable: false,
        };
      }
      let t = this.skuState;
      this.groupId.forEach((i) => {
        if (i == gid) {
          t = t[specId];
        } else {
          t = t[this.chiose[i]];
        }
      });
      return t;
    },
    getState(data) {
      let t = this.skuState;
      this.groupId.forEach((i) => {
        t = t[data[i]];
      });
      return t;
    },
    chioseSku(item, spec) {
      const state = this.getChooseState(item.Id, spec.Id);
      if (state.IsEnable) {
        this.initSku(state.SpecKey);
        this.goods.SpecGroup = this.goods.SpecGroup.concat();
      } else {
        const key = this.getChioseKey(spec.Id);
        if (key != null) {
          this.initSku(key);
          this.goods.SpecGroup = this.goods.SpecGroup.concat();
        }
      }
    },
    getChioseKey(specId) {
      for (const i in this.goods.SpecState) {
        const k = this.goods.SpecState[i];
        if (k.IsEnable && k.SpecId.includes(specId)) {
          return i;
        }
      }
      return null;
    },
    initChiose(key) {
      this.chiose = {};
      const specId = this.goods.SpecState[key].SpecId;
      this.goods.SpecGroup.forEach((c) => {
        this.chiose[c.Id] = c.Spec.find((a) => specId.includes(a.Id)).Id;
      });
    },
    initSku(key) {
      this.initChiose(key);
      const sku = this.goods.Sku[key];
      if (sku.Attrs.length > 0) {
        const attr = sku.Attrs[0];
        if (attr.IsDir) {
          this.mainAttr = attr.Attrs;
        } else {
          this.mainAttr = [attr];
        }
        this.attrs = sku.Attrs.slice(0, sku.Attrs.length - 1);
      } else {
        this.mainAttr = [];
      }
      this.sku = sku;
      this.activeTab = "show";
    },
    reset() {
      if (this.isInit) {
        return;
      }
      this.isInit = true;
      this.goods = Object.assign({}, this.source);
      this.category = this.goods.FrontCategory;
      this.category.push(this.goods.GoodsName);
      this.groupId = this.goods.SpecGroup.map((c) => c.Id);
      const key = this.initSkuState();
      if (key == null) {
        this.chiose = null;
        return;
      }
      this.initSku(key);
    },
    initSkuState() {
      this.skuState = {};
      const state = this.goods.SpecState;
      let curSku = null;
      for (const i in state) {
        const val = state[i];
        let t = this.skuState;
        val.SpecId.forEach((e) => {
          if (t[e] == null) {
            t[e] = {};
          }
          t = t[e];
        });
        if (val.IsEnable) {
          t.IsEnable = this.goods.Sku[i] != null;
          val.IsEnable = t.IsEnable;
        } else {
          t.IsEnable = false;
        }
        val.IsEnable = t.IsEnable;
        t.SpecKey = i;
        if (t.IsEnable && curSku == null) {
          curSku = i;
        }
      }
      return curSku;
    },
  },
};
</script>
<style lang="scss" scoped>
.pcMain {
  width: 100%;
  font-family: "microsoft yahei";
  min-width: 1200px;
  text-align: center;
  padding-top: 20px;
  .content {
    width: 1200px;
    text-align: left;
    display: inline-block;
    .category {
      width: 100%;
      line-height: 30px;
      height: 30px;
    }

    .header {
      margin-top: 10px;
      width: 100%;
      min-height: 570px;
      .imgList {
        width: 450px;
        min-height: 450px;
        float: left;
      }
      .title {
        width: 750px;
        float: right;
        font-size: 12px;
        font-weight: 400;
        padding-left: 20px;
        .buy {
          width: 100%;
          line-height: 60px;
          padding-left: 50px;
          .el-button {
            margin-left: 10px;
            line-height: 35px;
            font-size: 24px;
            font-weight: 700;
          }
          .el-input-number {
            width: 120px;
          }
        }
        .row {
          width: 100%;
          line-height: 45px;
          .label {
            text-align: right;
            font-family: simsun;
            color: #999;
          }
          .text {
            font-size: 12px;
            color: #000;
          }
          .spec {
            .item {
              margin-right: 7px;
              float: left;
              margin-bottom: 12px;
              display: flex;
              line-height: 30px;
              cursor: pointer;
              padding: 5px;
              border: 1px solid #999;
              span {
                margin: 0 8px;
              }
            }
            .itemPic {
              line-height: 45px !important;
              img {
                width: 40px;
                height: 40px;
              }
            }
            .noActive {
              color: #999;
            }
            .disabled {
              color: #999;
              cursor: default;
              text-decoration: line-through;
            }
            .active {
              border-color: #e4393c;
            }
          }
        }
        .skuName {
          font: 700 16px Arial, "microsoft yahei";
          color: #666;
          padding-top: 10px;
          line-height: 28px;
          margin-bottom: 5px;
          padding: 0;
        }
        .skuShow {
          font-size: 14px;
          line-height: 24px;
        }
        .skuPrice {
          color: #e4393c;
          span {
            font-size: 22px;
          }
        }
      }
    }

    .body {
      width: 100%;
      margin-top: 20px;
      .left {
        float: left;
        width: 210px;
      }
      .right {
        float: right;
        width: 980px;
        .attrTable {
          font-size: 12px;
          color: #666;
          .el-row {
            line-height: 30px;
          }
          .title {
            text-align: right;
            padding-right: 10px;
          }
          .value {
            text-align: left;
            padding-left: 20px;
          }
        }
        .attrShow {
          font-size: 12px;
          color: #666;
          padding-top: 10px;
          padding-bottom: 10px;
          .attrs {
            display: inline-block;
            li {
              display: inline-block;
              overflow: hidden;
              white-space: nowrap;
              text-overflow: ellipsis;
              height: 20px;
              line-height: 20px;
              width: 200px;
              margin-bottom: 5px;
            }
          }
          .more {
            height: 20px;
            line-height: 20px;
            text-align: right;
            .el-button {
              margin: 0;
              padding: 0;
              font-size: 12px;
            }
          }
        }
        .brand {
          padding-left: 43px;
        }
        .imageShow {
          width: 100%;
          text-align: center;
          .el-image {
            width: 800px;
          }
        }
        .serverShow {
          width: 100%;
          .head {
            background-color: #f7f7f7;
            border: 1px solid #eee;
            padding-left: 10px;
            h3 {
              font: 700 14px "microsoft yahei";
            }
          }
          .show {
            width: 100%;
            padding: 5px;
            font-size: 12px;
            strong {
              color: #e4393c;
            }
            p {
              white-space: break-spaces;
              line-height: 20px;
            }
          }
        }
      }
    }
  }
}
</style>
<style lang="css">
.bigNumber .el-input-number {
  width: 120px;
  height: 50px;
  line-height: 50px;
}
.bigNumber .el-input--medium .el-input__inner {
  height: 50px !important;
  line-height: 50px !important;
}
.bigNumber .el-input-number__decrease {
  height: 48px;
  line-height: 50px;
}

.bigNumber .el-input-number__increase {
  height: 48px;
  line-height: 50px;
}
.goodsTabs .el-tabs__header .el-tabs__item {
  height: 38px;
  line-height: 38px;
  font-size: 14px;
  color: #666;
  border-style: none;
  width: 106px;
}

.goodsTabs .el-tabs__header .el-tabs__item:hover {
  color: #e4393c;
}
.goodsTabs .el-tabs__header {
  border-bottom: 1px solid #e4393c;
}
.goodsTabs .is-active {
  background-color: #e4393c;
  color: #fff !important;
  cursor: default;
}
</style>
