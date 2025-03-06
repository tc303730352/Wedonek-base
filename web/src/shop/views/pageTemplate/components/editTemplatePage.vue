<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="editTemplate"
      :model="templatePage"
      label-width="120px"
      :rules="rules"
    >
      <el-form-item label="页面名称" prop="TemplateName">
        <el-input
          v-model="templatePage.TemplateName"
          maxlength="20"
          placeholder="页面名称"
        />
      </el-form-item>
      <el-form-item label="页面说明" prop="Show">
        <el-input
          v-model="templatePage.Show"
          maxlength="100"
          placeholder="页面说明"
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
import * as pageApi from '@/shop/api/page'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    range: {
      type: Number,
      default: null
    },
    place: {
      type: String,
      default: null
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑页面',
      templatePage: {
        TemplateName: null,
        Show: null
      },
      loading: false,
      rules: {
        TemplateName: [
          {
            required: true,
            message: '页面名称不能为空！',
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
      if (this.id == null) {
        this.title = '新增页面'
        this.templatePage = {
          TemplateName: null,
          Show: null,
          TemplatePlace: this.place,
          UseRange: this.range
        }
      } else {
        const res = await pageApi.Get(this.id)
        this.templatePage = {
          TemplateName: res.TemplateName,
          Show: res.Show
        }
        this.title = '编辑页面(' + res.TemplateName + ')资料'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['editTemplate'].validate((valid) => {
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
      await pageApi.Add(this.templatePage)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await pageApi.Set(this.id, this.templatePage)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
