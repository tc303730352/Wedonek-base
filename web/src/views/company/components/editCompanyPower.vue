<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-card>
      <span slot="header">权限目录</span>
      <div style="width: 100%; height: 500px; overflow-y: auto">
        <el-tree
          ref="powerTree"
          :data="trees"
          :default-expand-all="true"
          :highlight-current="true"
          :expand-on-click-node="false"
          style="width: 100%"
          :show-checkbox="true"
          :default-checked-keys="chioseKeys"
          :check-strictly="false"
          node-key="key"
          @check-change="checkChange"
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
      </div>
    </el-card>
    <div slot="footer">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { GetTrees } from '@/api/role/power'
import * as comPowerApi from '@/api/base/companyPower'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    companyId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑公司菜单目录',
      trees: [],
      chioseKeys: [],
      source: []
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.loadTrees()
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {
  },
  methods: {
    async loadTrees() {
      const list = await GetTrees({
        IsEnable: true
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
        t.children = this.getPowers(c.Powers)
        return t
      })
    },
    checkChange(data, checked) {
      if (data.type === 'isSubSystem') {
        return
      }
      const index = this.powerId.findIndex((c) => c === data.key)
      if (checked) {
        if (index !== -1) {
          return
        }
        this.powerId.push(data.key)
      } else if (index !== -1) {
        this.powerId.splice(index, 1)
      }
    },
    getPowers(list) {
      return list.map((c) => {
        const t = {
          key: c.Id,
          type: c.PowerType,
          label: c.Name
        }
        if (c.PowerType === 1) {
          t.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
        } else if (c.PowerType === 0) {
          t.style = {
            icon: 'el-icon-document',
            color: '#000'
          }
        }
        if (c.Children && c.Children.length !== 0) {
          t.children = this.getPowers(c.Children)
        }
        return t
      })
    },
    async save() {
      await comPowerApi.Sync(this.companyId, this.chioseKeys)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close')
    },
    async reset() {
      const list = await comPowerApi.Gets(this.companyId)
      if (list != null) {
        this.source = list
        this.chioseKeys = list.concat()
      } else {
        this.chioseKeys = []
        this.source = []
      }
    },
    resetForm() {
      this.chioseKeys = this.source.concat()
    },
    closeForm() {
      this.$emit('close')
    }
  }
}
</script>
