<template>
  <div>
    <el-card style="margin-top: 20px">
      <div slot="header" class="clearfix">
        <span>主单信息</span>
      </div>
      <el-form
        :model="change"
        ref="deptChange"
        :rules="rules"
        label-width="150px"
      >
        <el-form-item label="变动的单位/部门" prop="DeptId">
          <deptSelect
            v-model="change.DeptId"
            @change="deptSelect"
            placeholder="变动的部门"
          />
        </el-form-item>
        <el-form-item label="变动方式" prop="ChangeType" required>
          <el-select v-model="change.ChangeType" placeholder="请选择">
            <el-option label="合并" :value="0">合并</el-option>
            <el-option label="解散" :value="1">解散</el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="目标单位/部门"
          prop="ToDeptId"
          required
          v-if="change.ChangeType == 0"
        >
          <deptSelect
            :disabled="change.DeptId == null"
            v-model="change.ToDeptId"
            :isUnit="change.IsUnit"
            :isDept="change.IsDept"
            :unitId="change.UnitId"
            :isChioseUnit="change.isChioseUnit"
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
      :deptId="deptId"
      :toDeptId="toDeptId"
      :ver="ver"
      @complete="reset"
    ></mergeDept>
    <disbandedDept
      v-else-if="changeType == 1 && isLoad"
      :deptId="deptId"
      :ver="ver"
      @complete="reset"
    ></disbandedDept>
  </div>
</template>
  
<script>
import { HrItemDic } from "@/config/publicDic";
import deptSelect from "@/components/unit/deptSelect.vue";
import mergeDept from "./components/mergeDept.vue";
import disbandedDept from './components/disbandedDept.vue';
export default {
  computed: {
    comName() {
      const comId = this.$store.getters.curComId;
      return this.$store.getters.company[comId];
    },
    comId() {
      return this.$store.getters.curComId;
    },
  },
  data() {
    const validateToDeptId = async (rule, value, callback) => {
      if (this.change.DeptId == null || this.change.ChangeType != 0) {
        callback();
      } else if (value == null) {
        callback(new Error("请选择目的地单位/部门!"));
      } else {
        callback();
      }
    };
    return {
      HrItemDic,
      isLoad: false,
      ver: 0,
      placeholder: "目标单位/部门",
      isNext: false,
      deptId: null,
      toDeptId: null,
      changeType: 0,
      rules: {
        DeptId: [
          {
            required: true,
            message: "请选择变动的单位/部门！",
            trigger: "blur",
          },
        ],
        ToDeptId: [
          {
            required: false,
            trigger: "blur",
            validator: validateToDeptId,
          },
        ],
      },
      change: {
        DeptId: null,
        ToDeptId: null,
        isChioseUnit: true,
        ChangeType: 0,
        IsUnit: false,
        UnitId: null,
      },
    };
  },
  methods: {
    reset() {
      this.isLoad = false;
      this.changeType = 0;
      this.deptId = null;
      this.toDeptId = null;
      this.change = {
        DeptId: null,
        ToDeptId: null,
        isChioseUnit: true,
        ChangeType: 0,
        IsUnit: false,
        UnitId: null,
      };
    },
    save() {
      const that = this;
      this.$refs["deptChange"].validate((valid) => {
        if (valid) {
          that.changeType = that.change.ChangeType;
          that.deptId = that.change.DeptId;
          that.toDeptId = that.change.ToDeptId;
          that.isLoad = true;
          that.ver = that.ver + 1;
        } else {
          return false;
        }
      });
    },
    deptSelect(e) {
      if (e.value == null) {
        this.change.IsUnit = false;
        this.change.UnitId = null;
      } else {
        this.change.ToDeptId = null;
        const dept = e.dept[0];
        if (dept.IsUnit) {
          this.placeholder = "目标单位";
          this.change.IsUnit = true;
          this.change.IsDept = null;
          this.change.isChioseUnit = true;
          this.change.UnitId = null;
        } else {
          this.placeholder = "目标部门";
          this.change.IsUnit = false;
          this.change.IsDept = true;
          this.change.isChioseUnit = false;
          this.change.UnitId = dept.UnitId;
        }
      }
    },
  },
  components: {
    deptSelect,
    mergeDept,
    disbandedDept
  },
};
</script>