<template>
  <div>
    <el-upload
      ref="upBigFile"
      :show-file-list="false"
      :disabled="isloading"
      :auto-upload="false"
      :on-success="upComplate"
      :before-upload="checkUpFile"
      :multiple="false"
      :accept="accept"
      :on-change="fileChange"
    >
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
      <i
        v-if="upFilelimit != files.length"
        slot="trigger"
        class="el-icon-plus uploader-icon"
      />
      <div slot="tip" class="el-upload__tip">
        <span v-if="error" style="color: red">{{ error }}</span>
        <template v-else>
          <div style="height: 30px;line-height: 30px;">{{ upConfig.UpShow }}</div>
          <div v-if="imgSet && imgSet.Show" style="height: 30px;line-height: 30px;">{{ imgSet.Show }}</div>
        </template>
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
import { fileType } from '@/config/fileUpConfig'
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
      imgSrc: null
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
      const config = await fileApi.getUpConfig(
        this.fileKey,
        this.linkBizPk,
        this.tag
      )
      if (config.Extension == null) {
        config.Extension = []
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
    async fileChange(file) {
      this.$refs.upFileImg.abort(file)
      if (!this.checkUpFile(file)) {
        this.cancelUp(file.uid)
        return
      }
      const md5 = await this.loadMD5(file.raw)
      this.preFileUp(md5, file)
    },
    cancelUp(uid) {
      const index = this.files.findIndex(c => c.uid === uid)
      this.files.splice(index, 1)
    },
    async preFileUp(md5, file) {
      const taskId = await fileApi.blockUpFile(
        {
          FileMd5: md5,
          FileName: file.name,
          FileSize: file.size,
          Form: {
            LinkBizPk: this.linkBizPk,
            DirKey: this.fileKey,
            Tag: this.tag
          }
        }
      )
      this.checkState(taskId, file)
    },
    async checkState(taskId, file) {
      const res = await fileApi.getUpState(taskId)
      if (res.UpState === 5) {
        this.cancelUp(file.uid)
      } else if (res.UpState === 2) {
        this.uploadFile(res.Block)
      }
    },
    uploadFile(block) {
      for (let i = 0; i < block.NoUpIndex.length; i++) {
        const index = block.NoUpIndex[i]
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
