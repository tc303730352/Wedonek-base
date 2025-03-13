<template>
  <el-card>
    <div slot="header">
      <span>文件列表</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="文件名">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="文件名"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="文件类型">
          <enumItem
            v-model="queryParam.FileType"
            :dic-key="FileEnumDic.fileType"
            placeholder="文件类型"
            :multiple="true"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="文件保存方式">
          <enumItem
            v-model="queryParam.SaveType"
            :dic-key="FileEnumDic.fileSaveType"
            placeholder="文件保存方式"
            :multiple="true"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="files"
      :columns="columns"
      :is-paging="true"
      :paging="paging"
      @load="load"
    >
      <template slot="File" slot-scope="e">
        <fileShow :file-name="e.row.FileName" :file-uri="e.row.FileUri" style="max-width: 200px; max-height: 120px;" />
      </template>
      <template slot="FileName" slot-scope="e">
        {{ e.row.FileName }}
      </template>
      <template slot="FileType" slot-scope="e">
        {{ FileType[e.row.FileType].text }}
      </template>
      <template slot="FileSize" slot-scope="e">
        {{ formatFileSize(e.row.FileSize) }}
      </template>
      <template slot="SaveType" slot-scope="e">
        {{ FileSaveType[e.row.SaveType].text }}
      </template>
      <template slot="SaveTime" slot-scope="e">
        {{ moment(e.row.SaveTime).format("YYYY-MM-DD HH:mm") }}
      </template>
    </w-table>
  </el-card>
</template>

<script>
import moment from 'moment'
import * as fileApi from '@/api/file/fileList'
import fileShow from './components/fileShow.vue'
import { FileEnumDic, FileType, FileSaveType } from '@/config/fileConfig'
export default {
  components: {
    fileShow
  },
  data() {
    return {
      FileType,
      FileSaveType,
      FileEnumDic,
      files: [],
      queryParam: {
        QueryKey: null,
        FileType: null,
        SaveType: null,
        Begin: null,
        End: null
      },
      columns: [
        {
          sortby: 'File',
          key: 'File',
          title: '文件',
          slotName: 'File',
          align: 'center',
          width: 200
        },
        {
          sortby: 'FileName',
          key: 'FileName',
          title: '文件名',
          slotName: 'FileName',
          align: 'left',
          minWidth: 150,
          sortable: 'custom'
        },
        {
          sortby: 'FileMd5',
          key: 'FileMd5',
          title: '文件MD5',
          align: 'center',
          width: 320
        },
        {
          sortby: 'FileType',
          key: 'FileType',
          title: '文件类型',
          align: 'center',
          slotName: 'FileType',
          width: 120,
          sortable: 'custom'
        },
        {
          sortby: 'FileSize',
          key: 'FileSize',
          title: '文件大小',
          align: 'center',
          slotName: 'FileSize',
          minWidth: 100,
          sortable: 'custom'
        },
        {
          sortby: 'FileNum',
          key: 'FileNum',
          title: '文件引用数',
          align: 'right',
          width: 120
        },
        {
          sortby: 'SaveType',
          key: 'SaveType',
          title: '保存方式',
          align: 'center',
          width: 120,
          slotName: 'SaveType',
          sortable: 'custom'
        },
        {
          key: 'SaveTime',
          title: '添加时间',
          align: 'center',
          minWidth: 120,
          slotName: 'SaveTime'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      }
    }
  },
  mounted() {
    this.load()
  },
  methods: {
    moment,
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.FileType = null
      this.queryParam.SaveType = null
      this.queryParam.Begin = null
      this.queryParam.End = null
      this.paging.Index = 1
      this.load()
    },
    formatFileSize(size) {
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
    async load() {
      const res = await fileApi.Query(this.queryParam, this.paging)
      this.files = res.List
      this.paging.Total = res.Count
    }
  }
}
</script>
