<template>
  <el-card>
    <div slot="header">
      <span>组织结构图</span>
    </div>
    <div class="structure">
      <zm-tree-org
        ref="tree"
        :draggable="true"
        :node-draggable="false"
        :data="depts"
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
            <p class="row">
              负责人: {{ node.LeaderName }}
            </p>
          </el-card>
        </template>
      </zm-tree-org>
    </div>
  </el-card>
</template>
<script>
import { getTallyTrees } from '@/api/unit/dept'
import { HrDeptStatus } from '@/config/publicDic'
export default {
  data() {
    return {
      depts: {}
    }
  },
  computed: {
    comName() {
      const comId = this.$store.getters.curComId
      return this.$store.getters.company[comId]
    },
    comId() {
      return this.$store.getters.curComId
    }
  },
  mounted() {
    this.init()
  },
  methods: {
    async init() {
      const list = await getTallyTrees({
        CompanyId: this.comId,
        Status: [HrDeptStatus[1].value]
      })
      this.depts = {
        id: this.comId,
        label: this.comName,
        type: 'company',
        children: list.map((c) => {
          return {
            id: c.Id,
            label: c.Name,
            type: c.IsUnit ? 'unit' : 'dept',
            EmpNum: c.EmpNum,
            EmpTotal: c.EmpTotal,
            LeaderName: c.LeaderName,
            LeaderId: c.LeaderId,
            DeptShow: c.DeptShow,
            children: this.getChilldren(c)
          }
        })
      }
    },
    getChilldren(c) {
      if (c.Children == null || c.Children.length === 0) {
        return null
      }
      return c.Children.map((c) => {
        return {
          id: c.Id,
          label: c.Name,
          type: c.IsUnit ? 'unit' : 'dept',
          EmpNum: c.EmpNum,
          EmpTotal: c.EmpTotal,
          LeaderName: c.LeaderName,
          LeaderId: c.LeaderId,
          DeptShow: c.DeptShow,
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
.structure .zm-tree-handle {
  bottom: inherit !important;
  top: 0px;
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
  height: 20px;
  line-height: 20px;
  margin: 0;
  font-size: 14px;
}
</style>
