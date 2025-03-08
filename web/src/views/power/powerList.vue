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
    </el-card>
  </leftRightSplit>
</template>

<script>
import { GetTrees } from '@/api/role/prower'
export default {
  components: {},
  data() {
    return {
      title: '新增角色',
      menus: [],
      columns: [{
        key: 'OperateName',
        title: '权限名',
        align: 'center',
        minWidth: 150
      }, {
        key: 'Show',
        title: '说明',
        align: 'left',
        minWidth: 150
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
    async loadTrees() {
      const list = await GetTrees({
        ProwerType: 1
      })
      this.trees = list.map((c) => {
        const t = {
          key: c.SubSysId,
          type: 'isSubSystem',
          label: c.SubSysName,
          style: {
            icon: 'el-icon-s-home',
            color: '#f56c6c'
          }
        }
        t.children = this.getProwers(c.Prowers)
        return t
      })
    },
    getProwers(list) {
      return list.map((c) => {
        const t = {
          key: c.Id,
          type: c.ProwerType,
          label: c.Name
        }
        if (c.ProwerType === 1) {
          t.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
        }
        if (c.Children && c.Children.length !== 0) {
          t.children = this.getProwers(c.Children)
        }
        return t
      })
    }
  }
}
</script>
