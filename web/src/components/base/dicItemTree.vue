<template>
  <el-tree
    ref="dictTree"
    :data="trees"
    :default-expand-all="true"
    :highlight-current="true"
    :props="props"
    :expand-on-click-node="false"
    :style="styleSet"
    :show-checkbox="isMultiple"
    :current-node-key="chioseKey"
    :default-checked-keys="checkboxKey"
    :check-strictly="checkstrictly"
    node-key="Id"
    @node-click="chioseNode"
    @check-change="checkChange"
  >
    <span slot-scope="{ node, data }" class="slot-t-node">
      <template>
        <template v-if="data.style">
          <i
            v-if="data.style.icon.indexOf('el-') == 0"
            :class="data.style.icon"
            :style="{ color: data.style.color, marginRight: '5px' }"
          />
          <svg-icon
            v-else
            :icon-class="data.style.icon"
            :style="{ color: data.style.color, marginRight: '5px' }"
          />
        </template>
        <span>{{ node.label }}</span>
      </template>
    </span>
  </el-tree>
</template>

<script>
import { getTrees } from '@/api/base/treeDicItem'
export default {
  props: {
    dicId: {
      type: String,
      default: null
    },
    checkstrictly: {
      type: Boolean,
      default: false
    },
    selectKeys: {
      type: Array,
      default: () => []
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isAutoChiose: {
      type: Boolean,
      default: false
    },
    isChioseEnd: {
      type: Boolean,
      default: true
    },
    styleSet: {
      type: Object,
      default: () => {
        return {
          width: '100%'
        }
      }
    }
  },
  data() {
    return {
      trees: [],
      chioseKey: null,
      checkboxKey: [],
      deptDic: {},
      name: null,
      cache: {},
      valKey: {},
      props: {
        key: 'Id',
        label: 'DicText',
        children: 'Children'
      }
    }
  },
  computed: {
  },
  watch: {
    dicId: {
      handler(val) {
        if (val != null) {
          this.loadTree()
        }
      },
      immediate: true
    },
    selectKeys: {
      handler(val) {
        this.checkboxKey = val
      },
      immediate: true
    }
  },
  methods: {
    async load() {
      const res = await getTrees(this.dicId)
      if (res == null) {
        return []
      }
      res.forEach(a => {
        this.initTree(a)
      })
      return res
    },
    initTree(res) {
      if (res.DicValue != null && res.DicValue !== '') {
        this.valKey[res.DicValue] = res.DicText
      }
      if (res.Children != null && res.Children.length > 0) {
        res.style = {
          icon: 'el-icon-folder-opened',
          color: '#409eff'
        }
        res.isEnd = false
        res.Children.forEach(c => {
          this.initTree(c)
        })
      } else {
        res.style = {
          icon: 'el-icon-document',
          color: '#000'
        }
        res.isEnd = true
      }
      this.cache[res.Id] = {
        value: res.DicValue,
        text: res.DicText,
        isEnd: res.isEnd
      }
    },
    findDef(list) {
      if (this.isChioseEnd === false) {
        return list[0].Id
      }
      for (let i = 0; i < list.length; i++) {
        const t = list[i]
        if (t.isEnd) {
          return t.Id
        } else if (t.Children != null && t.Children.length > 0) {
          const res = this.findDef(t.Children)
          if (res != null) {
            return res
          }
        }
      }
      return null
    },
    async loadTree() {
      if (this.selectKeys == null || this.selectKeys.length === 0) {
        this.checkboxKey = []
      }
      const res = await this.load()
      this.trees = res
      const e = {
        isMultiple: this.isMultiple,
        text: this.valKey,
        cache: this.cache,
        value: []
      }
      if (this.isAutoChiose && res.length > 0) {
        const chioseId = this.findDef(res)
        this.chioseKey = chioseId
        e.value.push(this.cache[chioseId])
      } else {
        this.chioseKey = null
      }
      this.$emit('load', e)
    },
    autoChiose(data) {
      if (data.Children && data.Children.length !== 0) {
        let isSet = false
        data.Children.forEach((c) => {
          if (c.type === 'unit') {
            return
          }
          if (!this.checkboxKey.includes(c.Id)) {
            this.checkboxKey.push(c.Id)
            isSet = true
            this.autoChiose(c)
          }
        })
        return isSet
      }
      return false
    },
    checkChange(data, checked, indeterminate) {
      const index = this.checkboxKey.findIndex((c) => c === data.Id)
      if (checked) {
        if (index !== -1) {
          return
        }
        this.checkboxKey.push(data.Id)
        if (this.autoChiose(data)) {
          this.$refs.dictTree.setCheckedKeys(this.checkboxKey, false)
        }
      } else if (index === -1) {
        return
      } else {
        this.checkboxKey.splice(index, 1)
      }
      const e = {
        isMultiple: this.isMultiple,
        cache: this.cache,
        text: this.valKey,
        value: this.checkboxKey.map((c) => this.cache[c])
      }
      this.$emit('change', e)
    },
    chioseNode(node) {
      this.chioseKey = node.Id
      const e = {
        isMultiple: this.isMultiple,
        cache: this.cache,
        text: this.valKey,
        value: []
      }
      if (node !== null) {
        this.chioseKey = node.Id
        e.value.push(this.cache[node.Id])
      } else {
        this.chioseKey = null
      }
      this.$emit('change', e)
    }
  }
}
</script>
