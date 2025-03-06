<template>
  <el-cascader
    v-model="chioseKey"
    :options="dataList"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    @change="handleChange"
  />
</template>

<script>
import { GetTree } from '../../api/category'
export default {
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: '选择类目'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    parentId: {
      type: String,
      default: null
    },
    isEnable: {
      type: Boolean,
      default: true
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isChioseEnd: {
      type: Boolean,
      default: false
    },
    lvl: {
      type: Number,
      default: null
    },
    value: {
      type: String | Array,
      default: null
    }
  },
  data() {
    return {
      dataList: [],
      chioseKey: null,
      category: {},
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: 'Id',
        label: 'CategoryName',
        children: 'Children'
      }
    }
  },
  computed: {},
  watch: {
    value: {
      handler(val) {
        if (this.chioseKey !== val) {
          this.chioseKey = val
          this.initKey()
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.loadTree()
  },
  methods: {
    initKey() {
      if (this.dataList.length === 0) {
        return
      }
      if (this.chioseKey != null) {
        if (Array.isArray(this.chioseKey)) {
          this.handleChange(this.chioseKey)
        } else {
          this.handleChange([this.chioseKey])
        }
      } else {
        this.handleChange([])
      }
    },
    async loadTree() {
      const res = await GetTree({
        IsEnable: this.isEnable,
        ParentId: this.parentId
      })
      this.props.multiple = this.isMultiple
      this.props.checkStrictly = this.isChioseEnd === false
      this.category = {}
      res.forEach((c) => {
        this.category[c.Id] = {
          Id: c.Id,
          Lvl: 1,
          CategoryName: c.CategoryName
        }
        this.format(c, 2)
      })
      this.dataList = res
      if (this.chioseKey != null) {
        if (Array.isArray(this.chioseKey)) {
          this.handleChange(this.chioseKey)
        } else {
          this.handleChange([this.chioseKey])
        }
      }
    },
    format(row, lvl) {
      if (row.Children.length === 0) {
        row.Children = null
      } else {
        const next = lvl + 1
        row.Children.forEach((c) => {
          this.category[c.Id] = {
            Id: c.Id,
            Lvl: lvl,
            CategoryName: c.CategoryName
          }
          this.format(c, next)
        })
      }
    },
    handleChange(val) {
      const e = {
        isMultiple: this.isMultiple,
        value: null,
        category: null
      }
      let value = null
      let lvl = null
      if (val == null || val.length === 0) {
        e.value = []
        e.category = []
      } else {
        if (this.isMultiple) {
          e.value = val
          e.category = val.map((c) => this.category[c])
          value = val
        } else if (Array.isArray(val)) {
          e.value = val
          e.category = val.map((c) => this.category[c])
          value = val[0]
          lvl = e.category[0].Lvl
        } else if (val) {
          e.value = [val]
          e.category = [this.category[val]]
          value = val
          lvl = e.category[0].Lvl
        } else {
          e.value = []
          e.category = []
        }
      }
      this.$emit('input', value)
      this.$emit('update:lvl', lvl)
      this.$emit('change', e)
    }
  }
}
</script>
