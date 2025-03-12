<template>
  <div :style="style">
    <img
      v-if="type=='img'"
      :src="fileUri"
    >
    <video v-else-if="type=='video'" controls="" autoplay="false" name="media">
      <source :src="fileUri" type="video/mp4">
    </video>
  </div>
</template>
<script>
export default {
  components: {},
  props: {
    fileUri: {
      type: String,
      default: null
    },
    style: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      visible: false,
      title: null,
      file: {},
      type: 'img',
      icon: null,
      iconList: {
        '.pdf': 'pdf',
        '.docx': 'word',
        '.doc': 'word',
        '.xlsx': 'excel',
        '.xls': 'excel',
        '.mkv': 'video'
      },
      doc: ['.xlsx', '.docx', '.doc', '.pdf'],
      img: ['.jpg', '.png', '.gif', '.bmp', '.jpeg'],
      video: ['.mp4']
    }
  },
  watch: {
    fileUri: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    reset() {
      const ext = this.getExtName(this.fileUri)
      if (ext == null) {
        this.icon = 'file'
        this.type = 'file'
      } else {
        this.icon = this.getIcon(ext)
        if (this.img.includes(ext)) {
          this.type = 'img'
        } else if (this.video.includes(ext)) {
          this.type = 'video'
        } else if (this.doc.includes(ext)) {
          this.type = 'doc'
        } else {
          this.type = 'file'
        }
      }
    },
    getIcon(ext) {
      const icon = this.iconList[ext]
      if (icon == null) {
        return 'file'
      }
      return icon
    },
    getExtName(uri) {
      let index = uri.lastIndexOf('?')
      if (index !== -1) {
        uri = uri.substring(0, index)
      }
      index = uri.lastIndexOf('.')
      if (index === -1) {
        return null
      }
      return uri.substring(index).toLowerCase()
    }
  }
}
</script>
  <style scoped>
.fileShow {
  width: 100%;
  text-align: center;
}
</style>
