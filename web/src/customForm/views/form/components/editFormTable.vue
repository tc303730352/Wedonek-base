<template>
  <el-dialog
    :title="title"
    :visible.sync="visible"
    width="800px"
    :close-on-click-modal="false"
    :before-close="handleClose"
  >
    <el-form ref="tableEdit" :model="table" :rules="rules" label-width="120px">
      <el-form-item label="标题" prop="Title">
        <el-input v-model="table.Title" :maxlength="50" placeholder="标题" />
      </el-form-item>
      <el-form-item label="是否隐藏标题" prop="IsHidden">
        <el-switch v-model="table.IsHidden" active-text="隐藏" inactive-text="显示" />
      </el-form-item>
      <el-form-item v-if="tableType ==0" label="每行列数" prop="ColNum">
        <el-input-number
          v-model="table.ColNum"
          :precision="0"
          :min="1"
          :max="6"
          placeholder="每行列数"
        />
      </el-form-item>
      <el-form-item v-if="tableType ==0" label="Label宽度" prop="LabelWidth">
        <el-input-number
          v-model="table.LabelWidth"
          :precision="0"
          :min="80"
          :max="200"
          placeholder="每行列数"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="reset">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as tableApi from '@/customForm/api/table'
export default {
  components: {
  },
  props: {
    visible: {
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
      title: '编辑表单信息',
      table: {},
      rules: {
        Title: [
          {
            required: true,
            message: '标题不能为空！',
            trigger: 'blur'
          }
        ]
      },
      tableType: 0
    }
  },
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
    async reset() {
      const res = await tableApi.Get(this.id)
      this.tableType = res.TableType
      this.table = {
        Title: res.Title,
        IsHidden: res.IsHidden,
        ColNum: res.ColNum,
        LabelWidth: res.LabelWidth,
        TableSet: res.TableSet
      }
    },
    handleClose() {
      this.$emit('update:visible', false)
      this.$emit('close', false)
    },
    async set() {
      const isSet = await tableApi.Set(this.id, this.table)
      this.$emit('update:visible', false)
      this.$emit('close', isSet, this.table)
    },
    save() {
      this.$refs['tableEdit'].validate((valid) => {
        if (valid) {
          this.set()
        } else {
          return false
        }
      })
    }
  }
}
</script>
