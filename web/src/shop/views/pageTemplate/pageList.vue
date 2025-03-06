<template>
  <div>
    <el-card>
      <span slot="header">页面管理</span>
      <el-tabs v-model="active">
        <el-tab-pane label="首页" name="index">
          <pageTemplate :place="0" :is-load="active=='index'" />
        </el-tab-pane>
        <el-tab-pane label="二级页" name="second">
          <pageTemplate :place="1" :is-load="active=='second'" />
        </el-tab-pane>
      </el-tabs>
    </el-card>
  </div>
</template>

<script>
import pageTemplate from './components/pageTemplate.vue'
export default {
  components: {
    pageTemplate
  },
  data() {
    return {
      loading: false,
      active: null
    }
  },
  computed: {},
  mounted() {
    this.active = 'index'
  },
  methods: {
  }
}
</script>
