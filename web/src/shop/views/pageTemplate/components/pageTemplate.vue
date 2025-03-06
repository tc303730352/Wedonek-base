<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
      <el-button
        style="float: right"
        type="success"
        @click="add()"
      >添加页面</el-button>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="模版名"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="使用范围">
          <enumItem
            v-model="queryParam.UseRange"
            :dic-key="EnumDic.pageUseRange"
            placeholder="使用范围"
            :multiple="false"
            sys-head="shop"
            :clearable="false"
            @change="search"
          />
        </el-form-item>
        <el-form-item label="状态">
          <enumItem
            v-model="queryParam.Status"
            :dic-key="EnumDic.pageStatus"
            placeholder="状态"
            :multiple="false"
            sys-head="shop"
            :is-clear="false"
            @change="search"
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
      <template slot="Status" slot-scope="e">
        <span :style="{ color: PageStatus[e.row.Status].color }">{{
          PageStatus[e.row.Status].text
        }}</span>
      </template>
      <template slot="EditTime" slot-scope="e">
        {{ moment(e.row.EditTime).format("YYYY-MM-DD HH:mm") }}
      </template>
      <template slot="UseRange" slot-scope="e">
        {{ PageUseRange[e.row.UseRange].text }}
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="页面装修"
          icon="el-icon-view"
          circle
          @click="show(e.row)"
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
      </template>
    </w-table>
    <editTemplatePage
      :id="id"
      :place="place"
      :range="queryParam.UseRange"
      :visible="visible"
      @close="close"
    />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as pageApi from '@/shop/api/page'
import { EnumDic, PageStatus, PageUseRange } from '@/shop/config/shopConfig'
import editTemplatePage from './editTemplatePage.vue'
export default {
  components: {
    editTemplatePage
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    place: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      PageStatus,
      PageUseRange,
      visible: false,
      id: null,
      dataList: [],
      title: '',
      columns: [
        {
          key: 'TemplateName',
          title: '页面名',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'UseRange',
          title: '页面使用范围',
          align: 'center',
          slotName: 'UseRange',
          minWidth: 100
        },
        {
          key: 'Show',
          title: '页面说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'EditTime',
          title: '最后更新时间',
          align: 'center',
          slotName: 'EditTime',
          width: 180
        },
        {
          key: 'Status',
          title: '状态',
          align: 'center',
          slotName: 'Status',
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
        UseRange: 1,
        Place: null
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
    search() {
      this.paging.Index = 1
      this.load()
    },
    show(row) {
      this.$router.push({ name: 'editPage', params: { id: row.Id, range: this.queryParam.UseRange }})
    },
    edit(row) {
      this.id = row.Id
      this.visible = true
    },
    drop(row) {
      const title = '确认删除该页面: ' + row.TemplateName + '?'
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
      await pageApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    reset() {
      this.queryParam.Place = this.place
      if (this.place === 0) {
        this.title = '首页'
      } else {
        this.title = '二级页面'
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
      const res = await pageApi.Query(this.queryParam, this.paging)
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
