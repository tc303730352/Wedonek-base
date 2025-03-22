<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="600px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <unitDeptTree
      ref="deptTree"
      :select-keys="chioseId"
      :is-multiple="true"
      :checkstrictly="true"
    />
    <p v-if="chioseId.length == 0" style="color: #f56c6c; text-align: center">
      至少选择一个部门!
    </p>
    <el-row slot="footer" :gutter="24" style="text-align: center">
      <el-button
        :disabled="chioseId.length == 0"
        type="primary"
        @click="save"
      >保存</el-button>
      <el-button @click="closeForm">取消</el-button>
    </el-row>
  </el-dialog>
</template>

<script>
import unitDeptTree from '@/components/unit/unitDeptTree.vue'
import * as deptPowerApi from '@/api/unit/deptPower'
export default {
  name: 'Layout',
  components: {
    unitDeptTree
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    empId: {
      type: String,
      default: null
    },
    title: {
      type: String,
      default: '设定用户部门数据权限'
    }
  },
  data() {
    return {
      chioseId: []
    }
  },
  computed: {
    comId() {
      return this.$store.getters.curComId
    }
  },
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.load()
        }
      },
      immediate: true
    }
  },
  methods: {
    async load() {
      const list = await deptPowerApi.get(this.empId, this.comId)
      if (list) {
        this.chioseId = list
      } else {
        this.chioseId = []
      }
    },
    async save() {
      if (this.chioseId.length === 0) {
        return
      }
      await deptPowerApi.set(this.empId, this.comId, this.chioseId)
      this.$message({
        type: 'success',
        message: '设置成功!'
      })
      this.$emit('close', true)
    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
