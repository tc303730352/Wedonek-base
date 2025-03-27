<template>
  <el-dialog
    :title="title"
    :visible="visible"
    min-width="1280px"
    width="80%"
    :before-close="closeForm"
    :modal-append-to-body="false"
    :append-to-body="true"
    :close-on-click-modal="false"
  >
    <el-row>
      <el-col :span="6">
        <el-form label-width="120px">
          <el-form-item label="公司全称">
            {{ company.FullName }}
          </el-form-item>
          <el-form-item label="简称">
            {{ company.ShortName }}
          </el-form-item>
          <el-form-item label="所属上级">
            <companySelect
              :value="company.ParentId"
              :readonly="true"
              placeholder="所属上级"
            />
          </el-form-item>
          <el-form-item label="公司类型">
            <span v-if="company.CompanyType">{{ hrCompanyType[company.CompanyType].text }}</span>
          </el-form-item>
          <el-form-item label="联系人">
            <span>{{ company.Contacts }}</span>
            <span v-if="company.Telephone != null && company.Telephone != ''">({{ company.Telephone }})</span>
          </el-form-item>
          <el-form-item label="负责人">
            <el-button v-if="company.LeaverId" type="text" @click="showEmp(company.LeaverId)"> {{ company.Leaver }}</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="18">
        <el-card>
          <div slot="header">
            <span>{{ company.FullName }}{{ isEntry? '入职': '' }}员工列表</span>
            <el-switch
              v-model="isEntry"
              style="float:right"
              active-text="显示入职员工"
              inactive-text="显示本公司员工"
            />
          </div>
          <empListView :company-id="id" :is-entry="isEntry" :is-load="isLoad" />
        </el-card>
      </el-col>
    </el-row>
    <empModel
      :id="empId"
      :visible="empVisible"
      @close="empVisible=false"
    />
  </el-dialog>
</template>

<script>
import * as companyApi from '@/api/base/company'
import companySelect from '@/components/company/companySelect.vue'
import { hrCompanyType } from '@/config/publicDic'
import empListView from '@/components/emp/empListView.vue'
import empModel from '@/components/emp/empModel.vue'
export default {
  name: 'DeptView',
  components: {
    companySelect,
    empListView,
    empModel
  },
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
      hrCompanyType,
      title: '公司信息',
      company: {},
      isLoad: false,
      isEntry: false,
      empId: null,
      empVisible: false
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        } else {
          this.isLoad = false
        }
      },
      immediate: true
    }
  },
  methods: {
    showEmp(empId) {
      this.empId = empId
      this.empVisible = true
    },
    async reset() {
      const data = await companyApi.Get(this.id)
      this.company = data
      this.title = '查看公司:' + data.FullName
      this.isLoad = true
    },
    closeForm() {
      this.$emit('update:visible', false)
    }
  }
}
</script>
