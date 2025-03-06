<template>
  <div>
    <activityDetailed :id="id" :isLoad="isLoad" @load="load"></activityDetailed>
    <el-row style="text-align: center; margin-top: 20px">
      <el-button
        :disabled="status != 1"
        :loading="loading"
        type="success"
        @click="auditPass"
        >审核通过</el-button
      >
      <el-button
        :disabled="status != 1"
        :loading="loading"
        type="warning"
        @click="auditNoPass"
        >审核未通过</el-button
      >
      <el-button @click="prevStep">返回</el-button>
    </el-row>
    <el-dialog
      title="请输入审批意见"
      :visible="visible"
      width="600px"
      :close-on-click-modal="false"
    >
      <el-form label-width="120px">
        <el-form-item label="审批意见">
          <el-input
            v-model="opinion"
            type="textarea"
            :maxlength="255"
            placeholder="审批意见"
          />
        </el-form-item>
      </el-form>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button :loading="loading" type="warning" @click="submitNoPass"
          >确定审批未通过</el-button
        >
        <el-button @click="close">取消</el-button>
      </el-row>
    </el-dialog>
  </div>
</template>
      
      <script>
import activityDetailed from "./components/activityDetailed";
import { AuditPass, AuditNoPass } from "@/shop/api/activity";
export default {
  computed: {},
  data() {
    return {
      status: 1,
      id: null,
      loading: false,
      isLoad: false,
      visible: false,
      opinion: null,
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
    async auditPass() {
      this.loading = true;
      try {
        await AuditPass(this.id);
        this.loading = false;
        this.status = 2
        this.$message({
          type: "success",
          message: "审核成功!",
        });
      } catch {
        this.loading = false;
      }
    },
    close() {
      this.visible = false;
    },
    async submitNoPass() {
      this.visible = false;
      this.loading = true;
      try {
        await AuditNoPass(this.id, this.opinion);
        this.loading = false;
        this.status = 4
        this.$message({
          type: "success",
          message: "审核成功!",
        });
      } catch {
        this.loading = false;
      }
    },
    auditNoPass() {
      this.opinion = null;
      this.visible = true;
    },
  },
};
</script>  