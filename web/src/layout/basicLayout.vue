<template>
  <div class="main">
    <app-main id="mainDiv" />
  </div>
</template>

<script>
import { AppMain } from './components'
import store from '@/store'

export default {
  name: 'BasicLayout',
  components: {
    AppMain
  },
  mounted() {
    this.$nextTick(() => {
      const control = document.getElementById('mainDiv')
      store.dispatch('app/setMainHeight', control.offsetHeight)
    })
  },
  methods: {
  }
}
</script>

  <style lang="scss" scoped>
  .main{
    width: 100%;
    height: 100%;
    padding: 10px;
  }
  </style>
