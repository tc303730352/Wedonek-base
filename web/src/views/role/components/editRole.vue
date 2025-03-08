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
      <el-form ref="roleEdit" :model="role" :rules="rules">
        <el-form-item label="角色名" prop="RoleName">
          <el-input
            v-model="role.RoleName"
            maxlength="100"
            placeholder="角色名"
          />
        </el-form-item>
        <el-form-item label="是否为管理员" prop="IsAdmin">
          <el-switch
            v-model="role.IsAdmin"
            :inactive-value="false"
            class="el-input"
            :active-value="true"
          />
        </el-form-item>
        <el-form-item label="备注" prop="Remark">
          <el-input
            v-model="role.Remark"
            type="textarea"
            maxlength="255"
            placeholder="备注"
          />
        </el-form-item>
      </el-form>
    </el-card>
    <el-card v-if="!role.IsAdmin" style="margin-top: 10px">
      <span slot="header">权限目录</span>
      <el-row style="width: 100%;">
        <el-col :span="8">
          <div style="width: 100%; height: 350px; overflow-y: auto;">
            <el-tree
              ref="prowerTree"
              :data="trees"
              :default-expand-all="true"
              :highlight-current="true"
              :expand-on-click-node="false"
              style="width: 100%"
              :show-checkbox="true"
              :default-checked-keys="chioseKeys"
              :check-strictly="false"
              node-key="key"
              @node-click="chiosePrower"
              @check-change="checkChange"
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
          </div>
        </el-col>
        <el-col :span="16">
          <el-card>
            <span slot="header">{{ pTitle }}</span>
            <div style="width: 100%; height: 350px; overflow-y: auto;">
              <w-table
                :data="prowers"
                row-key="Id"
                :is-select="true"
                :is-multiple="true"
                :columns="columns"
                :is-paging="false"
                @selected="saveOperPrower"
              />
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-card>
    <div slot="footer">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { getTreeBySystem } from '@/api/role/prower'
import * as roleApi from '@/api/role/role'
import { GetEnables } from '@/api/role/opPrower'
export default {
  name: 'EditRole',
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    roleId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '新增角色',
      prowers: [],
      pTitle: '操作权限列表',
      columns: [{
        key: 'OperateName',
        title: '权限名',
        align: 'center',
        minWidth: 150
      }, {
        key: 'Show',
        title: '说明',
        align: 'left',
        minWidth: 150
      }],
      role: {},
      trees: [],
      source: null,
      chioseKeys: [],
      prowerId: null,
      chioseId: null,
      rules: {
        RoleName: [
          {
            required: true,
            message: '角色名不能为空！',
            trigger: 'blur'
          }
        ]
      }
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
          this.reset()
        }
      },
      immediate: true
    }
  },
  mounted() {
    this.loadTrees()
  },
  methods: {
    saveOperPrower() {

    },
    async loadTrees() {
      const list = await getTreeBySystem()
      this.trees = list.map((c) => {
        const t = {
          key: c.SubSysId,
          type: 'isSubSystem',
          label: c.SubSysName,
          style: {
            icon: 'el-icon-s-home',
            color: '#f56c6c'
          }
        }
        t.children = this.getProwers(c.Prowers)
        return t
      })
    },
    chiosePrower(data) {
      if (data.type === 0 && this.chioseId !== data.key) {
        this.chioseId = data.key
        this.pTitle = data.label + '操作权限列表'
        this.loadPrower(data.key)
      }
    },
    checkChange(data, checked) {
      if (data.type === 'isSubSystem') {
        return
      }
      const index = this.prowerId.findIndex((c) => c === data.key)
      if (checked) {
        if (index !== -1) {
          return
        }
        this.prowerId.push(data.key)
      } else if (index !== -1) {
        this.prowerId.splice(index, 1)
      }
    },
    getProwers(list) {
      return list.map((c) => {
        const t = {
          key: c.Id,
          type: c.ProwerType,
          label: c.Name
        }
        if (c.ProwerType === 1) {
          t.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
        } else if (c.ProwerType === 0) {
          t.style = {
            icon: 'el-icon-document',
            color: '#000'
          }
        } else if (c.ProwerType === 2) {
          t.style = {
            icon: 'component',
            color: '#000'
          }
        }
        if (c.Children && c.Children.length !== 0) {
          t.children = this.getProwers(c.Children)
        }
        return t
      })
    },
    save() {
      const that = this
      this.$refs['roleEdit'].validate((valid) => {
        if (valid) {
          if (that.roleId) {
            that.setRole()
          } else {
            that.addRole()
          }
        } else {
          return false
        }
      })
    },
    async setRole() {
      this.role.ProwerId = this.prowerId
      await roleApi.set(this.roleId, this.role)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async addRole() {
      this.role.ProwerId = this.prowerId
      this.roleId = await roleApi.add(this.role)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async loadPrower(prowerId) {
      const list = await GetEnables(prowerId, this.roleId)
      this.prowers = list
    },
    async reset() {
      if (this.roleId == null) {
        this.role = {
          IsAdmin: false
        }
        this.prowerId = []
        this.chioseKeys = []
        this.source = {}
        this.$refs.prowerTree.setCheckedKeys([], false)
      } else {
        const res = await roleApi.get(this.roleId)
        this.role = res
        this.source = res
        this.prowerId = res.ProwerId
        this.chioseKeys = res.ProwerId
        if (!res.IsAdmin) {
          this.$refs.prowerTree.setCheckedKeys(this.chioseKeys, false)
        }
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.roleId == null) {
        this.role = {
          IsAdmin: false
        }
        this.prowerId = []
        this.chioseKeys = []
        this.$refs.prowerTree.setCheckedKeys([], false)
      } else {
        this.role = this.source
        this.prowerId = this.role.ProwerId
        this.chioseKeys = this.role.ProwerId
        if (!this.role.IsAdmin) {
          this.$refs.prowerTree.setCheckedKeys(this.chioseKeys, false)
        }
      }
    }
  }
}
</script>
