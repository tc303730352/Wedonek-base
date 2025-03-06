<template>
  <el-card>
    <div slot="header">
      <span>物流费用明细</span>
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
        <el-form-item label="是否启用">
          <el-select
            v-model="queryParam.IsEnable"
            placeholder="是否启用"
            @change="load"
          >
            <el-option label="全部" :value="null">全部</el-option>
            <el-option label="启用" :value="true">启用</el-option>
            <el-option label="未启用" :value="false">未启用</el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
          <el-button type="success" @click="add">添加物流明细</el-button>
          <el-button type="info" @click="calaulatePrice">计算物流费用</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table :data="itemList" :isPaging="false" :columns="columns" rowKey="Id">
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          v-model="e.row.IsEnable"
          @change="setIsEnabe(e.row)"
        ></el-switch>
      </template>
      <template slot="AreaType" slot-scope="e">
        {{ AreaType[e.row.AreaType].text }}
      </template>
      <template slot="ValuationMode" slot-scope="e">
        {{ ValuationMode[e.row.ValuationMode].text }}
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
    <editLConfig
      :visible="visible"
      :templateId="templateId"
      :id="id"
      @close="closeEdit"
    ></editLConfig>
    <calaulate
      :visible="visiblePrice"
      :templateId="templateId"
      @close="visiblePrice = false"
    ></calaulate>
  </el-card>
</template>
  
  
  <script>
import * as logisticsApi from "../../api/logisticsConfig";
import { AreaType, ValuationMode } from "@/shop/config/shopConfig";
import editLConfig from "./components/editLogisticsConfig";
import calaulate from "./components/calculatePrice.vue";
export default {
  computed: {},
  data() {
    return {
      AreaType,
      ValuationMode,
      itemList: [],
      visible: false,
      area: {},
      id: null,
      templateId: null,
      visiblePrice: false,
      columns: [
        {
          key: "AreaName",
          title: "地区名",
          align: "center",
          width: 150,
        },
        {
          key: "AreaType",
          title: "地区类型",
          align: "center",
          slotName: "AreaType",
          width: 120,
        },
        {
          key: "ValuationMode",
          title: "计费方式",
          align: "center",
          slotName: "ValuationMode",
          width: 100,
        },
        {
          key: "IsEnable",
          title: "是否启用",
          align: "center",
          slotName: "IsEnable",
          width: 100,
        },
        {
          key: "LogisticsPrice",
          title: "物流费用",
          align: "right",
          minWidth: 100,
        },
        {
          key: "FirstVal",
          title: "起始值",
          align: "right",
          minWidth: 100,
        },
        {
          key: "ContinueVal",
          title: "续",
          align: "right",
          minWidth: 100,
        },
        {
          key: "ContinuePrice",
          title: "续费用",
          align: "right",
          minWidth: 100,
        },
        {
          key: "FreeValue",
          title: "免物流费最低值",
          align: "right",
          minWidth: 130,
        },
        {
          key: "FreePrice",
          title: "免物流费最低金额",
          align: "right",
          minWidth: 130,
        },
        {
          key: "CcValue",
          title: "立方厘米比例值",
          align: "right",
          minWidth: 130,
        },
        {
          key: "Remark",
          title: "备注",
          align: "left",
          minWidth: 150,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          width: 150,
          slotName: "action",
        },
      ],
      queryParam: {
        QueryKey: null,
        TemplateId: null,
        IsEnable: null,
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
    editLConfig,
    calaulate,
  },
  mounted() {
    this.templateId = this.$route.params.id;
    this.queryParam.TemplateId = this.templateId;
    this.load();
  },
  methods: {
    calaulatePrice() {
      this.visiblePrice = true;
    },
    async setIsEnabe(row) {
      await logisticsApi.SetIsEnable(row.Id, row.IsEnable);
    },
    drop(row) {
      const title = "确认删除该物流费用明细?";
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
      const list = await logisticsApi.Gets(this.queryParam);
      this.itemList = list;
    },
    reset() {
      this.queryParam.QueryKey = null;
      this.queryParam.IsEnable = null;
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
  