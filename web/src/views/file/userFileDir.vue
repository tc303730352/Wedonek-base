<template>
  <el-card>
    <div slot="header">
      <span>用户文件目录</span>
    </div>
    <el-row>
      <el-form :inline="true" :model="queryParam">
        <el-form-item label="目录名/目录Key">
          <el-input
            v-model="queryParam.QueryKey"
            placeholder="文件名"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="目录状态">
          <enumItem
            v-model="queryParam.DirStatus"
            :dic-key="FileEnumDic.dirStatus"
            placeholder="目录状态"
            :multiple="true"
            @change="load"
          />
        </el-form-item>
        <el-form-item label="访问权限类型">
          <enumItem
            v-model="queryParam.Power"
            :dic-key="FileEnumDic.filePower"
            placeholder="访问权限类型"
            :multiple="false"
            @change="load"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="success" @click="add">新增用户文件目录</el-button>
          <el-button @click="reset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-row>
    <w-table
      :data="dirList"
      :columns="columns"
      :is-paging="true"
      :paging="paging"
      @load="load"
    >
      <template slot="UpLimit" slot-scope="e">
        <p>文件大小限制：{{ formatFileSize(e.row.AllowUpFileSize) }}。</p>
        <p>允许上传的文件：{{ e.row.AllowExtension.join(',') }}。</p>
      </template>
      <template slot="DirStatus" slot-scope="e">
        {{ DirStatus[e.row.DirStatus].text }}
      </template>
      <template slot="FileSize" slot-scope="e">
        {{ formatFileSize(e.row.FileSize) }}
      </template>
      <template slot="UpPower" slot-scope="e">
        <p>是否需要登陆：{{ e.row.IsAccredit ? '是' : '否' }}。</p>
        <p v-if="e.row.IsAccredit">需要的权限码：{{ e.row.UpPower && e.row.UpPower.length !== 0 ? e.row.UpPower.join(',') : '无' }}。</p>
      </template>
      <template slot="ReadPower" slot-scope="e">
        <p>访问权限类型：{{ FilePower[e.row.Power].text }}。</p>
        <p v-if="e.row.ReadPower">需要的权限码：{{ e.row.ReadPower.join(',') }}。</p>
      </template>
      <template slot="UpImgSet" slot-scope="e">
        <template v-if="e.row.UpImgSet">
          <p v-if="e.row.UpImgSet.Ratio">高宽比：{{ e.row.UpImgSet.Ratio[0] + ':' +e.row.UpImgSet.Ratio[1] }}。</p>
          <p v-if="e.row.UpImgSet.MinWidth || e.row.UpImgSet.MaxWidth">宽度范围：{{ e.row.UpImgSet.MinWidth }} - {{ e.row.UpImgSet.MaxWidth }}。</p>
          <p v-if="e.row.UpImgSet.MinHeight || e.row.UpImgSet.MaxHeight">高度范围：{{ e.row.UpImgSet.MinHeight }} - {{ e.row.UpImgSet.MaxHeight }}。</p>
        </template>
      </template>
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="编辑"
          icon="el-icon-edit"
          circle
          @click="edit(e.row.Id)"
        />
        <el-button
          v-if="e.row.FileNum == 0"
          size="mini"
          type="danger"
          title="删除"
          icon="el-icon-delete"
          circle
          @click="drop(e.row)"
        />
      </template>
    </w-table>
    <editUserFileDir :id="id" :visible="visible" @close="closeEdit" />
  </el-card>
</template>

<script>
import moment from 'moment'
import * as dirApi from '@/api/file/userFileDir'
import { FileEnumDic, DirStatus, FilePower } from '@/config/fileConfig'
import editUserFileDir from './editUserFileDir.vue'
export default {
  components: {
    editUserFileDir
  },
  data() {
    return {
      DirStatus,
      FileEnumDic,
      FilePower,
      dirList: [],
      id: null,
      visible: false,
      queryParam: {
        QueryKey: null,
        Power: null,
        DirStatus: null
      },
      columns: [
        {
          sortby: 'DirKey',
          key: 'DirKey',
          title: '目录Key',
          align: 'center',
          width: 150,
          sortable: 'custom'
        },
        {
          sortby: 'DirName',
          key: 'DirName',
          title: '目录名',
          align: 'center',
          minWidth: 150,
          sortable: 'custom'
        },
        {
          sortby: 'DirStatus',
          key: 'DirStatus',
          title: '目录状态',
          align: 'center',
          slotName: 'DirStatus',
          width: 120,
          sortable: 'custom'
        },
        {
          key: 'UpLimit',
          title: '上传限制',
          align: 'left',
          slotName: 'UpLimit',
          minWidth: 100
        },
        {
          key: 'UpPower',
          title: '上传权限',
          align: 'left',
          slotName: 'UpPower',
          minWidth: 100
        },
        {
          key: 'ReadPower',
          title: '访问权限',
          align: 'left',
          slotName: 'ReadPower',
          minWidth: 100
        },
        {
          key: 'FileNum',
          title: '文件引用数',
          align: 'right',
          width: 120
        },
        {
          key: 'UpImgSet',
          title: '图片配置',
          align: 'center',
          width: 200,
          slotName: 'UpImgSet'
        },
        {
          key: 'UpShow',
          title: '上传说明',
          align: 'left',
          minWidth: 120
        },
        {
          key: 'action',
          title: '操作',
          align: 'left',
          minWidth: 100,
          slotName: 'action'
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
    closeEdit(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    edit(id) {
      this.id = id
      this.visible = true
    },
    drop(row) {
      const title = '确认删除用户文件目录：' + row.DirName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row.Id)
      })
    },
    async submitDrop(id) {
      await dirApi.deleteDir(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    add() {
      this.id = null
      this.visible = true
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.Power = null
      this.queryParam.DirStatus = null
      this.paging.Index = 1
      this.load()
    },
    formatFileSize(size) {
      if (size == null) {
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
    async load() {
      const res = await dirApi.Query(this.queryParam, this.paging)
      this.dirList = res.List
      this.paging.Total = res.Count
    }
  }
}
</script>
<style scoped>
p {
    margin: 0;
}
</style>
