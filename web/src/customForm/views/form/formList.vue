<template>
  <div>
    <leftRightSplit :left-span="4">
      <el-card slot="left">
        <div slot="header">
          <span>表单分类</span>
        </div>
        <dicItemTree
          ref="dicItemTree"
          :dic-id="DictItemDic.formType"
          @change="chioseFormType"
          @load="loadFormType"
        />
      </el-card>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
        </div>
        <el-row>
          <el-form :inline="true" :model="queryParam">
            <el-form-item label="表单名">
              <el-input
                v-model="queryParam.QueryKey"
                placeholder="表单名"
                @change="load"
              />
            </el-form-item>
            <el-form-item label="表单状态">
              <enumItem
                v-model="queryParam.Status"
                :dic-key="EnumDic.FormStatus"
                placeholder="表单状态"
                sys-head="form"
                :multiple="true"
                @change="load"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                type="success"
                @click="add"
              >添加表单</el-button>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <w-table
          :data="formList"
          :columns="columns"
          :is-paging="true"
          row-key="Id"
          :paging="paging"
          @load="load"
        >
          <template slot="formType" slot-scope="e">
            {{ formType[e.row.FormType] }}
          </template>
          <template slot="createTime" slot-scope="e">
            {{ moment(e.row.CreateTime).format('YYYY-MM-DD HH:mm') }}
          </template>
          <template slot="status" slot-scope="e">
            <el-switch
              :value="e.row.FormStatus == 1"
              active-text="启用"
              :inactive-text="e.row.FormStatus == 0 ? '起草' : '停用'"
              @change="(val)=> setStatus(e.row, val)"
            />
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              size="mini"
              type="primary"
              title="编辑表单"
              icon="el-icon-edit"
              circle
              @click="edit(e.row)"
            />
            <el-button
              size="mini"
              type="primary"
              title="编辑表单结构"
              icon="el-icon-setting"
              circle
              @click="editForm(e.row)"
            />
            <el-button
              size="mini"
              type="danger"
              title="删除表单"
              icon="el-icon-delete"
              circle
              @click="drop(e.row)"
            />
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <editForm :id="id" :form-type="queryParam.FormType" :visible="visible" @close="close" />
  </div>
</template>

<script>
import moment from 'moment'
import * as formApi from '@/customForm/api/form'
import {
  FormStatus,
  DictItemDic,
  EnumDic
} from '@/customForm/config/formConfig'
import dicItemTree from '@/components/base/dicItemTree.vue'
import editForm from './editForm.vue'
export default {
  components: {
    dicItemTree,
    editForm
  },
  data() {
    return {
      DictItemDic,
      FormStatus,
      EnumDic,
      formList: [],
      rolePower: [
        'form.add',
        'form.set',
        'form.delete'
      ],
      isShowChildren: false,
      title: '表单列表',
      formType: {},
      columns: [
        {
          key: 'FormName',
          title: '表单标题',
          align: 'left',
          width: 150
        },
        {
          key: 'FormShow',
          title: '表单使用说明',
          align: 'right',
          minWidth: 200
        },
        {
          key: 'FormType',
          title: '表单类型',
          align: 'center',
          slotName: 'formType',
          minWidth: 100
        },
        {
          key: 'FormStatus',
          title: '表单状态',
          align: 'center',
          slotName: 'status',
          minWidth: 120
        },
        {
          key: 'Ver',
          title: '版本号',
          align: 'center',
          width: 125,
          sortable: 'custom'
        },
        {
          key: 'CreateTime',
          title: '创建时间',
          align: 'center',
          slotName: 'createTime',
          sortable: 'custom',
          minWidth: 100
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          fixed: 'right',
          width: '180px',
          slotName: 'action'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'Id',
        IsDesc: false,
        Total: 0
      },
      visible: false,
      id: null,
      queryParam: {
        CompanyId: null,
        QueryKey: null,
        Status: null,
        FormType: null
      }
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  mounted() {
    this.initPower()
    this.title = '表单列表'
  },
  methods: {
    moment,
    close(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.load()
      }
    },
    loadFormType(e) {
      this.formType = e.text
      this.load()
    },
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
    editForm(row) {
      this.$router.push({ name: 'formBodyEdit', params: { id: row.Id }})
    },
    add() {
      this.id = null
      this.visible = true
    },
    async setStatus(row, isEnable) {
      if (isEnable) {
        await formApi.Enable(row.Id)
      } else {
        await formApi.Disable(row.Id)
      }
      this.$message({
        type: 'success',
        message: '设置成功!'
      })
      row.FormStatus = isEnable ? 1 : 0
    },
    async load() {
      this.queryParam.CompanyId = this.comId
      const res = await formApi.Query(this.queryParam, this.paging)
      if (res.List) {
        this.formList = res.List
      } else {
        this.formList = []
      }
      this.paging.Total = res.Count
    },
    drop(row) {
      const title = '确认删除表单 ' + row.FormName + '?'
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
      await formApi.Delete(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.load()
    },
    reset() {
      this.queryParam.QueryKey = null
      this.queryParam.Status = null
      this.paging.Index = 1
      this.load()
    },
    edit(row) {
      this.id = row.Id
      this.visible = true
    },
    chioseFormType(e) {
      if (e.value.length === 0) {
        this.queryParam.FormType = null
        this.title = '表单列表'
      } else {
        const item = e.value[0]
        this.queryParam.FormType = item.value
        this.title = item.text + '-表单列表'
      }
      this.load()
    }
  }
}
</script>
