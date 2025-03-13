<template>
  <div :style="style" class="fileShow">
    <el-image v-if="type !== 'file'" :src="icon" :alt="fileName" :preview-src-list="[icon]" />
    <svg-icon v-else :icon-class="icon" @click="open" />
    <el-dialog
      :title="fileName"
      :visible.sync="visible"
      width="80%"
      :destroy-on-close="true"
    >
      <video controls="" autoplay="false" name="media">
        <source :src="fileUri | imageUri" type="video/mp4">
      </video>
    </el-dialog>
  </div>
</template>
<script>
import store from '@/store'
import { getThumbnailUri } from '@/api/base/file'
export default {
  components: {},
  props: {
    fileUri: {
      type: String,
      default: null
    },
    fileName: {
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
    getFileId() {
      const begin = this.fileUri.lastIndexOf('/')
      const end = this.fileUri.lastIndexOf('.')
      if (begin !== -1 && end !== -1) {
        return this.fileUri.substring(begin + 1, end)
      }
      return null
    },
    open() {
      if (this.type === 'video') {
        this.visible = true
      } else {
        window.open(this.fileUri + '?accreditId=' + store.getters.token + '&isDown=true')
      }
    },
    reset() {
      const token = store.getters.token
      const begin = this.fileUri.lastIndexOf('/')
      const end = this.fileUri.lastIndexOf('.')
      if (begin !== -1 && end !== -1) {
        this.fileId = this.fileUri.substring(begin + 1, end)
      }
      const ext = this.getExtName(this.fileUri)
      if (ext == null) {
        this.icon = 'file'
        this.type = 'file'
      } else {
        if (this.img.includes(ext)) {
          this.icon = this.fileUri + '?accreditId=' + token
          this.type = 'img'
        } else if (this.video.includes(ext)) {
          const fileId = this.getFileId()
          this.icon = getThumbnailUri(fileId) + '?accreditId=' + token
          this.type = 'video'
        } else {
          this.icon = this.getIcon(ext)
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
.fileShow img{
  width: 100%;
}
.fileShow .el-button {
  padding: 0;
  border: none;
}
.fileShow .svg-icon {
  font-size: 36px;
}
</style>
