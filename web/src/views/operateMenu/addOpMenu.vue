<template>
  <el-dialog
    title="添加操作权限配置"
    :visible="visible"
    width="800px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="menuEdit" :model="menu" :rules="rules">
      <el-form-item label="权限名称" prop="Title">
        <el-input
          v-model="menu.Title"
          maxlength="100"
          placeholder="权限名称"
        />
      </el-form-item>
      <el-form-item label="访问路径" prop="RoutePath">
        <el-input
          v-model="menu.RoutePath"
          maxlength="500"
          placeholder="访问路径"
        />
      </el-form-item>
      <el-form-item label="业务类型" prop="BusType">
        <dictItem
          v-model="menu.BusType"
          dic-id="6"
          placeholder="业务类型"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { Add } from '@/api/opMenu'
export default {
  components: {
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      menu: {},
      rules: {
        Title: [
          {
            required: true,
            message: '权限名称不能为空！',
            trigger: 'blur'
          }
        ],
        RoutePath: [
          {
            required: true,
            message: '访问路径不能为空！',
            trigger: 'blur'
          }
        ],
        BusType: [
          {
            required: true,
            message: '业务类型不能为空！',
            trigger: 'blur'
          }
        ]
      }
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
    save() {
      const that = this
      this.$refs['menuEdit'].validate((valid) => {
        if (valid) {
          that.add()
        } else {
          return false
        }
      })
    },
    async add() {
      await Add(this.menu)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    reset() {
      this.menu = {
        Title: null,
        RoutePath: null,
        BusType: null
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      this.menu = {
        Title: null,
        RoutePath: null,
        BusType: null
      }
    }
  }
}
</script>
