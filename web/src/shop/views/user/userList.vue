<template>
  <div>
    <el-card>
      <div slot="header">
        <span>用户管理</span>
      </div>
      <el-row>
        <el-form :inline="true" :model="query">
          <el-form-item label="关键字">
            <el-input
              v-model="query.QueryKey"
              placeholder="昵称"
              @change="search"
            />
          </el-form-item>
          <el-form-item label="性别">
            <el-select v-model="query.Sex" placeholder="选择性别" @change="search">
              <el-option :value="null">全部</el-option>
              <el-option :value="1" label="男">男</el-option>
              <el-option :value="2" label="女">女</el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="状态">
            <el-select v-model="query.UserState" placeholder="选择用户状态" :multiple="true" @change="search">
              <el-option :value="0" label="禁用">禁用</el-option>
              <el-option :value="1" label="启用">启用</el-option>
              <el-option :value="2" label="已删除">已删除</el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="注册时间">
            <el-date-picker
              v-model="query.TimeRange"
              type="daterange"
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              @change="search"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-button @click="reset">重置</el-button>
          </el-form-item>
        </el-form>
      </el-row>
      <w-table
        :data="dataList"
        :columns="columns"
        :isPaging="true"
        :paging="paging"
        @load="load"
        rowKey="Id"
      >
        <template slot="UserInfo" slot-scope="e">
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
                :src="e.row.UserHead"
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
                {{ e.row.NickName }}
              </div>
              <div class="empNo">{{ e.row.Phone }}</div>
            </div>
          </div>
        </template>
        <template slot="LastLoginDate" slot-scope="e">
          <span v-if="e.row.LastLoginDate">{{
            moment(e.row.LastLoginDate).format("YYYY-MM-DD")
          }}</span>
        </template>
        <template slot="DisableTime" slot-scope="e">
          <span v-if="e.row.DisableTime">{{
            moment(e.row.DisableTime).format("YYYY-MM-DD")
          }}</span>
        </template>
        <template slot="RegTime" slot-scope="e">
          {{ moment(e.row.RegTime).format("YYYY-MM-DD HH:mm") }}
        </template>
        <template slot="UserState" slot-scope="e">
          <span :style="{ color: UserStatus[e.row.UserState].color }">{{
            UserStatus[e.row.UserState].text
          }}</span>
        </template>
        <template slot="Birthday" slot-scope="e">
          <span v-if="e.row.Birthday">{{
            moment(e.row.Birthday).format("YYYY-MM-DD")
          }}</span>
        </template>
        <template slot="RegMode" slot-scope="e">
          {{ UserRegMode[e.row.RegMode].text }}
        </template>
        <template slot="action" slot-scope="e">
          <el-button
            v-if="e.row.UserState == 1"
            size="mini"
            type="danger"
            title="禁用"
            icon="el-icon-video-pause"
            @click="disable(e.row)"
            circle
          ></el-button>
          <el-button
            v-else-if="e.row.UserState == 0"
            size="mini"
            type="primary"
            title="启用"
            icon="el-icon-switch-button"
            @click="enable(e.row)"
            circle
          ></el-button>
        </template>
      </w-table>
    </el-card>
  </div>
</template>
    
<script>
import moment from "moment";
import * as userApi from "@/shop/api/user";
import { UserStatus, UserRegMode } from "@/shop/config/shopConfig";
export default {
  computed: {},
  data() {
    return {
      UserStatus,
      UserRegMode,
      dataList: [],
      id: null,
      columns: [
        {
          key: "UserInfo",
          title: "用户信息",
          align: "left",
          slotName: "UserInfo",
          minWidth: 300,
        },
        {
          key: "Email",
          title: "Email",
          align: "left",
          minWidth: 255,
        },
        {
          key: "Birthday",
          title: "生日",
          align: "center",
          slotName: "Birthday",
          width: 120,
        },
        {
          key: "UserState",
          title: "状态",
          align: "center",
          slotName: "UserState",
          sortable: true,
          sortby: 'UserState',
          width: 80,
        },
        {
          key: "RegMode",
          title: "注册方式",
          align: "center",
          sortable: true,
          slotName: "RegMode",
          width: 120,
        },
        {
          key: "LastLoginDate",
          title: "最后登陆日期",
          align: "center",
          sortable: true,
          slotName: "LastLoginDate",
          width: 130,
        },
        {
          key: "DisableTime",
          title: "禁用时间",
          align: "center",
          sortable: true,
          slotName: "DisableTime",
          width: 120,
        },
        {
          key: "RegTime",
          title: "注册时间",
          align: "center",
          sortable: true,
          slotName: "RegTime",
          width: 140,
        },
        {
          key: "Action",
          title: "操作",
          align: "left",
          minWidth: 120,
          slotName: "action",
        },
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: "Id",
        IsDesc: false,
        Total: 0,
      },
      query: {
        Sex: null,
        QueryKey: null,
        UserState: null,
        TimeRange: []
      },
      queryParam: {
        Sex: null,
        QueryKey: null,
        UserState: null,
        RegBegin: null,
        RegEnd: null,
        LoginBegin: null,
        LoginEnd: null,
      },
    };
  },
  components: {},
  mounted() {
    this.load();
  },
  methods: {
    moment,
    reset() {
      this.queryParam.QueryKey = null;
      this.queryParam.Sex = null;
      this.queryParam.UserState = null;
      this.queryParam.RegBegin = null
      this.queryParam.RegEnd = null
      this.paging.Index = 1;
      this.load()
    },
    search() {
      this.queryParam.QueryKey = this.query.QueryKey;
      this.queryParam.Sex = this.query.Sex;
      this.queryParam.UserState = this.query.UserState;
      if (this.query.TimeRange.length !=0) {
        this.queryParam.RegBegin = this.query.TimeRange[0]
        this.queryParam.RegEnd = this.query.TimeRange[1]
      } else {
        this.queryParam.RegBegin = null
        this.queryParam.RegEnd = null
      }
      this.load()
    },
    async load() {
      const res = await userApi.Query(this.queryParam, this.paging);
      if (res.List) {
        this.dataList = res.List;
      } else {
        this.dataList = [];
      }
      this.paging.Total = res.Count;
    },
    disable(row) {
      const title = "确认禁用用户: " + row.NickName + "?";
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.submitDisable(row);
      });
    },
    async submitDisable(row) {
      await userApi.Disable(row.Id);
      this.$message({
        type: "success",
        message: "禁用成功!",
      });
      row.UserState = 0;
      row.DisableTime = moment();
    },
    enable(row) {
      const title = "确认启用用户: " + row.NickName + "?";
      const that = this;
      this.$confirm(title, "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        that.submitEnable(row);
      });
    },
    async submitEnable(row) {
      await userApi.Enable(row.Id);
      this.$message({
        type: "success",
        message: "启用成功!",
      });
      row.UserState = 1;
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