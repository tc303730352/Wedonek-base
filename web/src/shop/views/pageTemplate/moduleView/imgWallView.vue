<template>
  <div :class="item.class">
    <div class="leftTitle">
      {{ item.title }}
    </div>
    <div class="rightBtn">
      <el-button type="text" :loading="loading" style="color: red;" icon="el-icon-setting" />
      <el-button type="text" :loading="loading" icon="el-icon-document-copy" />
      <el-button type="text" :loading="loading" icon="el-icon-delete" @click="remove" />
    </div>
    <div class="coverImg" @click="chiose">
      <img :src="item.cover">
    </div>
  </div>
</template>

<script>
export default {
  components: {
  },
  props: {
    value: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      loading: false,
      id: null,
      item: {
        class: 'module',
        title: null,
        cover: null
      }
    }
  },
  watch: {
    value: {
      handler(val) {
        if (val && val.mid != null) {
          this.reset()
        }
      },
      immediate: true,
      deep: true
    }
  },
  methods: {
    reset() {
      this.id = this.value.mid
      this.item.class = this.value.isChiose ? 'module active' : 'module'
      this.item.title = this.value.title
      if (this.value.config && this.value.config.ImgWall) {
        this.item.cover = this.value.config.ImgWall.ImgSrc
      }
      if (this.item.cover == null) {
        this.item.cover = '/pageTemplate/image/cover.png'
      }
    },
    chiose() {
      if (this.value.isChiose) {
        this.$emit('click', this.value)
        return
      }
      this.loading = true
      this.$emit('chiose', this.value)
      this.loading = false
    },
    remove() {
      this.loading = true
      this.$emit('remove', this.value)
      this.loading = false
    }
  }
}
</script>
    <style scoped>

    .active {
        border-color: #29f;
        border-width: .15rem;
        border-style: solid;
    }

    .active .leftTitle {
      background-color: hsla(0, 0%, 43%, .85) !important;
      color: #fff !important;
    }

    .module {
        width: 100%;
        min-height: 195px;
    }

    .module .coverImg {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
    }

    .module .leftTitle {
        width: 28px;
        position: absolute;
        left: -50px;
        z-index: 1;
        margin-top: 0px;
        background-color: #fff;
        text-align: center;
        padding-top: 10px;
        padding-bottom: 10px;
        border-radius: 10px;
        color: gray;
        font-size: 14px;
        padding-left: 5px;
        padding-right: 5px;
        border: 1px solid #e1e1e1;
    }

    .module .rightBtn {
        position: absolute;
        left: 375px;
        z-index: 1;
        margin-top: 0px;
        background-color: #fff;
        text-align: center;
        padding: 10px;
        border-radius: 10px;
        border: 1px solid #e1e1e1;
        width: 50px;
    }

    .module .rightBtn .el-button {
        margin: 0px;
        padding: 5px;
        font-size: 16px;
    }

    .module .coverImg img {
        width: 100%;
    }
    </style>
