<template>
  <el-card>
    <div slot="header">
      <span>商品后台类目</span>
    </div>
    <el-tree
      ref="categoryTree"
      :data="categoryList"
      :default-expand-all="true"
      :highlight-current="true"
      :props="props"
      :expand-on-click-node="false"
      :current-node-key="chioseKey"
      node-key="Id"
      @node-click="chiose"
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
  </el-card>
</template>

<script>
import { GetTree } from '../../api/category'
export default {
  components: {},
  props: {
    parentId: {
      type: String,
      default: null
    },
    isEnable: {
      type: Boolean,
      default: true
    },
    isChioseEnd: {
      type: Boolean,
      default: true
    },
    isAutoChiose: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      title: null,
      categoryList: [],
      chioseKey: null,
      props: {
        label: 'CategoryName',
        children: 'Children'
      }
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    async load() {
      const res = await GetTree({
        IsEnable: this.isEnable,
        ParentId: this.parentId
      })
      if (res.length > 0) {
        this.format(res, 1)
        this.categoryList = res
        if (this.isAutoChiose) {
          const chiose = this.getdefValue(res)
          this.chioseKey = chiose.Id
          this.$emit('change', {
            Id: chiose.Id,
            Name: chiose.CategoryName
          })
        }
      }
    },
    getdefValue(list) {
      if (list == null || list.length == 0) {
        return null
      }
      for (let i = 0; i < list.length; i++) {
        const item = list[i]
        if (item.disabled == false) {
          return item
        } else {
          const val = this.getdefValue(item.Children)
          if (val != null) {
            return val
          }
        }
      }
      return null
    },
    format(list, lvl) {
      const next = lvl + 1
      list.forEach((e) => {
        e.Lvl = lvl
        if (e.Children != null && e.Children.length > 0) {
          e.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
          e.disabled = this.isChioseEnd
          this.format(e.Children, next)
        } else {
          e.disabled = false
          e.style = {
            icon: 'el-icon-document',
            color: '#000'
          }
        }
      })
    },
    chiose(e) {
      this.chioseKey = e.Id
      this.$emit('change', {
        Id: e.Id,
        Name: e.CategoryName,
        Lvl: e.Lvl
      })
    }
  }
}
</script>
