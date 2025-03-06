<template>
  <div>
    <leftRightSplit :leftSpan="4">
      <categoryTree slot="left" @change="change"></categoryTree>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
          <el-button style="float: right" type="success" @click="add()"
            >添加规格组</el-button
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
          <template slot="Sort" slot-scope="e">
            <el-input-number
              :min="1"
              :max="maxSort"
              v-model="e.row.Sort"
              placeholder="排序位"
              @change="SetSort(e.row)"
            />
          </template>
          <template slot="Spec" slot-scope="e">
            <el-tag
              v-for="(item, index) in e.row.Spec"
              style="margin-right: 5px"
              :key="index"
              >{{ item }}</el-tag
            >
            <el-button
              type="default"
              title="编辑规格"
              icon="el-icon-edit"
              @click="editSpec(e.row)"
              plain
            ></el-button>
          </template>
          <template slot="action" slot-scope="e">
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
    <editSpecGroupT
      :visible="visible"
      :categoryId="categoryId"
      @close="closeEdit"
      :id="id"
    ></editSpecGroupT>
    <editSpecTemplate
      :visible="specVisible"
      :templateId="templateId"
      @close="closeSpec"
      :groupName="groupName"
    ></editSpecTemplate>
  </div>
</template>
  
  <script>
import categoryTree from "../../components/category/categoryTree.vue";
import * as specApi from "../../api/specGroupTemplate";
import editSpecGroupT from "./components/editSpecGroupT.vue";
import editSpecTemplate from "./components/editSpecTemplateList.vue";
export default {
  computed: {},
  data() {
    return {
      visible: false,
      title: "所有类目",
      dataList: [],
      maxSort: 1,
      categoryId: null,
      groupName: null,
      templateId: null,
      specVisible: false,
      id: null,
      columns: [
        {
          key: "GroupName",
          title: "组名",
          align: "left",
          width: 255,
          fixed: "left",
        },
        {
          key: "Sort",
          title: "排序位",
          align: "left",
          slotName: "Sort",
          width: 255,
        },
        {
          key: "IsEnable",
          title: "是否启用",
          align: "center",
          slotName: "IsEnable",
          width: 130,
        },
        {
          key: "Spec",
          title: "规格",
          align: "left",
          slotName: "Spec",
          minWidth: 130,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          width: 120,
          slotName: "action",
        },
      ],
    };
  },
  components: {
    categoryTree,
    editSpecGroupT,
    editSpecTemplate,
  },
  mounted() {},
  methods: {
    add() {
      this.id = null;
      this.visible = true;
    },
    edit(id) {
      this.id = id;
      this.visible = true;
    },
    closeSpec(isRefresh) {
      this.specVisible = false;
      if (isRefresh) {
        this.load();
      }
    },
    editSpec(row) {
      this.templateId = row.Id;
      this.groupName = row.GroupName;
      this.specVisible = true;
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
    async load() {
      const res = await specApi.Gets(this.categoryId);
      if (res.length == 0) {
        this.maxSort = 1;
      } else {
        this.maxSort = res.length;
      }
      this.dataList = res;
    },
    drop(row) {
      const title = "确认删除该规格组：" + row.GroupName + "?";
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
      await specApi.Delete(row.Id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      const index = this.dataList.findIndex((c) => c.Id == row.Id);
      this.dataList.splice(index, 1);
    },
    async setIsEnabe(row) {
      if (row.IsEnable && (row.Spec == null || row.Spec.length == 0)) {
        this.$message({
          type: "error",
          message: "规格为空不能启用!",
        });
        row.IsEnable = false
        return;
      }
      await specApi.SetIsEnable(row.Id, row.IsEnable);
    },
    async SetSort(row) {
      const res = await specApi.SetSort(row.Id, row.Sort);
      this.dataList.forEach((c) => {
        if (res[c.Id] != null) {
          c.Sort = res[c.Id];
        }
      });
      this.dataList.OrderBy("Sort");
    },
  },
};
</script>