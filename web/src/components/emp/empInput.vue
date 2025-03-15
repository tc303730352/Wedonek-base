<template>
  <span v-if="readonly">
    <slot name="empName" :emp="{EmpName: empName, EmpId: value}">
      {{ empName }}
    </slot>
  </span>
  <div v-else>
    <el-input
      v-model="empName"
      :readonly="true"
      :disabled="disabled"
      :clearable="clearable"
      :placeholder="placeholder"
    >
      <i
        slot="suffix"
        class="el-icon-s-custom"
        @click="openEmp"
      />
    </el-input>
    <empChoice
      :visible="empVisible"
      :emp-id="empId"
      :title="placeholder"
      :unit-id="unitId"
      :dept-id="deptId"
      :is-entry="isEntry"
      @save="setEmp"
      @close="closeChoice"
    />
  </div>
</template>

<script>
import empChoice from './empChoice.vue'
import { getBasic } from '@/api/emp/emp'
export default {
  components: {
    empChoice
  },
  props: {
    clearable: {
      type: Boolean,
      default: true
    },
    disabled: {
      type: Boolean,
      default: false
    },
    value: {
      type: String,
      default: null
    },
    placeholder: {
      type: String,
      default: '选择员工'
    },
    unitId: {
      type: String,
      default: null
    },
    deptId: {
      type: String,
      default: null
    },
    isEntry: {
      type: Boolean,
      default: true
    },
    readonly: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      emp: null,
      empName: null,
      empId: null,
      empVisible: false
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  watch: {
    value: {
      handler(val) {
        if (val) {
          this.reset()
        } else {
          this.emp = null
          this.empName = null
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    async reset() {
      if (this.emp == null) {
        this.emp = await getBasic(this.value, this.comId)
        this.empName = this.emp.EmpName
      }
    },
    closeChoice() {
      this.empVisible = false
    },
    openEmp() {
      if (this.value != null && this.value != NaN) {
        this.empId = [this.value]
      } else {
        this.empId = null
      }
      this.empVisible = true
    },
    setEmp(e) {
      this.empVisible = false
      const user = e.user.length === 0 ? null : e.user[0]
      this.emp = user
      if (user == null) {
        this.empName = null
        this.$emit('input', null)
        this.$emit('change', null, null)
      } else {
        this.empName = user.EmpName
        this.$emit('input', user.EmpId)
        this.$emit('change', user.EmpId, user)
      }
    }
  }
}
</script>
