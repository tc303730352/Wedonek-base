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
      <el-form ref="roleEdit" :model="role">
        <el-form-item label="角色名">
          <el-input
            :value="role.RoleName"
            :readonly="true"
            placeholder="角色名"
          />
        </el-form-item>
        <el-form-item label="是否为管理员">
          <el-switch
            :readonly="true"
            :checked="role.IsAdmin"
            :inactive-value="false"
            class="el-input"
            :active-value="true"
          />
        </el-form-item>
        <el-form-item label="备注">
          <el-input
            :readonly="true"
            :value="role.Remark"
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
              ref="powerTree"
              :data="trees"
              :default-expand-all="true"
              :highlight-current="true"
              :expand-on-click-node="false"
              style="width: 100%"
              :show-checkbox="false"
              :default-checked-keys="chioseKeys"
              :check-strictly="false"
              node-key="key"
              @node-click="chiosePower"
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
                :data="powers"
                :select-keys="selectKeys"
                row-key="Id"
                :disabled="true"
                :is-select="true"
                :is-multiple="true"
                :columns="columns"
                :is-paging="false"
              />
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-card>
  </el-dialog>
</template>

<script>
import { GetTrees } from '@/api/role/power'
import * as roleApi from '@/api/role/role'
import { GetEnables } from '@/api/role/opPower'
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
      powers: [],
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
      selectKeys: [],
      powerId: null,
      chioseId: null
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
    checkIsChiose() {
      if (this.chioseId == null ? false : this.chioseKeys.includes(this.chioseId)) {
        return true
      }
      return false
    },
    async loadTrees() {
      const list = await GetTrees({
        IsEnable: true
      })
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
        t.children = this.getPowers(c.Powers)
        return t
      })
    },
    chiosePower(data) {
      if (data.type === 0 && this.chioseId !== data.key) {
        this.chioseId = data.key
        this.pTitle = data.label + '操作权限列表'
        this.loadPower(data.key)
      }
    },
    getPowers(list) {
      return list.map((c) => {
        const t = {
          key: c.Id,
          type: c.PowerType,
          label: c.Name
        }
        if (c.PowerType === 1) {
          t.style = {
            icon: 'el-icon-folder',
            color: '#409eff'
          }
        } else if (c.PowerType === 0) {
          t.style = {
            icon: 'el-icon-document',
            color: '#000'
          }
        }
        if (c.Children && c.Children.length !== 0) {
          t.children = this.getPowers(c.Children)
        }
        return t
      })
    },
    async loadPower(powerId) {
      const res = await GetEnables(powerId, this.roleId)
      if (res == null) {
        this.powers = []
        this.selectKeys = []
        return
      }
      this.powers = res.Powers
      this.selectKeys = res.Selected
    },
    async reset() {
      const res = await roleApi.get(this.roleId)
      this.role = res
      this.source = res
      this.powerId = res.PowerId
      this.chioseKeys = res.PowerId
      if (!res.IsAdmin) {
        this.$refs.powerTree.setCheckedKeys(this.chioseKeys, false)
      }
    },
    closeForm() {
      this.$emit('close')
    }
  }
}
</script>
