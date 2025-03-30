<template>
  <div>
    <el-table
      :ref="id"
      v-loading="loading"
      :data="dataList"
      :style="styleSet"
      :class="className"
      :row-key="rowKey"
      :show-header="showHeader"
      :span-method="rowSpanAction"
      :border="border"
      :max-height="maxHeight"
      :height="height"
      default-expand-all
      :tree-props="treeProps"
      @sort-change="sortChange"
      @current-change="currentChange"
      @header-dragend="refreshCol"
      @select="selectedEvent"
      @select-all="selectAll"
    >
      <el-table-column
        v-if="isSelect && isMultiple"
        type="selection"
        :resizable="false"
        :selectable="checkIsDisabled"
        fixed="left"
        width="55"
      />
      <el-table-column
        v-else-if="isSelect && isMultiple == false"
        width="55"
        fixed="left"
      >
        <template slot-scope="scope">
          <el-radio
            v-model="chioseKey"
            :disabled="!checkIsDisabled(scope.row)"
            :label="scope.row[rowKey]"
          >{{
          }}</el-radio>
        </template>
      </el-table-column>
      <tableColumn v-for="item in colList" :key="item.key" :column="item">
        <template v-slot:[item.slotName]="scope">
          <slot :name="item.slotName" v-bind="scope" />
        </template>
      </tableColumn>
    </el-table>
    <div style="text-align: right">
      <el-pagination
        v-if="isPaging"
        :current-page.sync="paging.Index"
        :page-size="paging.Size"
        layout="total, prev, pager, next"
        :total="paging.Total"
        @size-change="sizeChange"
        @current-change="indexChange"
      />
    </div>
  </div>
</template>

<script>
import tableColumn from './tableColumn.vue'
export default {
  components: {
    tableColumn
  },
  props: {
    data: {
      type: Array,
      default: null
    },
    disabled: {
      type: Boolean,
      default: false
    },
    border: {
      type: Boolean,
      default: true
    },
    maxHeight: {
      type: String,
      default: null
    },
    height: {
      type: String,
      default: null
    },
    columns: {
      type: Array,
      default: null
    },
    showHeader: {
      type: Boolean,
      default: true
    },
    rowKey: {
      type: String,
      default: null
    },
    selectKeys: {
      type: Array,
      default: null
    },
    isSelect: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    checkIsSelect: {
      type: Function,
      default: () => true
    },
    treeProps: {
      type: Object,
      default: () => {
        return {
          children: 'Children',
          hasChildren: 'HasChildren'
        }
      }
    },
    paging: {
      type: Object,
      default: () => {
        return {
          Size: 20,
          Index: 1,
          SortName: null,
          IsDesc: false,
          Total: 0
        }
      }
    },
    isPaging: {
      type: Boolean,
      default: true
    },
    className: {
      type: String,
      default: null
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
      colList: [],
      rowSpan: {},
      colSpan: {},
      id: 'cur',
      curTable: null,
      dataList: null,
      isMerge: false,
      chioseKey: null,
      selected: [],
      allKey: [],
      loadRows: {},
      loading: false
    }
  },
  watch: {
    columns: {
      handler(val) {
        this.colList = val
        this.isMerge = val.findIndex((c) => c.isMerge) !== -1
      },
      immediate: true
    },
    isSelect: {
      handler(val) {
        if (val && this.allKey.length !== 0) {
          return
        }
        this.loadSelected(this.data)
      },
      immediate: false
    },
    selectKeys: {
      handler(val) {
        if (val === this.selected) {
          return
        }
        this.initSelect(val)
      },
      immediate: true
    },
    data: {
      handler(val) {
        if (!this.loading) {
          this.loading = true
          this.rowSpan = {}
          this.colSpan = {}
          if (val && val.length > 0) {
            this.loadSelected(val)
          }
          this.dataList = val == null ? [] : val
          this.loading = false
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.id = Math.random().toString(36).substring(2, 15)
    this.curTable = this.$refs[this.id]
  },
  methods: {
    checkIsDisabled(row) {
      return this.disabled === false && this.checkIsSelect(row)
    },
    initSelect(val) {
      if (val == null || val.length === 0) {
        this.chioseKey = null
        this.selected = []
      } else if (this.isSelect && !this.isMultiple) {
        this.chioseKey = val[0]
      } else if (this.isSelect && this.isMultiple) {
        this.selected = [...val]
        this.initSelected()
      }
      if (!this.isMultiple) {
        this.$nextTick(() => {
          this.$refs[this.id].setCurrentRow(this.chioseKey)
        })
      }
    },
    refreshCol(newWidth, oldWidth, column, event) {
      if (this.isMerge) {
        this.$nextTick(function() {
          this.rowSpan = {}
          this.colSpan = {}
          this.$refs[this.id].doLayout()
        })
      }
    },
    initSelected() {
      this.$nextTick(() => {
        this.curTable = this.$refs[this.id]
        this.curTable.clearSelection()
        if (this.selected.length > 0) {
          this.selected.forEach((c) => {
            const row = this.dataList.find((a) => a[this.rowKey] === c)
            if (row) {
              this.curTable.toggleRowSelection(row, true)
            }
          })
        }
      })
    },
    rowSpanAction(e) {
      let row = 1
      let col = 1
      const key = e.column.property
      if (e.row.rowSpan != null && e.row.rowSpan[key] != null) {
        const num = this.rowSpan[key]
        if (num == null || num === Number.NaN || num === e.rowIndex) {
          this.rowSpan[key] = e.rowIndex + e.row.rowSpan[key]
          row = e.row.rowSpan[key]
        } else if (num > e.rowIndex) {
          row = 0
          col = 0
        }
      }
      if (e.row.colSpan != null && e.row.colSpan[key] != null) {
        col = e.row.colSpan[key]
        this.colSpan[e.rowIndex] = {
          begin: e.columnIndex,
          end: e.columnIndex + col
        }
      } else {
        const span = this.colSpan[e.rowIndex]
        if (
          span != null &&
          span.end > e.columnIndex &&
          span.begin < e.columnIndex
        ) {
          col = 0
          row = 0
        }
      }
      return {
        rowspan: row,
        colspan: col
      }
    },
    selectAll(list) {
      if (list.length > 0) {
        this.allKey.forEach((c) => {
          if (!this.selected.includes(c)) {
            this.selected.push(c)
          }
        })
      } else {
        this.selected = this.selected.filter((c) => !this.allKey.includes(c))
      }
      this.loading = true
      this.selectEvent()
    },
    loadSelected(rows) {
      if (!this.isSelect || rows == null || rows.length === 0) {
        return
      }
      const keys = []
      rows.forEach((c) => {
        if (this.checkIsSelect(c)) {
          const key = c[this.rowKey]
          keys.push(key)
          if (this.isMultiple) {
            this.loadRows[key] = c
          }
        }
      })
      this.allKey = keys
      if (this.isMultiple) {
        this.initSelected()
      }
    },
    sortChange(e) {
      this.paging.SortName = e.column.sortBy == null ? e.prop : e.column.sortBy
      this.paging.IsDesc = e.order === 'descending'
      this.$emit('load', this.paging)
    },
    currentChange(e) {
      if (this.loading || !this.isSelect || e == null) {
        return
      }
      this.loading = true
      const key = e[this.rowKey]
      if (!this.allKey.includes(key)) {
        this.loading = false
      } else if (this.isSelect && this.isMultiple === false) {
        this.chioseKey = key
        this.selected = [key]
        this.$emit('selected', {
          list: [e],
          keys: this.selected
        })
        this.loading = false
      } else if (this.isSelect && this.isMultiple) {
        if (this.selected.includes(key) === false) {
          this.selected.push(key)
          this.curTable.toggleRowSelection(e, true)
          this.selectEvent()
          // this.initAllState()
        } else {
          this.loading = false
        }
      } else {
        this.loading = false
      }
    },
    selectEvent() {
      const res = {
        list: [],
        keys: this.selected
      }
      if (this.selected.length !== 0) {
        this.selected.forEach((c) => {
          const item = this.loadRows[c]
          if (item != null) {
            res.list.push(item)
          }
        })
      }
      this.$emit('selected', res)
      this.loading = false
    },
    selectedEvent(e, row) {
      if (this.loading) {
        return
      }
      this.loading = true
      const key = row[this.rowKey]
      const index = this.selected.findIndex((a) => a === key)
      if (index === -1) {
        this.selected.push(key)
      } else {
        this.selected.splice(index, 1)
      }
      this.selectEvent()
      this.loading = false
    },
    sizeChange(val) {
      this.paging.Size = val
    },
    indexChange(val) {
      this.paging.Index = val
      this.$emit('load', this.paging)
    }
  }
}
</script>
