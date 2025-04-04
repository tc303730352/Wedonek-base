<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="1200px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form ref="controlEdit" :model="control" :rules="rules">
      <el-row :gutter="24">
        <el-col :span="10">
          <el-card header="控件基本信息">
            <el-form-item label="控件名" prop="Name">
              <el-input
                v-model="control.Name"
                maxlength="50"
                placeholder="控件名"
              />
            </el-form-item>
            <el-form-item label="控件类型" prop="ControlType">
              <enumItem
                v-model="control.ControlType"
                :dic-key="EnumDic.ControlType"
                sys-head="form"
                placeholder="控件类型"
              />
            </el-form-item>
            <el-form-item label="是否为基础控件" prop="IsBaseControl">
              <el-switch
                v-model="control.IsBaseControl"
              />
            </el-form-item>
            <el-form-item v-if="!control.IsBaseControl" label="编辑组件路径" prop="EditControl">
              <el-input
                v-model="control.EditControl"
                maxlength="255"
                placeholder="编辑控件路径"
              />
            </el-form-item>
            <el-form-item v-if="!control.IsBaseControl" label="显示组件路径" prop="ShowControl">
              <el-input
                v-model="control.EditControl"
                maxlength="255"
                placeholder="编辑控件路径"
              />
            </el-form-item>
            <el-form-item label="选择图标" prop="Icon">
              <iconChiose v-model="control.Icon" />
            </el-form-item>
            <el-form-item label="控件宽度" prop="MinWidth">
              <el-input-number
                v-model="control.MinWidth"
                :precision="0"
                placeholder="控件宽度"
              />
            </el-form-item>
            <el-form-item label="控件说明" prop="Description">
              <el-input
                v-model="control.Description"
                maxlength="100"
                placeholder="控件说明"
              />
            </el-form-item>
          </el-card>
        </el-col>
        <el-col :span="14">
          <el-card header="控件扩展属性">
          </el-card>
        </el-col>
      </el-row>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="reset">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import iconChiose from '@/components/tools/iconChiose.vue'
import * as controlApi from '@/customForm/api/control'
import {
  EnumDic
} from '@/customForm/config/formConfig'
export default {
  components: {
    iconChiose
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      EnumDic,
      control: {},
      title: '新增控件',
      rules: {
        Name: [
          {
            required: true,
            message: '控件名称不能为空！',
            trigger: 'blur'
          }
        ],
        ControlType: [
          {
            required: true,
            message: '控件类型不能为空！',
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
  },
  methods: {
    async reset() {
      if (this.id == null) {
        this.control = {
          IsBaseControl: true
        }
      } else {
        const res = await controlApi.Get(this.id)
        this.control = res
      }
    },
    save() {

    },
    closeForm() {
      this.$emit('close', false)
    }
  }
}
</script>
