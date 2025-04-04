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
    node-key="Id"
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
import { getTrees, GetNames } from '@/api/base/treeDicItem'
export default {
  props: {
    dicId: {
      type: String,
      default: null
    },
    checkstrictly: {
      type: Boolean,
      default: false
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
      comNameCache: {},
      props: {
        key: 'Id',
        label: 'Name',
        children: 'Children'
      }
    }
  },
  computed: {
  },
  watch: {
    dicId: {
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
    async load() {
      const res = await getTrees({
        CompanyId: this.companyId,
        Status: this.status,
        ParentId: this.unitId,
        IsUnit: this.isUnit,
        IsSubCompany: this.isSubCompany
      })
      if (res == null) {
        return await this.loadDef()
      }
      this.initTree(res)
      return this.units
    },
    initTree(res) {
      this.comNameCache[res.Id] = res.Name
      const data = {
        Id: 'root',
        Name: res.Name,
        type: 'com',
        disabled: this.isMultiple,
        CompanyId: res.Id,
        style: {
          icon: 'el-icon-s-flag',
          color: '#409eff'
        },
        Children: null
      }
      this.initChildren(data, res)
      this.units = [data]
    },
    format(data) {
      if (data.IsUnit) {
        data.type = 'unit'
        data.style = {
          icon: 'tree-table',
          color: '#409eff'
        }
      } else {
        data.type = 'dept'
        data.style = {
          icon: 'peoples',
          color: '#000'
        }
      }
      if (data.Children && data.Children.length > 0) {
        data.Children.forEach((c) => {
          c.CompanyId = data.CompanyId
          this.deptDic[c.Id] = c
          this.format(c)
        })
      }
    },
    initChildren(data, res) {
      if (this.isNull(res.Children) && this.isNull(res.Dept)) {
        return
      }
      const list = []
      if (!this.isNull(res.Dept)) {
        res.Dept.forEach((c) => {
          c.CompanyId = res.Id
          this.deptDic[c.Id] = c
          this.format(c)
          list.push(c)
        })
      }
      if (!this.isNull(res.Children)) {
        res.Children.forEach(c => {
          this.comNameCache[c.Id] = c.Name
          const data = {
            Id: 'com_' + c.Id,
            Name: c.Name,
            disabled: this.isMultiple,
            type: 'com',
            CompanyId: c.Id,
            style: {
              icon: 'el-icon-s-flag',
              color: '#409eff'
            },
            Children: null
          }
          this.initChildren(data, c)
          list.push(data)
        })
      }
      data.Children = list
    },
    isNull(array) {
      return array == null || array.length === 0
    },
    findDept(list) {
      for (let i = 0; i < list.length; i++) {
        const t = list[i]
        if (t.type !== 'com') {
          return t
        } else if (t.Children != null) {
          const res = this.findDept(t.Children)
          if (res != null) {
            return res
          }
        }
      }
      return null
    },
    async loadTree() {
      const res = await this.load()
      const e = {
        isMultiple: this.isMultiple,
        companyId: this.companyId,
        comName: this.comNameCache,
        value: []
      }
      if (this.isAutoChiose && res.length > 0) {
        const chiose = this.findDept(res)
        this.chioseKey = chiose.Id
        e.value.push(chiose)
      } else {
        this.chioseKey = 'root'
      }
      this.$emit('load', e)
    },
    autoChiose(data) {
      if (data.Children && data.Children.length !== 0) {
        let isSet = false
        data.Children.forEach((c) => {
          if (c.type === 'unit') {
            return
          }
          if (!this.checkboxKey.includes(c.Id)) {
            this.checkboxKey.push(c.Id)
            isSet = true
            this.autoChiose(c)
          }
        })
        return isSet
      }
      return false
    },
    checkChange(data, checked, indeterminate) {
      const index = this.checkboxKey.findIndex((c) => c === data.Id)
      if (checked) {
        if (index !== -1) {
          return
        }
        this.checkboxKey.push(data.Id)
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
        comName: this.comNameCache,
        value: this.checkboxKey.map((c) => this.deptDic[c])
      }
      this.$emit('change', e)
    },
    chioseUnit(dept) {
      this.chioseKey = dept.Id
      const e = {
        isMultiple: this.isMultiple,
        companyId: dept.CompanyId,
        comName: this.comNameCache,
        value: []
      }
      if (dept.type !== 'com') {
        e.value.push(this.deptDic[dept.Id])
      }
      this.$emit('change', e)
    }
  }
}
</script>
