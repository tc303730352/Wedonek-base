<template>
  <div>
    <leftRightSplit :leftSpan="4">
      <el-card slot="left">
        <div slot="header">
          <span>所在单位部门</span>
        </div>
        <unitDeptTree
          ref="deptTree"
          @load="chioseDept"
          @change="chioseDept"
        ></unitDeptTree>
      </el-card>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
        </div>
        <el-row>
          <el-form :inline="true" :model="queryParam">
            <el-form-item label="关键字">
              <el-input
                v-model="queryParam.QueryKey"
                placeholder="人员名\编号\手机号"
                @change="loadEmp"
              />
            </el-form-item>
            <el-form-item label="岗位">
              <treeDicItem
                :dicId="HrItemDic.post"
                v-model="queryParam.Post"
                :isMultiple="true"
                placeholder="人员岗位"
                @change="loadEmp"
              ></treeDicItem>
            </el-form-item>
            <el-form-item label="职务">
              <dictItem
                v-model="queryParam.Title"
                :multiple="true"
                :dicId="HrItemDic.title"
                @change="loadEmp"
                placeholder="职务"
              />
            </el-form-item>
            <el-form-item label="员工类型">
              <enumItem
                :dicKey="HrEnumDic.hrUserType"
                placeholder="员工类型"
                :multiple="true"
                v-model="queryParam.UserType"
                @change="loadEmp"
              ></enumItem>
            </el-form-item>
            <el-form-item>
              <el-checkbox
                v-model="queryParam.IsNoOpen"
                label="未开通账号"
                @change="loadEmp"
              ></el-checkbox>
            </el-form-item>
            <el-form-item>
              <el-button type="success" @click="addEmp">添加员工</el-button>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <p :gutter="24" v-if="chioseEmp.length > 0">
          <span style="margin-right: 10px">已选择:{{ chioseEmp.length }}</span>
          <el-button type="primary" size="mini" @click="openAccount"
            >开通账号</el-button
          >
        </p>
        <w-table
          :data="emps"
          :columns="columns"
          :isPaging="true"
          :isSelect="true"
          rowKey="EmpId"
          :selectKeys="chioseEmp"
          :isMultiple="true"
          :checkIsSelect="checkIsSelect"
          @selected="selected"
          @load="loadEmp"
          :paging="paging"
        >
          <template slot="empBase" slot-scope="e">
            <div class="userInfo">
              <div class="head">
                <img
                  v-if="e.row.UserHead == null || e.row.UserHead == ''"
                  src="/image/defhead.png"
                  width="50"
                  height="50"
                />
                <el-avatar
                  v-else
                  shape="square"
                  :size="50"
                  :src="e.row.UserHead | imageUri"
                ></el-avatar>
              </div>
              <div class="content">
                <div class="item">
                  <i
                    style="color: #409eff"
                    v-if="e.row.Sex == 1"
                    class="el-icon-male"
                  ></i>
                  <i
                    v-else-if="e.row.Sex == 2"
                    style="color: #f56c6c"
                    class="el-icon-female"
                  ></i>
                  {{ e.row.EmpName }}
                </div>
                <div class="empNo">{{ e.row.EmpNo }}</div>
              </div>
            </div>
          </template>
          <template slot="status" slot-scope="e">
            <el-switch
              :value="e.row.Status"
              :inactive-value="2"
              :active-value="1"
              @change="(val) => setStatus(e.row, val)"
            ></el-switch>
          </template>
          <template slot="UserType" slot-scope="e">
            {{ HrUserType[e.row.UserType].text }}
          </template>
          <template slot="empTitle" slot-scope="e">
            <span v-if="e.row.DeptTitle">{{ e.row.DeptTitle }}</span>
            <el-tooltip
              v-if="e.row.Title && e.row.Title.length > 0"
              effect="dark"
              placement="bottom"
            >
              <div slot="content">
                <p v-for="(t, index) in e.row.Title" :key="index">
                  {{ t.Show }}
                </p>
              </div>
              <i class="el-icon-more"></i>
            </el-tooltip>
          </template>
          <template slot="birthday" slot-scope="e">
            {{
              e.row.Birthday
                ? moment(e.row.Birthday).format("YYYY-MM-DD")
                : null
            }}
          </template>
          <template slot="regTime" slot-scope="e">
            {{ moment(e.row.RegTime).format("YYYY-MM-DD") }}
          </template>
          <template slot="isOpenAccount" slot-scope="e">
            <el-tag v-if="e.row.IsOpenAccount == false">未开通</el-tag>
            <el-tag type="success" v-else>已开通</el-tag>
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              size="mini"
              type="primary"
              title="编辑人员资料"
              icon="el-icon-edit"
              @click="editEmp(e.row.EmpId)"
              circle
            ></el-button>
            <el-button
              size="mini"
              type="default"
              title="编辑人员职务"
              icon="el-icon-postcard"
              @click="editEmpTitle(e.row.EmpId)"
              circle
            ></el-button>
            <el-button
              v-if="e.row.Status == 0"
              size="mini"
              type="danger"
              title="删除员工"
              icon="el-icon-delete"
              @click="dropEmp(e.row)"
              circle
            ></el-button>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <empTitleEdit
      :empId="empId"
      :visible="visible"
      @cancel="() => (visible = false)"
    ></empTitleEdit>
  </div>
</template>
  
  <script>
import moment from "moment";
import * as empApi from "@/api/emp/emp";
import { open } from "@/api/emp/loginUser";
import empTitleEdit from "../empTitle/empTitleList.vue";
import {
  HrEnumDic,
  HrItemDic,
  HrUserType,
  HrEmpStatus,
} from "@/config/publicDic";
import unitDeptTree from "@/components/unit/unitDeptTree.vue";
export default {
  name: "deptList",
  components: {
    unitDeptTree,
    empTitleEdit,
  },
  data() {
    return {
      HrEnumDic,
      HrItemDic,
      HrUserType,
      HrEmpStatus,
      emps: [],
      chioseEmp: [],
      status: [],
      title: "员工列表",
      columns: [
        {
          key: "EmpNo",
          title: "人员信息",
          align: "left",
          width: 250,
          fixed: "left",
          slotName: "empBase",
          sortable: "custom",
        },
        {
          sortby: "DeptId",
          key: "Dept",
          title: "单位部门",
          align: "center",
          minWidth: 120,
          sortable: "custom",
        },
        {
          sortby: "PostCode",
          key: "Post",
          title: "岗位",
          align: "center",
          sortable: "custom",
        },
        {
          key: "DeptTitle",
          title: "职务",
          align: "center",
          slotName: "empTitle",
          minWidth: 150,
        },
        {
          key: "Status",
          title: "用户状态",
          align: "center",
          slotName: "status",
          minWidth: 120,
          sortable: "custom",
        },
        {
          key: "Phone",
          title: "手机号",
          align: "center",
          width: 125,
          sortable: "custom",
        },
        {
          key: "Birthday",
          title: "生日",
          align: "center",
          slotName: "birthday",
          sortable: "custom",
          minWidth: 100,
        },
        {
          key: "UserType",
          title: "人员类型",
          align: "center",
          sortable: "custom",
          slotName: "UserType",
          minWidth: 110,
        },
        {
          key: "RegTime",
          title: "注册日期",
          align: "center",
          slotName: "regTime",
          sortable: "custom",
          minWidth: 110,
        },
        {
          key: "IsOpenAccount",
          title: "是否开通账号",
          align: "center",
          slotName: "isOpenAccount",
          sortable: "custom",
          fixed: "right",
          minWidth: 130,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          fixed: "right",
          width: "100px",
          slotName: "action",
        },
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: "EmpId",
        IsDesc: false,
        Total: 0,
      },
      visible: false,
      empId: null,
      queryParam: {
        CompanyId: null,
        QueryKey: null,
        IsEntry: true,
        DeptId: null,
        UnitId: null,
        Status: null,
        UserType: null,
        Post: null,
        Title: null,
      },
    };
  },
  mounted() {},
  methods: {
    moment,
    editEmpTitle(empId) {
      this.empId = empId;
      this.visible = true;
    },
    addEmp() {
      this.$router.push({ name: "addEmp" });
    },
    checkIsSelect(row) {
      return row.IsOpenAccount == false && row.Status == 1 && row.DeptId != 0;
    },
    async setStatus(row, status) {
      await empApi.setStatus(row.EmpId, status);
      row.Status = status;
      if (status != 1 && this.chioseEmp.length > 0) {
        this.chioseEmp = this.chioseEmp.filter((c) => c != row.EmpId);
      }
    },
    async openAccount() {
      if (this.chioseEmp.length == 0) {
        return;
      }
      await open(this.queryParam.CompanyId, this.chioseEmp);
      this.$message({
        type: "success",
        message: "开通成功!",
      });
      this.chioseEmp = [];
      this.loadEmp();
    },
    selected(e) {
      this.chioseEmp = e.keys;
    },
    async loadEmp() {
      const res = await empApi.queryEmp(this.queryParam, this.paging);
      if (res.List) {
        this.emps = res.List;
      } else {
        this.emps = [];
      }
      this.paging.Total = res.Count;
    },
    dropEmp(emp) {
      const title = "确认删除员工 " + emp.EmpName + "?";
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.submitDrop(emp.EmpId);
      });
    },
    async submitDrop(id) {
      await empApi.deleteEmp(id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      this.chioseEmp = [];
      this.loadEmp();
    },
    reset() {
      this.queryParam.QueryKey = null;
      this.queryParam.Title = null;
      this.queryParam.IsEntry = false;
      this.queryParam.Status = null;
      this.queryParam.UserType = null;
      this.queryParam.Post = null;
      this.chioseEmp = [];
      this.paging.Index = 1;
      this.loadEmp();
    },
    editEmp(id) {
      this.$router.push({
        name: "editEmp",
        params: {
          id: id,
        },
      });
    },
    chioseDept(e) {
      this.queryParam.CompanyId = e.companyId;
      if (e.value.length == 0) {
        this.queryParam.UnitId = null;
        this.queryParam.DeptId = null;
        this.title = e.comName + "公司-人员列表";
      } else {
        const dept = e.value[0];
        if (dept.IsUnit) {
          this.title = dept.DeptName + "单位-人员列表";
          this.queryParam.UnitId = dept.DeptId;
          this.queryParam.DeptId = null;
        } else {
          this.title = dept.DeptName + "部门-人员列表";
          this.queryParam.UnitId = null;
          this.queryParam.DeptId = [dept.DeptId];
        }
      }
      this.loadEmp();
    },
  },
};
</script>
<style  scoped>
.userInfo {
  width: 250px;
  height: 60px;
}
.userInfo .head {
  width: 50px;
  float: left;
  height: 60px;
  text-align: center;
}
.userInfo .content {
  width: 180px;
  float: left;
  text-align: left;
}
.userInfo .content .item {
  text-indent: 5px;
  margin: 0;
  font-size: 16px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}

.userInfo .content .empNo {
  padding-left: 10px;
  margin: 0;
  color: #82848a;
  font-size: 12px;
  text-overflow: ellipsis;
  width: 100%;
  overflow: hidden;
  display: inline-block;
  white-space: nowrap;
}
.userInfo .content i {
  font-weight: 600;
}
</style>