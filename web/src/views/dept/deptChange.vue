<template>
  <div>
    <el-card style="margin-top: 20px">
      <div slot="header" class="clearfix">
        <span>主单信息</span>
      </div>
      <el-form
        ref="deptChange"
        :model="change"
        :rules="rules"
        label-width="150px"
      >
        <el-form-item label="变动的单位/部门" prop="DeptId">
          <deptSelect
            v-model="change.DeptId"
            placeholder="变动的部门"
            @change="deptSelectChange"
          />
        </el-form-item>
        <el-form-item label="变动方式" prop="ChangeType" required>
          <el-select v-model="change.ChangeType" placeholder="请选择">
            <el-option label="合并" :value="0">合并</el-option>
            <el-option label="解散" :value="1">解散</el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          v-if="change.ChangeType == 0"
          label="目标单位/部门"
          prop="ToDeptId"
          required
        >
          <deptSelect
            v-model="change.ToDeptId"
            :disabled="change.DeptId == null"
            :is-unit="change.IsUnit"
            :is-dept="change.IsDept"
            :unit-id="change.UnitId"
            :is-chiose-unit="change.isChioseUnit"
            :placeholder="placeholder"
          />
        </el-form-item>
      </el-form>
      <div style="text-align: center">
        <el-button type="primary" @click="save">查询受影响的部门员工</el-button>
      </div>
    </el-card>
    <mergeDept
      v-if="changeType == 0 && isLoad"
      :dept-id="deptId"
      :to-dept-id="toDeptId"
      :ver="ver"
      @complete="reset"
    />
    <disbandedDept
      v-else-if="changeType == 1 && isLoad"
      :dept-id="deptId"
      :ver="ver"
      @complete="reset"
    />
  </div>
</template>

<script>
import { HrItemDic } from '@/config/publicDic'
import deptSelect from '@/components/unit/deptSelect.vue'
import mergeDept from './components/mergeDept.vue'
import disbandedDept from './components/disbandedDept.vue'
export default {
  components: {
    deptSelect,
    mergeDept,
    disbandedDept
  },
  data() {
    const validateToDeptId = async(rule, value, callback) => {
      if (this.change.DeptId == null || this.change.ChangeType !== 0) {
        callback()
      } else if (value == null) {
        callback(new Error('请选择目的地单位/部门!'))
      } else if (value === this.change.DeptId) {
        callback(new Error('目的地部门不能和变动的部门一样!'))
      } else {
        callback()
      }
    }
    return {
      HrItemDic,
      isLoad: false,
      ver: 0,
      placeholder: '目标单位/部门',
      isNext: false,
      deptId: null,
      toDeptId: null,
      changeType: 0,
      rules: {
        DeptId: [
          {
            required: true,
            message: '请选择变动的单位/部门！',
            trigger: 'blur'
          }
        ],
        ToDeptId: [
          {
            required: false,
            trigger: 'blur',
            validator: validateToDeptId
          }
        ]
      },
      change: {
        DeptId: null,
        ToDeptId: null,
        isChioseUnit: true,
        ChangeType: 0,
        IsUnit: false,
        UnitId: null
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
  methods: {
    reset() {
      this.isLoad = false
      this.changeType = 0
      this.deptId = null
      this.toDeptId = null
      this.change = {
        DeptId: null,
        ToDeptId: null,
        isChioseUnit: true,
        ChangeType: 0,
        IsUnit: false,
        UnitId: null
      }
    },
    save() {
      const that = this
      this.$refs['deptChange'].validate((valid) => {
        if (valid) {
          that.changeType = that.change.ChangeType
          that.deptId = that.change.DeptId
          that.toDeptId = that.change.ToDeptId
          that.isLoad = true
          that.ver = that.ver + 1
        } else {
          return false
        }
      })
    },
    deptSelectChange(e) {
      if (e.value == null) {
        this.change.IsUnit = false
        this.change.UnitId = null
      } else {
        this.change.ToDeptId = null
        const dept = e.dept[0]
        if (dept.IsUnit) {
          this.placeholder = '目标单位'
          this.change.IsUnit = true
          this.change.IsDept = null
          this.change.isChioseUnit = true
          this.change.UnitId = null
        } else {
          this.placeholder = '目标部门'
          this.change.IsUnit = false
          this.change.IsDept = true
          this.change.isChioseUnit = false
          this.change.UnitId = dept.UnitId
        }
      }
    }
  }
}
</script>
