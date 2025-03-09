<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="500px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="dicEdit" :model="dic" :rules="rules">
      <el-form-item label="字典名" prop="DicName">
        <el-input v-model="dic.DicName" maxlength="100" placeholder="字典名" />
      </el-form-item>
      <el-form-item label="说明" prop="Remark">
        <el-input
          v-model="dic.Remark"
          type="textarea"
          maxlength="255"
          placeholder="说明"
        />
      </el-form-item>
      <el-form-item label="是否是树形字典">
        <el-switch
          v-model="dic.IsTreeDic"
          :disabled="id != null"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as dicApi from '@/api/base/dict'
export default {
  name: 'EditDic',
  components: {},
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
      title: '新增字典',
      dic: {},
      source: null,
      rules: {
        DicName: [
          {
            required: true,
            message: '字典名不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 100,
            message: '字典名长度在 2 到 100 个字之间',
            trigger: 'blur'
          }
        ]
      }
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
  methods: {
    save() {
      const that = this
      this.$refs['dicEdit'].validate((valid) => {
        if (valid) {
          if (that.id != null) {
            that.setDic()
          } else {
            that.addDic()
          }
        } else {
          return false
        }
      })
    },
    async setDic() {
      await dicApi.Set(this.id, this.dic)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async addDic() {
      await dicApi.Add(this.dic)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async reset() {
      if (this.id == null) {
        this.title = '新增字典'
        this.source = null
        this.dic = {}
      } else {
        const data = await dicApi.Get(this.id)
        this.source = data
        this.dic = data
        this.title = '编辑字典-' + data.DicName
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.id == null) {
        this.dic = {}
      } else {
        this.dic = this.source
      }
    }
  }
}
</script>
