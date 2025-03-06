<template>
  <el-card>
    <div slot="header">
      <span>{{ this.putInRange == 0 ? "店铺" : "商品" }}优惠卷列表</span>
      <el-button
        style="float: right"
        type="success"
        @click="add"
      >新增优惠卷</el-button>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-col :span="24">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="优惠卷标题"
              @change="search"
            />
          </el-form-item>
          <el-form-item v-if="putInRange == 1" label="优惠商品">
            <el-input
              v-model="skuName"
              placeholder="选择优惠商品"
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
          <el-form-item label="投放时间">
            <el-date-picker
              v-model="queryParam.Begin"
              type="date"
              placeholder="投放开始时间"
              @change="search"
            />
            <span style="margin-left: 5px; margin-right: 5px">至</span>
            <el-date-picker
              v-model="queryParam.End"
              type="date"
              placeholder="促投放截止时间"
              @change="search"
            />
          </el-form-item>
          <el-form-item>
            <el-button @click="reset">重置</el-button>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="优惠卷状态">
            <enumItem
              v-model="queryParam.CouponStatus"
              :dic-key="EnumDic.couponStatus"
              placeholder="优惠卷状态"
              :multiple="false"
              sys-head="shop"
              mode="radio-button"
              @change="search"
            />
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="优惠卷类型">
            <enumItem
              v-model="queryParam.CouponType"
              :dic-key="EnumDic.couponType"
              placeholder="优惠卷类型"
              :multiple="false"
              sys-head="shop"
              mode="radio-button"
              @change="search"
            />
          </el-form-item>
        </el-col>
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
      <template slot="PutInNum" slot-scope="e">
        <span style="color: #e4393c">{{ e.row.SurplusNum }}</span>
        <span>/</span>
        <span style="color: #1890ff">{{ e.row.PutInNum }}</span>
      </template>
      <template slot="UserRange" slot-scope="e">
        <div v-if="e.row.UserRange == 0">
          <div style="margin: 0; line-height: 25px; height: 25px">普通用户</div>
          <div
            v-if="e.row.UserGrade"
            style="margin: 0; line-height: 25px; height: 25px; color: #1890ff"
          >
            {{ e.row.GradeName }}
          </div>
        </div>
        <span v-else-if="e.row.UserRange == 1"> 新用户 </span>
      </template>
      <template slot="PutInRange" slot-scope="e">
        <span v-if="e.row.PutInRange == 0">全场</span>
        <span v-else> {{ e.row.SkuNum }}件商品参与 </span>
      </template>
      <template slot="ReceiveType" slot-scope="e">
        <p v-if="e.row.ReceiveType == 0">每人限领一次</p>
        <p v-else>每人每天限领一次</p>
        <p>单次领取数：{{ e.row.ReceiveNum }}</p>
      </template>
      <template slot="UseLimit" slot-scope="e">
        <template v-if="e.row.UseBegin && e.row.UseEnd">
          <div>
            开始：{{ moment(e.row.UseBegin).format("YYYY-MM-DD HH:mm") }}
          </div>
          <div>结束: {{ moment(e.row.UseEnd).format("YYYY-MM-DD HH:mm") }}</div>
        </template>
        <span v-else>领取后{{ e.row.ExpireDay }}天内有效</span>
      </template>
      <template slot="Status" slot-scope="e">
        <span :style="{ color: CouponStatus[e.row.Status].color }">{{
          CouponStatus[e.row.Status].text
        }}</span>
      </template>
      <template slot="CouponType" slot-scope="e">
        {{ CouponType[e.row.CouponType].text }}
      </template>
      <template slot="PutInTime" slot-scope="e">
        <div>
          开始：{{ moment(e.row.PutInBegin).format("YYYY-MM-DD HH:mm") }}
        </div>
        <div>结束: {{ moment(e.row.PutInEnd).format("YYYY-MM-DD HH:mm") }}</div>
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
        <el-button
          v-if="e.row.Status == 0 || e.row.Status == 2"
          size="mini"
          type="success"
          title="启用"
          icon="el-icon-open"
          circle
          @click="enable(e.row)"
        />
        <el-button
          v-if="e.row.Status == 0 || e.row.Status == 2"
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
        <el-button
          v-if="e.row.Status == 0"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
        <el-button
          v-if="e.row.Status == 1 && e.row.PutInBegin >= moment()"
          size="mini"
          type="warning"
          title="下架"
          icon="el-icon-download"
          circle
          @click="offShelf(e.row)"
        />
        <el-button
          v-else-if="e.row.Status == 1"
          size="mini"
          type="warning"
          title="停用"
          circle
          @click="stop(e.row)"
        >■</el-button>
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
import * as couponApi from '@/shop/api/coupon'
import chioseSku from '@/shop/components/goods/chioseSku.vue'
import {
  EnumDic,
  CouponStatus,
  CouponType
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
    putInRange: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      CouponStatus,
      CouponType,
      dataList: [],
      title: '',
      id: null,
      skuName: null,
      visible: false,
      columns: [
        {
          key: 'CouponTitle',
          title: '标题',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'CouponType',
          title: '优惠卷类型',
          align: 'center',
          slotName: 'CouponType',
          minWidth: 100
        },
        {
          key: 'PutInTime',
          title: '领取时段',
          align: 'center',
          slotName: 'PutInTime',
          minWidth: 200
        },
        {
          key: 'PutInNum',
          title: '投放数',
          align: 'center',
          slotName: 'PutInNum',
          minWidth: 200
        },
        {
          key: 'UseLimit',
          title: '使用限制',
          align: 'center',
          slotName: 'UseLimit',
          minWidth: 200
        },
        {
          key: 'UserRange',
          title: '定向人群',
          align: 'center',
          slotName: 'UserRange',
          minWidth: 200
        },
        {
          key: 'PutInRange',
          title: '投放范围',
          slotName: 'PutInRange',
          align: 'center',
          minWidth: 120
        },
        {
          key: 'ReceiveType',
          title: '限购类型',
          align: 'center',
          slotName: 'ReceiveType',
          minWidth: 200
        },
        {
          key: 'CouponShow',
          title: '优惠说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'Status',
          title: '状态',
          align: 'center',
          slotName: 'Status',
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
          minWidth: 180,
          slotName: 'action'
        }
      ],
      queryParam: {
        QueryKey: null,
        Status: null,
        CouponType: null,
        UserRange: null,
        PutInRange: null,
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
        if (val && this.putInRange != null) {
          this.reset()
        }
      },
      immediate: true
    },
    putInRange: {
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
      const title = '确认删除该优惠卷 ' + row.CouponTitle + '?'
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
      await couponApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.reset()
    },
    enable(row) {
      const title = '确认启用该优惠卷 ' + row.CouponTitle + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitEnable(row)
      })
    },
    async submitEnable(row) {
      await couponApi.Enable(row.Id)
      this.$message({
        type: 'success',
        message: '下架成功!'
      })
      row.Status = 1
    },
    offShelf(row) {
      const title = '确认下架该优惠卷 ' + row.CouponTitle + '?'
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
      await couponApi.OffShelf(row.Id)
      this.$message({
        type: 'success',
        message: '下架成功!'
      })
      row.Status = 3
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
    search() {
      this.paging.Index = 1
      this.load()
    },
    reset() {
      this.queryParam.UserRange = null
      this.queryParam.PutInRange = this.putInRange
      this.queryParam.QueryKey = null
      this.queryParam.Status = null
      this.queryParam.SkuId = null
      this.queryParam.CouponStatus = null
      this.queryParam.CouponType = null
      this.queryParam.Begin = null
      this.skuName = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    },
    edit(row) {
      this.$router.push({
        name: 'couponEdit',
        params: { id: row.Id, range: this.putInRange }
      })
    },
    show(id) {
      this.$router.push({
        name: 'couponView',
        params: { id: id }
      })
    },
    add() {
      this.$router.push({
        name: 'couponAdd',
        params: {
          range: this.putInRange
        }
      })
    },
    format() {
      const query = Object.assign({}, this.queryParam)
      if (query.CouponStatus != null && query.CouponStatus != '') {
        query.Status = [query.CouponStatus]
      }
      delete query.CouponStatus
      return query
    },
    async load() {
      const res = await couponApi.Query(this.format(), this.paging)
      if (res.List) {
        this.dataList = res.List
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
    }
  }
}
</script>
