<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :modal="false"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="specEdit" :model="spec" label-width="100px" :rules="rules">
      <el-form-item label="规格名" prop="SpecName">
        <el-input v-model="spec.SpecName" maxlength="50" placeholder="规格名" />
      </el-form-item>
      <el-form-item label="图标" prop="SpecsIcon">
        <imageUpBtn
          v-model="fileId"
          file-key="SpecIcon"
          :link-biz-pk="id"
          @change="upComplete"
        />
      </el-form-item>
      <el-form-item label="是否启用" prop="IsEnable">
        <el-switch v-model="spec.IsEnable" />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button :loading="loading" type="primary" @click="save">保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import * as specApi from '@/shop/api/specTemplate'
import imageUpBtn from '@/components/fileUp/imageUpBtn'
export default {
  components: {
    imageUpBtn
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    },
    templateId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑规格',
      spec: {},
      loading: false,
      empId: null,
      fileId: null,
      icon: null,
      rules: {
        SpecName: [
          {
            required: true,
            message: '规格名不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 50,
            message: '规格名长度在 2 到 50 个字之间',
            trigger: 'blur'
          }
        ]
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
    upComplete(fileId, files) {
      if (files != null && files.length > 0) {
        this.icon = files[0].FileUri
      } else {
        this.icon = null
      }
    },
    async reset() {
      if (this.id == null) {
        this.title = '新建规格'
        this.fileId = []
        this.icon = null
        this.spec = {
          SpecName: null,
          IsEnable: false
        }
      } else {
        const res = await specApi.Get(this.id)
        this.spec = res
        this.icon = res.Icon
        this.title = '编辑规格'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['specEdit'].validate((valid) => {
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
    async add() {
      this.loading = true
      this.spec.SpecsIcon = this.icon
      this.spec.TemplateId = this.templateId
      this.spec.FileId = this.fileId.length == 0 ? null : this.fileId[0]
      await specApi.Add(this.spec)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await specApi.Set(this.id, {
        SpecsIcon: this.icon,
        SpecName: this.spec.SpecName,
        IsEnable: this.spec.IsEnable
      })
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
