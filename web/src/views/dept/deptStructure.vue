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
        <template slot-scope="{node}">
          <el-card class="dept">
            <div slot="header" class="header">
              <svg-icon icon-class="tree" />
              <span>{{ node.label }}</span>
            </div>
            <p>员工数：10人</p>
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
        children: list.map(c => {
          return {
            id: c.Id,
            label: c.Name,
            type: c.IsUnit ? 'unit' : 'dept',
            EmpNum: c.EmpNum,
            children: this.getChilldren(c)
          }
        })
      }
    },
    getChilldren(c) {
      if (c.Children == null) {
        return null
      }
      return c.Children.map(c => {
        return {
          id: c.Id,
          label: c.Name,
          type: c.IsUnit ? 'unit' : 'dept',
          EmpNum: c.EmpNum,
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
.structure .zm-tree-org .zoom-container {
    overflow: auto;
}
.structure .el-card{
   width: 250px;
}

.structure .dept .header{
    text-align: left;
}
.structure .dept .header span{
    padding-left: 10px;
}
.structure .dept .el-el-card__header{
    text-align: left;
}
</style>
