<template>
  <el-card>
    <div slot="header" class="clearfix">
      <span>{{ title }}</span>
    </div>
    <el-form label-width="100px">
      <el-card>
        <div slot="header" class="clearfix">
          <span>基础信息</span>
        </div>
        <el-row :gutter="24">
          <el-col :md="12" :sm="24">
            <el-form-item label="员工名">
              {{ emp.EmpName }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="员工编号">
              {{ emp.EmpNo }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="性别">
              <enumItem
                :value="emp.Sex"
                :readonly="true"
                :dic-key="HrEnumDic.hrUserSex"
                placeholder="性别"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="入职部门">
              <deptSelect
                :readonly="true"
                :value="emp.DeptId"
                placeholder="入职部门"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="岗位">
              <treeDicItem
                :readonly="true"
                :value="emp.PostCode"
                :dic-id="HrItemDic.post"
                placeholder="岗位"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="职务">
              <dictItem
                :readonly="true"
                :value="emp.Title"
                :multiple="true"
                :dic-id="HrItemDic.title"
                placeholder="职务"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="员工类型">
              <enumItem
                :readonly="true"
                :value="emp.UserType"
                :dic-key="HrEnumDic.hrUserType"
                placeholder="员工类型"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="备注说明">
              {{ emp.Show }}
            </el-form-item>
          </el-col>
        </el-row>
      </el-card>
      <el-card style="margin-top: 10px">
        <div slot="header" class="clearfix">
          <span>个人资料</span>
        </div>
        <el-row :gutter="24">
          <el-col :md="12" :sm="24">
            <el-form-item label="手机号" prop="Phone">
              {{ emp.Phone }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="备用联系电话">
              {{ emp.BackupPhone }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="名族" prop="Nation">
              <dictItem
                :readonly="true"
                :value="emp.Nation"
                :dic-id="HrItemDic.nation"
                placeholder="名族"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="身份证号">
              {{ emp.IDCard }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="生日" prop="Birthday">
              <span v-if="emp.Birthday">{{ moment(emp.Birthday).format('YYYY-MM-DD') }}</span>
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="Email">
              {{ emp.Email }}
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="住址" prop="Address">
              {{ emp.Address }}
            </el-form-item>
          </el-col>
          <el-col :md="24" :sm="24">
            <el-form-item label="头像">
              <img v-if="emp.UserHead" :src="emp.UserHead | imageUri">
            </el-form-item>
          </el-col>
        </el-row>
      </el-card>
    </el-form>
  </el-card>
</template>

<script>
import deptSelect from '@/components/unit/deptSelect.vue'
import { HrItemDic, HrEnumDic } from '@/config/publicDic'
import * as empApi from '@/api/emp/emp'
import moment from 'moment'
export default {
  components: {
    deptSelect
  },
  props: {
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrEnumDic,
      HrItemDic,
      title: '查看员工资料',
      emp: {}
    }
  },
  computed: {
  },
  watch: {
    id: {
      handler(val) {
        if (val != null) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    moment,
    async reset() {
      const res = await empApi.get(this.id)
      res.Title = res.TitleCode
      delete res.TitleCode
      this.emp = res
      this.title = '查看员工' + res.EmpName + '资料'
    }
  }
}
</script>
