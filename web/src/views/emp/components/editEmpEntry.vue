<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="empEntry" :model="empEntry" :rules="rules">
      <el-form-item label="任职公司" prop="CompanyId">
        <companySelect
          v-model="empEntry.CompanyId"
          :parent-id="comId"
          placeholder="选择任职公司"
          @change="companyChange"
        />
      </el-form-item>
      <el-form-item label="入职部门" prop="DeptId">
        <deptSelect
          v-model="empEntry.DeptId"
          :company-id="empEntry.CompanyId"
          :is-chiose-unit="false"
          placeholder="入职部门"
          @change="deptChange"
        />
      </el-form-item>
      <el-form-item label="岗位" prop="PostCode">
        <treeDicItem
          v-model="empEntry.PostCode"
          :dic-id="HrItemDic.post"
          placeholder="岗位"
        />
      </el-form-item>
      <el-form-item label="职务" prop="Title">
        <dictItem
          v-model="empEntry.Title"
          :dic-id="HrItemDic.title"
          :multiple="true"
          placeholder="职务"
        />
      </el-form-item>
      <el-form-item label="保留原职务" prop="IsRetainTitle">
        <el-switch v-model="empEntry.IsRetainTitle" active-text="是" inactive-text="否" />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="save">保存</el-button>
        <el-button @click="resetForm">重置</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { setEmpEntry } from '@/api/emp/emp'
import { HrItemDic } from '@/config/publicDic'
import deptSelect from '@/components/unit/deptSelect.vue'
import companySelect from '@/components/company/companySelect.vue'
import { getTitles } from '@/api/emp/empTitle'
export default {
  components: {
    deptSelect,
    companySelect
  },
  props: {
    emp: {
      type: Object,
      default: () => {
        return {
          CompanyId: null,
          EmpId: null,
          EmpName: null
        }
      }
    },
    visible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      HrItemDic,
      title: '人员调职',
      empEntry: {
        CompanyId: null,
        DeptId: null,
        PostCode: null,
        Title: []
      },
      rules: {
        CompanyId: [
          {
            required: true,
            message: '请选择任职公司',
            trigger: 'blur'
          }
        ],
        PostCode: [
          {
            required: true,
            message: '岗位不能为空！',
            trigger: 'blur'
          }
        ],
        DeptId: [
          {
            required: true,
            message: '请选择部门',
            trigger: 'blur'
          }
        ],
        Title: [
          {
            required: true,
            message: '请选择职务',
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
          this.resetForm()
        }
      },
      immediate: true
    }
  },
  methods: {
    save() {
      const that = this
      this.$refs['empEntry'].validate((valid) => {
        if (valid) {
          that.set()
        } else {
          return false
        }
      })
    },
    async set() {
      await setEmpEntry(this.emp.EmpId, this.empEntry)
      this.$message({
        message: '保存成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    closeForm() {
      this.$emit('close', false)
    },
    companyChange(e) {
      this.empEntry.DeptId = null
      this.empEntry.Title = null
    },
    async deptChange(e) {
      if (this.empEntry.DeptId == null) {
        this.empEntry.Title = null
      } else {
        this.empEntry.Title = await getTitles(
          this.emp.EmpId,
          this.empEntry.DeptId
        )
      }
    },
    resetForm() {
      this.title = '员工(' + this.emp.EmpName + ')调职'
      this.empEntry = {
        CompanyId: null,
        PostCode: null,
        Title: null,
        DeptId: null
      }
    }
  }
}
</script>
