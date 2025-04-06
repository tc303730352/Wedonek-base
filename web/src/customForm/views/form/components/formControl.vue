<template>
  <el-input v-if="type===ControlType.input.value" v-model="value" v-bind="controlSet" />
  <el-input-number v-else-if="type===ControlType.number.value" v-model="value" v-bind="controlSet" />
  <el-input v-else-if="type===ControlType.text.value" v-model="value" type="textarea" v-bind="controlSet" />
  <span v-else-if="type===ControlType.label.value">{{ value }}</span>
  <el-date-picker v-else-if="type===ControlType.date.value" v-model="value" type="date" v-bind="controlSet" />
  <el-date-picker v-else-if="type===ControlType.dateTime.value" v-model="value" type="datetime" v-bind="controlSet" />
  <el-time-select v-else-if="type===ControlType.time.value" v-model="value" v-bind="controlSet" />
  <el-switch
    v-else-if="type===ControlType.switch.value"
    v-model="value"
    v-bind="controlSet"
  />
  <empInput v-else-if="type===ControlType.emp.value" v-model="value" v-bind="controlSet" />
  <deptSelect v-else-if="type===ControlType.dept.value" v-model="value" v-bind="controlSet" />
  <companySelect v-else-if="type===ControlType.company.value" v-model="value" v-bind="controlSet" />
</template>

<script>
import { ControlType } from '@/customForm/config/formConfig'
import empInput from '@/components/emp/empInput.vue'
import deptSelect from '@/components/unit/deptSelect.vue'
import companySelect from '@/components/company/companySelect.vue'
export default {
  components: {
    empInput,
    deptSelect,
    companySelect
  },
  props: {
    control: {
      type: Object,
      default: null
    },
    source: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      ControlType,
      type: 0,
      value: null,
      controlSet: {}
    }
  },
  watch: {
    control: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    reset() {
      this.type = this.control.ColType
      const set = this.control.ControlSet != null ? this.control.ControlSet : {}
      set.placeholder = this.control.Description
      set.maxlength = this.control.MaxLen
      this.controlSet = set
    }
  }
}
</script>
