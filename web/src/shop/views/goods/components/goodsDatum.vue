<template>
  <div>
    <el-form ref="goodsEdit" :model="goods" label-width="150px" :rules="rules">
      <el-card>
        <div slot="header">
          <span>基本信息</span>
        </div>
        <el-form-item label="SPU编号" prop="GoodsSpu">
          <el-input
            v-model="goods.GoodsSpu"
            maxlength="64"
            placeholder="SPU编号"
          />
        </el-form-item>
        <el-form-item label="商品名" prop="GoodsName">
          <el-input
            v-model="goods.GoodsName"
            maxlength="50"
            placeholder="商品名"
          />
        </el-form-item>
        <el-form-item label="商品类目" prop="CategoryId">
          <categorySelect
            v-model="goods.CategoryId"
            :is-chiose-end="true"
            placeholder="商品类目"
          />
        </el-form-item>
        <el-form-item label="品牌名称" prop="BrandName">
          <el-input
            v-model="goods.BrandName"
            maxlength="50"
            placeholder="品牌名称"
          />
        </el-form-item>
        <el-form-item label="商品封面图" prop="MainImg">
          <imageUpBtn
            v-model="goods.ImgFileId"
            file-key="goodsCoverImg"
            :multiple="false"
            :file-uri.sync="goods.MainImg"
            :link-biz-pk="id"
          />
        </el-form-item>
        <el-form-item label="应用的物流费用摸版" prop="LogisticsId">
          <logisticsSelect
            v-model="goods.LogisticsId"
            placeholder="应用的物流费用摸版"
          />
        </el-form-item>
        <el-form-item label="是否是虚拟商品" prop="IsVirtual">
          <el-switch v-model="goods.IsVirtual" />
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 20px">
        <div slot="header">
          <span>服务说明</span>
          <el-button
            style="float: right"
            type="success"
            @click="addShow"
          >添加说明</el-button>
        </div>
        <el-form-item
          v-for="(item, index) in goods.ServerList"
          :key="item.Title"
          :label="item.Title"
        >
          <el-row style="margin-bottom: 10px">
            <span style="padding-right: 5px">已图片形式展示: </span><el-switch v-model="item.IsImage" />
            <span style="padding-right: 5px; margin-left: 10px">排序位：</span>
            <el-input-number
              :min="1"
              :max="goods.ServerList.length"
              placeholder="排序位"
              :value="index + 1"
              style="margin-left: 20px; width: 200px"
              @change="(val) => setSort(index, val - 1)"
            />
            <el-button
              size="mini"
              type="danger"
              title="删除"
              icon="el-icon-delete"
              style="margin-left: 20px"
              circle
              @click="dropShow(index, item.Title)"
            />
          </el-row>
          <el-input
            v-if="item.IsImage == false"
            v-model="item.Description"
            maxlength="1000"
            type="textarea"
            :autosize="{ minRows: 2 }"
            :placeholder="item.Title"
          />
          <imageUpBtn
            v-else
            v-model="item.FileId"
            file-key="goodsServerShow"
            :tag="item.Title"
            :up-filelimit="3"
            :multiple="true"
            :file-uri.sync="item.ImgSrc"
            :link-biz-pk="id"
          />
        </el-form-item>
      </el-card>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >保存并下一步</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
    <el-dialog
      title="新增服务说明"
      :visible="visible"
      width="600px"
      :before-close="closeShow"
      :close-on-click-modal="false"
    >
      <el-form
        ref="addShow"
        :model="serverShow"
        label-width="130px"
        :rules="showRules"
      >
        <el-form-item label="标题" prop="Title">
          <el-input
            v-model="serverShow.Title"
            maxlength="50"
            placeholder="标题"
          />
        </el-form-item>
        <el-form-item label="已图片形式展示" prop="IsImage">
          <el-switch v-model="serverShow.IsImage" />
        </el-form-item>
        <el-row style="text-align: center; margin-top: 20px">
          <el-button
            :loading="showLoad"
            type="primary"
            @click="saveShow"
          >确定</el-button>
          <el-button @click="resetShow">重置</el-button>
        </el-row>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
import * as goodsApi from '@/shop/api/goods'
import categorySelect from '@/shop/components/category/categorySelect'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
import logisticsSelect from '@/shop/components/logistics/logisticsSelect.vue'
export default {
  components: {
    categorySelect,
    logisticsSelect,
    imageUpBtn
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      visible: false,
      showLoad: false,
      goods: {
        GoodsName: null,
        GoodsSpu: null,
        CategoryId: null,
        LogisticsId: null,
        IsVirtual: false,
        ServerList: []
      },
      serverShow: {
        IsImage: false,
        Title: null
      },
      loading: false,
      showRules: {
        Title: [
          {
            required: true,
            message: '标题不能为空！',
            trigger: 'blur'
          }
        ]
      },
      rules: {
        GoodsSpu: [
          {
            required: true,
            message: '商品SPU不能为空！',
            trigger: 'blur'
          }
        ],
        CategoryId: [
          {
            required: true,
            message: '请选择商品类目！',
            trigger: 'blur'
          }
        ],
        GoodsName: [
          {
            required: true,
            message: '商品名不能为空！',
            trigger: 'blur'
          },
          {
            min: 1,
            max: 50,
            message: '商品名长度在 2 到 50 个字之间',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {},
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    async reset() {
      this.loading = true
      if (this.id == null) {
        this.goods = {
          GoodsName: null,
          GoodsSpu: null,
          CategoryId: null,
          LogisticsId: null,
          BrandName: null,
          ImgFileId: null,
          MainImg: null,
          IsVirtual: false,
          ServerList: [
            {
              Title: '权利声明',
              Description: null,
              IsImage: false,
              FileId: null,
              ImgSrc: null
            },
            {
              Title: '售后服务',
              Description: null,
              IsImage: false,
              FileId: null,
              ImgSrc: null
            },
            {
              Title: '价格说明',
              Description: null,
              IsImage: false,
              FileId: null,
              ImgSrc: null
            }
          ]
        }
      } else {
        const res = await goodsApi.Get(this.id)
        this.goods = {
          GoodsName: res.GoodsName,
          GoodsSpu: res.GoodsSpu,
          CategoryId: res.CategoryId,
          MainImg: null,
          ImgFileId: null,
          LogisticsId: res.LogisticsId,
          BrandName: res.BrandName,
          IsVirtual: res.IsVirtual,
          ServerList:
            res.ServerShow == null
              ? []
              : res.ServerShow.map((c) => {
                return {
                  Title: c.Title,
                  Description: c.Description,
                  IsImage: c.ImgSrc != null,
                  ImgSrc: c.ImgSrc,
                  FileId: null
                }
              })
        }
      }
      this.loading = false
    },
    save() {
      const that = this
      this.$refs['goodsEdit'].validate((valid) => {
        if (valid) {
          if (that.id == null) {
            that.add()
          } else {
            that.set()
          }
        } else {
          return false
        }
      })
    },
    closeShow() {
      this.visible = false
    },
    setSort(index, val) {
      if (index === val) {
        return
      }
      const items = this.goods.ServerList
      const t = items[val]
      items[val] = items[index]
      items[index] = t
      this.goods.ServerList = items.concat()
    },
    dropShow(index, label) {
      const title = '确认删除服务说明：' + label + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.goods.ServerList.splice(index, 1)
      })
    },
    addShow() {
      this.serverShow.Title = null
      this.serverShow.IsImage = false
      this.showLoad = false
      this.visible = true
    },
    resetShow() {
      this.serverShow.Title = null
      this.serverShow.IsImage = false
    },
    saveShow() {
      const that = this
      this.$refs['addShow'].validate((valid) => {
        if (valid) {
          that.visible = false
          that.goods.ServerList.push({
            Title: that.serverShow.Title,
            Description: null,
            ImgSrc: null,
            FileId: null,
            IsImage: that.serverShow.IsImage
          })
        } else {
          return false
        }
      })
    },
    format() {
      const data = {
        GoodsName: this.goods.GoodsName,
        GoodsSpu: this.goods.GoodsSpu,
        CategoryId: this.goods.CategoryId,
        LogisticsId: this.goods.LogisticsId,
        IsVirtual: this.goods.IsVirtual,
        BrandName: this.goods.BrandName
      }
      const list = []
      let fileId = []
      this.goods.ServerList.forEach((c) => {
        if (c.IsImage && c.ImgSrc != null && c.ImgSrc.length > 0) {
          list.push({
            Title: c.Title,
            ImgSrc: c.ImgSrc
          })
          fileId = fileId.concat(c.FileId)
        } else if (c.IsImage === false && !c.Description.isNull()) {
          list.push({
            Title: c.Title,
            Description: c.Description
          })
        }
      })
      if (this.goods.ImgFileId && this.goods.ImgFileId.length > 0) {
        fileId.push(this.goods.ImgFileId[0])
        data.MainImg = this.goods.MainImg[0]
      }
      data.ServerShow = list
      data.FileId = fileId
      return data
    },
    async add() {
      this.loading = true
      const data = this.format()
      try {
        const id = await goodsApi.Add(data)
        this.$message({
          type: 'success',
          message: '添加成功!'
        })
        data.Id = id
        this.$emit('next', data)
      } catch {
        this.loading = false
      }
    },
    async set() {
      this.loading = true
      const data = this.format()
      try {
        await goodsApi.Set(this.id, data)
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        data.Id = this.id
        this.$emit('next', data)
      } catch {
        this.loading = false
      }
    }
  }
}
</script>
