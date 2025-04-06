<template>
  <el-input v-if="type===ControlType.input.value" v-model="value" :placeholder="control.Description" :maxlength="control.MaxLen" />
  <el-input-number v-else-if="type===ControlType.number.value" v-model="value" :placeholder="control.Description" />
  <el-input v-else-if="type===ControlType.text.value" v-model="value" type="textarea" :placeholder="control.Description" :maxlength="control.MaxLen" />
  <span v-else-if="type===ControlType.label.value">{{ value }}</span>
  <el-date-picker v-else-if="type===ControlType.date.value" v-model="value" type="date" :placeholder="control.Description" />
  <el-date-picker v-else-if="type===ControlType.dateTime.value" v-model="value" type="datetime" :placeholder="control.Description" />
  <el-time-select v-else-if="type===ControlType.time.value" v-model="value" :placeholder="control.Description" />
  <el-switch
    v-else-if="type===ControlType.switch.value"
    v-model="value"
    :active-text="config.Active"
    :inactive-text="config.Inactive"
  />
  <empInput v-else-if="type===ControlType.emp.value" v-model="value" :placeholder="control.Description" />
  <deptSelect v-else-if="type===ControlType.dept.value" v-model="value" :placeholder="control.Description" />
  <companySelect v-else-if="type===ControlType.company.value" v-model="value" :placeholder="control.Description" />
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
      config: {}
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
    }
  }
}
</script>
