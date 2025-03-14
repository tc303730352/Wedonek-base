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
export default {
  data() {
    return {
      depts: {
        id: 1,
        label: 'Wedonek总公司',
        type: 'company',
        children: [
          {
            id: 2,
            label: '四川省分公司',
            type: 'unit',
            children: [
              {
                id: 6,
                label: '成都分公司',
                type: 'unit'
              },
              {
                id: 7,
                label: '眉山市分公司'
              }
            ]
          },
          {
            id: 3,
            label: '北京市分公司',
            children: [
              {
                id: 11,
                pid: 3,
                label: '客服一部'
              },
              {
                id: 12,
                pid: 3,
                label: '客服二部'
              }
            ]
          },
          {
            id: 4,
            pid: 1,
            label: '业务部'
          },
          {
            id: 5,
            pid: 1,
            label: '人力资源中心'
          }
        ]
      }
    }
  },
  created() {
  },
  methods: {
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
