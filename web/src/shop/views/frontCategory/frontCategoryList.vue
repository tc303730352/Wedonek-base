<template>
  <div>
    <leftRightSplit :leftSpan="4">
      <el-card slot="left">
        <div slot="header">
          <span>商品前台类目列表</span>
        </div>
        <el-tree
          :data="category"
          :default-expand-all="true"
          :highlight-current="true"
          :props="props"
          ref="categoryTree"
          @node-click="chiose"
          :expand-on-click-node="false"
          :current-node-key="chioseKey"
          node-key="Id"
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
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
          <el-button style="float: right" type="success" @click="add()"
            >添加类目</el-button
          >
        </div>
        <w-table
          :data="dataList"
          :columns="columns"
          :tree-props="{ children: 'Children' }"
          :isPaging="false"
          rowKey="Id"
        >
          <template slot="IsEnable" slot-scope="e">
            <el-switch
              v-model="e.row.IsEnable"
              @change="setIsEnabe(e.row)"
            ></el-switch>
          </template>
          <template slot="Icon" slot-scope="e">
            <img
              v-if="e.row.Icon != null && e.row.Icon != ''"
              :src="e.row.Icon"
              :alt="e.row.CategoryName"
              width="40px"
            />
          </template>
          <template slot="Category" slot-scope="e">
            <el-tag
              v-for="item in e.row.Category"
              style="margin-right: 5px"
              :key="item.Id"
              >{{ item.Name }}</el-tag
            >
            <el-button
              type="default"
              title="关联的后台类目"
              icon="el-icon-edit"
              @click="chioseBind(e.row)"
              plain
            ></el-button>
          </template>
          <template slot="Sort" slot-scope="e">
            <el-input-number
              v-if="maxSort[e.row.ParentId] > 1"
              :min="1"
              :max="maxSort[e.row.ParentId]"
              v-model="e.row.Sort"
              placeholder="排序位"
              @change="SetSort(e.row)"
            />
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              size="mini"
              type="primary"
              title="添加下级"
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
    <editCategory
      :visible="visible"
      :parentId="parentId"
      @close="closeEdit"
      :id="id"
    ></editCategory>
    <editCategoryBind
      :visible="visibleBind"
      :categoryId="categoryId"
      @close="closeBind"
      :id="id"
    ></editCategoryBind>
  </div>
</template>
  
  <script>
import * as categoryApi from "../../api/frontCategory";
import editCategory from "./components/editFrontCategory.vue";
import editCategoryBind from "./components/editCategoryBind.vue";
export default {
  computed: {},
  data() {
    return {
      category: [],
      dataList: [],
      chioseKey: null,
      visible: false,
      maxSort: {},
      visibleBind: false,
      categoryId: null,
      source: null,
      id: null,
      parentId: null,
      title: "所有类目",
      columns: [
        {
          key: "CategoryName",
          title: "类目名称",
          align: "left",
          width: 255,
          fixed: "left",
        },
        {
          key: "Icon",
          title: "图标",
          align: "center",
          slotName: "Icon",
          width: 200,
        },
        {
          key: "Sort",
          title: "排序位",
          align: "left",
          slotName: "Sort",
          width: 255,
        },
        {
          key: "PrtName",
          title: "所属上级",
          align: "left",
          width: 255,
        },
        {
          key: "Category",
          title: "关联后台类目",
          align: "left",
          slotName: "Category",
          minWidth: 255,
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
          minWidth: 120,
          slotName: "action",
        },
      ],
      props: {
        label: "CategoryName",
        children: "Children",
      },
    };
  },
  components: {
    editCategory,
    editCategoryBind,
  },
  mounted() {
    this.load();
  },
  methods: {
    initMaxSort(list, parent) {
      list.forEach((a) => {
        a.ParentId = parent.Id;
        a.PrtName = parent.CategoryName;
        if(a.Category == null){
          a.Category = []
        }
        if (a.Children && a.Children.length > 0) {
          this.maxSort[a.Id] = a.Children.length;
          this.initMaxSort(a.Children, a);
        }
      });
    },
    drop(row) {
      const title = "确认删除该类目 " + row.CategoryName + "?";
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
      await categoryApi.Delete(row.Id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      const list = this.findRow(row.ParentId, this.dataList);
      const index = list.findIndex((c) => c.Id == row.Id);
      list.splice(index, 1);
    },
    async setIsEnabe(row) {
      if (row.IsEnable) {
        await categoryApi.Enable(row.Id);
      } else {
        await categoryApi.Disable(row.Id);
      }
    },
    async SetSort(row) {
      const res = await categoryApi.SetSort(row.Id, row.Sort);
      const list = this.findRow(row.ParentId, this.dataList);
      list.forEach((c) => {
        if (res[c.Id] != null) {
          c.Sort = res[c.Id];
        }
      });
      list.OrderBy("Sort");
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
    async load() {
      const res = await categoryApi.GetFullTree();
      this.maxSort[0] = res.length;
      this.initMaxSort(res, {
        Id: 0,
      });
      this.category = res;
      this.dataList = res;
    },
    reset() {
      this.chioseKey = null;
      this.dataList = this.category;
    },
    add(pid) {
      this.id = null;
      this.parentId = pid == null ? this.chioseKey : pid;
      this.visible = true;
    },
    closeEdit(isRefresh) {
      this.visible = false;
      if (isRefresh) {
        this.load();
      }
    },
    chioseBind(row) {
      this.id = row.Id;
      this.source = row;
      this.categoryId = row.Category.map((c) => c.Id);
      this.visibleBind = true;
    },
    closeBind(isRefresh, items) {
      this.visibleBind = false;
      if (isRefresh) {
        this.source.Category = items;
      }
    },
    edit(id) {
      this.id = id;
      this.parentId = null;
      this.visible = true;
    },
    chiose(e) {
      this.chioseKey = e.Id;
      this.title = e.CategoryName;
      this.dataList = [e];
    },
  },
};
</script>
    