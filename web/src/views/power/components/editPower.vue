<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="1000px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="powerEdit" :model="power" :rules="rules" label-width="100px">
      <el-card>
        <span slot="header">基础信息</span>
        <el-form-item label="所属子系统">
          {{ subSystem }}
        </el-form-item>
        <el-form-item label="类型" prop="PowerType">
          <el-select v-model="power.PowerType" :disabled="id!=null" placeholder="类型">
            <el-option :value="1" label="目录">目录</el-option>
            <el-option :value="0" label="菜单">菜单</el-option>
          </el-select>
        </el-form-item>
        <el-form-item :label="power.PowerType == 0 ? '菜单名': '目录名'" prop="Name">
          <el-input
            v-model="power.Name"
            maxlength="100"
            :placeholder="power.PowerType == 0 ? '菜单名': '目录名'"
          />
        </el-form-item>
        <el-form-item label="父级目录" prop="ParentId">
          <el-cascader
            v-model="power.ParentId"
            :disabled="id!=null"
            :options="menus"
            placeholder="父级目录"
            :props="props"
            :clearable="true"
          />
        </el-form-item>
        <el-form-item label="图标" prop="Icon">
          <iconChiose v-model="power.Icon" />
        </el-form-item>
        <el-form-item v-if="id != null" label="排序位" prop="Sort">
          <el-input-number v-model="power.Sort" :min="1" placeholder="排序位" />
        </el-form-item>
        <el-form-item label="是否启用" prop="IsEnable">
          <el-switch v-model="power.IsEnable" />
        </el-form-item>
        <el-form-item label="备注" prop="Description">
          <el-input
            v-model="power.Description"
            type="textarea"
            maxlength="255"
            placeholder="备注"
          />
        </el-form-item>
      </el-card>
      <el-card v-if="power.PowerType == 0" style="margin-top: 10px;">
        <span slot="header">菜单配置</span>
        <el-form-item label="路由路径" prop="RoutePath">
          <el-input
            v-model="power.RoutePath"
            maxlength="100"
            placeholder="路由路径"
          />
        </el-form-item>
        <el-form-item label="路由名" prop="RouteName">
          <el-input
            v-model="power.RouteName"
            maxlength="50"
            placeholder="路由名"
          />
        </el-form-item>
        <el-form-item label="页面路径" prop="PagePath">
          <el-input
            v-model="power.PagePath"
            maxlength="255"
            placeholder="页面路径"
          />
        </el-form-item>
        <el-form-item label="页面参数">
          <el-row v-for="item in items" :key="item.id" style="margin-bottom: 5px;">
            <el-col :span="8">
              <el-input
                v-model="item.key"
                maxlength="50"
                placeholder="参数名"
              />
            </el-col>
            <el-col :span="1" style="text-align: center;">=</el-col>
            <el-col :span="13">
              <el-input
                v-model="item.value"
                maxlength="50"
                placeholder="参数值"
              />
            </el-col>
            <el-col :span="2" style="text-align: center;">
              <el-button type="danger" icon="el-icon-delete" @click="dropParam(item)" />
            </el-col>
          </el-row>
          <p>
            <el-button type="success" @click="addItem">新增参数</el-button>
          </p>
        </el-form-item>
      </el-card>
    </el-form>
    <div slot="footer">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as powerApi from '@/api/role/power'
import iconChiose from '@/components/tools/iconChiose.vue'
export default {
  components: {
    iconChiose
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    subSystemId: {
      type: String,
      default: null
    },
    subSystem: {
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
      title: '新增菜单',
      powerId: null,
      menus: [],
      itemId: 1,
      rules: {
        Name: [
          {
            required: true,
            message: '菜单/目录名不能为空！',
            trigger: 'blur'
          }
        ],
        PowerType: [
          {
            required: true,
            message: '类型不能为空！',
            trigger: 'blur'
          }
        ],
        RoutePath: [
          {
            required: true,
            message: '页面路由路径不能为空！',
            trigger: 'blur'
          }
        ],
        RouteName: [
          {
            required: true,
            message: '页面路由名不能为空！',
            trigger: 'blur'
          }
        ],
        PagePath: [
          {
            required: true,
            message: '页面路径不能为空！',
            trigger: 'blur'
          }
        ]
      },
      items: [],
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
        PowerType: 1,
        RoutePath: null,
        RouteName: null,
        PagePath: null,
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
    addItem() {
      this.items.push({ id: this.itemId, key: null, value: null })
      this.itemId = this.itemId + 1
    },
    dropParam(item) {
      this.items = this.items.filter(c => c.id !== item.id)
    },
    checkItems() {
      for (let i = 0; i < this.items.length; i++) {
        const item = this.items[i]
        if (this.items.findIndex(a => a.key === item.key, i + 1) !== -1) {
          return false
        }
      }
      return true
    },
    save() {
      if (!this.checkItems()) {
        this.$message({
          message: '页面参数重复!',
          type: 'error'
        })
        return
      }
      const that = this
      this.$refs['powerEdit'].validate((valid) => {
        if (valid) {
          if (that.id != null) {
            that.setPower()
          } else {
            that.addPower()
          }
        } else {
          return false
        }
      })
    },
    formatPower() {
      const data = Object.assign({}, this.power)
      if (data.PowerType === 1) {
        delete data.RoutePath
        delete data.RouteName
        delete data.PagePath
      } else {
        data.PageParam = {}
        this.items.forEach(c => {
          data.PageParam[c.key] = c.value
        })
      }
      return data
    },
    async setPower() {
      const data = this.formatPower()
      await powerApi.Set(this.id, data)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async addPower() {
      const data = this.formatPower()
      data.SubSystemId = this.subSystemId
      this.powerId = await powerApi.Add(data)
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
      this.itemId = 1
      if (this.id == null) {
        this.power = {
          ParentId: this.parentId,
          Name: null,
          Description: null,
          Icon: null,
          PowerType: 1,
          RoutePath: null,
          RouteName: null,
          PagePath: null,
          IsEnable: false
        }
        this.items = []
      } else {
        const res = await powerApi.Get(this.id)
        this.power = res
        this.source = res
        if (res.PageParam) {
          const items = []
          for (const i in res.PageParam) {
            items.push({ id: this.itemId, key: i, value: res.PageParam[i] })
            this.itemId = this.itemId + 1
          }
          this.items = items
        } else {
          this.items = []
        }
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      this.itemId = 1
      if (this.id == null) {
        this.power = {
          ParentId: null,
          Name: null,
          Description: null,
          Icon: null,
          PowerType: 1,
          RoutePath: null,
          RouteName: null,
          PagePath: null,
          IsEnable: false
        }
        if (this.source.PageParam) {
          const items = []
          for (const i in this.source.PageParam) {
            items.push({ id: this.itemId, key: i, value: this.source.PageParam[i] })
            this.itemId = this.itemId + 1
          }
          this.items = items
        } else {
          this.items = []
        }
      } else {
        this.power = this.source
      }
    }
  }
}
</script>
