<template>
  <div>
    <activityDetailed :id="id" :isLoad="isLoad" @load="load"></activityDetailed>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        v-if="status == 0 || status == 4"
        :loading="loading"
        type="success"
        @click="subAudit"
        >提交审核</el-button
      >
      <el-button @click="prevStep">返回上一步</el-button>
    </el-row>
  </div>
</template>
  
  <script>
import activityDetailed from "./activityDetailed.vue";
import { SubmitAudit } from "@/shop/api/activity";
export default {
  computed: {},
  data() {
    return {
      status: 1,
      loading: false,
    };
  },
  components: {
    activityDetailed,
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false,
    },
    id: {
      type: Number,
      default: null,
    },
  },
  methods: {
    prevStep() {
      this.$emit("prev");
    },
    load(data) {
      this.status = data.ActivityStatus;
      this.loading = false;
    },
    async subAudit() {
      this.loading = true;
      await SubmitAudit(this.id);
      this.$message({
        type: "success",
        message: "提交审核成功!",
      });
      this.loading = false;
      this.$router.back()
    },
  },
};
</script>  