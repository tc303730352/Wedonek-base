<template>
  <el-row id="row" :gutter="24" class="content">
    <el-col id="left-tree" :span="leftSpan" class="left-tree">
      <slot name="left" />
      <div id="resize" class="resize" title="长按左右拖动">⋮</div>
    </el-col>
    <el-col id="right-content" :span="rightSpan" class="right-content">
      <slot name="right" />
    </el-col>
  </el-row>
</template>

<script>
export default {
  name: 'LeftRightSplit', // 用于匹配路由
  components: {},
  props: {
    leftSpan: {
      type: Number,
      default: 4
    }
  },
  data() {
    return {
      islock: false,
      isMove: false,
      top: window.innerHeight / 2,
      isInit: false,
      width: null,
      row: null,
      left: null,
      right: null,
      rightSpan: 20,
      resize: null
    }
  },
  watch: {
    leftSpan: {
      handler(val) {
        this.rightSpan = 24 - val
      },
      immediate: true
    }
  },
  mounted() {
    this.isMove = false
    this.islock = false
    this.isInit = false
    this.$nextTick(this.initMove)
  },
  methods: {
    hideLeft(isAutoRecovery = true) {
      this.islock = isAutoRecovery
      this.isInit = true
      this.width = this.left.clientWidth
      const height = this.row.clientHeight
      // 鼠标拖动事件
      if (this.right.clientHeight < height) {
        this.right.style.minHeight = height + 'px'
      }
      this.resize.style.left = '0px'
      this.left.style.width = this.resize.clientWidth + 'px'
      this.right.style.width =
        this.row.clientWidth - this.resize.clientWidth + 'px'
    },
    recovery() {
      this.islock = false
      this.isInit = true
      this.resize.style.left = this.width + 'px'
      this.left.style.width = this.width + 'px'
      this.right.style.width = this.row.clientWidth - this.width + 'px'
    },
    initMove() {
      this.row = document.getElementsByClassName('content')[0]
      this.left = document.getElementById('left-tree')
      this.resize = document.getElementById('resize')
      this.right = document.getElementById('right-content')
      const that = this
      this.resize.onclick = function(e) {
        if (that.islock === false) {
          return
        }
        that.recovery()
      }
      // 鼠标按下事件
      this.resize.onmousedown = function(e) {
        if (that.islock) {
          return
        }
        const startX = e.clientX
        that.resize.left = that.resize.offsetLeft
        const width = that.row.clientWidth
        if (width == null) {
          return
        }
        that.isMove = true
        that.isInit = true
        const height = that.row.clientHeight
        const minWidth = that.resize.clientWidth
        const rightWidth = width - that.resize.left - minWidth
        // 鼠标拖动事件
        if (that.right.clientHeight < height) {
          that.right.style.minHeight = height + 'px'
        }
        document.onmousemove = function(e) {
          const size = e.clientX - startX
          const leftX = that.resize.left + size
          if (leftX >= minWidth && leftX <= width - minWidth) {
            that.resize.style.left = leftX + 'px'
            that.left.style.width = leftX + 'px'
            that.right.style.width = rightWidth - size + 'px'
          }
        }
        document.onmouseup = function() {
          document.onmousemove = null
          document.onmouseup = null
          that.isMove = false
        }
      }

      this.$erd.listenTo(this.row, (ele) => {
        if (that.isMove || that.isInit === false) {
          return
        }
        const width =
          ele.clientWidth - that.right.clientWidth - that.left.clientWidth
        if (width !== 0) {
          that.right.style.width = that.right.clientWidth + width + 'px'
        }
      })
    }
  }
}
</script>
<style scoped>
.ant-card-body {
  padding-top: 0 !important;
}
.content {
  display: flex !important;
  width: 100%;
  .left-tree {
    position: relative;
    height: 100%;
    background: #fff;
    box-shadow: 0 0 2px 2px rgba(0, 0, 0, 0.050980392156862744);
    border-radius: 4px;
    vertical-align: top;
    display: inline-block;
    box-sizing: border-box;
    -ms-flex-negative: 0;
    flex-shrink: 0;
    padding: 10px 0 0 10px;
    margin-right: 8px;
  }
  .resize {
    cursor: col-resize;
    position: absolute;
    top: 45%;
    right: -8px;
    background-color: #d6d6d6;
    border-radius: 5px;
    margin-top: -10px;
    width: 10px;
    height: 50px;
    background-size: cover;
    background-position: 50%;
    font-size: 32px;
    color: #fff;
  }
  .right-content {
    display: inline-block;
    height: 100%;
    background: #fff;
    -webkit-box-shadow: 0 0 2px 2px rgba(0, 0, 0, 0.050980392156862744);
    box-shadow: 0 0 2px 2px rgba(0, 0, 0, 0.050980392156862744);
    border-radius: 4px;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
    padding: 10px;
    vertical-align: top;
    overflow: auto;
  }
}
</style>
