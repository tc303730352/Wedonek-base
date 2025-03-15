<template>
  <el-dialog
    :title="title"
    :visible="visible"
    min-width="1280px"
    width="80%"
    :before-close="closeForm"
    :modal-append-to-body="false"
    :append-to-body="true"
    :close-on-click-modal="false"
  >
    <el-row>
      <el-col :span="6">
        <el-form label-width="120px">
          <el-form-item :label="titleName + '全称'">
            {{ dept.DeptName }}
          </el-form-item>
          <el-form-item :label="titleName + '简称'">
            {{ dept.ShortName }}
          </el-form-item>
          <el-form-item label="所属上级">
            <deptSelect
              :value="dept.ParentId"
              :readonly="true"
              placeholder="所属上级"
            />
          </el-form-item>
          <el-form-item label="说明">
            {{ dept.DeptShow }}
          </el-form-item>
          <el-form-item label="负责人">
            <empInput
              :value="dept.LeaderId"
              :readonly="true"
              placeholder="负责人"
            >
              <template slot="empName" slot-scope="{emp}">
                <el-button type="text" @click="showEmp(emp.EmpId)"> {{ emp.EmpName }}</el-button>
              </template>
            </empInput>
          </el-form-item>
          <el-form-item v-if="isUnit == false" label="部门分类">
            <dictItem
              :value="dept.DeptTag"
              :readonly="true"
              :multiple="true"
              :dic-id="HrItemDic.deptTag"
              placeholder="部门分类"
            />
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="18">
        <el-card>
          <div slot="header">
            <span>{{ dept.DeptName }}入职员工列表</span>
          </div>
          <empListView :unit-id="unitId" :dept-id="deptId" :is-load="isLoad" />
        </el-card>
      </el-col>
    </el-row>
    <empModel
      :id="empId"
      :visible="empVisible"
      @close="empVisible=false"
    />
  </el-dialog>
</template>

<script>
import * as deptApi from '@/api/unit/dept'
import deptSelect from '@/components/unit/deptSelect.vue'
import { HrItemDic } from '@/config/publicDic'
import empInput from '@/components/emp/empInput.vue'
import empModel from '@/components/emp/empModel.vue'
import empListView from '@/components/emp/empListView.vue'
export default {
  name: 'DeptView',
  components: {
    deptSelect,
    empInput,
    empModel,
    empListView
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
      HrItemDic,
      title: '部门信息',
      titleName: '',
      empVisible: false,
      empId: null,
      dept: {},
      isUnit: false,
      unitId: null,
      deptId: null,
      empTitle: null,
      isLoad: false
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        } else {
          this.isLoad = false
        }
      },
      immediate: true
    }
  },
  methods: {
    showEmp(empId) {
      this.empId = empId
      this.empVisible = true
    },
    async reset() {
      const data = await deptApi.get(this.id)
      this.dept = data
      this.isUnit = data.IsUnit
      if (this.isUnit) {
        this.unitId = this.id
        this.deptId = null
        this.titleName = '单位'
        this.title = '查看单位:' + data.DeptName
      } else {
        this.unitId = data.UnitId
        this.deptId = [this.id]
        this.titleName = '部门'
        this.title = '查看部门:' + data.DeptName
      }
      this.isLoad = true
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
