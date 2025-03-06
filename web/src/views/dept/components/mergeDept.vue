<template>
  <div style="min-height: 600px">
    <leftRightSplit :left-span="4">
      <el-card slot="left">
        <div slot="header" class="clearfix">
          <span>受影响的部门</span>
        </div>
        <el-tree
          ref="deptTree"
          :data="depts"
          :default-expand-all="true"
          :highlight-current="true"
          :props="props"
          style="height: 100%"
          :expand-on-click-node="false"
          :current-node-key="chioseKey"
          node-key="Id"
          @current-change="chioseDept"
        >
          <span slot-scope="{ node, data }" class="slot-t-node">
            <template>
              <template v-if="data.style">
                <i
                  v-if="data.style.icon.indexOf('el-') == 0"
                  :class="data.style.icon"
                  :style="{ color: data.style.color, marginRight: '5px' }"
                />
                <svg-icon
                  v-else
                  :icon-class="data.style.icon"
                  :style="{ color: data.style.color, marginRight: '5px' }"
                />
              </template>
              <span>{{ node.label }}</span>
            </template>
          </span>
        </el-tree>
      </el-card>
      <el-card slot="right">
        <div slot="header" class="clearfix" style="line-height: 36px;">
          <span>{{ title }}</span>
          <el-button
            style="float: right;"
            :disabled="loading"
            icon="el-icon-check"
            type="success"
            @click="submitMerge()"
          >合并部门</el-button>
        </div>
        <w-table
          :data="source.Emps"
          style="height: 100%"
          :columns="columns"
          :is-paging="false"
          row-key="EmpId"
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
                  {{ e.row.EmpName }}
                </div>
                <div class="empNo">{{ e.row.EmpNo }}</div>
              </div>
            </div>
          </template>
          <template slot="post" slot-scope="e">
            {{ e.row.Post }}
            <el-button
              icon="el-icon-edit-outline"
              type="text"
              @click="editEmpPost(e.row)"
            />
          </template>
          <template slot="unit" slot-scope="e">
            {{ source.UnitName + " " + source.DeptName }}
            <el-button
              icon="el-icon-edit-outline"
              type="text"
              @click="editEmpEntry(e.row)"
            />
          </template>
          <template slot="toUnit" slot-scope="e">
            {{ source.ToUnitName + " " + source.ToDeptName }}
          </template>
          <template slot="status" slot-scope="e">
            <el-switch
              :value="e.row.Status"
              :inactive-value="2"
              :active-value="1"
              @change="(val) => setStatus(e.row, val)"
            />
          </template>
          <template slot="empTitle" slot-scope="e">
            <span v-for="(t, index) in e.row.Title" :key="index" style="margin-right: 5px;">
              {{ t }}
            </span>
          </template>
          <template slot="toTitle" slot-scope="e">
            <el-row>
              <el-col :span="20">
                <p>
                  <span
                    v-for="(t, index) in e.row.Title"
                    :key="index"
                    style="margin-right: 5px"
                  >
                    {{ t }}
                  </span>
                </p>
                <p
                  v-if="e.row.ToTitleId != null && e.row.ToTitleId.length > 0"
                  style="color: #409eff"
                >
                  已有职位：<span
                    v-for="(t, index) in e.row.ToTitle"
                    :key="index"
                  >
                    {{ t }}
                  </span>
                </p>
              </el-col>
              <el-col :span="4" style="line-height: 50px">
                <el-button
                  icon="el-icon-edit-outline"
                  type="text"
                  @click="editEmpTitle(e.row.EmpId)"
                />
              </el-col>
            </el-row>
          </template>
        </w-table>
      </el-card>
    </leftRightSplit>
    <empTitleEdit
      :emp-id="empId"
      :visible="visible"
      @cancel="closeEmpTitle"
    />
    <editEmpPost :emp="emp" :visible="postVisible" @close="closeEmpPost" />
    <editEmpEntry :emp="emp" :visible="entryVisible" @close="closeEntry" />
  </div>
</template>

<script>
import * as empApi from '@/api/emp/emp'
import { GetDept, GetMergeEmp, Merge } from '@/api/base/depChange'
import empTitleEdit from '../../empTitle/empTitleList.vue'
import editEmpPost from '@/views/emp/components/editEmpPost.vue'
import editEmpEntry from '@/views/emp/components/editEmpEntry.vue'
export default {
  components: {
    empTitleEdit,
    editEmpPost,
    editEmpEntry
  },
  props: {
    deptId: {
      type: String,
      default: null
    },
    toDeptId: {
      type: String,
      default: null
    },
    ver: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      depts: [],
      emps: [],
      emp: null,
      empId: null,
      loading: false,
      postVisible: false,
      entryVisible: false,
      visible: false,
      chioseKey: null,
      title: null,
      props: {
        label: 'title',
        children: 'Children'
      },
      source: {
        Emps: []
      },
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
          key: 'Unit',
          title: '原单位部门',
          align: 'center',
          slotName: 'unit',
          minWidth: 180
        },
        {
          key: 'ToUnit',
          title: '合并后单位部门',
          align: 'center',
          slotName: 'toUnit',
          minWidth: 180
        },
        {
          key: 'Post',
          title: '岗位',
          align: 'center',
          slotName: 'post',
          minWidth: 200
        },
        {
          key: 'DeptTitle',
          title: '原职务',
          align: 'center',
          slotName: 'empTitle',
          minWidth: 150
        },
        {
          key: 'ToTitle',
          title: '合并后职务',
          align: 'center',
          slotName: 'toTitle',
          minWidth: 200
        },
        {
          key: 'Status',
          title: '用户状态',
          align: 'center',
          slotName: 'status',
          minWidth: 120,
          sortable: 'custom'
        }
      ]
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  watch: {
    ver: {
      handler(val) {
        if (this.deptId != null && this.toDeptId != null && val > 0) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    async submitMerge() {
      this.loading = true
      await Merge(this.deptId, this.toDeptId)
      this.$message({
        type: 'success',
        message: '合并成功!'
      })
      this.$emit('complete')
      this.loading = false
    },
    editEmpTitle(empId) {
      this.empId = empId
      this.visible = true
    },
    closeEntry(isRefresh) {
      this.entryVisible = false
      if (isRefresh) {
        this.reset()
      }
    },
    closeEmpTitle(isRefresh) {
      this.visible = false
      if (isRefresh) {
        this.loadEmp()
      }
    },
    editEmpEntry(emp) {
      this.emp = emp
      this.entryVisible = true
    },
    editEmpPost(emp) {
      this.emp = emp
      this.postVisible = true
    },
    closeEmpPost(isRefresh) {
      this.postVisible = false
      if (isRefresh) {
        this.loadEmp()
      }
    },
    chioseDept(data) {
      this.title = data.DeptName + '受影响的员工'
      this.loadEmp()
    },
    async reset() {
      const res = await GetDept(this.deptId, false)
      if (res != null) {
        this.format(res)
        this.depts = [res]
        this.chioseKey = res.Id
        this.title = res.DeptName + '受影响的员工'
        this.loadEmp()
      } else {
        this.depts = []
      }
    },
    async setStatus(row, status) {
      await empApi.setStatus(row.EmpId, status)
      row.Status = status
    },
    async loadEmp() {
      const merge = await GetMergeEmp({
        CompanyId: this.comId,
        DeptId: this.chioseKey,
        ToDeptId: this.toDeptId
      })
      this.source = merge
    },
    format(data) {
      data.title = data.DeptName + '(' + data.EmpNum + ')'
      if (data.IsUnit) {
        data.style = {
          icon: 'tree-table',
          color: '#409eff'
        }
      } else {
        data.style = {
          icon: 'peoples',
          color: '#000'
        }
      }
      if (data.Children && data.Children.length > 0) {
        data.Children.forEach((c) => {
          this.format(c)
        })
      }
    }
  }
}
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
