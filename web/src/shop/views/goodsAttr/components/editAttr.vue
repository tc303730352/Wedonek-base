<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="close"
    :close-on-click-modal="false"
  >
    <el-form
      :model="attrModel"
      label-width="130px"
      ref="attrEdit"
      :rules="rules"
    >
      <el-form-item label="是否为目录" prop="IsDir">
        <el-switch :disabled="attrModel.Id != null || attrModel.Pid != 0" @change="attrTypeChange" v-model="attrModel.IsDir" active-text="目录" inactive-text="属性" ></el-switch>
      </el-form-item>
      <el-form-item :label="nameLabel" prop="Name">
        <el-input
          v-model="attrModel.Name"
          :maxlength="50"
          :placeholder="nameLabel"
        ></el-input>
      </el-form-item>
      <el-form-item v-if="attrModel.IsDir == false" label="属性值" prop="Value">
        <el-input
          v-model="attrModel.Value"
          :maxlength="255"
          placeholder="属性值"
        ></el-input>
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button :loading="loading" type="primary" @click="save"
          >确定</el-button
        >
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
export default {
  computed: {},
  data() {
    return {
      loading: false,
      nameLabel: "属性名称",
      rules: {
        Name: [
          {
            required: true,
            message: "名称不能为空！",
            trigger: "blur",
          },
        ],
      },
      title: "",
      attrModel: {
        Id: null,
        Name: null,
        IsDir: false,
        Value: null,
        Pid: null,
      },
    };
  },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    attr: {
      type: Object,
      default: () => {
        return {
          Id: null,
          Name: null,
          IsDir: false,
          Value: null,
          Pid: null,
        };
      },
    },
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset();
        }
      },
      immediate: true,
    },
  },
  methods: {
    reset() {
      this.attrModel = this.attr;
      this.nameLabel = this.attr.IsDir == false ? "属性名" : "目录名";
      if (this.attrModel.Id == null) {
        this.title = "新增商品属性";
      } else {
        this.title =
          this.attr.IsDir == false
            ? "编辑属性：" + this.attr.Name
            : "编辑目录：" + this.attr.Name;
      }
    },
    attrTypeChange() {
      this.nameLabel = this.attr.IsDir == false ? "属性名" : "目录名";
    },
    save() {
      const that = this;
      this.$refs["attrEdit"].validate((valid) => {
        if (valid) {
          that.loading = true;
          this.$emit("close", true, this.attrModel);
          that.loading = false;
        } else {
          return false;
        }
      });
    },
    close() {
      this.$emit("close", false);
    },
  },
  components: {
  },
};
</script>
<style type="less" scoped>
</style>