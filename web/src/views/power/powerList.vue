<template>
  <leftRightSplit :left-span="4">
    <el-card slot="left">
      <div slot="header">
        <span>菜单目录</span>
      </div>
      <el-tree
        ref="menus"
        :data="menus"
        :default-expand-all="true"
        :highlight-current="true"
        :expand-on-click-node="false"
        :current-node-key="chioseKey"
        style="width: 100%"
        :check-strictly="false"
        node-key="key"
        @node-click="chioseMenu"
      >
        <span slot-scope="{ node, data }" class="slot-t-node">
          <template>
            <template v-if="data.style">
              <i
                v-if="data.style.icon.indexOf('el-') == 0"
                :class="data.style.icon"
                :style="{ color: data.style.color, marginRight: '5px' }"
              />
              <svg-icon
                v-else
                :icon-class="data.style.icon"
                :style="{ color: data.style.color, marginRight: '5px' }"
              />
            </template>
            <span>{{ node.label }}</span>
          </template>
        </span>
      </el-tree>
    </el-card>
    <el-card slot="right">
      <div slot="header">
        <span>{{ title }}</span>
      </div>
      <el-row>
        <el-form :inline="true" :model="queryParam">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="菜单名"
              @change="loadPower"
            />
          </el-form-item>
          <el-form-item label="启用状态">
            <el-select
              v-model="queryParam.IsEnable"
              placeholder="启用状态"
              @change="loadPower"
            >
              <el-option :value="null">全部</el-option>
              <el-option :value="true">启用</el-option>
              <el-option :value="false">未启用</el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <enumItem
              v-model="queryParam.ProwerType"
              :dic-key="HrEnumDic.hrProwerType"
              placeholder="权限类型"
              :multiple="true"
              @change="loadPower"
            />
          </el-form-item>
          <el-form-item>
            <el-button type="success" @click="addPower">添加员工</el-button>
            <el-button @click="reset">重置</el-button>
          </el-form-item>
        </el-form>
      </el-row>
      <w-table
        :data="dataList"
        :columns="columns"
        :is-paging="false"
        row-key="Id"
      >
        <template slot="Icon" slot-scope="e">
          <i :class="e.row.Icon" />
        </template>
        <template slot="IsEnable" slot-scope="e">
          <el-switch
            v-model="e.row.IsEnable"
            active-text="启用"
            inactive-text="停用"
            @change="setIsEnable(e.row)"
          />
        </template>
        <template slot="Sort" slot-scope="e">
          <el-input-number v-model="e.row.Sort" style="width: 150px;" placeholder="排序位" @change="setPowerSort(e.row)" />
        </template>
        <template slot="ProwerType" slot-scope="e">
          <span v-if="e.row.ProwerType == 0">菜单</span>
          <span v-else-if="e.row.ProwerType == 1">目录</span>
        </template>
      </w-table>
    </el-card>
  </leftRightSplit>
</template>

<script>
import { GetTrees, GetProwerTrees, SetSort } from '@/api/role/prower'
import {
  HrEnumDic
} from '@/config/publicDic'
export default {
  components: {},
  data() {
    return {
      HrEnumDic,
      title: '新增角色',
      menus: [],
      dataList: null,
      queryParam: {
        SubSystemId: null,
        QueryKey: null,
        ParentId: null,
        ProwerType: null,
        IsEnable: null
      },
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      },
      chioseKey: null,
      maxSort: 1,
      columns: [{
        key: 'Icon',
        title: '图标',
        align: 'left',
        slotName: 'Icon',
        width: 100
      },
      {
        key: 'Name',
        title: '权限名',
        align: 'left',
        width: 200
      },
      {
        key: 'ProwerType',
        title: '权限类型',
        align: 'center',
        slotName: 'ProwerType',
        width: 80
      },
      {
        key: 'RouteName',
        title: '页面路由名',
        align: 'left',
        width: 150
      },
      {
        key: 'IsEnable',
        title: '是否启用',
        align: 'center',
        slotName: 'IsEnable',
        width: 200
      },
      {
        key: 'Sort',
        title: '排序位',
        align: 'center',
        slotName: 'Sort',
        width: 200
      },
      {
        key: 'Description',
        title: '说明',
        align: 'left',
        minWidth: 150
      }, {
        key: 'Action',
        title: '操作',
        align: 'left',
        width: 200
      }]
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  mounted() {
    this.loadTrees()
  },
  methods: {
    addPower() {

    },
    async setPowerSort(row) {
      await SetSort(row.Id, row.Sort)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.IsShowAll = true
      this.queryParam.IsEnable = true
      this.queryParam.ProwerType = null
      this.loadPower()
    },
    chioseMenu(e) {
      if (e.type === 'isSubSystem' && e.key !== this.queryParam.SubSystemId) {
        this.queryParam.SubSystemId = e.key
        this.queryParam.ParentId = null
      } else if (e.type === 1 && e.key !== this.queryParam.ParentId) {
        this.queryParam.SubSystemId = e.sysId
        this.queryParam.ParentId = e.key
      } else {
        return
      }
      this.loadPower()
    },
    async loadPower() {
      this.dataList = await GetProwerTrees(this.queryParam)
      this.maxSort = this.dataList.Max('Sort')
    },
    async loadTrees() {
      const list = await GetTrees({
        ProwerType: 1
      })
      const subSys = list[0]
      this.chioseKey = subSys.SubSysId
      this.queryParam.SubSystemId = this.chioseKey
      this.title = subSys.SubSysName + '菜单列表'
      this.menus = list.map((c) => {
        const t = {
          key: c.SubSysId,
          type: 'isSubSystem',
          label: c.SubSysName,
          style: {
            icon: 'el-icon-s-home',
            color: '#f56c6c'
          }
        }
        t.children = this.getProwers(c.Prowers, c.SubSysId)
        return t
      })
      this.loadPower()
    },
    getProwers(list, sysId) {
      return list.map((c) => {
        const t = {
          key: c.Id,
          type: c.ProwerType,
          label: c.Name,
          sysId: sysId
        }
        if (c.ProwerType === 1) {
          t.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
        }
        if (c.Children && c.Children.length !== 0) {
          t.children = this.getProwers(c.Children, sysId)
        }
        return t
      })
    }
  }
}
</script>
