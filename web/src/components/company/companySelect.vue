<template>
  <div v-if="readonly" class="itemNames">
    <span v-for="i in names" :key="i">{{ i }}</span>
  </div>
  <el-cascader
    v-else
    v-model="chioseKey"
    :options="comList"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    @change="handleChange"
  />
</template>

<script>
import { GetTreeItems, GetNameList } from '@/api/base/company'
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
      default: false
    },
    parentId: {
      type: String,
      default: null
    },
    value: {
      default: null
    }
  },
  data() {
    return {
      comList: [],
      chioseKey: null,
      cache: {},
      names: [],
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: 'Id',
        label: 'Name',
        children: 'Children'
      }
    }
  },
  watch: {
    parentId: {
      handler(val) {
        this.reset()
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
      const res = await GetTreeItems(this.parentId)
      this.props.checkStrictly = this.isChoiceEnd === false
      this.props.multiple = this.isMultiple
      this.cache = {}
      res.forEach((c) => {
        this.cache[c.Id] = c
        this.format(c)
      })
      this.comList = res
    },
    async loadNames(ids) {
      this.names = await GetNameList(ids)
    },
    format(row) {
      if (row.Children) {
        if (row.Children.length === 0) {
          row.Children = null
        } else {
          row.Children.forEach((c) => {
            this.cache[c.Id] = c
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
        e.item = val.map(c => this.cache[c])
      } else if (val) {
        e.value = [val]
        e.item = [this.cache[val]]
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
