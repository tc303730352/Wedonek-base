<template>
  <el-dialog
    title="选择促销活动"
    :visible="visible"
    width="900"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="促销标题"
            @change="search"
          />
        </el-form-item>
        <el-form-item label="活动类型">
          <enumItem
            v-model="queryParam.ActivityType"
            :dic-key="EnumDic.activityType"
            placeholder="活动类型"
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
      :is-select="true"
      :is-multiple="false"
      :select-keys="selectKeys"
      row-key="Id"
      @selected="selectEvent"
      @load="load"
    >
      <template slot="ActivityType" slot-scope="e">
        {{
          ActivityType[e.row.ActivityType].text
        }}
      </template>
      <template slot="ActivityTime" slot-scope="e">
        {{
          moment(e.row.BeginTime).format("YYYY-MM-DD HH:mm") +
            "至" +
            moment(e.row.EndTime).format("YYYY-MM-DD HH:mm")
        }}
      </template>
    </w-table>
    <el-row style="text-align: center;margin-top: 10px;">
      <el-button type="success" @click="save">确认选择</el-button>
      <el-button type="default" @click="handleClose">关闭</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import moment from 'moment'
import * as activityApi from '@/shop/api/activity'
import {
  EnumDic,
  ActivityType
} from '@/shop/config/shopConfig'
export default {
  components: {
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    activityId: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      ActivityType,
      EnumDic,
      dataList: [],
      selectKeys: [],
      activity: {},
      isInit: false,
      columns: [
        {
          key: 'ActivityTitle',
          title: '促销名',
          align: 'left',
          minWidth: 200
        },
        {
          key: 'ActivityType',
          title: '促销类型',
          align: 'center',
          slotName: 'ActivityType',
          width: 100
        },
        {
          key: 'ActivityTime',
          title: '促销时段',
          align: 'center',
          slotName: 'ActivityTime',
          minWidth: 200
        },
        {
          key: 'DiscountShow',
          title: '优惠说明',
          align: 'left',
          minWidth: 255
        }
      ],
      queryParam: {
        ActivityType: null,
        Status: [2],
        UserRange: null,
        Range: null,
        QueryKey: null,
        PutInDate: null,
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
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    moment,
    handleClose() {
      this.$emit('close')
    },
    selectEvent(e) {
      this.selectKeys = e.keys
    },
    reset() {
      if (this.activityId == null || this.activityId.length === 0) {
        this.selectKeys = []
      } else {
        this.selectKeys = this.activityId
      }
      this.isInit = false
      this.resetQuery()
    },
    search() {
      this.paging.Index = 1
      this.load()
    },
    resetQuery() {
      this.queryParam.ActivityType = null
      this.queryParam.QueryKey = null
      this.queryParam.PutInDate = null
      this.queryParam.Begin = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    },
    save() {
      if (this.selectKeys.length === 0) {
        this.$message({
          message: '请选择促销活动!',
          type: 'error'
        })
        return
      }
      this.$emit('save', {
        keys: this.selectKeys,
        activity: this.selectKeys.map(c => this.activity[c])
      })
    },
    async loadChiose() {
      if (this.selectKeys.length === 0) {
        return
      }
      const ids = this.selectKeys.filter(c => this.activity[c] == null)
      if (ids.length > 0) {
        const res = await activityApi.Gets(ids)
        res.forEach(c => {
          this.activity[c.Id] = c
        })
      }
    },
    async load() {
      if (this.queryParam.ActivityType === '') {
        this.queryParam.ActivityType = null
      }
      const res = await activityApi.Query(this.queryParam, this.paging)
      if (res.List) {
        res.List.forEach(c => {
          this.activity[c.Id] = c
        })
      }
      if (!this.isInit) {
        this.isInit = true
        this.loadChiose()
      }
      this.dataList = res.List
      this.paging.Total = res.Count
    }
  }
}
</script>
