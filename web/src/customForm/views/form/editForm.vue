<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="700px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="formEdit"
      :model="formData"
      :rules="rules"
      label-width="120px"
    >
      <el-form-item label="表单标题" prop="FormName">
        <el-input
          v-model="formData.FormName"
          maxlength="50"
          placeholder="表单标题"
        />
      </el-form-item>
      <el-form-item label="表单分类" prop="FormType">
        <treeDicItem
          v-model="formData.FormType"
          :dic-id="DictItemDic.formType"
          placeholder="表单分类"
        />
      </el-form-item>
      <el-form-item label="版本号" prop="Ver">
        <el-input
          v-model="formData.Ver"
          :disabled="id != null"
          maxlength="10"
          placeholder="版本号"
        />
      </el-form-item>
      <el-form-item label="表单说明" prop="FormShow">
        <el-input
          v-model="formData.FormShow"
          type="textarea"
          :rows="2"
          maxlength="100"
          placeholder="表单说明"
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
import * as formApi from '@/customForm/api/form'
import { DictItemDic } from '@/customForm/config/formConfig'
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
    formType: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      DictItemDic,
      formData: {},
      title: '新增表单',
      rules: {
        FormName: [
          {
            required: true,
            message: '表单标题不能为空！',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
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
      if (this.id != null) {
        const res = await formApi.Get(this.id)
        this.formData = {
          FormName: res.FormName,
          FormShow: res.FormShow,
          FormType: res.FormType,
          Ver: res.Ver
        }
      } else {
        this.title = '新增表单'
        this.formData = {
          FormName: null,
          FormShow: null,
          FormType: this.formType,
          Ver: 'V1.0.0'
        }
      }
    },
    async set() {
      await formApi.Set(this.id, this.formData)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async add() {
      this.formData.CompanyId = this.comId
      this.id = await formApi.Add(this.formData)
      this.$message({
        message: '保存成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    save() {
      this.$refs['formEdit'].validate((valid) => {
        if (valid) {
          if (this.id == null) {
            this.add()
          } else {
            this.set()
          }
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
