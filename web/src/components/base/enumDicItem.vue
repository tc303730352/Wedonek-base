<template>
  <el-select
    v-if="mode == 'select'"
    :disabled="disabled"
    :placeholder="place"
    :style="styleSet"
    :clearable="clearable"
    :class="className"
    :multiple="multiple"
    :value="value"
    @change="chooseChange"
  >
    <el-option
      v-for="item in items"
      :key="item.Value"
      :label="item.Text"
      :value="item.Value"
    />
  </el-select>
  <el-radio-group
    v-else-if="mode == 'radio'"
    :disabled="disabled"
    :value="value"
    @input="chooseChange"
  >
    <el-radio v-for="item in items" :key="item.Value" :label="item.Value">{{
      item.Text
    }}</el-radio>
  </el-radio-group>
  <el-radio-group
    v-else
    :disabled="disabled"
    :value="value"
    @input="chooseChange"
  >
    <el-radio-button
      v-for="item in items"
      :key="item.Value"
      :label="item.Value"
    >{{ item.Text }}</el-radio-button>
  </el-radio-group>
</template>

<script>
import { GetItems } from '@/api/base/enumDic'
export default {
  props: {
    dicKey: {
      type: String,
      default: null
    },
    clearable: {
      type: Boolean,
      default: true
    },
    disabled: {
      type: Boolean,
      default: false
    },
    className: {
      type: String,
      default: 'el-input'
    },
    value: {
      default: null
    },
    placeholder: {
      type: String,
      default: '选择字典'
    },
    filters: {
      type: Array,
      default: null
    },
    filterFunc: {
      type: Function,
      default: null
    },
    multiple: {
      type: Boolean,
      default: false
    },
    styleSet: {
      type: Object,
      default: null
    },
    mode: {
      type: String,
      default: 'select'
    },
    sysHead: {
      type: String,
      default: 'hr'
    }
  },
  data() {
    return {
      place: null,
      items: []
    }
  },
  watch: {
    dicKey: {
      handler(val) {
        if (val) {
          this.loadDict()
        }
      },
      immediate: true
    },
    placeholder: {
      handler(val) {
        this.place = val
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    async loadDict() {
      const res = await GetItems(this.dicKey, this.sysHead)
      if (this.filterFunc) {
        this.items = this.filterFunc(res)
      } else if (this.filters) {
        this.items = res.filter((c) => !this.filters.includes(c.Value))
      } else {
        this.items = res
      }
    },
    chooseChange(value) {
      let item = null
      if (value != null) {
        if (this.multiple) {
          item = this.items.filter((c) => value.includes(c.Value))
        } else {
          item = this.items.find((c) => c.Value == value)
        }
      }
      this.$emit('input', value)
      this.$emit('change', value, item)
    }
  }
}
</script>
