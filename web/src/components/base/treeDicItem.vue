<template>
  <el-cascader
    v-model="chioseKey"
    :options="treeItems"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    @change="handleChange"
  />
</template>

<script>
import { getTrees } from '@/api/base/treeDicItem'
export default {
  name: 'TreeDicItem',
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: '选择字典'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isChoiceEnd: {
      type: Boolean,
      default: true
    },
    dicId: {
      type: String,
      default: null
    },
    value: {
      type: String | Array,
      default: null
    }
  },
  data() {
    return {
      treeItems: [],
      chioseKey: null,
      item: {},
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: 'DicValue',
        label: 'DicText',
        children: 'Children'
      }
    }
  },
  watch: {
    dicId: {
      handler(val) {
        if (val != null) {
          this.loadTree()
        }
      },
      immediate: true
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
      const res = await getTrees(this.dicId)
      this.props.checkStrictly = this.isChoiceEnd == false
      this.props.multiple = this.isMultiple
      this.item = {}
      res.forEach((c) => {
        this.item[c.DicValue] = c
        this.format(c)
      })
      this.treeItems = res
    },
    format(row) {
      if (row.Children) {
        if (row.Children.length == 0) {
          row.Children = null
        } else {
          row.Children.forEach((c) => {
            this.item[c.DicValue] = c
            this.format(c)
          })
        }
      }
    },
    handleChange(val) {
      const e = {
        isMultiple: this.isMultiple,
        value: null,
        item: null
      }
      if (this.isMultiple) {
        e.value = val
        e.item = val.map(c => this.item[c])
      } else if (val) {
        e.value = [val]
        e.item = [this.item[val]]
      } else {
        e.value = []
      }
      this.$emit('input', val)
      this.$emit('change', e)
    }
  }
}
</script>
