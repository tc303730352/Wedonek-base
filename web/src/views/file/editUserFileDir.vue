<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="dirEdit" :model="userDir" :rules="rules" label-width="150px">
      <el-form-item label="目录Key" prop="DirKey">
        <el-input
          v-model="userDir.DirKey"
          maxlength="50"
          placeholder="目录Key"
        />
      </el-form-item>
      <el-form-item label="目录名" prop="DirName">
        <el-input
          v-model="userDir.DirName"
          maxlength="50"
          placeholder="目录名"
        />
      </el-form-item>
      <el-form-item label="目录状态">
        <enumItem
          v-model="userDir.DirStatus"
          :dic-key="FileEnumDic.dirStatus"
          placeholder="目录状态"
        />
      </el-form-item>
      <el-form-item label="上传说明" prop="UpShow">
        <el-input
          v-model="userDir.UpShow"
          maxlength="100"
          placeholder="上传说明"
        />
      </el-form-item>
      <el-card>
        <span slot="header">上传限制</span>
        <el-form-item label="最大文件大小" prop="AllowUpFileSize">
          <el-input-number
            v-model="userDir.AllowUpFileSize"
            placeholder="最大文件上传大小(B)"
          />
          <span style="margin-left: 10px;">{{ formatFileSize(userDir.AllowUpFileSize) }}</span>
        </el-form-item>
        <el-form-item label="允许上传的文件">
          <p v-for="(item, index) in extension" :key="index">
            <el-input
              v-model="item.value"
              style="width: 200px"
              maxlength="10"
              :clearable="true"
              placeholder="文件扩展名"
            >
              <el-button
                slot="append"
                style="color: red"
                icon="el-icon-delete"
                @click="dropItem(index)"
              />
            </el-input>
          </p>
          <el-button type="primary" @click="addExtension">添加扩展名</el-button>
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 10px">
        <span slot="header">上传图片限制</span>
        <el-form-item label="限制的宽高比">
          <el-input-number v-model="upImgSet.RatioWidth" placeholder="宽" />
          <span
            style="
              width: 20px;
              display: inline-block;
              text-align: center;
              font-weight: 700;
            "
          >:</span>
          <el-input-number v-model="upImgSet.RatioHeight" placeholder="高" />
        </el-form-item>
        <el-form-item label="宽度范围">
          <el-input-number v-model="upImgSet.MinWidth" placeholder="最小宽度" />
          <span
            style="
              width: 20px;
              display: inline-block;
              text-align: center;
              font-weight: 700;
            "
          >-</span>
          <el-input-number v-model="upImgSet.MaxWidth" placeholder="最大宽度" />
        </el-form-item>
        <el-form-item label="高度范围">
          <el-input-number
            v-model="upImgSet.MinHeight"
            placeholder="最小高度"
          />
          <span
            style="
              width: 20px;
              display: inline-block;
              text-align: center;
              font-weight: 700;
            "
          >-</span>
          <el-input-number
            v-model="upImgSet.MaxHeight"
            placeholder="最大高度"
          />
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 10px">
        <span slot="header">上传权限</span>
        <el-form-item label="是否需登陆" prop="IsAccredit">
          <el-switch v-model="userDir.IsAccredit" />
        </el-form-item>
        <el-form-item v-if="userDir.IsAccredit" label="上传权限">
          <p v-for="(item, index) in upPower" :key="index">
            <el-input
              v-model="item.value"
              style="width: 255px"
              maxlength="50"
              :clearable="true"
              placeholder="上传权限值"
            >
              <el-button
                slot="append"
                style="color: red"
                icon="el-icon-delete"
                @click="dropPower(index)"
              />
            </el-input>
          </p>
          <el-button type="primary" @click="addPower">添加权限值</el-button>
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 10px">
        <span slot="header">文件访问权限</span>
        <el-form-item label="访问权限类型" prop="Power">
          <enumItem
            v-model="userDir.Power"
            :dic-key="FileEnumDic.filePower"
            placeholder="访问权限类型"
          />
        </el-form-item>
        <el-form-item v-if="userDir.Power === 2" label="访问权限值">
          <p v-for="(item, index) in readPower" :key="index">
            <el-input
              v-model="item.value"
              style="width: 255px"
              maxlength="50"
              :clearable="true"
              placeholder="访问权限值"
            >
              <el-button
                slot="append"
                style="color: red"
                icon="el-icon-delete"
                @click="dropReadPower(index)"
              />
            </el-input>
          </p>
          <el-button type="primary" @click="addReadPower">添加权限值</el-button>
        </el-form-item>
      </el-card>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { FileEnumDic } from '@/config/fileConfig'
import * as dirApi from '@/api/file/userFileDir'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      FileEnumDic,
      title: '新增用户文件目录',
      source: null,
      userDir: {},
      upImgSet: {},
      extension: [],
      upPower: [],
      readPower: [],
      rules: {
        DirKey: [
          {
            required: true,
            message: '目录Key不能为空！',
            trigger: 'blur'
          }
        ],
        DirName: [
          { required: true, message: '目录名不能为空!', trigger: 'blur' }
        ],
        DirStatus: [
          { required: true, message: '目录状态不能为空!', trigger: 'blur' }
        ]
      }
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    save() {
      const that = this
      this.$refs['dirEdit'].validate((valid) => {
        if (valid) {
          if (that.id) {
            that.set()
          } else {
            that.add()
          }
        } else {
          return false
        }
      })
    },
    dropItem(index) {
      this.extension.splice(index, 1)
    },
    addExtension() {
      if (this.extension.findIndex((c) => c.value == null) !== -1) {
        return
      }
      this.extension.push({
        value: null
      })
    },
    dropPower(index) {
      this.upPower.splice(index, 1)
    },
    addPower() {
      if (this.upPower.findIndex((c) => c.value == null) !== -1) {
        return
      }
      this.upPower.push({
        value: null
      })
    },
    dropReadPower(index) {
      this.readPower.splice(index, 1)
    },
    addReadPower() {
      if (this.readPower.findIndex((c) => c.value == null) !== -1) {
        return
      }
      this.readPower.push({
        value: null
      })
    },
    format() {
      if (
        this.extension.findIndex(
          (c) => c.value == null || !/^[.]\w+$/g.test(c.value)
        ) !== -1
      ) {
        this.$message({
          message: '文件扩展名格式错误或为空!',
          type: 'error'
        })
        return
      }
      if (
        this.userDir.IsAccredit &&
        this.upPower.findIndex(
          (c) => c.value == null || !/^(\w|[.])+$/g.test(c.value)
        ) !== -1
      ) {
        this.$message({
          message: '上传权限码错误或为空!',
          type: 'error'
        })
        return
      }
      if (
        this.userDir.Power === 2 &&
        this.readPower.findIndex(
          (c) => c.value == null || !/^(\w|[.])+$/g.test(c.value)
        ) !== -1
      ) {
        this.$message({
          message: '访问权限码错误或为空!',
          type: 'error'
        })
        return
      }
      const data = Object.assign({}, this.userDir)
      if (data.AllowUpFileSize === 0) {
        data.AllowUpFileSize = null
      }
      if (this.extension) {
        data.AllowExtension = this.extension.map((c) => c.value)
      } else {
        delete data.AllowExtension
      }
      if (data.IsAccredit) {
        data.UpPower = this.upPower.map((c) => c.value)
      } else {
        delete data.UpPower
      }
      if (data.Power === 2) {
        data.ReadPower = this.readPower.map((c) => c.value)
      } else {
        delete data.ReadPower
      }
      delete data.UpImgSet
      if (this.upImgSet != null) {
        data.UpImgSet = {
          MinHeight: this.upImgSet.MinHeight === 0 ? null : this.upImgSet.MinHeight,
          MinWidth: this.upImgSet.MinWidth === 0 ? null : this.upImgSet.MinWidth,
          MaxHeight: this.upImgSet.MaxHeight === 0 ? null : this.upImgSet.MaxHeight,
          MaxWidth: this.upImgSet.MaxWidth === 0 ? null : this.upImgSet.MaxWidth
        }
        if (this.upImgSet.RatioWidth != null && this.upImgSet.RatioHeight != null && this.upImgSet.RatioHeight !== 0 && this.upImgSet.RatioWidth !== 0) {
          data.UpImgSet.Ratio = [this.upImgSet.RatioWidth, this.upImgSet.RatioHeight]
        }
      }
      return data
    },
    async set() {
      const data = this.format()
      delete data.DirKey
      await dirApi.set(this.id, data)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async add() {
      const data = this.format()
      await dirApi.add(data)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async reset() {
      if (this.id == null) {
        this.title = '新增用户文件目录'
        this.source = null
      } else {
        const data = await dirApi.get(this.id)
        this.title = '编辑' + data.DirName + '用户文件目录'
        this.source = data
      }
      this.resetForm()
    },
    closeForm() {
      this.$emit('close', false)
    },
    formatFileSize(size) {
      if (size == null || size === 0) {
        return '无限制'
      }
      size = parseInt(size)
      let t = 1024 * 1024 * 1024
      let num = Math.floor(size / t)
      let str = ''
      if (num !== 0) {
        str = num + 'GB '
        size = size % t
      }
      t = 1024 * 1024
      num = Math.floor(size / t)
      if (num !== 0) {
        str = str + num + 'MB '
        size = size % t
      }
      t = 1024
      num = Math.floor(size / t)
      if (num !== 0) {
        str = str + num + 'KB '
        size = size % t
      }
      if (size !== 0) {
        str = str + size + 'B'
      }
      return str
    },
    resetForm() {
      if (this.source == null) {
        this.userDir = {
          DirKey: null,
          DirName: null,
          DirStatus: 0,
          AllowUpFileSize: null,
          IsAccredit: false,
          Power: 0,
          UpShow: null
        }
        this.extension = []
        this.upPower = []
        this.readPower = []
        this.upImgSet = {
          RatioWidth: null,
          RatioHeight: null,
          MinWidth: null,
          MaxHeight: null,
          MinHeight: null,
          MaxWidth: null
        }
      } else {
        const data = this.source
        this.userDir = data
        this.extension = data.AllowExtension
          ? data.AllowExtension.map((c) => {
            return {
              value: c
            }
          })
          : []
        if (data.IsAccredit) {
          this.upPower = data.UpPower.map((c) => {
            return {
              value: c
            }
          })
        } else {
          this.upPower = []
        }
        if (data.Power === 2) {
          this.readPower = data.ReadPower.map((c) => {
            return {
              value: c
            }
          })
        } else {
          this.readPower = []
        }
        if (data.UpImgSet != null) {
          this.upImgSet = {
            RatioWidth: null,
            RatioHeight: null,
            MinWidth: data.UpImgSet.MinWidth,
            MaxHeight: data.UpImgSet.MaxHeight,
            MinHeight: data.UpImgSet.MinHeight,
            MaxWidth: data.UpImgSet.MaxWidth
          }
          if (data.UpImgSet.Ratio && data.UpImgSet.Ratio.length > 0) {
            this.upImgSet.RatioWidth = data.UpImgSet.Ratio[0]
            this.upImgSet.RatioHeight = data.UpImgSet.Ratio[1]
          }
        } else {
          this.upImgSet = {
            RatioWidth: null,
            RatioHeight: null,
            MinWidth: null,
            MaxHeight: null,
            MinHeight: null,
            MaxWidth: null
          }
        }
      }
    }
  }
}
</script>
