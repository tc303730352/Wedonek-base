<template>
  <div v-if="readonly" class="itemNames">
    <span v-for="i in names" :key="i">{{ i }}</span>
  </div>
  <el-select
    v-else-if="mode == 'select'"
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
import { gets, GetDicTextList } from '@/api/base/dictItem'
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
      type: String | Array,
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
    },
    readonly: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      place: null,
      items: [],
      names: []
    }
  },
  watch: {
    dicId: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    },
    value: {
      handler(val) {
        if (val && this.readonly) {
          this.reset()
        }
      },
      immediate: false
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
    reset() {
      if (this.readonly) {
        if (this.value == null) {
          this.names = []
          return
        } else if (this.multiple && this.value.length !== 0) {
          this.loadItemText(this.value)
        } else if (this.multiple === false) {
          this.loadName([this.value])
        } else {
          this.deptName = []
        }
      } else {
        this.loadDict()
      }
    },
    async loadDict() {
      const res = await gets(this.dicId)
      if (this.filterFunc) {
        this.items = this.filterFunc(res)
      } else {
        this.items = res
      }
    },
    async loadItemText(ids) {
      this.names = await GetDicTextList(this.dicId, ids)
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

<style scoped>
.itemNames {
  display: list-item;
}
.itemNames span{
  margin-right: 5px;
}
</style>
