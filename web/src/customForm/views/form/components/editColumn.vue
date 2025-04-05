<template>
  <el-dialog
    :title="title"
    :visible.sync="visible"
    width="800px"
    :close-on-click-modal="false"
    :before-close="handleClose"
  >
    <el-form ref="columnEdit" :model="column" :rules="rules" label-width="120px">
      <el-form-item label="标题" prop="ColTitle">
        <el-input v-model="column.ColTitle" :maxlength="50" placeholder="标题" @change="titleChange" />
      </el-form-item>
      <el-form-item label="列名" prop="ColName">
        <el-input v-model="column.ColName" :maxlength="50" placeholder="列名" />
      </el-form-item>
      <el-form-item label="控件" prop="ControlId">
        <controlSelect v-model="column.ControlId" :control.sync="control" placeholder="选择控件" />
      </el-form-item>
      <el-form-item label="提示信息" prop="Description">
        <el-input v-model="column.Description" :maxlength="50" placeholder="提示信息" />
      </el-form-item>
      <el-form-item label="必填" prop="IsNotNull">
        <el-switch v-model="column.IsNotNull" active-text="不能为空" inactive-text="可为空" />
      </el-form-item>
      <el-form-item v-if="control != null && (control.ColType == ControlType.input.value || control.ColType == ControlType.text.value)" label="最大文本长度" prop="MaxLen">
        <el-input-number
          v-model="column.MaxLen"
          :precision="0"
          placeholder="最大文本长度"
        />
      </el-form-item>
      <el-form-item label="默认值" prop="DefaultVal">
        <el-input v-model="column.DefaultVal" :maxlength="50" placeholder="默认值" />
      </el-form-item>
      <el-form-item v-if="tableType == 1" label="列最小宽" prop="Width">
        <el-input-number
          v-model="column.Width"
          :precision="0"
          placeholder="列最小宽"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="reset">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import { pinyin } from 'pinyin-pro'
import * as columnApi from '@/customForm/api/column'
import controlSelect from '@/customForm/components/control/controlSelect.vue'
import { ControlType } from '@/customForm/config/formConfig'
export default {
  components: {
    controlSelect
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    tableType: {
      type: Number,
      default: 0
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      ControlType,
      title: '编辑表单信息',
      column: {},
      rules: {
        ColTitle: [
          {
            required: true,
            message: '标题不能为空！',
            trigger: 'blur'
          }
        ],
        ColName: [
          {
            required: true,
            message: '列名不能为空！',
            trigger: 'blur'
          }
        ],
        ControlId: [
          {
            required: true,
            message: '请选择控件！',
            trigger: 'blur'
          }
        ]
      },
      isText: false,
      control: {}
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
  mounted() {},
  methods: {
    async reset() {
      const res = await columnApi.Get(this.id)
      this.column = res
    },
    titleChange() {
      this.column.ColName = pinyin(this.column.ColTitle, { pattern: 'first', toneType: 'none', separator: '' })
    },
    handleClose() {
      this.$emit('update:visible', false)
      this.$emit('close', false)
    },
    async set() {
      const isSet = await columnApi.Set(this.id, this.column)
      this.$emit('update:visible', false)
      this.$emit('close', isSet, this.column)
    },
    save() {
      this.$refs['columnEdit'].validate((valid) => {
        if (valid) {
          this.set()
        } else {
          return false
        }
      })
    }
  }
}
</script>
