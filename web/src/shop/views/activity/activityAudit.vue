<template>
  <el-card>
    <div slot="header">
      <span>促销活动审核</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="促销标题"
            @change="search"
          />
        </el-form-item>
        <el-form-item label="促销类型">
          <enumItem
            v-model="queryParam.ActivityType"
            :dic-key="EnumDic.activityType"
            placeholder="促销类型"
            :multiple="false"
            sys-head="shop"
            @change="search"
          />
        </el-form-item>
        <el-form-item label="促销时间">
          <el-date-picker
            v-model="queryParam.Begin"
            type="date"
            placeholder="促销开始时间"
            @change="search"
          />
          <span style="margin-left: 5px; margin-right: 5px">至</span>
          <el-date-picker
            v-model="queryParam.End"
            type="date"
            placeholder="促销截止时间"
            @change="search"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="resetQuery">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="dataList"
      :is-paging="true"
      :columns="columns"
      :paging="paging"
      row-key="Id"
      @load="load"
    >
      <template slot="UserRange" slot-scope="e">
        <div v-if="e.row.UserRange == 0">
          <p style="margin: 0; line-height: 25px; height: 25px">普通用户</p>
          <p
            v-if="e.row.UserGrade"
            style="margin: 0; line-height: 25px; height: 25px; color: #1890ff"
          >
            {{ e.row.GradeName }}
          </p>
        </div>
        <span v-else-if="e.row.UserRange == 1"> 新用户 </span>
      </template>
      <template slot="ActivityType" slot-scope="e">
        {{ ActivityType[e.row.ActivityType].text }}
      </template>
      <template slot="Range" slot-scope="e">
        <span v-if="e.row.Range == 0">全场</span>
        <span v-else-if="e.row.Range == 1"> {{ e.row.SkuNum }}件商品参与 </span>
        <span v-else> {{ e.row.SkuNum }}件商品不参与 </span>
      </template>
      <template slot="ActivityStatus" slot-scope="e">
        <span :style="{ color: ActivityStatus[e.row.ActivityStatus].color }">{{
          ActivityStatus[e.row.ActivityStatus].text
        }}</span>
      </template>
      <template slot="ActivityTime" slot-scope="e">
        {{
          moment(e.row.BeginTime).format("YYYY-MM-DD HH:mm") +
            "至" +
            moment(e.row.EndTime).format("YYYY-MM-DD HH:mm")
        }}
      </template>
      <template slot="AddTime" slot-scope="e">
        {{ moment(e.row.AddTime).format("YYYY-MM-DD HH:mm") }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="查看"
          icon="el-icon-view"
          circle
          @click="show(e.row.Id)"
        />
      </template>
    </w-table>
  </el-card>
</template>

<script>
import moment from 'moment'
import * as activityApi from '@/shop/api/activity'
import { EnumDic, ActivityType } from '@/shop/config/shopConfig'
export default {
  components: {},
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    activityType: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      ActivityType,
      dataList: [],
      title: '',
      id: null,
      skuName: null,
      visible: false,
      columns: [
        {
          key: 'ActivityTitle',
          title: '促销名',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'ActivityType',
          title: '促销类型',
          align: 'center',
          slotName: 'ActivityType',
          minWidth: 80
        },
        {
          key: 'UserRange',
          title: '定向人群',
          align: 'center',
          slotName: 'UserRange',
          minWidth: 100
        },
        {
          key: 'Range',
          title: '促销范围',
          align: 'center',
          slotName: 'Range',
          minWidth: 100
        },
        {
          key: 'ActivityTime',
          title: '促销时段',
          align: 'center',
          slotName: 'ActivityTime',
          minWidth: 200
        },
        {
          key: 'LimitBuy',
          title: '限购规则',
          align: 'center',
          minWidth: 120
        },
        {
          key: 'SkuNum',
          title: '促销Sku数',
          align: 'right',
          minWidth: 100
        },
        {
          key: 'DiscountShow',
          title: '优惠说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'AddTime',
          title: '添加时间',
          align: 'center',
          slotName: 'AddTime',
          minWidth: 200
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          minWidth: 120,
          slotName: 'action'
        }
      ],
      queryParam: {
        ActivityType: null,
        QueryKey: null,
        Begin: null,
        End: null
      },
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  computed: {},
  mounted() {
    this.resetQuery()
  },
  methods: {
    moment,
    search() {
      this.paging.Index = 1
      this.load()
    },
    resetQuery() {
      this.queryParam.QueryKey = null
      this.queryParam.ActivityType = null
      this.queryParam.Status = [1]
      this.queryParam.Begin = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    },
    show(id) {
      this.$router.push({
        name: 'auditActivity',
        params: { id: id }
      })
    },
    async load() {
      const res = await activityApi.Query(this.queryParam, this.paging)
      if (res.List) {
        res.List.forEach((c) => {
          if (c.IsLimitBuyNum) {
            c.LimitBuy =
              '每单限制购买' + c.MinBuyNum + '-' + c.MaxBuyNum + '件'
          } else {
            c.LimitBuy = '每单不限购'
          }
          if (c.IsLimitBuy) {
            c.LimitBuy = c.LimitBuy + '(限购一单)'
          }
        })
        this.dataList = res.List
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
    }
  }
}
</script>
