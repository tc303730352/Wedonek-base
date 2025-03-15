
<template>
  <empView :id="empId" />
</template>
<script>
import empView from './components/empView.vue'
export default {
  components: {
    empView
  },
  data() {
    return {
      empId: null
    }
  },
  mounted() {
    this.empId = this.$route.params.id
  },
  methods: {}
}
</script>
