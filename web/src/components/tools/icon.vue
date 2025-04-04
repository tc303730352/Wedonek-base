<template>
  <i
    v-if="icon && icon.startsWith('el-')"
    :style="$attrs.style"
    :class="icon + ($attrs.class == null ? '' : ' ' + $attrs.class)"
    @click="click"
  />
  <svg-icon
    v-else-if="icon != null"
    :class="$attrs.class"
    :style="$attrs.style"
    :icon-class="icon"
    @click="click"
  />
</template>

<script>
export default {
  components: {},
  props: {
    icon: {
      type: String,
      default: null
    }
  },
  data() {
    return {}
  },
  mounted() {
  },
  methods: {
    click(e) {
      this.$emit('click', e)
    }
  }
}
</script>
