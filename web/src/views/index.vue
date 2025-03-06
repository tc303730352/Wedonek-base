<template>
    <el-card>
      <p>登陆中....</p>
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
          text: '登陆中...',
          spinner: 'el-icon-loading'
        })
        this.jumpIndex(this.$store.getters.home)
        loadCon.close()
      },
      jumpIndex(home) {
        const redirect = this.$route.query.redirect
        if (redirect == null || redirect=='/loading') {
          this.$router.push({ name: home.Name })
        } else {
          this.$router.push({ path: redirect, query: this.getQuery(this.$route.query) })
        }
      },
      getQuery(query) {
        return Object.keys(query).reduce((acc, cur) => {
          if (cur !== 'redirect') {
            acc[cur] = query[cur]
          }
          return acc
        }, {})
      }
    }
  }
  </script>
  