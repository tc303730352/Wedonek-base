<template>
  <div>
    <leftRightSplit :left-span="4">
      <el-card slot="left">
        <div slot="header">
          <span>单位列表</span>
        </div>
        <unitDeptTree
          ref="deptTree"
          :status="status"
          :is-unit="true"
          @load="chioseDept"
          @change="chioseDept"
        />
      </el-card>
      <el-card slot="right">
        <div slot="header">
          <span>{{ title }}</span>
        </div>
        <div class="app-container">
          <el-form :inline="true" :model="queryParam">
            <el-form-item label="部门名">
              <el-input
                v-model="queryParam.QueryKey"
                placeholder="部门名"
                style="width: 200px"
                @change="loadDept"
              />
            </el-form-item>
            <el-form-item label="部门状态">
              <enumItem
                v-model="queryParam.Status"
                :dic-key="HrEnumDic.deptStatus"
                placeholder="部门状态"
                :multiple="true"
                class-name="filter-item"
                @change="loadDept"
              />
            </el-form-item>
            <el-form-item>
              <el-button type="warning" @click="deptChange">合并解散单位/部门</el-button>
              <el-button
                type="success"
                @click="addUnit(null)"
              >新增单位</el-button>
              <el-button
                v-if="this.queryParam.UnitId != null"
                type="primary"
                @click="addDept(null)"
              >新增部门</el-button>
            </el-form-item>
          </el-form>
          <w-table
            :data="depts"
            :columns="columns"
            :is-paging="false"
            row-key="Id"
          >
            <template slot="status" slot-scope="e">
              <el-switch
                :value="e.row.Status"
                :inactive-value="2"
                :active-value="1"
                @change="(value) => statusChange(e.row, value)"
              />
            </template>
            <template slot="Leader" slot-scope="e">
              <span>{{ e.row.LeaderName }}</span>
              <i
                class="el-icon-edit"
                style="margin-left: 5px; cursor: pointer"
                @click="editLeader(e.row)"
              />
            </template>
            <template slot="IsUnit" slot-scope="e">
              <el-tag v-if="e.row.IsUnit">是</el-tag>
              <el-tag v-else type="info">否</el-tag>
            </template>
            <template slot="action" slot-scope="e">
              <el-button
                icon="el-icon-plus"
                size="mini"
                circle
                @click="addDept(e.row.Id)"
              />
              <el-button
                v-if="e.row.Status == 0"
                size="mini"
                icon="el-icon-delete"
                type="danger"
                circle
                @click="deleteDept(e.row)"
              />
              <el-button
                icon="el-icon-edit"
                size="mini"
                type="primary"
                circle
                @click="editDept(e.row)"
              />
            </template>
          </w-table>
        </div>
      </el-card>
    </leftRightSplit>
    <editDept
      :dept-id="deptId"
      :is-unit="isUnit"
      :unit-id="queryParam.UnitId"
      :parent-id="parentId"
      :visible="visible"
      @close="closeDept"
    />
    <empChoice
      :visible="empVisible"
      :emp-id="empId"
      :title="empTitle"
      :unit-id="queryParam.UnitId"
      @save="setLeader"
      @close="closeChoice"
    />
  </div>
</template>

<script>
import * as deptApi from '@/api/unit/dept'
import { HrEnumDic, HrItemDic } from '@/config/publicDic'
import { GetItemName } from '@/api/base/dictItem'
import unitDeptTree from '@/components/unit/unitDeptTree.vue'
import editDept from './components/editDept.vue'
import empChoice from '@/components/emp/empChoice.vue'
export default {
  name: 'DeptList',
  components: {
    unitDeptTree,
    editDept,
    empChoice
  },
  data() {
    return {
      HrEnumDic,
      depts: [],
      empId: null,
      status: [],
      title: '部门列表',
      columns: [
        {
          key: 'DeptName',
          title: '全名',
          align: 'left'
        },
        {
          key: 'ShortName',
          title: '简称',
          align: 'left'
        },
        {
          key: 'LeaderName',
          title: '负责人',
          slotName: 'Leader'
        },
        {
          key: 'DeptShow',
          title: '说明',
          align: 'left'
        },
        {
          key: 'IsUnit',
          title: '是否为单位',
          align: 'center',
          slotName: 'IsUnit'
        },
        {
          key: 'DeptTag',
          title: '部门分类',
          align: 'left'
        },
        {
          key: 'Status',
          title: '状态',
          slotName: 'status'
        },
        {
          key: 'Action',
          title: '操作',
          align: 'left',
          slotName: 'action'
        }
      ],
      queryParam: {
        CompanyId: null,
        QueryKey: null,
        Status: null,
        IsUnit: null,
        UnitId: null
      },
      isUnit: false,
      empVisible: false,
      deptId: null,
      parentId: null,
      visible: false,
      dept: null,
      empTitle: null,
      deptTag: {}
    }
  },
  mounted() {
    this.initDic()
  },
  methods: {
    async initDic() {
      this.deptTag = await GetItemName(HrItemDic.deptTag)
    },
    deptChange() {
      this.$router.push({ name: 'deptChange' })
    },
    async setLeader(e) {
      this.empVisible = false
      const leader = e.user.length == 0 ? null : e.user[0]
      if (leader == null) {
        await deptApi.setLeader(this.dept.Id, null)
        this.dept.LeaderId = null
        this.dept.LeaderName = null
      } else {
        await deptApi.setLeader(this.dept.Id, leader.EmpId)
        this.dept.LeaderId = leader.EmpId
        this.dept.LeaderName = leader.EmpName
      }
    },
    editLeader(row) {
      this.dept = row
      this.empId = row.LeaderId == NaN || row.LeaderId == null ? null : [row.LeaderId]
      this.empTitle =
        '选择' + (row.IsUnit ? '单位' : '部门') + row.DeptName + '负责人'
      this.empVisible = true
    },
    closeDept(isRefresh) {
      this.visible = false
      if (!isRefresh) {
        return
      } else if (this.isUnit === false) {
        this.loadDept()
      } else {
        this.$refs.deptTree.load()
      }
    },
    openAction(row, command) {
      if (command === 'add') {
        this.addDept(row.Id)
      } else if (command === 'delete') {
        this.deleteDept(row)
      } else if (command === 'edit') {
        this.editDept(row)
      }
    },
    closeChoice() {
      this.empVisible = false
    },
    editDept(row) {
      this.parentId = null
      this.deptId = row.Id
      this.isUnit = row.IsUnit
      this.visible = true
    },
    deleteDept(row) {
      const title =
        '确认删除 ' + (row.IsUnit ? '单位' : '部门') + row.DeptName + '?'
      const that = this
      this.$confirm(title, '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        that.submitDrop(row.Id, row.IsUnit)
      })
    },
    async submitDrop(id, isUnit) {
      await deptApi.Delete(id)
      this.$message({
        type: 'success',
        message: '删除成功!'
      })
      this.loadDept()
      if (isUnit) {
        this.$refs.deptTree.load()
      }
    },
    addDept(pid) {
      this.parentId = pid
      this.deptId = null
      this.isUnit = false
      this.visible = true
    },
    addUnit(pid) {
      this.parentId = pid
      this.deptId = null
      this.isUnit = true
      this.visible = true
    },
    getTag(tags) {
      let tag = ''
      tags.forEach((c) => {
        tag = tag + ',' + this.deptTag[c]
      })
      if (tag.length === 0) {
        return null
      }
      return tag.substring(1)
    },
    async loadDept() {
      const list = await deptApi.gets(this.queryParam)
      list.forEach((c) => {
        this.formatRow(c)
      })
      this.depts = list
    },
    formatRow(row) {
      if (row.DeptTag != null) {
        row.DeptTag = this.getTag(row.DeptTag)
      }
      if (row.LeaderName == null) {
        row.LeaderName = null
      }
      if (row.Children && row.Children.length !== 0) {
        row.Children.forEach((c) => {
          this.formatRow(c)
        })
      }
    },
    chioseDept(e) {
      this.queryParam.CompanyId = e.companyId
      if (e.value.length === 0) {
        this.queryParam.UnitId = null
        this.queryParam.IsUnit = true
        this.title = e.comName + '公司-单位列表'
      } else {
        this.title = e.value[0].DeptName + '-部门列表'
        this.queryParam.IsUnit = false
        this.queryParam.UnitId = e.value[0].DeptId
      }
      this.loadDept()
    },
    async statusChange(row, value) {
      if (value === 1) {
        await deptApi.enable([row.Id])
      } else {
        await deptApi.stop(row.Id)
      }
      row.Status = value
    }
  }
}
</script>
