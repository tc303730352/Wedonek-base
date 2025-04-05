<template>
  <el-select v-model="value" filterable :placeholder="placeholder" @change="selectChange">
    <el-option-group
      v-for="group in groups"
      :key="group.key"
      :label="group.label"
    >
      <el-option
        v-for="item in group.list"
        :key="item.Id"
        :label="item.Name"
        :value="item.Id"
      />
    </el-option-group>
  </el-select>
</template>

<script>
import { GetItems } from '@/customForm/api/control'
import object from 'element-resize-detector/src/detection-strategy/object'
export default {
  components: {
  },
  props: {
    placeholder: {
      type: String,
      default: '请选择控件'
    },
    value: {
      type: String,
      default: null
    },
    control: {
      type: object,
      default: null
    }
  },
  data() {
    return {
      title: '编辑表单信息',
      groups: {},
      cache: {}
    }
  },
  watch: {
    value: {
      handler(val) {
        this.reset()
      },
      immediate: true
    }
  },
  mounted() {
    this.load()
  },
  methods: {
    reset() {
      if (this.groups.length === 0) {
        return
      }
      if (this.value == null) {
        this.$emit('update:control', null)
      } else {
        this.$emit('update:control', this.cache[this.value])
      }
    },
    async load() {
      const res = await GetItems(this.id)
      const base = {
        key: 'base',
        label: '基础控件',
        list: []
      }
      const extend = {
        key: 'base',
        label: '扩展控件',
        list: []
      }
      res.forEach(c => {
        this.cache[c.Id] = c
        if (c.IsBaseControl) {
          base.list.push({
            Id: c.Id,
            Name: c.Name
          })
        } else {
          extend.list.push({
            Id: c.Id,
            Name: c.Name
          })
        }
      })
      this.groups = [base, extend]
      if (this.value != null) {
        this.reset()
      }
    },
    selectChange(val) {
      if (val == null) {
        this.$emit('update:control', null)
        this.$emit('change', null, null)
      } else {
        this.$emit('update:control', this.cache[val])
        this.$emit('change', val, this.cache[val])
      }
    }
  }
}
</script>
