<template>
  <el-cascader
    v-model="chioseKey"
    :options="dataList"
    :placeholder="placeholder"
    :props="props"
    :clearable="clearable"
    :disabled="disabled"
    class="el-input"
    @change="handleChange"
  ></el-cascader>
</template>
    
<script>
import { GetTree } from "../../api/frontCategory";
export default {
  computed: {},
  data() {
    return {
      dataList: [],
      chioseKey: null,
      category: {},
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: "Id",
        label: "CategoryName",
        children: "Children",
      },
    };
  },
  props: {
    clearable: {
      type: Boolean,
      default: true,
    },
    placeholder: {
      type: String,
      default: "选择类目",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    parentId: {
      type: String,
      default: null,
    },
    isEnable: {
      type: Boolean,
      default: true,
    },
    isMultiple: {
      type: Boolean,
      default: false,
    },
    value: {
      type: String | Number | Array,
      default: null,
    },
  },
  watch: {
    value: {
      handler(val) {
        this.chioseKey = val;
      },
      immediate: true,
    },
  },
  mounted() {
    this.loadTree();
  },
  methods: {
    async loadTree() {
      const res = await GetTree({
        IsEnable: this.isEnable,
        ParentId: this.parentId,
      });
      this.props.multiple = this.isMultiple;
      this.category = {};
      res.forEach((c) => {
        this.category[c.Id] = {
          Id: c.Id,
          CategoryName: c.CategoryName,
        };
        this.format(c);
      });
      this.dataList = res;
    },
    format(row) {
      if (row.Children.length == 0) {
        row.Children = null;
      } else {
        row.Children.forEach((c) => {
          this.category[c.Id] = {
            Id: c.Id,
            CategoryName: c.CategoryName,
          };
          this.format(c);
        });
      }
    },
    handleChange(val) {
      const e = {
        isMultiple: this.isMultiple,
        value: null,
        category: null,
      };
      if (this.isMultiple) {
        e.value = val;
        e.category = val.map((c) => this.category[c]);
      } else if (val) {
        e.value = [val];
        e.category = [this.category[val]];
      } else {
        e.value = [];
        e.dept = [];
      }
      this.$emit("input", val);
      this.$emit("change", e);
    },
  },
};
</script>