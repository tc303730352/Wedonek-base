<template>
  <el-card>
    <div slot="header">
      <span>组织结构图</span>
    </div>
    <div class="structure" :style="{ height: height }">
      <zm-tree-org
        ref="tree"
        :draggable="true"
        :define-menus="initMenus"
        :node-draggable="false"
        :collapsable="true"
        :default-expand-level="5"
        :data="depts"
        :node-add="add"
        :node-edit="edit"
        @on-contextmenu="clickMenu"
      >
        <template slot-scope="{ node }">
          <div v-if="node.type == 'company'" :title="node.DeptShow" class="comName">
            {{ node.label }}
          </div>
          <div v-else-if="node.type == 'unit'" :title="node.DeptShow" class="unit">
            <div class="name">
              <svg-icon icon-class="tree" />
              {{ node.label }}
            </div>
            <div class="empNum">
              <svg-icon icon-class="peoples" />
              <span>{{ node.EmpTotal }}</span>
            </div>
          </div>
          <el-card v-else class="dept">
            <div slot="header" class="header">
              <svg-icon icon-class="peoples" />
              <span>{{ node.label }}</span>
            </div>
            <el-row class="row">
              <el-col
                :span="12"
                title="部门人数"
              >
                <svg-icon icon-class="user" />
                <span>{{ node.EmpNum }}</span>
              </el-col>
              <el-col
                v-if="node.children != null"
                title="总人数"
                :span="12"
              >
                <svg-icon icon-class="peoples" />
                <span>{{ node.EmpTotal }}</span>
              </el-col>
            </el-row>
            <p v-if="node.LeaderName">
              负责人:<el-button type="text">{{ node.LeaderName }}</el-button>
            </p>
            <p v-if="node.DeptTag">
              <el-tag v-for="item in node.DeptTag" :key="item">{{ deptTag[item] }}</el-tag>
            </p>
          </el-card>
        </template>
      </zm-tree-org>
    </div>
    <editDept
      :dept-id="id"
      :is-unit="isUnit"
      :unit-id="unitId"
      :parent-id="parentId"
      :visible="visible"
      @close="closeDept"
    />
    <deptView
      :id="id"
      :visible="viewVisible"
      @close="closeView"
    />
  </el-card>
</template>
<script>
import { getTallyTrees } from '@/api/unit/dept'
import { HrItemDic } from '@/config/publicDic'
import { GetItemName } from '@/api/base/dictItem'
import editDept from './components/editDept.vue'
import deptView from './components/deptView.vue'
export default {
  components: {
    editDept,
    deptView
  },
  data() {
    return {
      depts: {},
      deptTag: {},
      height: '800px',
      id: null,
      isUnit: false,
      unitId: null,
      visible: false,
      viewVisible: false,
      status: null,
      parentId: null
    }
  },
  computed: {
    comName() {
      const comId = this.$store.getters.curComId
      return this.$store.getters.company[comId]
    },
    comId() {
      return this.$store.getters.curComId
    },
    mainHeight() {
      return this.$store.getters.mainHeight
    }
  },
  watch: {
    mainHeight: {
      handler(val) {
        if (val != null) {
          this.height = (val - 140) + 'px'
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.initDic()
  },
  methods: {
    async initDic() {
      this.deptTag = await GetItemName(HrItemDic.deptTag)
      this.init()
    },
    initMenus(e, node) {
      if (node.type === 'company') {
        return [{ name: '查看公司信息', command: 'view' }]
      } else if (node.type === 'unit') {
        return [{ name: '查看单位信息', command: 'view' }, { name: '新增单位', command: 'add' }, { name: '新增部门', command: 'addDept' }, { name: '编辑单位信息', command: 'edit' }]
      }
      return [{ name: '查看部门信息', command: 'view' }, { name: '新增部门信息', command: 'add' }, { name: '编辑部门信息', command: 'edit' }]
    },
    closeDept(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.init()
      }
    },
    closeView() {
      this.viewVisible = false
    },
    edit(node) {
      if (node.type === 'unit') {
        this.id = node.id
        this.parentId = null
        this.isUnit = true
        this.unitId = node.unitId
        this.visible = true
      } else if (node.type === 'dept') {
        this.id = node.id
        this.parentId = null
        this.isUnit = false
        this.unitId = node.unitId
        this.visible = true
      }
    },
    add(node) {
      if (node.type === 'unit') {
        this.id = null
        this.parentId = node.id
        this.isUnit = true
        this.unitId = node.id
        this.visible = true
      } else if (node.type === 'dept') {
        this.id = null
        this.parentId = node.id
        this.isUnit = false
        this.unitId = node.unitId
        this.visible = true
      }
    },
    clickMenu(e) {
      const node = e.node
      if (e.command === 'addDept') {
        this.id = null
        this.parentId = node.id
        this.isUnit = false
        this.unitId = node.unitId
        this.visible = true
      } else if (e.command === 'view') {
        this.id = node.id
        this.viewVisible = true
      }
    },
    async init() {
      const list = await getTallyTrees({
        CompanyId: this.comId,
        Status: this.status
      })
      this.depts = {
        id: this.comId,
        label: this.comName,
        type: 'company',
        children: list.map((c) => {
          return {
            id: c.Id,
            pid: null,
            label: c.Name,
            type: c.IsUnit ? 'unit' : 'dept',
            isEnable: c.Status === 1,
            unitId: c.UnitId,
            expand: true,
            EmpNum: c.EmpNum,
            EmpTotal: c.EmpTotal,
            LeaderName: c.LeaderName,
            LeaderId: c.LeaderId,
            DeptShow: c.DeptShow,
            DeptTag: c.DeptTag,
            children: this.getChilldren(c)
          }
        })
      }
    },
    getChilldren(a) {
      if (a.Children == null || a.Children.length === 0) {
        return null
      }
      return a.Children.map((c) => {
        return {
          id: c.Id,
          pid: a.Id,
          label: c.Name,
          expand: true,
          type: c.IsUnit ? 'unit' : 'dept',
          unitId: c.UnitId,
          EmpNum: c.EmpNum,
          EmpTotal: c.EmpTotal,
          LeaderName: c.LeaderName,
          LeaderId: c.LeaderId,
          DeptShow: c.DeptShow,
          DeptTag: c.DeptTag,
          children: this.getChilldren(c)
        }
      })
    }
  }
}
</script>

<style>
.structure {
  text-align: center;
  width: 100%;
  min-height: 800px;
  display: inline-grid;
}
.zm-tree-handle {
  bottom: inherit !important;
  top: 0px;
}
.zm-tree-contextmenu {
  padding: 10px;
}
.zm-tree-menu-item {
  line-height: 30px;
  height:  30px;
  font-size: 14px;
}
.structure .zm-tree-org {
  background-color: #b1b1b1;
}
.structure .zm-tree-org .zoom-container {
  overflow: auto;
}
.structure .el-card {
  width: 250px;
}
.structure .comName {
  color: #fff;
  background-color: #43af2b;
  min-width: 250px;
  height: 50px;
  line-height: 35px;
  padding: 10px;
  font-size: 30px;
}
.structure .unit {
    color: #fff;
    background-color: #1890ff;
    min-width: 250px;
    height: 50px;
    line-height: 32px;
    font-size: 24px;
    display: inline-table;
    padding: 10px;
}
.structure .unit .name{
    display: table-cell;
}
.structure .unit .empNum{
    display: table-cell;
    padding-left: 5px;
    font-size: 18px;
}
.structure .unit .empNum span{
    padding-left: 2px;
}
.structure .dept .header {
  text-align: left;
}
.structure .dept .el-card__body {
  padding: 5px;
  text-align: left;
}
.structure .dept .el-card__header {
  background-color: #999;
  color: #fff;
}
.structure .dept .header span {
  padding-left: 10px;
}
.structure .dept .row {
  height: 20px;
  line-height: 20px;
}
.structure .dept .row span {
  padding-left: 5px;
}
.structure .dept p {
  margin: 2px;
  font-size: 14px;
}
.structure .dept p .el-tag{
 margin-right: 3px;
}
</style>
