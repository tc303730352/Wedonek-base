<template>
  <div v-if="readonly" class="itemNames">
    <span v-for="i in names" :key="i">{{ i }}</span>
  </div>
  <el-cascader
    v-else
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
import { getTrees, GetNames } from '@/api/base/treeDicItem'
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
    readonly: {
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
      default: null
    }
  },
  data() {
    return {
      treeItems: [],
      chioseKey: null,
      item: {},
      names: [],
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
          this.reset()
        }
      },
      immediate: true
    },
    value: {
      handler(val) {
        this.chioseKey = val
        if (this.readonly && val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    reset() {
      if (this.readonly) {
        if (this.value == null) {
          this.names = []
          return
        } else if (this.isMultiple && this.value.length !== 0) {
          this.loadNames(this.value)
        } else if (this.isMultiple === false) {
          this.loadNames([this.value])
        } else {
          this.names = []
        }
      } else {
        this.loadTree()
      }
    },
    async loadTree() {
      const res = await getTrees(this.dicId)
      this.props.checkStrictly = this.isChoiceEnd === false
      this.props.multiple = this.isMultiple
      this.item = {}
      res.forEach((c) => {
        this.item[c.DicValue] = c
        this.format(c)
      })
      this.treeItems = res
    },
    async loadNames(ids) {
      this.names = await GetNames(this.dicId, ids)
    },
    format(row) {
      if (row.Children) {
        if (row.Children.length === 0) {
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

<style scoped>
.itemNames {
  display: inline-block;
  line-height: 20px;
}
.itemNames span{
  padding: 5px;
}
</style>
