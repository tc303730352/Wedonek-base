<template>
  <div>
    <el-card>
      <div slot="header">
        <span>广告位管理</span>
        <el-button
          style="float: right"
          type="success"
          @click="add()"
        >添加广告位</el-button>
      </div>
      <el-form :inline="true" :model="queryParam">
        <el-row>
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="广告位名"
              @change="search"
            />
          </el-form-item>
          <el-form-item label="使用范围">
            <enumItem
              v-model="queryParam.UseRange"
              :dic-key="EnumDic.pageUseRange"
              placeholder="使用范围"
              :multiple="false"
              sys-head="shop"
              @change="search"
            />
          </el-form-item>
          <el-form-item>
            <el-button @click="reset">重置</el-button>
          </el-form-item>
        </el-row>
        <el-form-item label="分类">
          <dictItem
            v-model="queryParam.PlaceType"
            :dic-id="DictItemDic.placeType"
            placeholder="分类"
            mode="radio-button"
            @change="search"
          />
        </el-form-item>
      </el-form>
      <w-table
        :data="dataList"
        :columns="columns"
        :paging="paging"
        :is-paging="true"
        row-key="Id"
      >
        <template slot="ExampleImg" slot-scope="e">
          <img v-if="e.row.ExampleImg" style="max-width: 200px;max-height: 150px;" :src="e.row.ExampleImg">
        </template>
        <template slot="UseRange" slot-scope="e">
          {{ PageUseRange[e.row.UseRange].text }}
        </template>
        <template slot="PlaceStatus" slot-scope="e">
          <el-switch
            active-text="已启用"
            :active-color="PlaceStatus[1].color"
            :inactive-color="PlaceStatus[e.row.PlaceStatus].color"
            :value="e.row.PlaceStatus == 1"
            :inactive-text="e.row.PlaceStatus == 0 ? '起草' : '停用'"
            @change="setIsEnabe(e.row)"
          />
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            v-if="e.row.PlaceStatus != 1"
            size="mini"
            type="primary"
            title="编辑"
            icon="el-icon-edit"
            circle
            @click="edit(e.row.Id)"
          />
          <el-button
            v-if="e.row.PlaceStatus == 0"
            size="mini"
            type="danger"
            title="删除"
            icon="el-icon-delete"
            circle
            @click="drop(e.row)"
          />
        </template>
      </w-table>
    </el-card>
    <editAdvertPlace :id="id" :visible="visible" @close="closeEdit" />
  </div>
</template>

<script>
import * as placeApi from '../../api/advertPlace'
import { EnumDic, PlaceStatus, PageUseRange, DictItemDic } from '../../config/shopConfig'
import editAdvertPlace from './components/editAdvertPlace.vue'
export default {
  components: {
    editAdvertPlace
  },
  data() {
    return {
      EnumDic,
      PlaceStatus,
      PageUseRange,
      DictItemDic,
      dataList: [],
      id: null,
      visible: false,
      queryParam: {
        QueryKey: null,
        PlaceType: null,
        UseRange: null,
        PlaceStatus: null
      },
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      },
      columns: [
        {
          key: 'ExampleImg',
          title: '示例图',
          slotName: 'ExampleImg',
          align: 'center',
          width: 200
        },
        {
          key: 'PlaceTitle',
          title: '广告位标题',
          align: 'left',
          minWidth: 200
        },
        {
          key: 'PlaceTypeName',
          title: '分类',
          align: 'center',
          minWidth: 100
        },
        {
          key: 'UseRange',
          title: '使用范围',
          align: 'center',
          slotName: 'UseRange',
          minWidth: 100
        },
        {
          key: 'AdvertNum',
          title: '广告数',
          align: 'center',
          width: 120
        },
        {
          key: 'ImgShow',
          title: '图片说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'PlaceStatus',
          title: '状态',
          align: 'center',
          slotName: 'PlaceStatus',
          width: 180
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          minWidth: 120,
          slotName: 'action'
        }
      ]
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    drop(row) {
      const title = '确认删除该广告位 ' + row.PlaceTitle + '?'
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
      await placeApi.Delete(row.Id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      const index = this.dataList.findIndex((c) => c.Id === row.Id)
      this.dataList.splice(index, 1)
    },
    async setIsEnabe(row) {
      if (row.PlaceStatus !== PlaceStatus[1].value) {
        await placeApi.Enable(row.Id)
        // eslint-disable-next-line require-atomic-updates
        row.PlaceStatus = 1
      } else {
        await placeApi.Stop(row.Id)
        // eslint-disable-next-line require-atomic-updates
        row.PlaceStatus = 2
      }
    },
    async load() {
      const res = await placeApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.dataList = res.List
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
    },
    add() {
      this.id = null
      this.visible = true
    },
    reset() {
      this.paging.Index = 1
      this.queryParam.UseRange = null
      this.queryParam.PlaceType = null
      this.load()
    },
    search() {
      this.paging.Index = 1
      if (this.queryParam.UseRange === '') {
        this.queryParam.UseRange = null
      }
      this.load()
    },
    closeEdit(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    edit(id) {
      this.id = id
      this.visible = true
    }
  }
}
</script>
