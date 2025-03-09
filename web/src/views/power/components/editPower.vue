<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="1000px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-card>
      <span slot="header">基本信息</span>
      <el-form ref="powerEdit" :model="power" :rules="rules">
        <el-form-item label="菜单名" prop="Name">
          <el-input v-model="power.Name" maxlength="100" placeholder="菜单名" />
        </el-form-item>
        <el-form-item label="父级目录" prop="ParentId">
          <el-cascader
            v-model="power.ParentId"
            :options="menus"
            placeholder="父级目录"
            :props="props"
            :clearable="true"
            class="el-input"
          />
        </el-form-item>
        <el-form-item label="菜单类型" prop="PowerType">
          <el-select
            v-model="power.PowerType"
            placeholder="菜单类型"
          >
            <el-option :value="null" label="全部">全部</el-option>
            <el-option :value="1" label="目录">目录</el-option>
            <el-option :value="0" label="菜单">菜单</el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="备注" prop="Description">
          <el-input
            v-model="power.Description"
            type="textarea"
            maxlength="255"
            placeholder="备注"
          />
        </el-form-item>
      </el-form>
    </el-card>
    <div slot="footer">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as powerApi from '@/api/role/power'
import {
  HrEnumDic
} from '@/config/publicDic'
export default {
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    subSystemId: {
      type: String,
      default: null
    },
    parentId: {
      type: String,
      default: null
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrEnumDic,
      title: '新增菜单',
      powerId: null,
      menus: [],
      rules: {
        Name: [
          {
            required: true,
            message: '菜单名不能为空！',
            trigger: 'blur'
          }
        ]
      },
      props: {
        multiple: false,
        emitPath: false,
        checkStrictly: true,
        value: 'Id',
        label: 'Name',
        children: 'Children'
      },
      power: {
        ParentId: null,
        Name: null,
        Description: null,
        Icon: null,
        PowerType: null,
        RoutePath: null,
        RouteName: null,
        PagePath: null,
        PageParam: {},
        IsEnable: false
      }
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.loadPower()
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {},
  methods: {
    save() {
      const that = this
      this.$refs['powerEdit'].validate((valid) => {
        if (valid) {
          if (that.roleId) {
            that.setPower()
          } else {
            that.addPower()
          }
        } else {
          return false
        }
      })
    },
    async setPower() {
      await powerApi.Set(this.id, this.power)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async addPower() {
      this.power.SubSystemId = this.subSystemId
      this.powerId = await powerApi.Add(this.power)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async loadPower() {
      const list = await powerApi.GetTree({
        SubSystemId: this.subSystemId,
        PowerType: 1
      })
      this.menus = list
    },
    async reset() {
      if (this.id == null) {
        this.power = {
          ParentId: null,
          Name: null,
          Description: null,
          Icon: null,
          PowerType: null,
          RoutePath: null,
          RouteName: null,
          PagePath: null,
          PageParam: {},
          IsEnable: false
        }
      } else {
        const res = await powerApi.Get(this.id)
        this.power = res
        this.source = res
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.id == null) {
        this.power = {
          ParentId: null,
          Name: null,
          Description: null,
          Icon: null,
          PowerType: null,
          RoutePath: null,
          RouteName: null,
          PagePath: null,
          PageParam: {},
          IsEnable: false
        }
      } else {
        this.power = this.source
      }
    }
  }
}
</script>
