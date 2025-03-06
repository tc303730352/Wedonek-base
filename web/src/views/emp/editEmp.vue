<template>
  <el-card>
    <div slot="header" class="clearfix">
      <span>{{ title }}</span>
    </div>
    <el-form :model="emp" label-width="100px" ref="empEdit" :rules="rules">
      <el-card>
        <div slot="header" class="clearfix">
          <span>基础信息</span>
        </div>
        <el-row :gutter="24">
          <el-col :md="12" :sm="24">
            <el-form-item label="员工名" prop="EmpName">
              <el-input
                v-model="emp.EmpName"
                maxlength="100"
                placeholder="员工名"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="员工编号" prop="EmpNo">
              <el-input
                v-model="emp.EmpNo"
                maxlength="50"
                placeholder="员工编号"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="性别" prop="Sex">
              <enumItem
                :dicKey="HrEnumDic.hrUserSex"
                placeholder="性别"
                :filters="[0]"
                v-model="emp.Sex"
              ></enumItem>
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="入职部门" prop="DeptId">
              <deptSelect
                v-model="emp.DeptId"
                :isChioseUnit="false"
                placeholder="入职部门"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="岗位" prop="PostCode">
              <treeDicItem
                :dicId="HrItemDic.post"
                v-model="emp.PostCode"
                placeholder="岗位"
              ></treeDicItem>
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="职务" prop="Title">
              <dictItem
                v-model="emp.Title"
                :multiple="true"
                :dicId="HrItemDic.title"
                placeholder="职务"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="员工类型" prop="UserType">
              <enumItem
                :dicKey="HrEnumDic.hrUserType"
                placeholder="员工类型"
                v-model="emp.UserType"
              ></enumItem>
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="备注说明" prop="Show">
              <el-input
                type="textarea"
                v-model="emp.Show"
                maxlength="255"
                placeholder="备注说明"
              />
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
              <el-input
                v-model="emp.Phone"
                maxlength="15"
                placeholder="手机号"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="备用联系电话" prop="BackupPhone">
              <el-input
                v-model="emp.BackupPhone"
                maxlength="50"
                placeholder="备用联系电话"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="名族" prop="Nation">
              <dictItem
                v-model="emp.Nation"
                :dicId="HrItemDic.nation"
                placeholder="名族"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="身份证号" prop="IDCard">
              <el-input
                v-model="emp.IDCard"
                maxlength="20"
                placeholder="身份证号"
                @change="idCardChange"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="生日" prop="Birthday">
              <el-date-picker
                v-model="emp.Birthday"
                class="el-input"
                type="date"
                placeholder="生日"
              >
              </el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="Email" prop="Email">
              <el-input
                v-model="emp.Email"
                maxlength="255"
                placeholder="Email"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12" :sm="24">
            <el-form-item label="住址" prop="Address">
              <el-input
                v-model="emp.Address"
                maxlength="100"
                placeholder="住址"
              />
            </el-form-item>
          </el-col>
          <el-col :md="24" :sm="24">
            <el-form-item label="上传头像">
              <imageUpBtn
                fileKey="EmpHead"
                @change="fileChange"
                :linkBizPk="empId"
              ></imageUpBtn>
            </el-form-item>
          </el-col>
        </el-row>
      </el-card>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button type="primary" @click="save">保存</el-button>
        <el-button @click="callback">返回</el-button>
      </el-row>
    </el-form>
  </el-card>
</template>
  
  <script>
import deptSelect from "@/components/unit/deptSelect.vue";
import { HrItemDic, HrEnumDic } from "@/config/publicDic";
import imageUpBtn from "@/components/fileUp/imageUpBtn";
import {
  isIDCard,
  isLetterAndInt,
  isPhone,
  isTelephone,
  validEmail,
} from "@/utils/validate";
import * as empApi from "@/api/emp/emp";
import moment from "moment";
export default {
  name: "EditEmp",
  computed: {
    comName() {
      const comId = this.$store.getters.curComId;
      return this.$store.getters.company[comId];
    },
    comId() {
      return this.$store.getters.curComId;
    },
  },
  data() {
    const validateEmpNo = async (rule, value, callback) => {
      if (value.isNull()) {
        callback();
      } else if (isLetterAndInt(value) == false) {
        callback(new Error("员工编号应由数字和字母构成!"));
      } else if (await this.checkRepeat(1, value)) {
        callback(new Error("员工编号重复!"));
      } else {
        callback();
      }
    };
    const vaildatePhone = async (rule, value, callback) => {
      if (value.isNull()) {
        callback();
      } else if (isPhone(value) == false) {
        callback(new Error("手机号格式错误!"));
      } else if (await this.checkRepeat(0, value)) {
        callback(new Error("手机号重复!"));
      } else {
        callback();
      }
    };
    const vaildateEmail = async (rule, value, callback) => {
      if (value.isNull()) {
        callback();
      } else if (value && validEmail(value) == false) {
        callback(new Error("Email格式错误!"));
      } else if (await this.checkRepeat(3, value)) {
        callback(new Error("Email重复!"));
      } else {
        callback();
      }
    };
    const vaildateTelephone = (rule, value, callback) => {
      if (value.isNull()) {
        callback();
      } else if (isTelephone(value) == false) {
        callback(new Error("备用联系电话格式错误!"));
      } else {
        callback();
      }
    };
    const vaildateIDCard = async (rule, value, callback) => {
      if (value.isNull()) {
        callback();
      } else if (isIDCard(value) == false) {
        callback(new Error("身份证号格式错误!"));
      } else if (await this.checkRepeat(2, value)) {
        callback(new Error("身份证号重复!"));
      } else {
        callback();
      }
    };
    return {
      HrEnumDic,
      HrItemDic,
      title: "编辑员工资料",
      emp: {},
      loading: false,
      empId: null,
      rules: {
        EmpName: [
          {
            required: true,
            message: "员工名不能为空！",
            trigger: "blur",
          },
          {
            min: 2,
            max: 100,
            message: "员工名长度在 2 到 100 个字之间",
            trigger: "blur",
          },
        ],
        DeptId: [
          {
            required: true,
            message: "请选择部门",
            trigger: "blur",
          },
        ],
        UserType: [
          {
            required: true,
            message: "请选择员工类型",
            trigger: "blur",
          },
        ],
        Title: [
          {
            required: true,
            message: "请选择职务",
            trigger: "blur",
          },
        ],
        Phone: [
          {
            required: true,
            message: "手机号不能为空！",
            trigger: "blur",
          },
          {
            min: 11,
            max: 15,
            message: "手机号格式错误!",
            trigger: "blur",
          },
          {
            required: true,
            trigger: "blur",
            validator: vaildatePhone,
          },
        ],
        Email: [
          {
            required: false,
            trigger: "blur",
            validator: vaildateEmail,
          },
        ],
        IDCard: [
          {
            required: false,
            trigger: "blur",
            validator: vaildateIDCard,
          },
        ],
        BackupPhone: [
          {
            required: false,
            trigger: "blur",
            validator: vaildateTelephone,
          },
        ],
        EmpNo: [
          {
            required: true,
            message: "员工编号不能为空！",
            trigger: "blur",
          },
          {
            min: 2,
            max: 20,
            message: "员工编号长度在 2 到 50 个字符之间!",
            trigger: "blur",
          },
          {
            required: true,
            trigger: "blur",
            validator: validateEmpNo,
          },
        ],
      },
    };
  },
  mounted() {
    this.empId = this.$route.params.id;
    this.reset();
  },
  methods: {
    async reset() {
      this.loading = false;
      if (this.empId == null) {
        this.emp = {};
        this.title = "新增员工";
      } else {
        const res = await empApi.get(this.empId);
        res.Title = res.TitleCode
        delete res.TitleCode
        this.emp = res;
        this.title = "编辑员工资料";
      }
    },
    save() {
      const that = this;
      this.$refs["empEdit"].validate((valid) => {
        if (valid) {
          if (that.empId == null) {
            that.add();
          } else {
            that.set();
          }
        } else {
          return false;
        }
      });
    },
    async checkRepeat(type, value) {
      return await empApi.checkRepeat({
        EmpId: this.empId,
        CompanyId: this.comId,
        CheckType: type,
        Value: value,
      });
    },
    async add() {
      this.loading = true;
      this.emp.CompanyId = this.comId;
      await empApi.add(this.emp);
      this.$message({
        type: "success",
        message: "添加成功!",
      });
      this.$router.replace({ name: "emp" });
    },
    async set() {
      this.loading = true;
      await empApi.set(this.empId, this.emp);
      this.$message({
        type: "success",
        message: "保存成功!",
      });
      this.$router.replace({ name: "emp" });
    },
    idCardChange(e) {
      if (this.emp.IDCard && this.emp.IDCard.length >= 14) {
        const date =
          this.emp.IDCard.substring(6, 10) +
          "-" +
          this.emp.IDCard.substring(10, 12) +
          "-" +
          this.emp.IDCard.substring(12, 14);
        this.emp.Birthday = moment(date);
      }
    },
    fileChange(fileId, files) {
      if (files.length == 0) {
        this.emp.UserHead = null;
        this.emp.FileId = null;
      } else {
        this.emp.UserHead = files[0].FileUri;
        this.emp.FileId = fileId[0];
      }
    },
    async saveHead(uri, fileId) {
      await empApi.saveHead(this.empId, uri, fileId);
    },
    callback() {
      this.$router.replace({ name: "emp" });
    },
  },
  components: {
    deptSelect,
    imageUpBtn,
  },
};
</script>