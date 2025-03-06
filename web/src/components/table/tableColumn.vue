<template>
  <el-table-column
    :prop="column.key"
    :label="column.title"
    :sortable="column.sortable"
    :width="column.width"
    :sort-by="column.sortby"
    :fixed="column.fixed"
    :resizable="column.resizable"
    :render-header="column.renderHeader"
    :header-align="column.headerAlign ? column.headerAlign : 'center'"
    :align="column.align ? column.align : 'center'"
    :sort-orders="column.sortorders ? column.sortorders : ['ascending','descending']"
    :min-width="column.minWidth"
  >
    <template v-if="column.children && column.children.length != 0">
      <tableColumn
        v-for="child in column.children"
        :key="child.key"
        :column="child"
      >
        <template v-for="(_, name) in $scopedSlots" v-slot:[name]="data">
          <slot :name="name" v-bind="data" />
        </template>
      </tableColumn>
    </template>
    <template slot-scope="scope">
      <template v-if="column.slotName">
        <slot :name="column.slotName" :column="column" :text="scope.row[column.key]" :row="scope.row"></slot>
      </template>  
      <el-radio v-else-if="column.type=='radio'" v-model="scope.row[column.key]"></el-radio>
      <span v-else>{{ scope.row[column.key] }}</span>
    </template>
  </el-table-column>
</template>

<script>
export default {
  name: "tableColumn",
  components: {},
  data() {
    return {};
  },
  props: {
    column: {
      type: Object,
      default: null,
    },
  },
  methods: {},
};
</script>