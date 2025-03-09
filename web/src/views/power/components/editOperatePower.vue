<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
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
      <template slot="action" slot-scope="e">
        <el-button
          size="mini"
          type="primary"
          title="编辑菜单"
          icon="el-icon-edit"
          circle
          @click="editOp(e.row)"
        />
        <el-button
          size="mini"
          type="danger"
          title="删除菜单"
          icon="el-icon-delete"
          circle
          @click="dropOp(e.row)"
        />
      </template>
    </w-table>
  </el-dialog>
</template>

<script>
import { Gets } from '@/api/role/opPower'
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
      columns: [
        {
          key: 'OperateName',
          title: '权限名',
          align: 'center',
          minWidth: 150
        },
        {
          key: 'OperateVal',
          title: '权限值',
          align: 'center',
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
  mounted() {},
  methods: {
    async reset() {
      const res = await Gets(this.powerId)
      this.powers = res
    },
    closeForm() {
      this.$emit('close')
    }
  }
}
</script>
