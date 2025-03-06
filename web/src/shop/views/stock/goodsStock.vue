<template>
  <leftRightSplit :leftSpan="4">
    <categoryTree
      slot="left"
      :isChioseEnd="false"
      :isAutoChiose="false"
      @change="change"
    ></categoryTree>
    <el-card slot="right">
      <div slot="header">
        <span>{{ title }}</span>
      </div>
      <el-row>
        <el-form :inline="true" :model="queryParam">
          <el-form-item label="关键字">
            <el-input
              v-model="queryParam.QueryKey"
              placeholder="商品名\SPU"
              @change="load"
            />
          </el-form-item>
          <el-form-item label="库存量">
            <el-input
              v-model="queryParam.Stock"
              placeholder="库存量"
              type="number"
              :min="0"
              :step="1"
              @change="load"
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-button @click="reset">重置</el-button>
          </el-form-item>
        </el-form>
      </el-row>
      <w-table
        :data="dataList"
        :isPaging="true"
        :columns="columns"
        :paging="paging"
        @load="load"
        rowKey="SkuId"
      >
        <template slot="LastOutTime" slot-scope="e">
          {{
            e.row.LastOutTime
              ? moment(e.row.LastOutTime).format("YYYY-MM-dd HH:mm")
              : null
          }}
        </template>
        <template slot="SkuImg" slot-scope="e">
          <el-image
            v-if="e.row.SkuImg"
            style="width: 80px"
            :src="e.row.SkuImg"
          ></el-image>
        </template>
        <template slot="GoodsName" slot-scope="e">
          <el-button type="text" @click="show(e.row)">{{
            e.row.GoodsName
          }}</el-button>
        </template>
        <template slot="SkuStock" slot-scope="e">
          <span
            :style="{
              color: getColor(e.row.CurStock),
              fontSize: '16px',
              fontWeight: 700,
            }"
            >{{ e.row.CurStock }}</span
          >
          <span> / </span>
          <span style="color: #1890ff">{{ e.row.OutStock }}</span>
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            size="mini"
            type="primary"
            title="添加库存"
            icon="el-icon-plus"
            @click="add(e.row)"
            circle
          ></el-button>
        </template>
      </w-table>
      <addStock :visible="visible" :skuName="skuName" :skuId="skuId" @close="closeAdd"></addStock>
    </el-card>
  </leftRightSplit>
</template>

 
<script>
import moment from "moment";
import { Query } from "../../api/goodsStock";
import categoryTree from "../../components/category/categoryTree.vue";
import addStock from './components/addStock.vue'
export default {
  computed: {},
  data() {
    return {
      dataList: [],
      title: null,
      skuId: null,
      skuName: null,
      visible: false,
      columns: [
        {
          key: "GoodsName",
          title: "商品名",
          align: "left",
          isMerge: true,
          resizable: false,
          slotName: "GoodsName",
          width: 150,
        },
        {
          key: "SkuImg",
          title: "封面图",
          align: "center",
          width: 120,
          slotName: "SkuImg",
        },
        {
          key: "SkuName",
          title: "规格名",
          align: "left",
          minWidth: 300,
        },
        {
          key: "SpecsShow",
          title: "规格说明",
          align: "left",
          width: 255,
        },
        {
          key: "SkuStock",
          title: "库存(剩余/已出售)",
          align: "center",
          slotName: "SkuStock",
          width: 150,
        },
        {
          key: "LastOutTime",
          title: "最后出库时间",
          align: "left",
          slotName: "LastOutTime",
          minWidth: 120,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          minWidth: 150,
          slotName: "action",
        },
      ],
      queryParam: {
        QueryKey: null,
        CategoryId: null,
        Lvl: null,
        Stock: null,
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
    categoryTree,
    addStock
  },
  mounted() {
    this.title = "所有商品库存";
    this.load();
  },
  methods: {
    moment,
    closeAdd(isRefresh) {
      this.visible = false
      if(isRefresh) {
        this.load()
      }
    },
    show(row) {
      this.$router.push({ name: "goodsDetailed", params: { id: row.GoodsId } });
    },
    add(row) {
      this.skuId = row.SkuId
      this.skuName = row.SkuName
      this.visible = true
    },
    change(category) {
      this.title = category.Name + "-商品库存";
      this.queryParam.CategoryId = category.Id;
      this.queryParam.Lvl = category.Lvl;
      this.load();
    },
    async load() {
      const res = await Query(this.queryParam, this.paging);
      if (res.List) {
        this.dataList = this.format(res.List);
      } else {
        this.dataList = [];
      }
      this.paging.Total = res.Count;
    },
    reset() {
      this.queryParam.QueryKey = null;
      this.queryParam.CategoryId = null;
      this.queryParam.Status = null;
      this.queryParam.Lvl = null;
      this.queryParam.Stock = null;
      this.paging.Index = 1;
      this.load();
    },
    getColor(num) {
      if (num >= 200) {
        return "#43AF2B";
      } else if (num >= 100) {
        return "#F3A70F";
      } else {
        return "#e4393c";
      }
    },
    format(datas) {
      const rows = [];
      datas.forEach((e) => {
        const index = rows.length;
        e.SkuStock.forEach((a) => {
          a.GoodsId = e.Id;
          a.GoodsSpu = e.GoodsSpu;
          a.GoodsName = e.GoodsName;
          a.rowSpan = {
            GoodsName: 0,
          };
          rows.push(a);
        });
        rows[index].rowSpan.GoodsName = e.SkuStock.length;
      });
      return rows;
    },
  },
};
</script>