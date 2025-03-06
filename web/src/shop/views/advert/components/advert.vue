<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
      <el-button
        v-if="this.status == 'schedules'"
        style="float: right"
        type="success"
        @click="add"
      >新增广告</el-button>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="广告标题"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="投放时段">
          <el-date-picker
            v-model="queryParam.TimeRange"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
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
      <template slot="ImgSrc" slot-scope="e">
        <el-image :src="e.row.ImgSrc" style="width: 80px" />
      </template>
      <template slot="PutInTime" slot-scope="e">
        <p v-if="e.row.AdvertStatus == 4">
          <span>{{
            moment(e.row.ActualBegin).format("yyyy-MM-DD HH:mm")
          }}</span>
          <span style="padding-left: 5px; padding-right: 5px">-</span>
          <span>{{
            moment(e.row.StopTime).format("yyyy-MM-DD HH:mm")
          }}</span>
        </p>
        <p v-else-if="e.row.AdvertStatus == 3">
          <span>{{
            moment(e.row.ActualBegin).format("yyyy-MM-DD HH:mm")
          }}</span>
          <span style="padding-left: 5px; padding-right: 5px">-</span>
          <span>{{
            moment(e.row.PutInEnd).format("yyyy-MM-DD HH:mm")
          }}</span>
        </p>
        <p v-else-if="e.row.PutInBegin && e.row.PutInEnd">
          <span>{{
            moment(e.row.PutInBegin).format("yyyy-MM-DD HH:mm")
          }}</span>
          <span style="padding-left: 5px; padding-right: 5px">-</span>
          <span>{{
            moment(e.row.PutInEnd).format("yyyy-MM-DD HH:mm")
          }}</span>
        </p>
      </template>
      <template slot="RelationType" slot-scope="e">
        {{ RelationType[e.row.RelationType].text }}
      </template>
      <template slot="AdvertStatus" slot-scope="e">
        <span
          v-if="e.row.AdvertStatus == 3"
          style="color: #1890ff"
        >投放中</span>
        <span
          v-else-if="e.row.AdvertStatus == 4"
          style="color: #e4393c"
        >已结束</span>
        <el-switch
          v-else
          active-text="已启用"
          :active-color="AdvertStatus[1].color"
          :inactive-color="AdvertStatus[e.row.AdvertStatus].color"
          :value="e.row.AdvertStatus == 1"
          :inactive-text="e.row.AdvertStatus == 0 ? '起草' : '停用'"
          @change="setIsEnabe(e.row)"
        />
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          v-if="e.row.AdvertStatus == 0 || e.row.AdvertStatus == 2"
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="edit(e.row)"
        />
        <el-button
          v-if="e.row.AdvertStatus == 3"
          size="mini"
          type="primary"
          title="结束"
          icon="el-icon-switch-button"
          circle
          @click="end(e.row)"
        />
        <el-button
          v-if="e.row.AdvertStatus == 0"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <editAdvert
      :id="id"
      :place-id="placeId"
      :visible="visible"
      @close="close"
    />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as advertApi from '@/shop/api/advert'
import { AdvertStatus, RelationType } from '@/shop/config/shopConfig'
import editAdvert from './editAdvert.vue'
export default {
  components: {
    editAdvert
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    status: {
      type: String,
      default: null
    },
    placeId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      RelationType,
      AdvertStatus,
      dataList: [],
      title: '',
      id: null,
      visible: false,
      columns: [
        {
          key: 'ImgSrc',
          title: '广告图',
          align: 'center',
          slotName: 'ImgSrc',
          minWidth: 80
        },
        {
          key: 'Title',
          title: '广告标题',
          align: 'left',
          minWidth: 200
        },
        {
          key: 'PutInTime',
          title: '投放时段',
          align: 'center',
          slotName: 'PutInTime',
          minWidth: 200
        },
        {
          key: 'RelationType',
          title: '投放类型',
          align: 'center',
          slotName: 'RelationType',
          width: 120
        },
        {
          key: 'RelationTitle',
          title: '商品名/活动名',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'AdvertStatus',
          title: '状态',
          align: 'center',
          slotName: 'AdvertStatus',
          width: 180
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
        Status: null,
        QueryKey: null,
        PutInDate: null,
        TimeRange: []
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
    edit(row) {
      this.id = row.Id
      this.visible = true
    },
    drop(row) {
      const title = '确认删除广告: ' + row.Title + '?'
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
      await advertApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    end(row) {
      const title = '确认停止投放广告: ' + row.Title + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitEnd(row)
      })
    },
    async submitEnd(row) {
      await advertApi.End(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    async setIsEnabe(row) {
      if (row.AdvertStatus !== AdvertStatus[1].value) {
        await advertApi.Enable(row.Id)
        row.AdvertStatus = 1
      } else {
        await advertApi.Stop(row.Id)
        row.AdvertStatus = 2
      }
    },
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    reset() {
      if (this.status === 'ing') {
        this.title = '投放中的广告'
        this.queryParam.Status = [3]
      } else if (this.status === 'end') {
        this.title = '结束投放的广告'
        this.queryParam.Status = [4]
      } else {
        this.title = '排期中的广告'
        this.queryParam.Status = [0, 1, 2]
      }
      this.queryParam.QueryKey = null
      this.paging.Index = 1
      this.load()
    },
    add() {
      this.id = null
      this.visible = true
    },
    async load() {
      const query = {
        PlaceId: this.placeId,
        QueryKey: this.queryParam.QueryKey,
        Status: this.queryParam.Status,
        IsPutInIng: this.queryParam.IsPutInIng
      }
      if (this.queryParam.TimeRange && this.queryParam.TimeRange.length !== 0) {
        query.Begin = this.queryParam.TimeRange[0]
        query.End = this.queryParam.TimeRange[1]
      }
      const res = await advertApi.Query(query, this.paging)
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
