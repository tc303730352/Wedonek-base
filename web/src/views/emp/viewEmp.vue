
<template>
  <el-card>
    <div slot="header" class="clearfix">
      <span>{{ title }}</span>
    </div>
    <empView :id="empId" @load="loadEmp" />
  </el-card>
</template>
<script>
import empView from '@/components/emp/empView.vue'
export default {
  components: {
    empView
  },
  data() {
    return {
      empId: null,
      title: null
    }
  },
  mounted() {
    this.empId = this.$route.params.id
  },
  methods: {
    loadEmp(emp) {
      this.title = '查看员工(' + emp.EmpName + ')资料'
    }
  }
}
</script>
