<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="700px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="controlEdit" :model="control" :rules="rules" label-width="120px">
      <el-form-item label="控件名" prop="Name">
        <el-input v-model="control.Name" maxlength="50" placeholder="控件名" />
      </el-form-item>
      <el-form-item label="选择图标" prop="Icon">
        <iconChiose v-model="control.Icon" />
      </el-form-item>
      <el-form-item label="控件宽度" prop="MinWidth">
        <el-input-number
          v-model="control.MinWidth"
          :precision="0"
          placeholder="控件宽度"
        />
      </el-form-item>
      <el-form-item label="控件说明" prop="Description">
        <el-input
          v-model="control.Description"
          maxlength="100"
          placeholder="控件说明"
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
import iconChiose from '@/components/tools/iconChiose.vue'
import * as controlApi from '@/customForm/api/control'
import { EnumDic } from '@/customForm/config/formConfig'
export default {
  components: {
    iconChiose
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
      EnumDic,
      control: {},
      title: '新增控件',
      rules: {
        Name: [
          {
            required: true,
            message: '控件名称不能为空！',
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
  mounted() {},
  methods: {
    async reset() {
      const res = await controlApi.Get(this.id)
      this.control = {
        Name: res.Name,
        Description: res.Description,
        Icon: res.Icon,
        MinWidth: res.MinWidth
      }
    },
    async set() {
      await controlApi.Set(this.id, this.control)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    save() {
      this.$refs['controlEdit'].validate((valid) => {
        if (valid) {
          this.set()
        } else {
          return false
        }
      })
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
