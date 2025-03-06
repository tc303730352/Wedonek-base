<template>
  <el-form label-width="150px">
    <el-card>
      <div slot="header">
        <span>{{ title }}-活动信息</span>
      </div>
      <el-form-item label="促销名">
        {{ activity.ActivityTitle }}
      </el-form-item>
      <el-form-item label="促销时段">
        {{
          moment(activity.BeginTime).format("YYYY-MM-DD HH:mm") +
          " 至 " +
          moment(activity.EndTime).format("YYYY-MM-DD HH:mm")
        }}
      </el-form-item>
      <el-form-item label="活动简介">
        {{ activity.ActivityShow }}
      </el-form-item>
      <el-form-item label="定向人群">
        <span v-if="activity.UserRange == 0 && activity.UserGrade == null"
          >所有用户都可参与</span
        >
        <span
          v-else-if="activity.UserRange == 0 && activity.UserGrade != null"
          >{{ activity.GradeName }}</span
        >
        <span v-else>{{ activity.UserRangeName }}</span>
      </el-form-item>
      <el-form-item v-if="activity.ActivityType == 1" label="优惠范围">
        {{ activity.RangeName }}
      </el-form-item>
      <el-form-item label="限购规则">
        <span v-if="activity.IsLimitBuyNum == false">不限购</span>
        <span v-else
          >每单每个SKU最少{{ activity.MinBuyNum }}个，最多{{
            activity.MaxBuyNum
          }}个。</span
        >
      </el-form-item>
      <el-form-item label="是否限购一单">
        {{ activity.IsLimitBuy ? "是" : "否" }}
      </el-form-item>
      <el-form-item v-if="activity.ActivityType == 1" label="优惠门槛及内容">
        <template v-if="activity.DiscountShow != null">
          <div
            v-for="(item, index) in activity.DiscountShow.split('\n')"
            :key="index"
          >
            {{ index + 1 }}，{{ item }}
          </div>
        </template>
      </el-form-item>
      <el-form-item v-if="activity.ActivityStatus == 2 || activity.ActivityStatus == 3" label="上架时间">
        {{ moment(activity.OnTime).format("YYYY-MM-DD HH:mm") }}
      </el-form-item>
      <el-form-item v-if="activity.ActivityStatus == 3" label="下架时间">
        {{ moment(activity.DelistTime).format("YYYY-MM-DD HH:mm") }}
      </el-form-item>
    </el-card>
    <el-card
      style="margin-top: 20px"
      v-if="
        (activity.ActivityType == 1 && activity.Range != 0) ||
        activity.ActivityType != 1
      "
    >
      <div slot="header">
        <span>{{
          activity.ActivityType != 1
            ? "促销的商品"
            : activity.Range == 1
            ? "参与促销的商品"
            : "不参与促销的商品"
        }}</span>
      </div>
      <div>
        <el-form style="float: right" :inline="true" :model="queryParam">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="商品名\SKU"
              @change="search"
            />
          </el-form-item>
          <el-form-item>
            <el-button @click="resetSearch">重置</el-button>
          </el-form-item>
        </el-form>
      </div>
      <w-table
        :data="skuList"
        :isPaging="false"
        :columns="columns"
        maxHeight="600px"
        :isSelect="false"
        @selected="
          (e) => {
            this.selectKeys = e.keys;
          }
        "
        rowKey="Id"
      >
        <template slot="GoodsName" slot-scope="e">
          <span :style="{ color: GoodsStatus[e.row.GoodsStatus].color }"
            >({{ GoodsStatus[e.row.GoodsStatus].text }})</span
          >
          <span>{{ e.row.GoodsName }}</span>
        </template>
        <template slot="activityList" slot-scope="e">
          <template v-if="e.row.Items && e.row.Items.length > 0">
            <p>
              促销价范围：{{ e.row.Items.Min("ActivityPrice") }}-{{
                e.row.Items.Max("ActivityPrice")
              }}
            </p>
            <P v-for="(item, index) in e.row.Items" :key="item.ActivityId">{{
              index +
              1 +
              "，" +
              item.ActivityTitle +
              "，促销价：" +
              item.ActivityPrice +
              "元。"
            }}</P>
          </template>
          <template v-else> 同时段未参与其它单品和套餐类促销 </template>
        </template>
        <template slot="ActivityPrice" slot-scope="e">
          {{ e.row.ActivityPrice }}
        </template>
        <template slot="IsMust" slot-scope="e">
          {{ e.row.IsMust ? "是" : "否" }}
        </template>
        <template slot="SpecsShow" slot-scope="e">
          <span style="white-space: pre">{{ e.row.SpecsShow }}</span>
        </template>
        <template slot="sku" slot-scope="e">
          <div class="sku">
            <div class="img">
              <el-avatar
                shape="square"
                :size="80"
                :src="e.row.SkuImg"
              ></el-avatar>
            </div>
            <div class="content">
              <div class="item">
                <span v-if="e.row.SkuIsEnable" style="color: #43af2b"
                  >(已启用)</span
                >
                <span v-else style="color: #e4393c">(已停用)</span>
                {{ e.row.SkuName }}
              </div>
              <div class="skuNo">编号：{{ e.row.GoodsSku }}</div>
            </div>
          </div>
        </template>
      </w-table>
    </el-card>
    <el-card
      style="margin-top: 20px"
      v-if="activity.ActivityStatus == 2 || activity.ActivityStatus == 4"
    >
      <div slot="header">
        <span>审核信息</span>
      </div>
      <el-form-item label="审核状态">
        <span
          :style="{ color: ActivityStatus[activity.ActivityStatus].color }"
          >{{ ActivityStatus[activity.ActivityStatus].text }}</span
        >
      </el-form-item>
      <el-form-item label="审核人">
        {{ activity.AuditEmpName }}
      </el-form-item>
      <el-form-item v-if="activity.ActivityStatus == 4" label="审批意见">
        {{ activity.AuditOpinion }}
      </el-form-item>
    </el-card>
  </el-form>
</template>
  
  
  <script>
import {
  ActivityType,
  GoodsStatus,
  ActivityStatus,
} from "@/shop/config/shopConfig";
import moment from "moment";
import { GetDetailed } from "@/shop/api/activity";
export default {
  computed: {},
  data() {
    return {
      GoodsStatus,
      ActivityStatus,
      loading: false,
      activity: {},
      skuList: [],
      title: null,
      queryParam: {
        QueryKey: null,
      },
      columns: [
        {
          key: "GoodsName",
          title: "商品名",
          align: "left",
          slotName: "GoodsName",
          minWidth: 140,
          fixed: "left",
        },
        {
          key: "sku",
          title: "规格名",
          align: "left",
          slotName: "sku",
          minWidth: 300,
          fixed: "left",
        },
        {
          key: "SpecsShow",
          title: "规格说明",
          align: "left",
          slotName: "SpecsShow",
          minWidth: 150,
        },
        {
          key: "OriginalPrice",
          title: "原价格",
          align: "center",
          minWidth: 100,
        },
        {
          key: "ActivityPrice",
          title: "促销价",
          align: "center",
          slotName: "ActivityPrice",
          minWidth: 100,
        },
        {
          key: "IsMust",
          title: "是否必须",
          align: "center",
          slotName: "IsMust",
          width: 90,
        },
        {
          key: "activityList",
          title: "正在参与的促销活动",
          align: "left",
          slotName: "activityList",
          minWidth: 255,
        },
      ],
    };
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false,
    },
    id: {
      type: Number,
      default: null,
    },
  },
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.reset();
        }
      },
      immediate: true,
    },
  },
  components: {},
  methods: {
    moment,
    async reset() {
      this.loading = false;
      const res = await GetDetailed(this.id);
      this.title = ActivityType[res.ActivityType].text;
      if (res.ActivityType == 1 && res.Range != 0) {
        this.columns = this.columns.filter(
          (c) => c.key != "ActivityPrice" && c.key != "IsMust"
        );
      } else if (res.ActivityType != 2) {
        this.columns = this.columns.filter((c) => c.key != "IsMust");
      }
      this.skuList = res.Goods;
      this.activity = res;
      this.$emit("load", res);
    },
    resetSearch() {
      this.queryParam.QueryKey = null;
      this.skuList = this.activity.Goods;
    },
    search() {
      if (this.queryParam.QueryKey == null || this.queryParam.QueryKey == "") {
        this.resetSearch();
        return;
      }
      this.skuList = this.activity.Goods.filter(
        (c) =>
          c.SkuName.includes(this.queryParam.QueryKey) ||
          c.GoodsSku.includes(this.queryParam.QueryKey)
      );
    },
  },
};
</script> 

<style  scoped>
.sku {
  width: 100%;
  height: 80px;
}
.sku .img {
  width: 80px;
  float: left;
  height: 80px;
  text-align: center;
}
.sku image {
  width: 100%;
}
.sku .content {
  display: flow-root;
  text-align: left;
}
.sku .content .item {
  margin: 0;
  padding-left: 5px;
  font-size: 16px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: break-spaces;
  line-height: 20px;
  max-height: 40px;
}

.sku .content .skuNo {
  padding-left: 10px;
  margin: 0;
  color: #82848a;
  font-size: 12px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}
.sku .content i {
  font-weight: 600;
}
</style>