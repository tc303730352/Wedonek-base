<template>
  <el-dialog
    title="选择商品"
    :visible="visible"
    width="800"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <leftRightSplit :left-span="4">
      <categoryTree
        slot="left"
        :is-chiose-end="false"
        :is-auto-chiose="false"
        @change="change"
      />
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
            <el-form-item>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <w-table
          :data="dataList"
          :is-paging="true"
          :columns="columns"
          :paging="paging"
          :is-select="true"
          :is-multiple="isMultiple"
          :select-keys="selectKeys"
          row-key="Id"
          @selected="selectEvent"
          @load="load"
        >
          <template slot="IsVirtual" slot-scope="e">
            {{ e.row.IsVirtual ? "是" : "否" }}
          </template>
          <template slot="GoodsName" slot-scope="e">
            <div class="goods">
              <div v-if="e.row.MainImg" class="img">
                <el-image :src="e.row.MainImg" class="cover" />
              </div>
              <div class="content">
                <div class="item" :title="e.row.GoodsName" @click="show(e.row)">
                  {{
                    e.row.GoodsName
                  }}
                </div>
                <div class="spuNo">SPU：{{ e.row.GoodsSpu }}</div>
              </div>
            </div>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <el-row :gutter="24">
      <el-col
        :span="2"
        style="text-align: right; line-height: 30px"
      >已选:</el-col>
      <el-col :span="20" style="line-height: 30px">
        <el-tag
          v-for="spu in selectSpu"
          :key="spu.Id"
          :disable-transitions="true"
          style="margin-bottom: 5px; margin-right: 5px"
          closable
          @close="dropSpu(spu)"
        >
          {{ spu.GoodsName }}
        </el-tag>
      </el-col>
    </el-row>
    <el-row style="text-align: center">
      <el-button type="success" @click="save">确认选择</el-button>
      <el-button type="default" @click="handleClose">关闭</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import * as goodsApi from '@/shop/api/goods'
import categoryTree from '../../components/category/categoryTree.vue'
export default {
  components: {
    categoryTree
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    spuId: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      dataList: [],
      selectKeys: [],
      selectSpu: [],
      spuCache: {},
      title: '选择商品',
      isInit: false,
      columns: [
        {
          key: 'GoodsName',
          title: '商品名',
          align: 'left',
          slotName: 'GoodsName',
          width: 300,
          fixed: 'left'
        },
        {
          key: 'CategoryName',
          title: '所属类目',
          align: 'center',
          minWidth: 130
        },
        {
          key: 'SkuNum',
          title: '拥有的SKU数量',
          align: 'right',
          minWidth: 100
        },
        {
          key: 'IsVirtual',
          title: '是否为虚拟物品',
          slotName: 'IsVirtual',
          align: 'center',
          minWidth: 114
        }
      ],
      queryParam: {
        QueryKey: null,
        CategoryId: null,
        Lvl: null
      },
      paging: {
        Size: 5,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.resetAll()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    change(category) {
      this.title = category.Name + '-商品列表'
      this.queryParam.CategoryId = category.Id
      this.queryParam.Lvl = category.Lvl
      this.load()
    },
    handleClose() {
      this.$emit('close')
    },
    dropSpu(spu) {
      this.selectSpu = this.selectSpu.filter((c) => c.Id !== spu.Id)
      this.selectKeys = this.selectKeys.filter((c) => c !== spu.Id)
    },
    selectEvent(e) {
      this.selectKeys = e.keys
      e.list.forEach((c) => {
        this.spuCache[c.Id] = c
      })
      this.selectSpu = e.keys.map((c) => this.spuCache[c])
    },
    async load() {
      const res = await goodsApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.dataList = res.List
        res.List.forEach(c => {
          this.spuCache[c.Id] = c
        })
      } else {
        this.dataList = []
      }
      this.paging.Total = res.Count
      this.initData()
    },
    initData() {
      if (this.isInit) {
        return
      }
      this.isInit = true
      const ids = this.selectKeys.filter(c => this.spuCache[c] == null)
      if (ids.length > 0) {
        this.loadSpu(ids)
      } else {
        this.selectSpu = this.selectKeys.map(c => this.spuCache[c])
      }
    },
    save() {
      if (this.selectKeys.length === 0) {
        this.$message({
          message: '请选择商品!',
          type: 'error'
        })
        return
      }
      this.$emit('save', {
        keys: this.selectKeys,
        spus: this.selectSpu
      })
    },
    async loadSpu(ids) {
      const spus = await goodsApi.Gets(ids)
      if (spus && spus.length > 0) {
        spus.forEach((c) => {
          this.spuCache[c.Id] = c
        })
        this.selectSpu = this.selectKeys.map(c => this.spuCache[c])
      }
    },
    resetAll() {
      if (this.spuId == null || this.spuId.length === 0) {
        this.selectKeys = []
        this.selectSpu = []
      } else {
        this.selectKeys = this.spuId
      }
      this.isInit = false
      this.reset()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.CategoryId = null
      this.queryParam.Lvl = null
      this.paging.Index = 1
      this.load()
    }
  }
}
</script>

<style  scoped>
.goods {
  width: 100%;
  height: 80px;
}
.goods .img {
  width: 80px;
  float: left;
  height: 80px;
  text-align: center;
}
.goods .cover {
  width: 100%;
  max-height: 80px;
}
.goods .content {
  display: flow-root;
  text-align: left;
}
.goods .content .item {
  margin: 0;
  padding-left: 5px;
  color: #1890ff;
  font-size: 16px;
  cursor: pointer;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: break-spaces;
  line-height: 20px;
  max-height: 40px;
}

.goods .content .spuNo {
  padding-left: 10px;
  margin: 0;
  color: #82848a;
  font-size: 12px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}
.goods .content i {
  font-weight: 600;
}
</style>
