<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="specGroupEdit"
      :model="specGroup"
      label-width="100px"
      :rules="rules"
    >
      <el-form-item label="规格组名" prop="GroupName">
        <el-input
          v-model="specGroup.GroupName"
          maxlength="50"
          placeholder="规格组名"
        />
      </el-form-item>
      <el-form-item label="所属类目" prop="CategoryId">
        <categorySelect
          v-model="specGroup.CategoryId"
          placeholder="所属类目"
        />
      </el-form-item>
      <el-form-item label="是否启用" prop="IsEnable">
        <el-switch v-model="specGroup.IsEnable" />
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
import categorySelect from '@/shop/components/category/categorySelect'
import * as specApi from '@/shop/api/specGroupTemplate'
export default {
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
    categoryId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '编辑类目',
      specGroup: {
        CategoryId: null
      },
      loading: false,
      rules: {
        GroupName: [
          {
            required: true,
            message: '规格组名不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 50,
            message: '规格组名长度在 2 到 50 个字之间',
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
        this.title = '新建规格组摸版'
        this.fileId = []
        this.icon = null
        this.specGroup = {
          CategoryId: this.categoryId,
          GroupName: null,
          IsEnable: false
        }
      } else {
        const res = await specApi.Get(this.id)
        this.specGroup = res
        this.title = '编辑规格组(' + res.GroupName + ')摸版'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['specGroupEdit'].validate((valid) => {
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
      await specApi.Add(this.specGroup)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await specApi.Set(this.id, {
        GroupName: this.specGroup.GroupName,
        IsEnable: this.specGroup.IsEnable,
        CategoryId: this.specGroup.CategoryId
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
