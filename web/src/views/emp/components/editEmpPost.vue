<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form :model="empPost" ref="empPostEdit" :rules="rules">
      <el-form-item label="岗位" prop="PostCode">
        <treeDicItem
          :dicId="HrItemDic.post"
          v-model="empPost.PostCode"
          placeholder="岗位"
        ></treeDicItem>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="save">保存</el-button>
        <el-button @click="resetForm">重置</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>
    
<script>
import { setEmpPost } from "@/api/emp/emp";
import { HrItemDic } from "@/config/publicDic";
export default {
  data() {
    return {
      HrItemDic,
      title: "设置人员岗位",
      empPost: {},
      rules: {
        PostCode: [
          {
            required: true,
            message: "岗位不能为空！",
            trigger: "blur",
          },
        ],
      },
    };
  },
  props: {
    emp: {
      type: Object,
      default: () => {
        return {
          PostCode: null,
          EmpId: null,
          EmpName: null,
        };
      },
    },
    visible: {
      type: Boolean,
      default: false,
    },
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.resetForm();
        }
      },
      immediate: true,
    },
  },
  methods: {
    save() {
      const that = this;
      this.$refs["empPostEdit"].validate((valid) => {
        if (valid) {
          that.set();
        } else {
          return false;
        }
      });
    },
    async set() {
      await setEmpPost(this.emp.EmpId, this.empPost.PostCode);
      this.$message({
        message: "保存成功!",
        type: "success",
      });
      this.$emit("close", true);
    },
    closeForm() {
      this.$emit("close", false);
    },
    resetForm() {
      this.empPost = {
        PostCode: this.emp.PostCode,
      };
    },
  },
  components: {},
};
</script>