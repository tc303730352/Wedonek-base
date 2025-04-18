<template>
  <div>
    <el-upload
      ref="upFileImg"
      :action="action"
      :show-file-list="false"
      :headers="header"
      :disabled="isloading"
      :auto-upload="false"
      :on-success="upComplate"
      :before-upload="checkUpFile"
      :multiple="multiple"
      :accept="accept"
      :on-change="fileChange"
    >
      <slot v-if="files.length > 0" name="fileList" :files="files">
        <ul class="el-upload-list el-upload-list--picture-card">
          <li
            v-for="file in files"
            :key="file.FileId"
            class="el-upload-list__item is-success"
          >
            <img
              :src="file.FileUri | imageUri"
              :alt="file.FileName"
              class="el-upload-list__item-thumbnail"
            >

            <label class="el-upload-list__item-status-label">
              <i class="el-icon-upload-success el-icon-check" /></label>
            <span class="el-upload-list__item-actions">
              <span class="el-upload-list__item-delete">
                <i class="el-icon-delete" @click="dropFile(file.FileId)" />
              </span>
              <span v-if="allowAmplify" class="el-upload-list__item-delete">
                <i class="el-icon-zoom-in" @click="amplifyImg(file.FileId)" />
              </span>
              <span v-if="isOrderBy" class="el-upload-list__item-delete">
                <i class="el-icon-top" @click="setSort(file, 'up')" />
              </span>
              <span v-if="isOrderBy" class="el-upload-list__item-delete">
                <i class="el-icon-bottom" @click="setSort(file, 'bottom')" />
              </span>
            </span>
          </li>
        </ul>
      </slot>
      <slot v-if="upFilelimit != files.length" name="trigger">
        <i
          class="el-icon-plus uploader-icon"
        />
      </slot>
      <div v-if="isShowTip" slot="tip">
        <slot name="upTip" :show="imgLimit ? imgLimit.Show : null" :UpShow="upConfig.UpShow" :error="error">
          <div class="el-upload__tip">
            <span v-if="error" style="color: red">{{ error }}</span>
            <template v-else>
              <div style="height: 30px;line-height: 30px;">{{ upConfig.UpShow }}</div>
              <div v-if="imgLimit && imgLimit.Show" style="height: 30px;line-height: 30px;">{{ imgLimit.Show }}</div>
            </template>
          </div>
        </slot>
      </div>
    </el-upload>
    <el-dialog :visible.sync="amplifyVisible">
      <img width="100%" :src="imgSrc" alt="">
    </el-dialog>
  </div>
</template>

<script>
import * as fileApi from '@/api/base/file'
import sparkMD5 from 'spark-md5'
import { fileType } from '@/config/fileConfig'
export default {
  props: {
    fileKey: {
      type: String,
      default: null
    },
    tag: {
      type: String,
      default: null
    },
    linkBizPk: {
      type: String,
      default: null
    },
    fileUri: {
      type: Array,
      default: null
    },
    multiple: {
      type: Boolean,
      default: false
    },
    upFilelimit: {
      type: Number,
      default: 1
    },
    value: {
      type: Array,
      default: null
    },
    allowAmplify: {
      type: Boolean,
      default: true
    },
    isOrderBy: {
      type: Boolean,
      default: false
    },
    isShowTip: {
      type: Boolean,
      default: true
    },
    imgSet: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      action: null,
      error: null,
      isloading: false,
      isInit: false,
      upConfig: {},
      files: [],
      amplifyVisible: false,
      oldPk: null,
      accept: null,
      imgSrc: null,
      imgLimit: null,
      header: {
        accreditId: this.$store.getters.token
      }
    }
  },
  watch: {
    fileKey: {
      handler(val) {
        if (val && this.isInit === false) {
          this.isInit = true
          this.reset()
        }
      },
      immediate: true
    },
    linkBizPk: {
      handler(val) {
        if (this.fileKey && val !== this.oldPk) {
          this.oldPk = val
          this.reset()
        }
      },
      immediate: false
    },
    imgSet: {
      handler(val) {
        this.imgLimit = val
      },
      immediate: true
    },
    value: {
      handler(val) {
        if (
          this.linkBizPk == null &&
          (val == null || val.length === 0) &&
          this.files.length !== 0
        ) {
          this.files = []
        }
      },
      immediate: false
    }
  },
  methods: {
    dropFile(fileId) {
      this.isloading = true
      this.files = this.files.filter((c) => c.FileId !== fileId)
      this.isloading = false
      this.changeEvent('change')
    },
    amplifyImg(fileId) {
      const file = this.files.find((c) => c.FileId === fileId)
      this.imgSrc = file.FileUri
      this.amplifyVisible = true
    },
    upComplate(res, file) {
      if (res.errorcode === 0) {
        this.addFile(res.data)
      }
    },
    addFile(file) {
      this.files.push(file)
      this.changeEvent('change')
    },
    checkImgSize(img) {
      if (this.imgLimit.Ratio != null) {
        const sRatio = Math.round(
          (this.imgLimit.Ratio[0] / this.imgLimit.Ratio[1]) * 100
        )
        const ratio = Math.round((img.width / img.height) * 100)
        if (sRatio !== ratio) {
          return false
        }
      }
      if (this.imgLimit.MinWidth != null && img.width < this.imgLimit.MinWidth) {
        return false
      }
      if (this.imgLimit.MinHeight != null && img.height < this.imgLimit.MinHeight) {
        return false
      }
      if (this.imgLimit.MaxHeight != null && img.height > this.imgLimit.MaxHeight) {
        return false
      }
      if (this.imgLimit.MaxWidth != null && img.width > this.imgLimit.MaxWidth) {
        return false
      }
      return true
    },
    upFail(error) {
      this.error = error
      if (this.isShowTip === false) {
        this.$emit('error', error)
      }
    },
    async checkImg(file) {
      const that = this
      return new Promise((resolve) => {
        if (that.imgLimit == null) {
          resolve(true)
          return
        }
        const reader = new FileReader()
        reader.onload = function(e) {
          const img = new Image()
          img.onload = function() {
            if (!that.checkImgSize(img)) {
              that.upFail(that.imgLimit.Show)
              resolve(false)
            } else {
              resolve(true)
            }
          }
          img.src = e.target.result
        }
        reader.readAsDataURL(file)
      })
    },
    async loadMD5(file) {
      const blobSlice =
        File.prototype.slice ||
        File.prototype.mozSlice ||
        File.prototype.webkitSlice
      const chunkSize = 2097152
      const chunks = Math.ceil(file.size / chunkSize)
      return new Promise((resolve, reject) => {
        let currentChunk = 0
        const spark = new sparkMD5.ArrayBuffer()
        const reader = new FileReader()
        reader.onload = function(e) {
          spark.append(e.target.result)
          currentChunk++
          if (currentChunk < chunks) {
            loadNext()
          } else {
            resolve(spark.end())
          }
        }
        reader.onerror = function() {
          reject()
        }

        function loadNext() {
          const start = currentChunk * chunkSize
          const end =
            start + chunkSize >= file.size ? file.size : start + chunkSize
          reader.readAsArrayBuffer(blobSlice.call(file, start, end))
        }
        loadNext()
      })
    },
    setSort(file, type) {
      const index = this.files.findIndex((c) => c.FileId === file.FileId)
      if (type === 'up') {
        if (index <= 0) {
          return
        }
        const list = this.files.concat()
        const other = list[index]
        list[index] = list[index - 1]
        list[index - 1] = other
        this.files = list
      } else {
        if (index >= this.files.length - 1) {
          return
        }
        const list = this.files.concat()
        const other = list[index]
        list[index] = list[index + 1]
        list[index + 1] = other
        this.files = list
      }
      this.saveSort()
    },
    async saveSort() {
      const sort = {}
      for (let i = 0; i < this.files.length; i++) {
        sort[this.files[i].FileId] = i
      }
      await fileApi.saveSort(sort)
    },
    changeEvent(eventName) {
      const fileId = []
      const src = []
      this.files.forEach((c) => {
        fileId.push(c.FileId)
        src.push(c.FileUri)
      })
      this.$emit('input', fileId)
      this.$emit('update:fileUri', src)
      this.$emit(eventName, fileId, this.files)
    },
    async reset() {
      this.action = fileApi.getfileUpUri(this.fileKey)
      if (this.linkBizPk != null && this.linkBizPk !== '') {
        this.action =
          this.action +
          '?linkBizPk=' +
          this.linkBizPk +
          '&tag=' +
          (this.tag == null ? '' : this.tag)
      }
      const config = await fileApi.getUpConfig(
        this.fileKey,
        this.linkBizPk,
        this.tag
      )
      if (config.Extension == null) {
        config.Extension = []
      }
      if (config.UpImgSet !== null && this.imgSet == null) {
        this.imgLimit = config.UpImgSet
      }
      this.accept = config.Extension.join(',')
      this.upConfig = config
      if (config.Files != null) {
        this.files = config.Files
      } else {
        this.files = []
      }
      this.changeEvent('load')
    },
    getFileType(type, name) {
      const t = fileType[type]
      if (t) {
        return t
      }
      const index = name.lastIndexOf('.')
      if (index === -1) {
        return null
      }
      return name.substring(index)
    },
    async fileChange(file, fileList) {
      if (!this.checkUpFile(file)) {
        this.cancelUp(file, fileList)
        return
      }
      const res = await this.checkImg(file.raw)
      if (res === false) {
        this.cancelUp(file, fileList)
      } else {
        const md5 = await this.loadMD5(file.raw)
        this.preFileUp(md5, file, fileList)
      }
    },
    cancelUp(file, fileList) {
      this.$refs.upFileImg.abort(file)
      const index = fileList.findIndex(c => c.uid === file.uid)
      fileList.splice(index, 1)
    },
    async preFileUp(md5, file, fileList) {
      const res = await fileApi.preUpFile(
        {
          FileMd5: md5,
          FileName: file.name,
          LinkBizPk: this.linkBizPk,
          Tag: this.tag
        },
        this.fileKey
      )
      if (res.IsUp) {
        this.cancelUp(file, fileList)
        this.addFile(res)
      } else {
        this.$refs.upFileImg.submit()
      }
    },
    checkUpFile(file) {
      if (this.upConfig == null) {
        return false
      } else if (this.upConfig.Extension.length !== 0) {
        const type = this.getFileType(file.type, file.name)
        if (type == null) {
          this.error = '暂不支持该类型的文件!'
          return false
        } else if (!this.upConfig.Extension.includes(type)) {
          this.error = '暂不支持' + type + '类型的文件!'
          return false
        }
      }
      if (this.upConfig.UpFileSize && file.size > this.upConfig.UpFileSize) {
        this.error =
          '上传文件大小不能超过 ' +
          Math.round((this.upConfig.UpFileSize / 1024 / 1024) * 100) / 100 +
          'MB'
        return false
      }
      return true
    }
  }
}
</script>
<style scoped>
.uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 120px;
  height: 120px;
  line-height: 120px;
  text-align: center;
  border: 1px;
  border-color: #8c939d;
  border-style: dashed;
}
.el-upload-list--picture-card {
  padding-left: 20px;
}
.uploader-img {
  height: 120px;
}
</style>
