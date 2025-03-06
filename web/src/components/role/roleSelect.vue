<template>
    <el-select
      :disabled="disabled"
      :placeholder="place"
      :style="styleSet"
      :clearable="clearable"
      :class="className"
      :multiple="multiple"
      @change="chooseChange"
      :value="value"
    >
      <el-option
        v-for="item in roles"
        :key="item.Id"
        :label="item.RoleName"
        :title="item.Remark"
        :value="item.Id"
      ></el-option>
    </el-select>
  </template>
      
    <script>
  import { getSelect } from "@/api/role/role";
  export default {
    props: {
      clearable: {
        type: Boolean,
        default: true,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      className: {
        type: String,
        default: 'el-input',
      },
      value: {
        default: null,
      },
      placeholder: {
        type: String,
        default: "选择字典",
      },
      multiple: {
        type: Boolean,
        default: false,
      },
      styleSet: {
        type: Object,
        default: null,
      },
    },
    data() {
      return {
        place: null,
        roles: [],
      };
    },
    watch: {
      placeholder: {
        handler(val) {
          this.place = val;
        },
        immediate: true,
      },
    },
    mounted() {
        this.loadRole()
    },
    methods: {
      async loadRole() {
        const roles = await getSelect();
        if(roles.length > 0) {
          roles.forEach(e => {
            if(e.IsAdmin) {
              e.RoleName = e.RoleName + '(管理员)'
            }
          });
        } 
        this.roles  = roles
      },
      chooseChange(value) {
        let item = null;
        if (value != null) {
          if (this.multiple) {
            item = this.roles.filter((c) => value.includes(c.Id));
          } else {
            item = this.roles.find((c) => c.Id == value);
          }
        }
        this.$emit("input", value);
        this.$emit("change", value, item);
      },
    },
  };
  </script>  