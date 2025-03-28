<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <div v-if="checkPower('hr.power.operate.add')" style="width: 100%;text-align: right;margin-bottom: 10px;">
      <el-button type="success" @click="addOp">新增</el-button>
    </div>
    <w-table
      :data="powers"
      row-key="Id"
      max-height="500px"
      :columns="columns"
      :is-paging="false"
    >
      <template slot="IsEnable" slot-scope="e">
        <el-switch
          v-model="e.row.IsEnable"
          active-text="启用"
          inactive-text="停用"
          @change="setState(e.row)"
        />
      </template>
      <template slot="OperateName" slot-scope="e">
        <span v-if="e.row.op == 'view'">{{ e.row.OperateName }}</span>
        <el-input v-else v-model="e.row.OperateName" placeholder="权限名" class="el-input" />
      </template>
      <template slot="OperateVal" slot-scope="e">
        <span v-if="e.row.op == 'view' || e.row.op == 'edit'">{{ e.row.OperateVal }}</span>
        <el-input v-else v-model="e.row.OperateVal" placeholder="权限值" class="el-input" />
      </template>
      <template slot="Show" slot-scope="e">
        <span v-if="e.row.op == 'view'">{{ e.row.Show }}</span>
        <el-input v-else v-model="e.row.Show" placeholder="权限值" class="el-input" />
      </template>
      <template slot="action" slot-scope="e">
        <template v-if="e.row.op !== 'view'">
          <el-button
            size="mini"
            type="primary"
            title="保存"
            icon="el-icon-check"
            circle
            @click="saveOp(e.row)"
          />
          <el-button
            size="mini"
            type="danger"
            title="取消"
            icon="el-icon-close"
            circle
            @click="cancelOp(e.row)"
          />
        </template>
        <template v-else-if="e.row.IsEnable == false">
          <el-button
            v-if="checkPower('hr.power.operate.set')"
            size="mini"
            type="primary"
            title="编辑菜单"
            icon="el-icon-edit"
            circle
            @click="editOp(e.row)"
          />
          <el-button
            v-if="checkPower('hr.power.operate.delete')"
            size="mini"
            type="danger"
            title="删除菜单"
            icon="el-icon-delete"
            circle
            @click="dropOp(e.row)"
          />
        </template>
      </template>
    </w-table>
  </el-dialog>
</template>

<script>
import { Gets, SetIsEnable, Set, Add } from '@/api/role/opPower'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    powerName: {
      type: String,
      default: null
    },
    powerId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '新增菜单',
      powers: [],
      rolePower: ['hr.power.operate.add', 'hr.power.operate.set', 'hr.power.operate.delete'],
      columns: [
        {
          key: 'OperateName',
          title: '权限名',
          align: 'center',
          slotName: 'OperateName',
          minWidth: 150
        },
        {
          key: 'OperateVal',
          title: '权限值',
          align: 'center',
          slotName: 'OperateVal',
          minWidth: 150
        },
        {
          key: 'IsEnable',
          title: '是否启用',
          align: 'center',
          slotName: 'IsEnable',
          minWidth: 150
        },
        {
          key: 'Show',
          title: '说明',
          align: 'left',
          slotName: 'Show',
          minWidth: 150
        }, {
          key: 'Action',
          title: '操作',
          align: 'left',
          slotName: 'action',
          width: 100
        }
      ]
    }
  },
  computed: {},
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
  mounted() {
    this.initPower()
  },
  methods: {
    async initPower() {
      this.rolePower = await this.$checkPower(this.rolePower)
    },
    checkPower(power) {
      return this.rolePower.includes(power)
    },
    async reset() {
      const res = await Gets(this.powerId)
      res.forEach(a => {
        a.op = 'view'
      })
      this.powers = res
    },
    addOp() {
      if (this.powers.findIndex(c => c.Id == null) !== -1) {
        this.$message({
          type: 'warning',
          message: '还有未保存的权限!'
        })
        return
      }
      this.powers.push({
        Id: null,
        IsEnable: false,
        Show: null,
        OperateName: null,
        OperateVal: null,
        op: 'add'
      })
    },
    cancelOp() {
      this.reset()
    },
    saveOp(row) {
      if (row.OperateName == null || row.OperateName === '') {
        this.$message({
          type: 'warning',
          message: '权限名不能为空!'
        })
        return
      } else if (row.op === 'add' && (row.OperateVal == null || row.OperateVal === '')) {
        this.$message({
          type: 'warning',
          message: '权限值不能为空!'
        })
        return
      }
      if (row.op === 'edit') {
        this.set(row)
      } else {
        this.add(row)
      }
    },
    async set(row) {
      await Set(row.Id, {
        PowerId: this.powerId,
        IsEnable: row.IsEnable,
        Show: row.Show,
        OperateName: row.OperateName
      })
      this.reset()
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
    },
    async add(row) {
      await Add({
        PowerId: this.powerId,
        IsEnable: row.IsEnable,
        Show: row.Show,
        OperateName: row.OperateName,
        OperateVal: row.OperateVal
      })
      this.reset()
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
    },
    closeForm() {
      this.$emit('close')
    },
    editOp(row) {
      row.op = 'edit'
    },
    async setState(row) {
      if (row.op !== 'view') {
        return
      }
      await SetIsEnable(row.Id, row.IsEnable)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
    }
  }
}
</script>
