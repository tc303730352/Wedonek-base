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
    <formContent :controls="controls" :label-width="labelWidth" />
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
    }
  },
  data() {
    return {
      controls: [],
      labelWidth: 80
    }
  },
  watch: {
    table: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    reset() {
      this.labelWidth = 80
      this.controls = this.table.Controls
    }
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
