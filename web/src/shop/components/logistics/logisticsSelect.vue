<template>
  <el-select
    v-model="value"
    :placeholder="placeholder"
    :clearable="clearable"
    :disabled="disabled"
    :multiple="false"
    :filterable="filterable"
    class="el-input"
    @change="handleChange"
  >
    <el-option
      v-for="item in items"
      :key="item.Id"
      :label="item.TemplateName"
      :value="item.Id"
    />
  </el-select>
</template>

<script>
import { GetItems } from '@/shop/api/logistics'
export default {
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    filterable: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: '选择物流摸版'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    isDefChiose: {
      type: Boolean,
      default: true
    },
    value: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      items: []
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    async load() {
      const res = await GetItems()
      this.items = res
      if (this.isDefChiose) {
        const def = res.find((c) => c.IsDefault)
        if (def != null) {
          this.handleChange(def.Id)
        }
      }
    },
    handleChange(value) {
      const e = {
        value: value,
        item: this.items.find((c) => c.Id == value)
      }
      this.$emit('input', value)
      this.$emit('change', e)
    }
  }
}
</script>
