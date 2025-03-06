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
      :key="item.DicValue"
      :label="item.DicText"
      :value="item.DicValue"
    />
  </el-select>
  <el-radio-group
    v-else-if="mode == 'radio'"
    :disabled="disabled"
    :value="value"
    @input="chooseChange"
  >
    <el-radio v-for="item in items" :key="item.DicValue" :label="item.DicValue">{{
      item.DicText
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
      :key="item.DicValue"
      :label="item.DicValue"
    >{{ item.DicText }}</el-radio-button>
  </el-radio-group>
</template>

<script>
import { gets } from '@/api/base/dictItem'
export default {
  props: {
    dicId: {
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
      type: String,
      default: null
    },
    placeholder: {
      type: String,
      default: '选择字典'
    },
    filterFunc: {
      type: Function,
      default: null
    },
    mode: {
      type: String,
      default: 'select'
    },
    multiple: {
      type: Boolean,
      default: false
    },
    styleSet: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      place: null,
      items: []
    }
  },
  watch: {
    dicId: {
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
      const res = await gets(this.dicId)
      if (this.filterFunc) {
        this.items = this.filterFunc(res)
      } else {
        this.items = res
      }
    },
    chooseChange(value) {
      if (value === '') {
        value = null
      }
      let item = null
      if (value != null) {
        if (this.multiple) {
          item = this.items.filter((c) => value.includes(c.Value))
        } else {
          item = this.items.find((c) => c.Value === value)
        }
      }
      this.$emit('input', value)
      this.$emit('change', value, item)
    }
  }
}
</script>
