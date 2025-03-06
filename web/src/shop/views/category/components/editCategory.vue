<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="categoryEdit"
      :model="category"
      label-width="100px"
      :rules="rules"
    >
      <el-form-item label="类目名" prop="CategoryName">
        <el-input
          v-model="category.CategoryName"
          maxlength="100"
          placeholder="类目名"
        />
      </el-form-item>
      <el-form-item v-if="id == null" label="所属上级" prop="ParentId">
        <categorySelect
          v-model="category.ParentId"
          placeholder="所属上级"
        />
      </el-form-item>

      <el-form-item label="是否启用" prop="IsEnable">
        <el-switch v-model="category.IsEnable" />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button :loading="loading" type="primary" @click="save">保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import categorySelect from '@/shop/components/category/categorySelect'
import * as categoryApi from '@/shop/api/category'
export default {
  name: 'EditCategory',
  components: {
    categorySelect
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
    parentId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑类目',
      category: {},
      loading: false,
      empId: null,
      rules: {
        CategoryName: [
          {
            required: true,
            message: '类目名不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 50,
            message: '类目名长度在 2 到 50 个字之间',
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
        this.title = '新建类目'
        this.category = {
          ParentId: this.parentId,
          CategoryName: null,
          IsEnable: false
        }
      } else {
        const res = await categoryApi.Get(this.id)
        this.category = res
        this.title = '编辑类目'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['categoryEdit'].validate((valid) => {
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
      await categoryApi.Add(this.category)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await categoryApi.Set(this.id, this.category)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
