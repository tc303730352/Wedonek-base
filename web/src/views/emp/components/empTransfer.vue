<template>
  <div />
</template>

<script>
export default {
  components: {
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
      title: '人员调动',
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
