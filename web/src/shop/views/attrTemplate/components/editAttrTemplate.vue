<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="attrEdit" :model="attr" label-width="100px" :rules="rules">
      <el-form-item label="规格参数名" prop="Name">
        <el-input v-model="attr.Name" maxlength="50" placeholder="规格参数名" />
      </el-form-item>
      <el-form-item label="默认值" prop="DefValue">
        <el-input
          v-model="attr.DefValue"
          maxlength="100"
          placeholder="默认值"
        />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import * as attrApi from '@/shop/api/attrTemplate'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    },
    categoryId: {
      type: String,
      default: null
    },
    parentId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑规格参数',
      attr: {
        Name: null,
        DefValue: null
      },
      loading: false,
      rules: {
        Name: [
          {
            required: true,
            message: '规格参数名不能为空！',
            trigger: 'blur'
          },
          {
            min: 1,
            max: 50,
            message: '规格参数名长度在 1 到 50 个字之间',
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
    async reset() {
      this.loading = false
      if (this.id == null) {
        this.title = '新建规格参数摸版'
        this.fileId = []
        this.icon = null
        this.attr = {
          Name: null,
          DefValue: null
        }
      } else {
        const res = await attrApi.Get(this.id)
        this.attr = res
        this.title = '编辑规格参数(' + res.Name + ')摸版'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['attrEdit'].validate((valid) => {
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
      this.attr.ParentId = this.parentId
      this.attr.CategoryId = this.categoryId
      await attrApi.Add(this.attr)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await attrApi.Set(this.id, {
        Name: this.attr.Name,
        DefValue: this.attr.DefValue
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
