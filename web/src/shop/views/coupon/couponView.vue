<template>
  <div>
    <activityDetailed
      :isLoad="isLoad"
      :id="id"
    ></activityDetailed>
  </div>
</template>
   
  <script>
import activityDetailed from "./components/couponDetailed.vue";
export default {
  computed: {},
  data() {
    return {
      id: null,
      isLoad: false,
    };
  },
  components: {
    activityDetailed,
  },
  mounted() {
    const id = this.$route.params.id;
    if (id == null) {
      this.$router.back();
    } else {
      this.id = parseInt(id);
      this.isLoad = true
    }
  },
  methods: {
    callback() {
      this.$router.back();
    },
  },
};
</script>