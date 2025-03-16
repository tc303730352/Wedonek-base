<template>
  <el-tree
    ref="deptTree"
    :data="units"
    :default-expand-all="true"
    :highlight-current="true"
    :props="props"
    :expand-on-click-node="false"
    :style="styleSet"
    :show-checkbox="isMultiple"
    :current-node-key="chioseKey"
    :default-checked-keys="checkboxKey"
    :check-strictly="checkstrictly"
    node-key="DeptId"
    @node-click="chioseUnit"
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
</template>

<script>
import { getDeptTree } from '@/api/unit/unit'
import { GetName } from '@/api/base/company'
export default {
  name: 'Layout',
  props: {
    isUnit: {
      type: Boolean,
      default: false
    },
    checkstrictly: {
      type: Boolean,
      default: false
    },
    unitId: {
      type: String,
      default: null
    },
    status: {
      type: Array,
      default: () => [1]
    },
    companyId: {
      type: String,
      default: () => {
        return this.$store.getters.curComId
      }
    },
    selectKeys: {
      type: Array,
      default: () => []
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isAutoChiose: {
      type: Boolean,
      default: false
    },
    styleSet: {
      type: Object,
      default: () => {
        return {
          width: '100%'
        }
      }
    }
  },
  data() {
    return {
      units: [],
      chioseKey: null,
      checkboxKey: [],
      deptDic: {},
      name: null,
      props: {
        label: 'DeptName',
        children: 'Children'
      }
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
  watch: {
    companyId: {
      handler(val) {
        if (val !== this.comId || this.name == null) {
          this.name = null
          this.loadTree()
        }
      },
      immediate: true
    },
    unitId: {
      handler(val) {
        if (val != null) {
          this.loadTree()
        }
      },
      immediate: false
    },
    selectKeys: {
      handler(val) {
        this.checkboxKey = val
      },
      immediate: true
    }
  },
  methods: {
    format(data) {
      if (data.IsUnit) {
        data.style = {
          icon: 'tree-table',
          color: '#409eff'
        }
      } else {
        data.style = {
          icon: 'peoples',
          color: '#000'
        }
      }
      if (data.Children && data.Children.length > 0) {
        data.Children.forEach((c) => {
          this.deptDic[c.DeptId] = c
          this.format(c)
        })
      }
    },
    async GetComName() {
      if (this.name !== null) {
        return this.name
      } else if (this.companyId === this.comId) {
        this.name = this.comName
      } else {
        const name = this.$store.getters.company[this.companyId]
        if (name) {
          this.name = name
        } else {
          this.name = await GetName(this.companyId)
        }
      }
      return this.name
    },
    async load() {
      const res = await getDeptTree({
        CompanyId: this.companyId,
        Status: this.status,
        ParentId: this.unitId,
        IsUnit: this.isUnit
      })
      if (res.length > 0) {
        res.forEach((c) => {
          this.deptDic[c.DeptId] = c
          this.format(c)
        })
      }
      const comName = await this.GetComName()
      this.units = [
        {
          DeptId: 'root',
          DeptName: comName,
          disabled: this.isMultiple,
          style: {
            icon: 'el-icon-s-flag',
            color: '#409eff'
          },
          Children: res
        }
      ]
    },
    async loadTree() {
      const res = await this.load()
      const e = {
        isMultiple: this.isMultiple,
        companyId: this.companyId,
        comName: this.comName,
        value: []
      }
      if (this.isAutoChiose && res.length > 0) {
        this.chioseKey = res[0].DeptId
        e.value.push(res[0])
      } else {
        this.chioseKey = 'root'
      }
      this.$emit('load', e)
    },
    autoChiose(data) {
      if (data.Children && data.Children.length != 0) {
        let isSet = false
        data.Children.forEach((c) => {
          if (c.IsUnit) {
            return
          }
          if (!this.checkboxKey.includes(c.DeptId)) {
            this.checkboxKey.push(c.DeptId)
            isSet = true
            this.autoChiose(c)
          }
        })
        return isSet
      }
      return false
    },
    checkChange(data, checked, indeterminate) {
      const index = this.checkboxKey.findIndex((c) => c == data.DeptId)
      if (checked) {
        if (index !== -1) {
          return
        }
        this.checkboxKey.push(data.DeptId)
        if (this.autoChiose(data)) {
          this.$refs.deptTree.setCheckedKeys(this.checkboxKey, false)
        }
      } else if (index === -1) {
        return
      } else {
        this.checkboxKey.splice(index, 1)
      }
      const e = {
        isMultiple: this.isMultiple,
        companyId: this.companyId,
        comName: this.name,
        value: this.checkboxKey.map((c) => this.deptDic[c])
      }
      this.$emit('change', e)
    },
    chioseUnit(dept) {
      this.chioseKey = dept.DeptId
      const e = {
        isMultiple: this.isMultiple,
        companyId: this.companyId,
        comName: this.name,
        value: []
      }
      if (dept != null && dept.DeptId !== 'root') {
        e.value.push(dept)
      }
      this.$emit('change', e)
    }
  }
}
</script>
