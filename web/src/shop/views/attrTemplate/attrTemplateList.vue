<template>
  <div>
    <leftRightSplit :leftSpan="4">
      <categoryTree slot="left" @change="change"></categoryTree>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
          <el-button style="float: right" type="success" @click="add(0)"
            >添加属性</el-button
          >
        </div>
        <w-table
          :data="dataList"
          :columns="columns"
          :isPaging="false"
          rowKey="Id"
        >
          <template slot="IsEnable" slot-scope="e">
            <el-switch
              v-model="e.row.IsEnable"
              @change="setIsEnabe(e.row)"
            ></el-switch>
          </template>
          <template slot="Type" slot-scope="e">
            {{ e.row.Type == "dir" ? "目录" : "规格参数" }}
          </template>
          <template slot="Sort" slot-scope="e">
            <el-input-number
              v-if="maxSort[e.row.ParentId] > 1"
              :min="1"
              :max="maxSort[e.row.ParentId]"
              class="el-input"
              v-model="e.row.Sort"
              placeholder="排序位"
              @change="SetSort(e.row)"
            />
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              v-if="e.row.ParentId == 0"
              size="mini"
              type="primary"
              title="添加规格参数"
              icon="el-icon-plus"
              @click="add(e.row.Id)"
              circle
            ></el-button>
            <el-button
              v-if="e.row.IsEnable == false"
              size="mini"
              type="primary"
              title="编辑"
              icon="el-icon-edit"
              @click="edit(e.row.Id)"
              circle
            ></el-button>
            <el-button
              v-if="e.row.IsEnable == false"
              size="mini"
              type="danger"
              title="删除"
              icon="el-icon-delete"
              @click="drop(e.row)"
              circle
            ></el-button>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <editAttrTemplate
      :visible="visible"
      :parentId="parentId"
      :categoryId="categoryId"
      @close="closeEdit"
      :id="id"
    ></editAttrTemplate>
  </div>
</template>
    
<script>
import categoryTree from "../../components/category/categoryTree.vue";
import * as attrApi from "../../api/attrTemplate";
import editAttrTemplate from "./components/editAttrTemplate.vue";
export default {
  computed: {},
  data() {
    return {
      visible: false,
      title: "所有类目",
      dataList: [],
      maxSort: {},
      categoryId: null,
      id: null,
      columns: [
        {
          key: "Name",
          title: "属性名",
          align: "left",
          width: 250,
          fixed: "left",
        },
        {
          key: "DefValue",
          title: "默认值",
          align: "left",
          width: 200,
          fixed: "left",
        },
        {
          key: "PrtName",
          title: "所属上级",
          align: "left",
          width: 150,
        },
        {
          key: "Type",
          title: "类型",
          align: "left",
          slotName: "Type",
          width: 80,
        },
        {
          key: "Sort",
          title: "排序位",
          align: "left",
          slotName: "Sort",
          width: 150,
        },
        {
          key: "IsEnable",
          title: "是否启用",
          align: "center",
          slotName: "IsEnable",
          width: 130,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          slotName: "action",
        },
      ],
    };
  },
  components: {
    categoryTree,
    editAttrTemplate,
  },
  mounted() {},
  methods: {
    add(pid) {
      this.id = null;
      this.parentId = pid;
      this.visible = true;
    },
    edit(id) {
      this.id = id;
      this.parentId = 0;
      this.visible = true;
    },
    closeEdit(isRefresh) {
      this.visible = false;
      if (isRefresh) {
        this.load();
      }
    },
    change(category) {
      this.title = category.Name;
      this.categoryId = category.Id;
      this.load();
    },
    initMaxSort(list, parent) {
      list.forEach((a) => {
        a.ParentId = parent.Id;
        a.PrtName = parent.Name;
        a.Type = "menu";
        if (a.Children && a.Children.length > 0) {
          a.Type = "dir";
          this.maxSort[a.Id] = a.Children.length;
          this.initMaxSort(a.Children, a);
        }
      });
    },
    async load() {
      const res = await attrApi.GetTrees(this.categoryId, null);
      this.maxSort[0] = res.length;
      this.initMaxSort(res, {
        Id: 0,
      });
      this.dataList = res;
    },
    drop(row) {
      const title = "确认删除该属性：" + row.Name + "?";
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.submitDrop(row);
      });
    },
    async submitDrop(row) {
      await attrApi.Delete(row.Id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      const list = this.findRow(row.ParentId, this.dataList);
      const index = list.findIndex((c) => c.Id == row.Id);
      list.splice(index, 1);
    },
    findRow(pid, rows) {
      if (pid == 0) {
        return rows;
      }
      for (let i = 0; i < rows.length; i++) {
        const row = rows[i];
        if (row.Id == pid) {
          return row.Children;
        } else if (row.Children && row.Children.length > 0) {
          const res = this.findRow(pid, row.Children);
          if (res != null) {
            return res;
          }
        }
      }
      return null;
    },
    async setIsEnabe(row) {
      if (row.IsEnable) {
        await attrApi.Enable(row.Id);
      } else {
        await attrApi.Stop(row.Id);
      }
    },
    async SetSort(row) {
      const res = await attrApi.SetSort(row.Id, row.Sort);
      const list = this.findRow(row.ParentId, this.dataList);
      list.forEach((c) => {
        if (res[c.Id] != null) {
          c.Sort = res[c.Id];
        }
      });
      list.OrderBy("Sort");
    },
  },
};
</script>