<template>
  <el-card>
    <div slot="header">
      <span>物流费用摸版</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="关键字">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="摸版名"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
          <el-button type="success" @click="add">添加物流费用</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="dataList"
      :isPaging="true"
      :columns="columns"
      :paging="paging"
      @load="load"
      rowKey="Id"
    >
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          v-model="e.row.IsEnable"
          :disabled="e.row.IsDefault"
          @change="setIsEnabe(e.row)"
        ></el-switch>
      </template>
      <template slot="IsDefault" slot-scope="e">
        <el-switch
          v-model="e.row.IsDefault"
          :disabled="paging.Total == 1"
          @change="setIsDefault(e.row)"
        ></el-switch>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="明细"
          icon="el-icon-view"
          @click="showItem(e.row.Id)"
          circle
        ></el-button>
        <el-button
          size="mini"
          type="primary"
          title="计算物流费用"
          icon="el-icon-money"
          @click="calaulatePrice(e.row.Id)"
          circle
        ></el-button>
        <el-button
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
    <editLogistics
      :visible="visible"
      @close="closeEdit"
      :id="id"
    ></editLogistics>
    <calaulate
      :visible="visiblePrice"
      :templateId="id"
      @close="visiblePrice = false"
    ></calaulate>
  </el-card>
</template>


<script>
import * as logisticsApi from "../../api/logistics";
import editLogistics from "./components/editLogistics.vue";
import calaulate from "./components/calculatePrice.vue";
export default {
  computed: {},
  data() {
    return {
      dataList: [],
      visible: false,
      id: null,
      visiblePrice: false,
      columns: [
        {
          key: "TemplateName",
          title: "摸版名",
          align: "left",
          minWidth: 150,
          fixed: "left",
        },
        {
          key: "IsEnable",
          title: "是否启用",
          align: "center",
          slotName: "IsEnable",
          width: 130,
        },
        {
          key: "IsDefault",
          title: "是否默认",
          align: "center",
          slotName: "IsDefault",
          width: 130,
        },
        {
          key: "Remark",
          title: "备注",
          align: "left",
          minWidth: 200,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          minWidth: 120,
          slotName: "action",
        },
      ],
      queryParam: {
        QueryKey: null,
      },
      paging: {
        Size: 20,
        Index: 1,
        SortName: "Id",
        IsDesc: false,
        Total: 0,
      },
    };
  },
  components: {
    editLogistics,
    calaulate,
  },
  mounted() {
    this.load();
  },
  methods: {
    showItem(id) {
      this.$router.push({ name: "logisticsItem", params: { id } });
    },
    calaulatePrice(id) {
      this.id = id
      this.visiblePrice = true;
    },
    async setIsEnabe(row) {
      await logisticsApi.SetIsEnable(row.Id, row.IsEnable);
    },
    async setIsDefault(row) {
      const cancelId = await logisticsApi.SetIsDefault(row.Id, row.IsDefault);
      if (cancelId != 0) {
        const item = this.dataList.find((a) => a.Id == cancelId);
        if (item != null) {
          item.IsDefault = false;
        }
      }
    },
    drop(row) {
      const title = "确认删除该物流费用摸版：" + row.TemplateName + "?";
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
      await logisticsApi.Delete(row.Id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      this.load();
    },
    async load() {
      const res = await logisticsApi.Query(this.queryParam, this.paging);
      if (res.List) {
        this.dataList = res.List;
      } else {
        this.dataList = [];
      }
      this.paging.Total = res.Count;
    },
    reset() {
      this.queryParam.QueryKey = null;
      this.paging.Index = 1;
      this.load();
    },
    add() {
      this.id = null;
      this.visible = true;
    },
    closeEdit(isRefresh) {
      this.visible = false;
      if (isRefresh) {
        this.load();
      }
    },
    edit(id) {
      this.id = id;
      this.visible = true;
    },
  },
};
</script>
    