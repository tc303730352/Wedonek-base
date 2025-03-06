<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="80%"
    :modal="false"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <div class="choiceEmp" :style="{ minHeight: minHeight }">
      <el-row :gutter="24">
        <el-col :span="6">
          <el-card>
            <div slot="header" class="clearfix">
              <span>单位部门</span>
            </div>
            <deptTree :unit-id="unitId" @change="chioceDept" />
          </el-card>
        </el-col>
        <el-col :span="18">
          <el-card>
            <div slot="header" class="clearfix">
              <span>人员信息</span>
            </div>
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
            <el-row style="margin-bottom: 10px; margin-top: 10px">
              <el-tag
                v-for="(tag, index) in curEmp"
                :key="tag.EmpId"
                closable
                style="margin-right: 10px"
                type="default"
                @close="dropEmp(tag.EmpId, index)"
              >
                {{ tag.EmpName }}
              </el-tag>
            </el-row>
            <w-table
              :data="emps"
              :columns="columns"
              :is-paging="true"
              :is-select="true"
              :is-multiple="isMultiple"
              :select-keys="chioseKey"
              :paging="paging"
              row-key="EmpId"
              @selected="selected"
              @load="loadGrid"
            >
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
                    <p v-for="(t,index) in e.row.Title" :key="index">{{ t.Show }}</p>
                  </div>
                  <i class="el-icon-more" />
                </el-tooltip>
              </template>
            </w-table>
          </el-card>
        </el-col>
      </el-row>
    </div>
    <el-row slot="footer" style="text-align: center; margin-top: 20px">
      <el-button @click="reset">重 置</el-button>
      <el-button type="primary" @click="save">保 存</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import { HrItemDic, HrUserType } from '@/config/publicDic'
import deptTree from '../unit/unitDeptTree.vue'
import { query, getBasics } from '@/api/emp/emp'
export default {
  name: 'EmpChoice',
  components: {
    deptTree
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    title: {
      type: String,
      default: null
    },
    empId: {
      type: Array,
      default: null
    },
    status: {
      type: Array,
      default: () => [1]
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isAllowNull: {
      type: Boolean,
      default: true
    },
    unitId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrUserType,
      HrItemDic,
      subTitle: '选择人',
      minHeight: '500px',
      emps: [],
      columns: [
        {
          key: 'EmpNo',
          title: '人员编号',
          align: 'center'
        },
        {
          key: 'EmpName',
          title: '员工名',
          align: 'center'
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
      empCache: {},
      isload: false,
      curKey: null
    }
  },
  computed: {
    comName() {
      const comId = this.$store.getters.curComId
      return this.$store.getters.company[comId]
    },
    comId() {
      return this.$store.getters.curComId
    }
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
    dropEmp(key, index) {
      if (this.isload) {
        return
      }
      this.isload = true
      const ki = this.curKey.findIndex((c) => c === key)
      if (ki !== -1) {
        this.curKey.splice(ki, 1)
        this.curEmp.splice(index, 1)
      }
    },
    selected(e) {
      this.isload = true
      if (e.list) {
        e.list.forEach((c) => {
          this.empCache[c.EmpId] = c
        })
      }
      if (e.keys.length === 0) {
        this.curEmp = []
      } else {
        this.curEmp = e.keys.map((c) => this.empCache[c])
      }
      this.curKey = e.keys
      this.isload = false
    },
    async loadEmp() {
      const emps = await getBasics(this.comId, this.empId)
      emps.forEach((c) => {
        this.empCache[c.EmpId] = c
      })
      this.curEmp = emps
    },
    reset() {
      this.empCache = {}
      this.minHeight = window.innerHeight * 0.6 + 'px'
      this.queryParam.UnitId = this.unitId
      this.queryParam.DeptId = null
      if (this.empId != null && this.empId.length !== 0) {
        this.chioseKey = this.empId
        this.curKey = this.empId
        this.loadEmp()
      } else {
        this.curKey = []
        this.chioseKey = []
      }
      this.loadGrid()
    },
    save() {
      if (this.isAllowNull === false && this.curKey.length === 0) {
        this.$message({
          message: '请选择人员!',
          type: 'warning'
        })
        return
      }
      this.$emit('save', {
        isMultiple: this.isMultiple,
        comId: this.comId,
        user: this.curEmp,
        keys: this.curKey
      })
    },
    async loadGrid() {
      this.queryParam.CompanyId = this.comId
      this.queryParam.IsEntry = false
      this.queryParam.Status = this.status
      const data = await query(this.queryParam, this.paging)
      this.paging.Total = data.Count
      this.emps = data.List
    },
    chioceDept(e) {
      if (e.value.length === 0) {
        this.queryParam.UnitId = this.unitId
        this.queryParam.DeptId = null
      } else if (e.value[0].IsUnit) {
        this.queryParam.UnitId = e.value[0].DeptId
        this.queryParam.DeptId = null
      } else {
        this.queryParam.UnitId = e.value[0].UnitId
        this.queryParam.DeptId = [e.value[0].DeptId]
      }
      this.loadGrid()
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
</style>
