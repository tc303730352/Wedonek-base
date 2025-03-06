<template>
  <div>
    <leftRightSplit :leftSpan="6">
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
                @change="loadUser"
              />
            </el-form-item>
            <el-form-item>
              <roleSelect
                v-model="queryParam.RoleId"
                placeholder="拥有的角色"
                :multiple="true"
                @change="loadUser"
              ></roleSelect>
            </el-form-item>
            <el-form-item>
              <el-button @click="reset">重置</el-button>
            </el-form-item>
          </el-form>
        </el-row>
        <w-table
          :data="users"
          :columns="columns"
          :isPaging="true"
          rowKey="EmpId"
          @load="loadUser"
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
          <template slot="IsAdmin" slot-scope="e">
            <el-tag type="success" v-if="e.row.IsAdmin">是管理员</el-tag>
          </template>
          <template slot="Role" slot-scope="e">
            <el-row>
              <el-link
                style="margin-right: 10px"
                icon="el-icon-edit"
                @click="editRole(e.row)"
              ></el-link>
              <span
                style="color: #f56c6c"
                v-if="e.row.Role == null || e.row.Role.length == 0"
                >未设定角色</span
              >
              <template v-else>
                <span v-for="i in e.row.Role" :key="i">{{ i }}</span>
              </template>
            </el-row>
          </template>
          <template slot="Dept" slot-scope="e">
            <span style="color: #409eff" v-if="e.row.IsAdmin"
              >拥有所有部门权限</span
            >
            <el-row :gutter="24" v-else>
              <el-link
                style="margin-right: 10px; margin-left: 10px"
                icon="el-icon-edit"
                @click="editDept(e.row)"
              ></el-link>
              <span
                style="color: #f56c6c"
                v-if="e.row.Dept == null || e.row.Dept.length == 0"
                >未设定部门权限</span
              >
              <template v-else>
                <span style="margin-right: 5px;" v-for="i in e.row.Dept" :key="i">{{ i }}</span>
              </template>
            </el-row>
          </template>
          <template slot="action" slot-scope="e">
            <el-button
              size="mini"
              type="warning"
              title="重置登陆密码"
              icon="el-icon-refresh"
              @click="resetPwd(e.row)"
              circle
            ></el-button>
            <el-button
              size="mini"
              type="danger"
              title="删除用户"
              icon="el-icon-delete"
              @click="dropUser(e.row)"
              circle
            ></el-button>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <empRoleSet
      :visible="visible"
      :empId="empId"
      :title="roleTitle"
      @close="closeRoleSet"
    ></empRoleSet>
    <empDeptPrower
      :visible="visibleDept"
      :empId="empId"
      :title="roleTitle"
      @close="closeDeptSet"
    ></empDeptPrower>
  </div>
</template>
    
    <script>
import moment from "moment";
import * as userApi from "@/api/emp/loginUser";
import unitDeptTree from "@/components/unit/unitDeptTree.vue";
import roleSelect from "../../components/role/roleSelect.vue";
import empRoleSet from "./components/empRoleSet.vue";
import empDeptPrower from "./components/empDeptPrower.vue";
export default {
  name: "deptList",
  components: {
    unitDeptTree,
    roleSelect,
    empRoleSet,
    empDeptPrower,
  },
  data() {
    return {
      users: [],
      title: "用户列表",
      visible: false,
      empId: null,
      roleTitle: null,
      visibleDept: false,
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
          key: "Phone",
          title: "手机号",
          align: "center",
          minWidth: 120,
        },
        {
          key: "Email",
          title: "Email",
          align: "center",
          minWidth: 120,
        },
        {
          key: "LoginName",
          title: "登陆名",
          align: "center",
          minWidth: 120,
        },
        {
          key: "IsAdmin",
          title: "是否是管理员",
          align: "center",
          slotName: "IsAdmin",
          minWidth: 100,
        },
        {
          key: "Role",
          title: "拥有的角色",
          align: "left",
          slotName: "Role",
          minWidth: 200,
        },
        {
          key: "Dept",
          title: "部门权限",
          align: "left",
          slotName: "Dept",
          minWidth: 200,
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
      queryParam: {
        CompanyId: null,
        QueryKey: null,
        DeptId: null,
        UnitId: null,
        RoleId: null,
      },
    };
  },
  mounted() {
    const query = this.$route.query;
    if (query != null) {
      this.initQuery(query);
    }
  },
  methods: {
    moment,
    initQuery(query) {
      if (query.roleId) {
        this.queryParam.RoleId = [query.roleId];
      }
    },
    closeRoleSet(isRefresh) {
      this.visible = false;
      if (isRefresh) {
        this.loadUser();
      }
    },
    resetPwd(emp) {
      const title = "确认重置人员 " + emp.EmpName + "的密码?";
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.resetConfirm(emp.EmpId);
      });
    },
    async resetConfirm(empId) {
      await userApi.resetPwd(empId);
      this.$message({
        type: "success",
        message: "重置成功!",
      });
    },
    closeDeptSet(isRefresh) {
      this.visibleDept = false;
      if (isRefresh) {
        this.loadUser();
      }
    },
    editRole(row) {
      this.empId = row.EmpId;
      this.roleTitle = "编辑人员" + row.EmpName + "拥有的角色";
      this.visible = true;
    },
    editDept(row) {
      this.empId = row.EmpId;
      this.roleTitle = "编辑人员" + row.EmpName + "拥有的部门数据权限";
      this.visibleDept = true;
    },
    formatStr(rows, def) {
      if (rows == null || rows.length == 0) {
        return <span>{def}</span>;
      }
      const str = [];
      rows.forEach((c) => {
        str.push(<span>{c}</span>);
      });
      return str;
    },
    async loadUser() {
      const res = await userApi.query(this.queryParam, this.paging);
      if (res.List) {
        this.users = res.List;
      } else {
        this.users = [];
      }
      this.paging.Total = res.Count;
    },
    dropUser(emp) {
      const title = "确认删除人员 " + emp.EmpName + "的用户信息?";
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
      await userApi.deleteUser(id);
      this.$message({
        type: "success",
        message: "删除成功!",
      });
      this.loadUser();
    },
    reset() {
      this.queryParam.QueryKey = null;
      this.queryParam.RoleId = null;
      this.paging.Index = 1;
      this.loadUser();
    },
    chioseDept(e) {
      this.queryParam.CompanyId = e.companyId;
      if (e.value.length == 0) {
        this.queryParam.UnitId = null;
        this.queryParam.DeptId = null;
        this.title = e.comName + "公司-用户列表";
      } else {
        const dept = e.value[0];
        if (dept.IsUnit) {
          this.title = dept.DeptName + "单位-用户列表";
          this.queryParam.UnitId = dept.DeptId;
          this.queryParam.DeptId = null;
        } else {
          this.title = dept.DeptName + "部门-用户列表";
          this.queryParam.UnitId = null;
          this.queryParam.DeptId = [dept.DeptId];
        }
      }
      this.loadUser();
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