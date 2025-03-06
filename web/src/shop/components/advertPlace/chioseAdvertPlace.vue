<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <div>
      <w-table
        :data="dataList"
        :is-paging="false"
        :columns="columns"
        :is-select="true"
        :is-multiple="isMultiple"
        :select-keys="selectKeys"
        row-key="Id"
        @selected="selectEvent"
        @load="load"
      >
        <template slot="ExampleImg" slot-scope="e">
          <el-image
            v-if="e.row.ExampleImg"
            style="max-width: 200px"
            :src="e.row.ExampleImg"
            fit="contain"
          />
        </template>
      </w-table>
    </div>
    <el-row style="text-align: center;margin-top: 10px;">
      <el-button type="success" @click="save">确认选择</el-button>
      <el-button type="default" @click="handleClose">关闭</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import { GetItems } from '@/shop/api/advertPlace'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    placeType: {
      type: String,
      default: '001'
    },
    useRange: {
      type: Number,
      default: 1
    },
    status: {
      type: Number,
      default: 1
    },
    placeId: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      dataList: [],
      isNull: false,
      selectKeys: [],
      title: '选择广告位',
      columns: [
        {
          key: 'ExampleImg',
          title: '示例图',
          align: 'center',
          slotName: 'ExampleImg',
          width: 200
        },
        {
          key: 'PlaceTitle',
          title: '广告位标题',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'ImgShow',
          title: '图片说明',
          align: 'left',
          minWidth: 255
        },
        {
          key: 'ImgNum',
          title: '图片数',
          align: 'center',
          minWidth: 100
        }
      ]
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    handleClose() {
      this.$emit('close')
    },
    selectEvent(e) {
      this.selectKeys = e.keys
    },
    async load() {
      this.dataList = await GetItems({
        PlaceType: this.placeType,
        UseRange: this.useRange,
        PlaceStatus: this.status
      })
      this.isNull = this.dataList.length === 0
    },
    save() {
      if (this.selectKeys.length === 0) {
        this.$message({
          message: '请选择广告位!',
          type: 'error'
        })
        return
      }
      this.$emit('save', {
        keys: this.selectKeys,
        place: this.dataList.filter(c => this.selectKeys.includes(c.Id))
      })
    },
    reset() {
      if (this.placeId == null || this.placeId.length === 0) {
        this.selectKeys = []
      } else {
        this.selectKeys = this.placeId
      }
      this.load()
    }
  }
}
</script>
