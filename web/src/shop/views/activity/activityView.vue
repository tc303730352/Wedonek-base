<template>
  <div>
    <activityDetailed :id="id" :isLoad="isLoad" @load="load"></activityDetailed>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        v-if="status == 0 || status == 4"
        :loading="loading"
        type="primary"
        @click="subAudit"
        >提交审核</el-button
      >
      <el-button @click="prevStep">返回</el-button>
    </el-row>
  </div>
</template>
    
    <script>
import activityDetailed from "./components/activityDetailed";
import { SubmitAudit } from "@/shop/api/activity";
export default {
  computed: {},
  data() {
    return {
      status: 1,
      id: null,
      loading: false,
      isLoad: false,
    };
  },
  components: {
    activityDetailed,
  },
  mounted() {
    this.id = parseInt(this.$route.params.id);
    this.isLoad = true;
  },
  methods: {
    prevStep() {
      this.$router.back();
    },
    load(data) {
      this.status = data.ActivityStatus;
    },
    async subAudit() {
      this.loading = true;
      try {
        await SubmitAudit(this.id);
        this.loading = false;
        this.status = 1
        this.$message({
          type: "success",
          message: "提交审核成功!",
        });
      } catch {
        this.loading = false;
      }
    },
  },
};
</script>  