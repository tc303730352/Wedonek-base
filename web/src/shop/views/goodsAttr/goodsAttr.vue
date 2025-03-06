<template>
  <el-card style="margin-top: 20px">
    <div slot="header">
      <span>商品属性</span>
    </div>
    <w-table
      :data="attrList"
      :columns="columns"
      :treeProps="treeProps"
      :isPaging="false"
      rowKey="id"
    >
      <template slot="sort" slot-scope="e">
        <el-input-number
          :key="e.row.id"
          v-if="maxSort[e.row.pid] > 1"
          class="el-input"
          :min="1"
          placeholder="排序位"
          :value="e.row.sort"
          @change="(value) => setSort(e.row, value)"
        ></el-input-number>
      </template>
      <template slot="value" slot-scope="e">
        <el-input v-model="e.row.Value" placeholder="属性值"></el-input>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          v-if="e.row.IsDir"
          size="mini"
          type="primary"
          title="添加"
          icon="el-icon-plus"
          @click="add(e.row)"
          circle
        ></el-button>
        <el-button
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          @click="edit(e.row)"
          circle
        ></el-button>
        <el-button
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          @click="drop(e.row)"
          circle
        ></el-button>
      </template>
    </w-table>
    <editAttr :attr="attr" :visible="visible" @close="closeEdit"></editAttr>
  </el-card>
</template>


<script>
import editAttr from "./components/editAttr.vue";
export default {
  computed: {},
  data() {
    return {
      attrList: [],
      minSort: {},
      maxSort: {},
      attrName: {},
      visible: false,
      isLoad: false,
      id: 1,
      attr: {
        Id: null,
        Name: null,
        Type: 0,
        Value: null,
        Pid: null,
      },
      treeProps: {
        children: "Attrs",
        hasChildren: "HasAttrs",
      },
      curRow: null,
      columns: [
        {
          key: "Name",
          title: "属性名",
          align: "left",
          width: 250,
          renderHeader: this.createHeader,
        },
        {
          key: "Value",
          title: "属性值",
          align: "left",
          width: 600,
          slotName: "value",
        },
        {
          key: "sort",
          title: "排序位",
          align: "left",
          slotName: "sort",
          width: 230,
        },
        {
          key: "action",
          title: "操作",
          align: "left",
          slotName: "action",
        },
      ],
    };
  },
  props: {
    attrs: {
      type: Array,
      default: [],
    },
  },
  watch: {
    attrs: {
      handler(val) {
        if (val && val.length > 0) {
          if(this.isLoad) {
            return
          }
          this.isLoad = true
          this.reset(val);
        }
      },
      immediate: true,
    },
  },
  methods: {
    createHeader(h, e) {
      const list = [<span>{e.column.label}</span>];
      list.push(
        <el-button
          style="float:right;"
          size="small"
          type="primary"
          title="添加组"
          icon="el-icon-plus"
          vOn:click={() => this.add()}
          circle
        ></el-button>
      );
      return <div>{list}</div>;
    },
    add(row) {
      this.attr.Id = null;
      this.attr.IsDir = false;
      this.attr.Pid = row == null ? 0 : row.id;
      this.attr.Name = null;
      this.attr.Value = null;
      this.visible = true;
    },
    edit(row) {
      this.attr.Id = row.id;
      this.attr.IsDir = row.IsDir;
      this.attr.Pid = row.pid;
      this.attr.Name = row.Name;
      this.attr.Value = row.Value;
      this.visible = true;
    },
    closeEdit(isRefresh, attr) {
      this.visible = false;
      if (!isRefresh) {
        return;
      }
      let rows = this.findAttrs(attr.Pid, this.attrList);
      if (attr.Id != null) {
        const source = rows.find((c) => c.id == attr.Id);
        source.Name = attr.Name;
        source.Value = attr.Value;
      } else {
        const sort = this.maxSort[attr.Pid] + 1;
        const add = {
          id: this.id,
          Name: attr.Name,
          IsDir: attr.IsDir,
          Value: attr.Value,
          pid: attr.Pid,
          sort: sort,
        };
        this.id = this.id + 1;
        if (attr.IsDir == false && attr.Pid != 0) {
          add.dir = this.attrName[attr.Pid];
        } else if (attr.IsDir) {
          add.Attrs = [];
          add.colSpan = {
            Name: 2,
          };
          this.attrName[add.id] = add.Name;
        }
        rows.push(add);
        this.maxSort[attr.Pid] = sort;
      }
    },
    getValue() {
      return this.attrList.map((c) => {
        return this.getAttrs(c);
      });
    },
    getAttrs(row) {
      const add = {
        Name: row.Name,
        Value: row.Value,
        IsDir: row.IsDir,
      };
      if (row.IsDir && row.Attrs != null && row.Attrs.length > 0) {
        add.Attrs = row.Attrs.map((c) => {
          return this.getAttrs(c);
        });
      }
      return add;
    },
    setSort(row, value) {
      const list = this.findAttrs(row.pid, this.attrList);
      if (list == null) {
        return;
      }
      const cur = list.find((c) => c.id == row.id);
      const data = list.find((c) => c.sort == value);
      if (cur != null) {
        if (data != null) {
          data.sort = cur.sort;
        }
        cur.sort = value;
        list.OrderBy("sort");
      }
    },
    findAttrs(pid, rows) {
      if (pid == 0) {
        return rows;
      }
      for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        if (row.id == pid) {
          return row.Attrs;
        } else if (row.Attrs && row.Attrs.length > 0) {
          const res = this.findAttrs(pid, row.Attrs);
          if (res != null) {
            return res;
          }
        }
      }
      return null;
    },
    formatRow(parent, attrs, id) {
      let sort = 1;
      attrs.forEach((c) => {
        c.id = id;
        this.attrName[c.id] = c.Name;
        c.sort = sort;
        c.pid = parent.id;
        id += 1;
        sort += 1;
        if (c.IsDir) {
          c.dir = parent.Name;
          if (c.Attrs == null) {
            c.Attrs = [];
          } else {
            id = this.formatRow(c, c.Attrs, id);
          }
          c.colSpan = {
            Name: 2,
          };
        }
      });
      this.maxSort[parent.id] = attrs.length;
      return id;
    },
    reset(attrs) {
      this.attrName = {};
      this.id = this.formatRow(
        {
          id: 0,
          Name: null,
        },
        attrs,
        1
      );
      this.attrList = attrs.concat(); 
    },
    drop(row) {
      let title;
      if (row.IsDir) {
        title = "确认删除目录：" + row.Name + "?";
      } else {
        title = "确认删除目录" + row.dir + "的属性：" + row.Name + "?";
      }
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.remove(row);
      });
    },
    resetAttr() {
      const attrs = this.getValue()
      this.reset(attrs);
    },
    remove(row) {
      if (row.pid == 0) {
        const index = this.attrList.findIndex((c) => c.id == row.id);
        this.attrList.splice(index, 1);
      } else {
        const rows = this.findAttrs(row.pid, this.attrList);
        const index = rows.findIndex((c) => c.id == row.id);
        if (index != -1) {
          rows.splice(index, 1);
        }
      }
      this.resetAttr();
    },
  },
  components: {
    editAttr,
  },
};
</script>
    <style type="less" scoped>
</style>