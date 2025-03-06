<template>
  <el-card>
    <p>加载中....</p>
  </el-card>
</template>
<script>
import { Loading } from 'element-ui'
export default {
  data() {
    return {}
  },
  mounted() {
    this.load()
  },
  methods: {
    async load() {
      const loadCon = Loading.service({
        lock: true,
        text: '加载中...',
        spinner: 'el-icon-loading'
      })
      const isLogin = await this.$store.dispatch('user/init')
      if (isLogin) {
        this.jumpIndex(this.$store.getters.loadRoute)
      } else {
        await this.$store.dispatch('user/logout')
      }
      loadCon.close()
    },
    jumpIndex(name) {
      const redirect = this.$route.query.redirect
      if (redirect == null || redirect=='/loading') {
        this.$router.push({ name: name })
      } else {
        this.$router.push({ name: name, query: this.$route.query })
      }
    }
  }
}
</script>
