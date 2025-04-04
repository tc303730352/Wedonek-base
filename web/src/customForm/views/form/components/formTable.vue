<template>
  <el-card class="table">
    <div slot="header" class="header">
      <div class="title">
        <el-divider direction="vertical" />
        {{ table.Title }}
      </div>
      <div class="opBtn">
        <el-button
          type="primary"
          size="mini"
          icon="el-icon-edit"
        />
        <el-button
          type="danger"
          size="mini"
          icon="el-icon-delete"
        />
      </div>
    </div>
    <formContent :formId="formId" :table="table" />
  </el-card>
</template>

<script>
import formContent from './formContent.vue'
export default {
  components: {
    formContent
  },
  props: {
    table: {
      type: Object,
      default: null
    },
    formId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
    }
  },
  mounted() {},
  methods: {
  }
}
</script>

<style scoped>
.table {
  margin-bottom: 10px;
}
.table .header {
  text-align: left;
}
.table .header .opBtn{
 float: right;
}
.table .header .title {
  font-size: 1.1rem;
  display: inline-block;
}
.table .header .title .el-divider--vertical {
  width: 4px;
  margin: 0;
  height: 1.2em;
  background-color: #0095ff;
}
.table .formContent {
  width: 100%;
}
</style>
