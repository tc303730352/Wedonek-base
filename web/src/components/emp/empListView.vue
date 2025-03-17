<template>
  <div class="choiceEmp">
    <el-form :inline="true" :model="queryParam">
      <el-form-item label="关键字">
        <el-input
          v-model="queryParam.QueryKey"
          placeholder="人员名\编号\手机号"
          style="width: 200px"
          @change="loadGrid"
        />
      </el-form-item>
      <el-form-item label="岗位">
        <treeDicItem
          v-model="queryParam.Post"
          :dic-id="HrItemDic.post"
          style="width: 300px"
          :is-multiple="true"
          placeholder="人员岗位"
          @change="loadGrid"
        />
      </el-form-item>
      <el-form-item label="职务">
        <dictItem
          v-model="queryParam.Title"
          :multiple="true"
          :dic-id="HrItemDic.title"
          placeholder="职务"
          @change="loadGrid"
        />
      </el-form-item>
    </el-form>
    <w-table
      :data="emps"
      :columns="columns"
      :is-paging="true"
      :paging="paging"
      row-key="EmpId"
      @load="loadGrid"
    >
      <template slot="empBase" slot-scope="e">
        <div class="userInfo">
          <div class="head">
            <img
              v-if="e.row.UserHead == null || e.row.UserHead == ''"
              src="/image/defhead.png"
              width="50"
              height="50"
            >
            <el-avatar
              v-else
              shape="square"
              :size="50"
              :src="e.row.UserHead | imageUri"
            />
          </div>
          <div class="content">
            <div class="item">
              <i
                v-if="e.row.Sex == 1"
                style="color: #409eff"
                class="el-icon-male"
              />
              <i
                v-else-if="e.row.Sex == 2"
                style="color: #f56c6c"
                class="el-icon-female"
              />
              <el-link @click="showEmp(e.row.EmpId)">{{ e.row.EmpName }}</el-link>
            </div>
            <div class="empNo">{{ e.row.EmpNo }}</div>
          </div>
        </div>
      </template>
      <template slot="UserType" slot-scope="e">
        <span>{{ HrUserType[e.row.UserType].text }}</span>
      </template>
      <template slot="empTitle" slot-scope="e">
        <span v-if="e.row.DeptTitle">{{ e.row.DeptTitle }}</span>
        <el-tooltip
          v-if="e.row.Title && e.row.Title.length > 0"
          effect="dark"
          placement="bottom"
        >
          <div slot="content">
            <p v-for="(t, index) in e.row.Title" :key="index">{{ t.Show }}</p>
          </div>
          <i class="el-icon-more" />
        </el-tooltip>
      </template>
    </w-table>
    <empModel
      :id="empId"
      :visible="empVisible"
      @close="empVisible=false"
    />
  </div>
</template>

<script>
import { HrItemDic, HrUserType } from '@/config/publicDic'
import empModel from './empModel.vue'
import { query } from '@/api/emp/emp'
import store from '@/store'
export default {
  components: {
    empModel
  },
  props: {
    status: {
      type: Array,
      default: () => [1]
    },
    companyId: {
      type: String,
      default: () => {
        return store.getters.curComId
      }
    },
    unitId: {
      type: String,
      default: null
    },
    deptId: {
      type: Array,
      default: null
    },
    isEntry: {
      type: Boolean,
      default: true
    },
    isLoad: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      HrUserType,
      HrItemDic,
      empVisible: false,
      empId: null,
      emps: [],
      columns: [
        {
          key: 'EmpNo',
          title: '人员信息',
          align: 'left',
          width: 250,
          fixed: 'left',
          slotName: 'empBase',
          sortable: 'custom'
        },
        {
          key: 'Dept',
          title: '部门',
          align: 'center'
        },
        {
          key: 'Phone',
          title: '手机号',
          align: 'center'
        },
        {
          key: 'Post',
          title: '岗位',
          align: 'center'
        },
        {
          key: 'DeptTitle',
          title: '职务',
          align: 'center',
          slotName: 'empTitle'
        },
        {
          key: 'UserType',
          title: '人员类型',
          align: 'center',
          slotName: 'UserType'
        }
      ],
      paging: {
        Size: 20,
        Index: 1,
        SortName: 'EmpId',
        IsDesc: false,
        Total: 0
      },
      queryParam: {
        Title: null,
        Post: null,
        QueryKey: null
      },
      chioseKey: null,
      curEmp: [],
      isload: false,
      curKey: null
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    },
    isEntry: {
      handler(val) {
        if (this.isLoad) {
          this.reset()
        }
      },
      immediate: false
    }
  },
  methods: {
    showEmp(empId) {
      this.empId = empId
      this.empVisible = true
    },
    reset() {
      this.queryParam.CompanyId = this.companyId
      this.queryParam.UnitId = this.unitId
      this.queryParam.DeptId = this.deptId
      this.queryParam.IsEntry = this.isEntry
      this.loadGrid()
    },
    async loadGrid() {
      this.queryParam.Status = this.status
      const data = await query(this.queryParam, this.paging)
      this.paging.Total = data.Count
      this.emps = data.List
    },
    closeForm() {
      this.$emit('close')
    }
  }
}
</script>
  <style type="less" scoped>
.choiceEmp {
  width: 100%;
}
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
