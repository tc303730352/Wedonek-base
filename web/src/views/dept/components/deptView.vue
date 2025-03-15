<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form label-width="120px">
      <el-form-item :label="titleName + '全称'">
        {{ dept.DeptName }}
      </el-form-item>
      <el-form-item :label="titleName + '简称'">
        {{ dept.ShortName }}
      </el-form-item>
      <el-form-item label="所属上级">
        <deptSelect
          v-model="dept.ParentId"
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
        />
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
  </el-dialog>
</template>

<script>
import * as deptApi from '@/api/unit/dept'
import deptSelect from '@/components/unit/deptSelect.vue'
import { HrItemDic } from '@/config/publicDic'
import empInput from '@/components/emp/empInput.vue'
export default {
  name: 'DeptView',
  components: {
    deptSelect,
    empInput
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
      dept: {},
      isUnit: false,
      empTitle: null
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
    async reset() {
      const data = await deptApi.get(this.id)
      this.dept = data
      this.isUnit = data.IsUnit
      this.titleName = data.IsUnit ? '单位' : '部门'
      this.title = (data.IsUnit ? '查看单位:' : '查看部门:') + data.DeptName
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
