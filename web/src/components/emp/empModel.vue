<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="80%"
    :modal-append-to-body="false"
    :append-to-body="true"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <empView v-if="visible" :id="id" @load="loadEmp" />
  </el-dialog>
</template>

<script>

import empView from './empView.vue'
export default {
  components: {
    empView
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
      title: '查看员工信息'
    }
  },
  methods: {
    closeForm() {
      this.$emit('close')
    },
    loadEmp(emp) {
      this.title = '查看员工(' + emp.EmpName + ')资料'
    }
  }
}
</script>

