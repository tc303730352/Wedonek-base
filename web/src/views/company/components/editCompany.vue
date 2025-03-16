<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="60%"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="companyEdit" :model="company" :rules="rules">
      <el-form-item label="公司全称" prop="FullName">
        <el-input
          v-model="company.FullName"
          maxlength="100"
          placeholder="公司全称"
        />
      </el-form-item>
      <el-form-item label="公司简称" prop="ShortName">
        <el-input
          v-model="company.ShortName"
          maxlength="50"
          placeholder="公司简称"
        />
      </el-form-item>
      <el-form-item label="公司类型" prop="CompanyType">
        <enumItem
          v-model="company.CompanyType"
          :dic-key="HrEnumDic.companyType"
          placeholder="公司类型"
          @change="loadTrees"
        />
      </el-form-item>
      <el-form-item label="所属父级公司" prop="ParentId">
        <el-cascader
          v-model="company.ParentId"
          :options="trees"
          placeholder="所属父级公司"
          :props="{checkStrictly: true, emitPath: false}"
          :clearable="true"
          :disabled="company.CompanyType == null || company.CompanyType == 0"
          class="el-input"
        />
      </el-form-item>
      <el-form-item label="地址" prop="Address">
        <el-input
          v-model="company.Address"
          maxlength="50"
          placeholder="地址"
        />
      </el-form-item>
      <el-form-item label="联系人" prop="Contacts">
        <el-input
          v-model="company.Contacts"
          maxlength="50"
          placeholder="联系人"
        />
      </el-form-item>
      <el-form-item label="联系电话" prop="Telephone">
        <el-input
          v-model="company.Telephone"
          maxlength="30"
          placeholder="联系电话"
        />
      </el-form-item>
      <el-form-item v-if="id != null" label="负责人" prop="LeaderId">
        <empInput
          v-model="company.LeaderId"
          :company-id="id"
          placeholder="负责人"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as companyApi from '@/api/base/company'
import { HrEnumDic } from '@/config/publicDic'
import empInput from '@/components/emp/empInput.vue'
export default {
  name: 'EditDept',
  components: {
    empInput
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    },
    companys: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      HrEnumDic,
      title: '新增公司',
      source: null,
      company: {},
      trees: [],
      rules: {
        FullName: [
          {
            required: true,
            message: '公司全称不能为空！',
            trigger: 'blur'
          }
        ],
        CompanyType: [
          {
            required: true,
            message: '公司类型不能为空！',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {
  },
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
      this.$refs['companyEdit'].validate((valid) => {
        if (valid) {
          if (that.company.CompanyType === 0) {
            that.company.ParentId = null
          }
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
    async set() {
      await companyApi.Set(this.id, this.company)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async add() {
      await companyApi.Add(this.company)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async reset() {
      this.loadTrees()
      if (this.id == null) {
        this.title = '新增公司'
        this.source = null
        this.company = {}
      } else {
        const data = await companyApi.Get(this.id)
        this.source = data
        this.company = Object.assign({}, data)
        this.title = '编辑公司:' + data.FullName + '信息'
      }
    },
    isNull(str) {
      return str == null || str === ''
    },
    loadTrees() {
      const list = []
      this.companys.forEach(c => {
        const item = {
          value: c.Id,
          label: this.isNull(c.ShortName) ? c.FullName : c.ShortName,
          children: this.getChildren(c),
          disabled: c.CompanyType === 0 && this.company.CompanyType === 0
        }
        list.push(item)
      })
      this.trees = list
    },
    getChildren(c) {
      if (c.Children == null || c.Children.length === 0) {
        return null
      }
      const list = []
      c.Children.forEach(a => {
        if (a.CompanyType === 2) {
          return
        }
        list.push({
          value: a.Id,
          label: this.isNull(a.ShortName) ? a.FullName : a.ShortName,
          children: this.getChildren(a),
          disabled: false
        })
      })
      return list.length === 0 ? null : list
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.id == null) {
        this.dept = {}
      } else {
        this.company = Object.assign({}, this.source)
      }
    }
  }
}
</script>
