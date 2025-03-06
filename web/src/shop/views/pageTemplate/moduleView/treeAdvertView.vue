<template>
  <div :class="item.class">
    <div class="leftTitle">
      {{ item.title }}
    </div>
    <div class="rightBtn">
      <el-button
        type="text"
        :loading="loading"
        style="color: red"
        icon="el-icon-setting"
      />
      <el-button type="text" :loading="loading" icon="el-icon-document-copy" />
      <el-button
        type="text"
        :loading="loading"
        icon="el-icon-delete"
        @click="remove"
      />
    </div>
    <div class="coverImg" @click="chiose">
      <img v-if="item.cover" :src="item.cover">
      <el-row v-else class="advert">
        <el-col :span="12" class="leftAdvert">
          <img :src="item.left">
        </el-col>
        <el-col :span="12" class="rightAdvert">
          <el-row :gutter="24">
            <el-col :span="24" class="top">
              <img :src="item.top">
            </el-col>
            <el-col :span="24" class="bottom">
              <img :src="item.bottom">
            </el-col>
          </el-row>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    value: {
      type: Object,
      default: null
    },
    exampleImg: {
      type: Object,
      default: () => {
        return {}
      }
    }
  },
  data() {
    return {
      loading: false,
      id: null,
      item: {
        class: 'module',
        title: null,
        cover: null,
        left: null,
        top: null,
        bottom: null
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
      if (this.value.config != null && this.value.config.TreeAdvert) {
        const tree = this.value.config.TreeAdvert
        if (tree.Left != null) {
          this.item.left = this.exampleImg[tree.Left.PlaceId]
        }
        if (tree.Top != null) {
          this.item.top = this.exampleImg[tree.Top.PlaceId]
        }
        if (tree.Bottom != null) {
          this.item.bottom = this.exampleImg[tree.Bottom.PlaceId]
        }
      } else {
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
  border-width: 0.15rem;
  border-style: solid;
}

.active .leftTitle {
  background-color: hsla(0, 0%, 43%, 0.85) !important;
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
.module .advert {
  padding: 5px;
}
.module .leftAdvert {
  height: 100%;
}
.module .leftAdvert img {
  height: 240px;
  width: auto;
}
.module .rightAdvert {
  height: 100%;
}

.module .rightAdvert img {
  height: 118px;
  width: auto;
}

</style>
