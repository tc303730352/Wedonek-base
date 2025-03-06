<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="logisticsEdit"
      :model="logistics"
      label-width="100px"
      :rules="rules"
    >
      <el-form-item label="摸版名" prop="TemplateName">
        <el-input
          v-model="logistics.TemplateName"
          maxlength="50"
          placeholder="摸版名"
        />
      </el-form-item>
      <el-form-item label="备注" prop="Remark">
        <el-input
          v-model="logistics.Remark"
          maxlength="255"
          placeholder="备注"
        />
      </el-form-item>
      <el-form-item label="是否启用" prop="IsEnable">
        <el-switch v-model="logistics.IsEnable" :disabled="isAllowEnable == false" />
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
import * as logisticsApi from '@/shop/api/logistics'
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
      title: '新增物流价格摸版',
      logistics: {},
      loading: false,
      isAllowEnable: true,
      rules: {
        TemplateName: [
          {
            required: true,
            message: '摸版名不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 50,
            message: '摸版名长度在 2 到 50 个字之间',
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
        this.title = '新增物流价格摸版'
        this.isAllowEnable = true
        this.logistics = {
          TemplateName: null,
          Remark: null,
          IsEnable: false
        }
      } else {
        const res = await logisticsApi.Get(this.id)
        this.logistics = res
        this.isAllowEnable = res.IsDefault === false
        this.title = '编辑物流价格摸版'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['logisticsEdit'].validate((valid) => {
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
      await logisticsApi.Add(this.logistics)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      const isSet = await logisticsApi.Set(this.id, this.logistics)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
