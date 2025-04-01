<template>
  <div v-if="readonly" class="itemNames">
    <span v-for="item in chioses" :key="item.Value">{{ item.Text }}</span>
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
    readonly: {
      type: Boolean,
      default: false
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
    },
    enumItems: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      place: null,
      items: [],
      chioses: []
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
    value: {
      handler(val) {
        this.loadNames()
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
    async loadDict() {
      const res = await GetItems(this.dicKey, this.sysHead)
      if (this.filterFunc) {
        this.items = this.filterFunc(res)
      } else if (this.filters) {
        this.items = res.filter((c) => !this.filters.includes(c.Value))
      } else {
        this.items = res
      }
      this.$emit('update:enumItems', res)
      this.loadNames()
    },
    loadNames() {
      if (this.value == null || this.readonly === false) {
        this.chioses = []
        return
      } else if (this.multiple && this.value.length > 0) {
        this.chioses = this.items.filter(c => this.value.includes(c.Value))
      } else if (this.multiple === false) {
        this.chioses = this.items.filter(c => c.Value === this.value)
      } else {
        this.chioses = []
      }
    },
    chooseChange(value) {
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
  display: inline-block;
  line-height: 20px;
}
.itemNames span{
  padding: 5px;
}
</style>
