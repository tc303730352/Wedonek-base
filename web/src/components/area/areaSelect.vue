<template>
  <el-cascader
    v-model="chioseKey"
    :options="areaList"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    filterable
    @change="handleChange"
  />
</template>

<script>
import { getAreaTree } from '@/utils/areaTools'
export default {
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: '选择类目'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    value: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      areaList: [],
      chioseKey: [],
      area: {},
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
  computed: {},
  watch: {
    value: {
      handler(val) {
        if (val != null) {
          this.chioseKey = val
        } else {
          this.chioseKey = null
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.loadTree()
  },
  methods: {
    async loadTree() {
      const res = await getAreaTree()
      res.forEach((c) => {
        this.area[c.Id] = {
          Id: c.Id,
          Name: c.Name,
          AreaType: c.AreaType
        }
        this.format(c)
      })
      this.areaList = res
    },
    format(row) {
      if (row.Children == null || row.Children.length == 0) {
        row.Children = null
      } else {
        row.Children.forEach((c) => {
          this.area[c.Id] = {
            Id: c.Id,
            Name: c.Name,
            AreaType: c.AreaType
          }
          this.format(c)
        })
      }
    },
    handleChange(val) {
      let value = null
      if (Array.isArray(val)) {
        value = val[0]
      } else {
        value = val
      }
      this.$emit('input', value)
      this.$emit('change', this.area[value])
    }
  }
}
</script>
