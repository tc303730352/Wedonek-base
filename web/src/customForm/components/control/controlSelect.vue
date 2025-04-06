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
import { GetList } from '@/customForm/api/control'
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
      type: Object,
      default: null
    }
  },
  data() {
    return {
      title: '编辑表单信息',
      groups: [],
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
      const res = await GetList(this.id)
      const one = {
        key: 'base',
        label: '基础控件',
        list: []
      }
      const two = {
        key: 'extend',
        label: '扩展控件',
        list: []
      }
      res.forEach(c => {
        this.cache[c.Id] = c
        if (c.IsBaseControl) {
          one.list.push({
            Id: c.Id,
            Name: c.Name
          })
        } else {
          two.list.push({
            Id: c.Id,
            Name: c.Name
          })
        }
      })
      this.groups.push(one)
      this.groups.push(two)
      if (this.value != null) {
        this.reset()
      }
      this.$emit('load')
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
