<template>
  <div class="miniMain">
    <div class="content">
      <div class="head">
        <el-carousel trigger="click" height="720px">
          <el-carousel-item v-for="(src, index) in sku.ImgList" :key="index">
            <el-image :src="src"></el-image>
          </el-carousel-item>
        </el-carousel>
        <div class="price">
          ¥
          <em>{{ sku.Price }}</em>
        </div>
        <p class="skuName">
          <span :title="sku.SkuName">{{ sku.SkuName }}</span>
        </p>
        <div style="clear: both"></div>
      </div>
      <div class="buyBody">
        <div class="row" @click="showSpec">
          <div class="label">已选</div>
          <div class="value" :title="specName">{{ specName }}</div>
        </div>
        <div class="row">
          <div class="label">配送至</div>
          <div class="value">
            <areaSelect v-model="areaId" placeholder="选择目的地" />
          </div>
        </div>
        <div style="clear: both"></div>
      </div>
      <div class="body">
        <el-tabs v-model="activeTab" class="goodsTabs" type="card">
          <el-tab-pane label="商品介绍" name="show">
            <div class="imageShow">
              <el-image
                :src="src"
                :lazy="true"
                v-for="(src, index) in sku.ImgShow"
                :key="index"
              ></el-image>
            </div>
          </el-tab-pane>
          <el-tab-pane label="规格参数" name="attr">
            <div class="attrTable">
              <table border="0" cellpadding="0" cellspacing="1">
                <tr v-for="(attr, index) in attrs" :key="index">
                  <td
                    :colspan="attr.IsDir ? 2 : 1"
                    :class="attr.IsDir ? 'title dir' : 'title'"
                  >
                    {{ attr.Name }}
                  </td>
                  <td v-if="attr.IsDir == false" class="value">
                    {{ attr.Value }}
                  </td>
                </tr>
              </table>
            </div>
          </el-tab-pane>
          <el-tab-pane label="售后保障" name="server">
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
          </el-tab-pane>
        </el-tabs>
      </div>
    </div>
    <el-dialog
      :visible="chooseSpec"
      width="720px"
      :modal-append-to-body="false"
      class="chooseSku"
      :before-close="handleClose"
    >
      <el-row :gutter="24" slot="title">
        <el-col :span="6">
          <el-image :src="sku.SkuImg"></el-image>
        </el-col>
        <el-col :span="18" class="skuInfo">
          <div class="price">
            ¥
            <em>{{ sku.Price }}</em>
          </div>
          <div class="spec">
            <div class="label">已选</div>
            <div class="value">{{ specName }}</div>
          </div>
        </el-col>
      </el-row>
      <div slot="footer" class="btnFooter">
        <el-button class="shoppingCart" type="warning">加入购物车</el-button>
        <el-button class="buyBtn" type="danger">立即购买</el-button>
      </div>
      <div class="group" v-for="item in goods.SpecGroup" :key="item.Id">
        <div class="name">{{ item.GroupName }}</div>
        <div class="specList">
          <div
            :class="getItemClss(item, spec)"
            v-for="spec in item.Spec"
            :key="spec.Id"
            @click="chioseSku(item, spec)"
          >
            <span>{{ spec.SpecName }}</span>
          </div>
          <div style="clear: both"></div>
        </div>
      </div>
      <div style="line-height: 40px; height: 40px; text-align: right">
        <span style="float: left">数量</span>
        <el-input-number
          v-model="buyNum"
          placeholder="购买数"
        ></el-input-number>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import EditDic from "@/views/dict/components/editDic.vue";
import areaSelect from "@/components/area/areaSelect.vue";
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
  components: { EditDic, areaSelect },
  data() {
    return {
      isInit: false,
      buyNum: 1,
      goods: {},
      chooseSpec: false,
      activeTab: "show",
      specName: null,
      skuState: {},
      groupId: [],
      areaId: 0,
      chiose: null,
      attrs: [],
      sku: {
        ImgList: [],
      },
    };
  },
  methods: {
    showSpec() {
      this.chooseSpec = true;
    },
    getItemClss(item, spec) {
      let cls = "item";
      const state = this.getChooseState(item.Id, spec.Id);
      if (!state.IsEnable) {
        const key = this.getChioseKey(spec.Id);
        if (key == null) {
          cls = cls + " disabled";
        } else {
          cls = cls + " noActive";
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
    handleClose() {
      this.chooseSpec = false;
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
    initSku(key) {
      this.initChiose(key);
      this.sku = this.goods.Sku[key];
      this.specName =
        this.sku.SpecsShow.replaceAll("|", " ") + " " + this.buyNum + "个";
      this.initAttrs();
      this.activeTab = "show";
    },
    getState(data) {
      let t = this.skuState;
      this.groupId.forEach((i) => {
        t = t[data[i]];
      });
      return t;
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
    reset() {
      if (this.isInit) {
        return;
      }
      this.isInit = true;
      this.goods = this.source;
      this.groupId = this.goods.SpecGroup.map((c) => c.Id);
      const key = this.initSkuState();
      if (key == null) {
        this.chiose = null;
        return;
      }
      this.initSku(key);
    },
    initAttrs() {
      const list = [];
      this.sku.Attrs.forEach((c) => {
        list.push({
          Name: c.Name,
          Value: c.Value,
          IsDir: c.IsDir,
        });
        if (c.IsDir && c.Attrs) {
          c.Attrs.forEach((a) => {
            list.push({
              Name: a.Name,
              Value: a.Value,
              IsDir: false,
            });
          });
        }
      });
      this.attrs = list;
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
.miniMain {
  width: 100%;
  font-family: "microsoft yahei";
  min-width: 720px;
  text-align: center;
  padding-top: 20px;
  background-color: #f7f7f7 !important;
  .chooseSku {
    .el-image {
      width: 80px;
    }
    .btnFooter {
      text-align: center;
      padding-top: 10px;
      padding-bottom: 10px;
      border-top: 1px solid #c7c7c7;
      .shoppingCart {
        width: 40%;
        margin-right: 5%;
        border-radius: 20px;
      }
      .buyBtn {
        width: 40%;
        margin-left: 5%;
        border-radius: 20px;
      }
    }
    .skuInfo {
      text-align: left;
      height: 80px;
      .spec {
        font-size: 14px;
        display: flex;
        line-height: 25px;
        .label {
          color: #8c8c8c;
          margin-right: 10px;
        }
        .value {
          color: #333;
          font-size: 14px;
        }
      }
      .price {
        height: 30px;
        line-height: 30px;
        color: #e4393c;
        font-weight: 700;
        em {
          font-size: 24px;
        }
      }
    }
    .group {
      .name {
        color: #262626;
        font-size: 14px;
        text-align: left;
        font-weight: 700;
        height: 40px;
        line-height: 40px;
        margin-bottom: 5px;
      }
      .specList {
        width: 100%;
        .item {
          background-color: #f2f2f2;
          border-radius: 15px;
          color: #262626;
          float: left;
          cursor: pointer;
          font-size: 14px;
          line-height: 30px;
          margin-bottom: 10px;
          margin-left: 12px;
          border: 1px solid #f2f2f2;
          max-width: 270px;
          min-width: 20px;
          overflow: hidden;
          padding: 0 18px;
          text-align: center;
        }
        .active {
          background: #fcedeb;
          border: 1px solid #f2270c;
          color: #f2270c;
          font-weight: 700;
          font-size: 14px;
        }
        .noActive {
          color: #bfbfbf;
        }
        .disabled {
          color: #bfbfbf;
          text-decoration: line-through;
        }
      }
    }
  }
  .content {
    width: 720px;
    text-align: left;
    display: inline-block;
    .head {
      .el-image {
        width: 100%;
      }
      background-color: #fff;
      .price {
        height: 30px;
        line-height: 30px;
        color: #e4393c;
        margin-top: 12px;
        padding-left: 18px;
        font-weight: 700;
        em {
          font-size: 24px;
        }
      }
      .skuName {
        line-height: 21px;
        font-weight: 700;
        font-size: 16px;
        overflow-x: hidden;
        overflow-y: hidden;
        margin: 0;
        text-overflow: ellipsis;
        max-height: 42px;
        padding: 12px 18px 12px 18px;
      }
    }
    .buyBody {
      background-color: #fff;
      margin-top: 20px;
      padding-top: 15px;
      .row {
        padding-bottom: 15px;
        display: flex;
        line-height: 22px;
        .label {
          font-weight: 700;
          width: 20%;
          font-size: 14px;
          text-align: right;
        }
        .value {
          width: 80%;
          color: #333;
          font-size: 14px;
          cursor: pointer;
          padding-left: 18px;
        }
      }
    }
    .body {
      width: 100%;
      margin-top: 20px;
      margin-bottom: 20px;
      .attrTable {
        width: 100%;
        table {
          width: 100%;
          border-color: gray;
          border-collapse: collapse;
          border-spacing: 0;
          font-size: 12px;
          tr {
            line-height: 35px;
            height: 35px;
          }
          .title {
            padding: 8px;
            width: 120px;
          }
          .dir {
            font-weight: 700;
          }
          .value {
            padding: 8px;
            text-align: left;
            word-break: break-all;
          }
          td {
            border: 1px solid #dadada;
          }
        }
      }
      .imageShow {
        width: 100%;
        text-align: center;
        .el-image {
          width: 100%;
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
</style>
<style lang="scss">
.chooseSku .el-dialog__header {
  padding-top: 20px;
  padding-left: 0px;
  padding-right: 0px;
}
</style>
