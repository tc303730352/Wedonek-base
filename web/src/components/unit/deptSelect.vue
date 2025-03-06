<template>
  <el-cascader
    v-model="chioseKey"
    :options="depts"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    @change="handleChange"
  />
</template>

<script>
import { getDeptTree } from '@/api/unit/unit'
export default {
  name: 'Layout',
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: '选择部门'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    isUnit: {
      type: Boolean,
      default: null
    },
    status: {
      type: Array,
      default: () => [1]
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isChioseUnit: {
      type: Boolean,
      default: true
    },
    unitId: {
      type: String,
      default: null
    },
    isDept: {
      type: Boolean,
      default: null
    },
    value: {
      type: String | Array,
      default: null
    }
  },
  data() {
    return {
      depts: [],
      chioseKey: null,
      dept: {},
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: 'DeptId',
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
    comId: {
      handler(val) {
        if (val != null) {
          this.loadTree()
        }
      },
      immediate: true
    },
    unitId: {
      handler(val) {
        this.loadTree()
      },
      immediate: false
    },
    value: {
      handler(val) {
        this.chioseKey = val
      },
      immediate: true
    }
  },
  methods: {
    async loadTree() {
      const res = await getDeptTree({
        CompanyId: this.comId,
        Status: this.status,
        UnitId: this.unitId,
        IsUnit: this.isUnit,
        IsDept: this.isDept
      })
      this.props.multiple = this.isMultiple
      this.dept = {}
      res.forEach((c) => {
        this.dept[c.DeptId] = c
        this.format(c)
      })
      this.depts = res
    },
    format(row) {
      if (row.IsUnit && !this.isChioseUnit) {
        row.disabled = true
      }
      if (row.Children.length == 0) {
        row.Children = null
      } else {
        row.Children.forEach((c) => {
          this.dept[c.DeptId] = c
          this.format(c)
        })
      }
    },
    handleChange(val) {
      const e = {
        isMultiple: this.isMultiple,
        companyId: this.comId,
        comName: this.comName,
        value: null,
        dept: null
      }
      if (this.isMultiple) {
        e.value = val
        e.dept = val.map(c => this.dept[c])
      } else if (val) {
        e.value = [val]
        e.dept = [this.dept[val]]
      } else {
        e.value = []
        e.dept = []
      }
      this.$emit('input', val)
      this.$emit('change', e)
    }
  }
}
</script>
