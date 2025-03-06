<template>
  <div class="coverImgList">
    <div class="coverImg">
      <img :src="curImg" />
    </div>
    <div class="coverList">
      <el-button
        class="leftBtn"
        :disabled="pageIndex == 1"
        @click="jumpPage(pageIndex - 1)"
        icon="el-icon-arrow-left"
        type="text"
      />
      <el-button
        class="rightBtn"
        icon="el-icon-arrow-right"
        :disabled="pageIndex == max"
        @click="jumpPage(pageIndex + 1)"
        type="text"
      />
      <div class="cover">
        <div class="list" :style="coverStyle">
          <div
            class="item"
            @mouseover="chioseImg(index)"
            v-for="(src, index) in this.imgList"
            :key="index"
          >
            <img :src="src" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

   
<script>
export default {
  computed: {},
  data() {
    return {
      imgList: [],
      curImg: null,
      pageIndex: 0,
      max: 1,
      coverStyle: {
        width: "450px",
        left: 0
      },
    };
  },
  props: {
    images: {
      type: Array,
      default: [],
    },
  },
  watch: {
    images: {
      handler(val) {
        if (val) {
          this.imgList = val;
          this.curImg = val[0];
          this.reset();
        }
      },
      immediate: true,
    },
  },
  mounted() {},
  methods: {
    chioseImg(index) {
      this.curImg = this.imgList[index];
    },
    jumpPage(index) {
      if (this.pageIndex != index && index <= this.max) {
        this.pageIndex = index
        this.coverStyle.left = ((index - 1) * -384) + 'px'
      }
    },
    reset() {
      let width = this.imgList.length * 96;
      if (width < 384) {
        width = 384;
      }
      this.pageIndex = 1;
      this.max = Math.ceil(width / 384);
      this.coverStyle.left  = 0
      this.coverStyle.width = width + "px";
    },
  },
};
</script>

<style lang="scss" scoped>
.coverImgList {
  width: 450px;
  min-height: 570px;
  .coverImg {
    width: 100%;
    height: 450px;
    img {
      width: 450px;
      height: 450px;
      border: 0;
    }
  }
  .coverList {
    height: 120px;
    width: 450px;
    position: relative;
    .leftBtn {
      font-size: 25px;
      font-weight: 700;
      width: 33px;
      margin-top: 36px;
      position: absolute;
      z-index: 50;
      left: 0;
    }
    .rightBtn {
      font-size: 25px;
      font-weight: 700;
      width: 33px;
      margin-top: 36px;
      position: absolute;
      z-index: 50;
      right: 0;
    }
    .cover {
      margin-left: 33px;
      position: relative;
      width: 384px;
      overflow: hidden;
      height: 100%;
      .list {
        height: 100%;
        position: absolute;
        .item {
          width: 96px;
          padding: 10px 8px 10px 8px;
          float: left;
          cursor: pointer;
          img {
            width: 80px;
            height: 80px;
          }
        }
        .item:hover {
          img {
            border-color: #fa2c19;
            border-width: 1px;
            border-style: solid;
          }
        }
      }
    }
  }
}
</style>