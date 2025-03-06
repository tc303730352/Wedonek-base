<template>
  <div>
    <el-form label-width="150px">
      <el-card>
        <div slot="header">
          <span>券基本信息</span>
        </div>
        <el-form-item label="优惠卷名">
          {{ coupon.CouponTitle }}
        </el-form-item>
        <el-form-item label="投放时段">
          {{
            moment(coupon.PutInBegin).format("YYYY-MM-DD HH:mm") +
              " 至 " +
              moment(coupon.PutInEnd).format("YYYY-MM-DD HH:mm")
          }}
        </el-form-item>
        <el-form-item label="使用时间">
          <template v-if="useMode == 0">
            {{
              moment(coupon.UseBegin).format("YYYY-MM-DD HH:mm") +
                " 至 " +
                moment(coupon.UseEnd).format("YYYY-MM-DD HH:mm")
            }}
          </template>
          <template v-else>
            <span>领券领取后</span>
            <span style="margin-left: 5px; margin-right: 5px">{{
              coupon.ExpireDay
            }}</span>
            <span>天内使用，逾期失效。</span>
          </template>
        </el-form-item>
        <el-form-item label="优惠卷类型">
          {{ coupon.CouponType == 0 ? "折扣卷" : "满减卷" }}
        </el-form-item>
        <el-form-item label="优惠信息">
          {{ coupon.CouponShow }}
        </el-form-item>
        <el-form-item v-if="coupon.CouponType == 0" label="最高优惠">
          {{ coupon.MaxCouponVal + "元" }}
        </el-form-item>
        <el-form-item label="投放量">
          {{ coupon.PutInNum }}张
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 10px">
        <div slot="header">
          <span>其它设置</span>
        </div>
        <el-form-item label="定向人群">
          {{ coupon.UserRange == 0 ? "普通会员" : "新用户" }}
        </el-form-item>
        <el-form-item v-if="coupon.UserRange == 0" label="定向会员等级">
          {{ coupon.GradeId == null ? "全部" : coupon.GradeName }}
        </el-form-item>
        <el-form-item label="限领规则">
          {{ coupon.ReceiveTypeName }}
        </el-form-item>
        <el-form-item label="单次发放张数">
          {{ coupon.ReceiveNum }}张
        </el-form-item>
        <el-form-item label="是否公开">
          {{ coupon.IsPublic ? "公开" : "不公开" }}
        </el-form-item>
      </el-card>
      <el-card v-if="coupon.PutInRange != 0" style="margin-top: 10px">
        <div slot="header">
          <span>促销商品</span>
        </div>
        <w-table
          :data="dataList"
          :is-paging="false"
          :columns="columns"
          max-height="600px"
          :is-select="false"
          row-key="Id"
          @selected="
            (e) => {
              this.selectKeys = e.keys;
            }
          "
        >
          <template slot="GoodsName" slot-scope="e">
            <span
              :style="{ color: GoodsStatus[e.row.GoodsStatus].color }"
            >({{ GoodsStatus[e.row.GoodsStatus].text }})</span>
            <span>{{ e.row.GoodsName }}</span>
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
                />
              </div>
              <div class="content">
                <div class="item">
                  <span
                    v-if="e.row.SkuIsEnable"
                    style="color: #43af2b"
                  >(已启用)</span>
                  <span v-else style="color: #e4393c">(已停用)</span>
                  {{ e.row.SkuName }}
                </div>
                <div class="skuNo">编号：{{ e.row.GoodsSku }}</div>
              </div>
            </div>
          </template>
        </w-table>
      </el-card>
    </el-form>
  </div>
</template>

<script>
import { EnumDic, CouponType, GoodsStatus } from '@/shop/config/shopConfig'
import moment from 'moment'
import * as couponApi from '@/shop/api/coupon'
import { Gets } from '@/shop/api/couponGoods'
export default {
  components: {},
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      GoodsStatus,
      CouponType,
      coupon: {},
      useMode: 0,
      dataList: [],
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          minWidth: 140,
          slotName: 'GoodsName'
        },
        {
          key: 'sku',
          title: '规格名',
          align: 'left',
          slotName: 'sku',
          minWidth: 300
        },
        {
          key: 'SpecsShow',
          title: '规格说明',
          align: 'left',
          slotName: 'SpecsShow',
          minWidth: 150
        },
        {
          key: 'Price',
          title: '原价格',
          align: 'center',
          minWidth: 100
        }
      ]
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
    async reset() {
      const res = await couponApi.Get(this.id)
      this.useMode = res.UseBegin && res.UseEnd ? 1 : 0
      if (res.PutInRange === 1) {
        this.loadGoods()
      }
      this.coupon = res
    },
    async loadGoods() {
      this.dataList = await Gets(this.id)
    }
  }
}
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
