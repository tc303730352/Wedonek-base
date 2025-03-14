<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="deptEdit" :model="dept" :rules="rules">
      <el-form-item :label="titleName + '全称'" prop="DeptName">
        <el-input
          v-model="dept.DeptName"
          maxlength="100"
          :placeholder="titleName + '全称'"
        />
      </el-form-item>
      <el-form-item :label="titleName + '简称'" prop="ShortName">
        <el-input
          v-model="dept.ShortName"
          maxlength="50"
          :placeholder="titleName + '简称'"
        />
      </el-form-item>
      <el-form-item label="所属上级" prop="ParentId">
        <deptSelect
          v-model="dept.ParentId"
          :unit-id="unitId"
          :is-unit="isUnit ? true : null"
          :status="[0, 1, 2]"
          placeholder="所属上级"
        />
      </el-form-item>
      <el-form-item label="说明" prop="DeptShow">
        <el-input
          v-model="dept.DeptShow"
          type="textarea"
          maxlength="255"
          placeholder="说明"
        />
      </el-form-item>
      <el-form-item label="负责人" prop="LeaderId">
        <empInput
          v-model="dept.LeaderId"
          :unit-id="chioseUnitId"
          placeholder="负责人"
        />
      </el-form-item>
      <el-form-item v-if="isUnit == false" label="管理类目" prop="DeptTag">
        <dictItem
          v-model="dept.DeptTag"
          :multiple="true"
          :dic-id="HrItemDic.deptTag"
          placeholder="管理类目"
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
import * as deptApi from '@/api/unit/dept'
import deptSelect from '@/components/unit/deptSelect.vue'
import { HrItemDic } from '@/config/publicDic'
import empInput from '@/components/emp/empInput.vue'
export default {
  name: 'EditDept',
  components: {
    deptSelect,
    empInput
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    parentId: {
      type: String,
      default: null
    },
    isUnit: {
      type: Boolean,
      default: false
    },
    deptId: {
      type: String,
      default: null
    },
    unitId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrItemDic,
      title: '新增部门',
      titleName: '',
      dept: {},
      chioseUnitId: null,
      leader: null,
      empVisible: false,
      empTitle: null,
      source: null,
      rules: {
        DeptName: [
          {
            required: true,
            message: '部门/单位全称不能为空！',
            trigger: 'blur'
          },
          {
            min: 2,
            max: 100,
            message: '部门/单位全称长度在 2 到 100 个字之间',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {
    comName() {
      const comId = this.$store.getters.curComId
      return this.$store.getters.company[comId]
    },
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
    save() {
      const that = this
      this.$refs['deptEdit'].validate((valid) => {
        if (valid) {
          if (that.deptId) {
            that.setDept()
          } else {
            that.addDept()
          }
        } else {
          return false
        }
      })
    },
    async setDept() {
      await deptApi.set(this.deptId, this.dept)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async addDept() {
      this.dept.IsUnit = this.isUnit
      this.dept.CompanyId = this.comId
      await deptApi.add(this.dept)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async reset() {
      this.empTitle = '选择' + (this.isUnit ? '单位' : '部门') + '负责人'
      if (this.deptId == null) {
        this.title = this.isUnit ? '新增单位' : '新增部门'
        this.titleName = this.isUnit ? '单位' : '部门'
        this.source = null
        this.dept = {
          ParentId: this.parentId == null ? this.unitId : this.parentId
        }
        this.leader = null
        this.chioseUnitId = this.unitId
      } else {
        const data = await deptApi.get(this.deptId)
        this.source = data
        this.dept = data
        this.isUnit = data.IsUnit
        this.titleName = data.IsUnit ? '单位' : '部门'
        this.title = (data.IsUnit ? '编辑单位:' : '编辑部门:') + data.DeptName
        this.leader = data.LeaderId
        this.chioseUnitId = data.UnitId
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.deptId == null) {
        this.dept = {}
      } else {
        this.dept = this.source
      }
    }
  }
}
</script>
