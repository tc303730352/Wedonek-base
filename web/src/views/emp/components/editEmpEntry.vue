<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="empEntry" :model="empEntry" :rules="rules">
      <el-form-item label="入职部门" prop="DeptId">
        <deptSelect
          v-model="empEntry.DeptId"
          :is-chiose-unit="false"
          placeholder="入职部门"
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
export default {
  components: {
    deptSelect
  },
  props: {
    emp: {
      type: Object,
      default: () => {
        return {
          PostCode: null,
          EmpId: null,
          DeptId: null,
          EmpName: null,
          TitleId: null
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
      title: '设置人员岗位',
      empEntry: {
        DeptId: null,
        PostCode: null,
        Title: []
      },
      rules: {
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
    resetForm() {
      this.empEntry = {
        PostCode: this.emp.PostCode,
        Title: this.emp.TitleId,
        DeptId: this.emp.DeptId
      }
    }
  }
}
</script>
