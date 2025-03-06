<template>
  <el-dialog
    title="编辑关联的类目"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form label-width="100px">
      <el-form-item label="关联的类目">
        <categorySelect
          v-model="ids"
          :is-multiple="true"
          placeholder="关联的类目"
          @change="bindChange"
        />
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
import * as categoryApi from '@/shop/api/frontCategory'
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
      type: Array,
      default: null
    }
  },
  data() {
    return {
      ids: [],
      loading: false
    }
  },
  computed: {},
  watch: {
    categoryId: {
      handler(val) {
        this.ids = val
      },
      immediate: true
    }
  },
  methods: {
    closeForm() {
      this.$emit('close', false)
    },
    bindChange(e) {
      console.log(e)
      this.category = e.category
    },
    reset() {
      this.ids = this.categoryId
    },
    async save() {
      await categoryApi.SetBind(this.id, this.ids)
      this.$message({
        type: 'success',
        message: '设置成功!'
      })
      const list = this.category.map((c) => {
        return {
          Id: c.Id,
          Name: c.CategoryName
        }
      })
      this.$emit('close', true, list)
    }
  }
}
</script>
