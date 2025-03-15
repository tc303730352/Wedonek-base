<template>
  <div v-if="readonly" class="deptNames">
    <span v-for="i in deptName" :key="i">{{ i }}</span>
  </div>
  <el-cascader
    v-else
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
import { getDeptTree, GetNameList } from '@/api/unit/unit'
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
    readonly: {
      type: Boolean,
      default: false
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
      deptName: [],
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
          this.reset()
        }
      },
      immediate: true
    },
    unitId: {
      handler(val) {
        this.reset()
      },
      immediate: false
    },
    value: {
      handler(val) {
        this.chioseKey = val
        if (this.readonly) {
          this.reset()
        }
      },
      immediate: true
    },
    readonly: {
      handler(val) {
        if (val && this.value != null) {
          this.reset()
        }
      },
      immediate: false
    }
  },
  methods: {
    reset() {
      if (this.readonly) {
        if (this.chioseKey == null) {
          this.deptName = []
          return
        } else if (this.isMultiple && this.chioseKey.length !== 0) {
          this.loadName(this.chioseKey)
        } else if (this.isMultiple === false && this.chioseKey !== '0') {
          this.loadName([this.chioseKey])
        } else {
          this.deptName = []
        }
      } else {
        this.deptName = []
        this.loadTree()
      }
    },
    async loadName(ids) {
      this.deptName = await GetNameList(ids)
    },
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
      if (row.Children.length === 0) {
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
<style scoped>
.deptNames {
  display: inline-block;
  line-height: 20px;
}
.deptNames span{
  padding: 5px;
}
</style>
