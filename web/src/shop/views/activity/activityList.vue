<template>
  <div>
    <activity :is-load="isLoad" :activity-type="activityType" />
  </div>
</template>

<script>
import activity from './components/activity.vue'
export default {
  components: {
    activity
  },
  data() {
    return {
      activityType: 0,
      isLoad: false
    }
  },
  computed: {},
  mounted() {
    this.activityType = this.$route.params.type
    this.isLoad = true
  },
  methods: {}
}
</script>
