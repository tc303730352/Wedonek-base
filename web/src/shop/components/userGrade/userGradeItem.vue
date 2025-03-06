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
      :key="item.Id"
      :label="item.GradeName"
      :value="item.Id"
    />
  </el-select>
  <el-radio-group
    v-else-if="mode == 'radio'"
    :disabled="disabled"
    :value="value"
    @input="chooseChange"
  >
    <el-radio :label="null" :value="null">{{ nullText }}</el-radio>
    <el-radio
      v-for="item in items"
      :key="item.Id"
      :label="item.Id"
    >{{ item.GradeName }}</el-radio>
  </el-radio-group>
  <el-radio-group v-else :disabled="disabled" :value="value" @input="chooseChange">
    <el-radio-button :label="null">{{ nullText }}</el-radio-button>
    <el-radio-button
      v-for="item in items"
      :key="item.Id"
      :label="item.Id"
    >{{ item.GradeName }}</el-radio-button>
  </el-radio-group>
</template>

<script>
import { Gets } from '@/shop/api/userGrade'
export default {
  props: {
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
      default: '选择用户等级'
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
    nullText: {
      type: String,
      default: '全部'
    },
    mode: {
      type: String,
      default: 'select'
    }
  },
  data() {
    return {
      place: null,
      items: []
    }
  },
  watch: {
    placeholder: {
      handler(val) {
        this.place = val
      },
      immediate: true
    }
  },
  mounted() {
    this.loadGrade()
  },
  methods: {
    async loadGrade() {
      const res = await Gets()
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
