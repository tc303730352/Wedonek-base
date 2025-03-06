<template>
  <el-dialog
    title="编辑规格信息"
    :visible="visible"
    width="600px"
    :before-close="close"
    :close-on-click-modal="false"
  >
    <el-form
      ref="specEdit"
      :model="editSpec"
      label-width="130px"
      :rules="rules"
    >
      <el-form-item label="规格项名称" prop="SpecName">
        <el-input
          v-model="editSpec.SpecName"
          maxlength="50"
          placeholder="规格项名称"
        />
      </el-form-item>
      <el-form-item label="图标" prop="SpecIcon">
        <imageUpBtn
          v-model="editSpec.FileId"
          file-key="GoodsSpecIcon"
          :file-uri.sync="editSpec.SpecIcon"
          :link-biz-pk="editSpec.Id"
        />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >确定</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import { SetSpec, AddSpec } from '@/shop/api/goodsSpec'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
export default {
  components: {
    imageUpBtn
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    goodsId: {
      type: String,
      default: null
    },
    spec: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      loading: false,
      rules: {
        SpecName: [
          {
            required: true,
            message: '规格名不能为空！',
            trigger: 'blur'
          }
        ]
      },
      editSpec: {
        Id: null,
        SpecName: null,
        FileId: null,
        SpecIcon: null
      }
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
  methods: {
    reset() {
      this.editSpec.SpecIcon = []
      this.editSpec.FileId = []
      this.editSpec.SpecName = this.spec.SpecName
      this.editSpec.Id = this.spec.Id
    },
    save() {
      const that = this
      this.$refs['specEdit'].validate((valid) => {
        if (valid) {
          that.loading = true
          if (that.spec.Id == null) {
            that.add()
          } else {
            that.set()
          }
        } else {
          return false
        }
      })
    },
    close() {
      this.$emit('close')
    },
    async add() {
      const data = {
        SpecName: this.editSpec.SpecName,
        GoodsId: this.goodsId,
        GroupId: this.spec.GroupId
      }
      if (this.editSpec.SpecIcon.length > 0) {
        data.SpecIcon = this.editSpec.SpecIcon[0]
        data.FileId = this.editSpec.FileId[0]
      }
      const item = await AddSpec(data)
      this.loading = false
      item.GroupId = this.spec.GroupId
      this.$emit('save', {
        type: 'add',
        data: item
      })
    },
    async set() {
      const data = {
        SpecName: this.editSpec.SpecName
      }
      if (this.editSpec.SpecIcon.length > 0) {
        data.SpecIcon = this.editSpec.SpecIcon[0]
        data.FileId = this.editSpec.FileId[0]
      }
      const isSet = await SetSpec(this.spec.Id, data)
      data.Id = this.spec.Id
      data.GroupId = this.spec.GroupId
      this.loading = false
      this.$emit('save', {
        type: 'set',
        isUpdate: isSet,
        data
      })
    }
  }
}
</script>
<style type="less" scoped>
</style>
