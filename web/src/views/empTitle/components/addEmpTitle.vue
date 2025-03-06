<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :modal="false"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="empTitleEdit" :model="empTitle" :rules="rules">
      <el-form-item label="任职部门" prop="DeptId">
        <deptSelect
          v-model="empTitle.DeptId"
          :status="[1]"
          :is-chiose-unit="false"
          placeholder="选择任职部门"
        />
      </el-form-item>
      <el-form-item label="职务" prop="TitleCode">
        <dictItem
          v-model="empTitle.TitleCode"
          :dic-id="HrItemDic.title"
          placeholder="选择职务"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="save">保存</el-button>
        <el-button @click="resetForm">重置</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { add } from '@/api/emp/empTitle'
import deptSelect from '@/components/unit/deptSelect.vue'
import { HrItemDic } from '@/config/publicDic'
export default {
  name: 'AddEmpTitle',
  components: {
    deptSelect
  },
  props: {
    empId: {
      type: String,
      default: null
    },
    visible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      HrItemDic,
      title: '新增职务',
      empTitle: {},
      rules: {
        DeptId: [
          {
            required: true,
            message: '任职部门不能为空！',
            trigger: 'blur'
          }
        ],
        TitleCode: [
          { required: true, message: '职务不能为空!', trigger: 'blur' }
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
  methods: {
    reset() {
      this.empTitle = {}
    },
    save() {
      const that = this
      this.$refs['empTitleEdit'].validate((valid) => {
        if (valid) {
          that.add()
        } else {
          return false
        }
      })
    },
    async add() {
      this.empTitle.CompanyId = this.comId
      this.empTitle.EmpId = this.empId
      await add(this.empTitle)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      this.empTitle = {}
    }
  }
}
</script>
