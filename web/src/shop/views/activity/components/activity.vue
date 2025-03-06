<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
      <el-button
        style="float: right"
        type="success"
        @click="add"
      >新增活动</el-button>
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
        <el-form-item label="促销商品">
          <el-input
            v-model="skuName"
            placeholder="选择促销商品"
            :readonly="true"
          >
            <el-button
              slot="append"
              icon="el-icon-circle-plus-outline"
              @click="chiose"
            />
          </el-input>
        </el-form-item>
        <el-form-item label="定向人群">
          <enumItem
            v-model="queryParam.UserRange"
            :dic-key="EnumDic.userRange"
            placeholder="定向人群"
            :multiple="false"
            sys-head="shop"
            @change="search"
          />
        </el-form-item>
        <el-form-item v-if="activityType == 1" label="促销范围">
          <enumItem
            v-model="queryParam.Range"
            :dic-key="EnumDic.range"
            placeholder="促销范围"
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
        <template v-if="e.row.ActivityStatus == 0 || e.row.ActivityStatus == 4">
          <el-button
            size="mini"
            type="primary"
            title="编辑"
            icon="el-icon-edit"
            circle
            @click="edit(e.row)"
          />
          <el-button
            size="mini"
            type="danger"
            title="删除"
            icon="el-icon-delete"
            circle
            @click="drop(e.row)"
          />
        </template>
        <template v-else>
          <el-button
            v-if="e.row.ActivityStatus == 2"
            size="mini"
            type="warning"
            title="下架"
            icon="el-icon-download"
            circle
            @click="offShelf(e.row)"
          />
        </template>
      </template>
    </w-table>
    <chioseSku
      :visible="visible"
      :sku-id="queryParam.SkuId"
      @save="saveSku"
      @close="visible = false"
    />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as activityApi from '@/shop/api/activity'
import chioseSku from '@/shop/components/goods/chioseSku.vue'
import {
  EnumDic,
  ActivityStatus,
  ActivityType
} from '@/shop/config/shopConfig'
export default {
  components: {
    chioseSku
  },
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
      ActivityStatus,
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
          key: 'DiscountShow',
          title: '优惠说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'ActivityStatus',
          title: '状态',
          align: 'center',
          slotName: 'ActivityStatus',
          width: 100
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
        UserRange: null,
        Range: null,
        QueryKey: null,
        PutInDate: null,
        SkuId: null,
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
  watch: {
    isLoad: {
      handler(val) {
        if (val && this.activityType != null) {
          this.reset()
        }
      },
      immediate: true
    },
    activityType: {
      handler(val) {
        if (val && this.isLoad) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    moment,
    drop(row) {
      const title = '确认删除该促销活动 ' + row.ActivityTitle + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row)
      })
    },
    async submitDrop(row) {
      await activityApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.resetQuery()
    },
    offShelf(row) {
      const title = '确认下架该促销活动 ' + row.ActivityTitle + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitOffShelf(row)
      })
    },
    async submitOffShelf(row) {
      await activityApi.OffShelf(row.Id)
      this.$message({
        type: 'success',
        message: '下架成功!'
      })
      this.resetQuery()
    },
    chiose() {
      this.visible = true
    },
    saveSku(e) {
      this.visible = false
      this.queryParam.SkuId = e.keys[0]
      const sku = e.skus[0]
      this.skuName = sku.SkuName
      this.search()
    },
    reset() {
      this.queryParam.ActivityType = this.activityType
      this.title = ActivityType[this.activityType].text + '活动列表'
      if (this.activityType !== 1) {
        this.columns = this.columns.filter(
          (c) => c.key !== 'Range' && c.key !== 'DiscountShow'
        )
      }
      this.resetQuery()
    },
    search() {
      this.paging.Index = 1
      this.load()
    },
    resetQuery() {
      this.queryParam.UserRange = null
      this.queryParam.Range = null
      this.queryParam.QueryKey = null
      this.queryParam.PutInDate = null
      this.queryParam.SkuId = null
      this.queryParam.Begin = null
      this.skuName = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    },
    edit(row) {
      this.$router.push({
        name: 'activityEdit',
        params: { id: row.Id, type: this.activityType }
      })
    },
    show(id) {
      this.$router.push({
        name: 'activityView',
        params: { id: id }
      })
    },
    add() {
      this.$router.push({
        name: 'activityAdd',
        params: { type: this.activityType }
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
