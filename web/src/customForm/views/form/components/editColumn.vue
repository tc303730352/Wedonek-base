<template>
  <el-dialog
    :title="title"
    :visible.sync="visible"
    width="1200px"
    :close-on-click-modal="false"
    :before-close="handleClose"
  >
    <el-row>
      <el-col :span="10">
        <el-card header="基础配置">
          <el-form ref="columnEdit" :model="column" :rules="rules" label-width="120px">
            <el-form-item label="标题" prop="ColTitle">
              <el-input v-model="column.ColTitle" :maxlength="50" placeholder="标题" @change="titleChange" />
            </el-form-item>
            <el-form-item label="列名" prop="ColName">
              <el-input v-model="column.ColName" :maxlength="50" placeholder="列名" />
            </el-form-item>
            <el-form-item label="控件" prop="ControlId">
              <controlSelect v-model="column.ControlId" :control.sync="control" placeholder="选择控件" @load="intiControl" @change="intiControl" />
            </el-form-item>
            <el-form-item label="提示信息" prop="Description">
              <el-input v-model="column.Description" :maxlength="50" placeholder="提示信息" />
            </el-form-item>
            <el-form-item label="必填" prop="IsNotNull">
              <el-switch v-model="column.IsNotNull" active-text="不能为空" inactive-text="可为空" />
            </el-form-item>
            <el-form-item v-if="colType == ControlType.input.value || colType == ControlType.text.value" label="最大文本长度" prop="MaxLen">
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
        </el-card>
      </el-col>
      <el-col :span="14">
        <el-card header="控件配置">
          <el-form v-if="control.Config" ref="controlEdit" :model="controlSet" label-width="120px">
            <el-form-item v-for="item in control.Config" :key="item.Key" :label="item.Label" :prop="item.key">
              <el-input v-if="item.Type == 0" v-model="controlSet[item.key]" :maxlength="50" :placeholder="item.Placeholder" />
              <treeDicItem
                v-else-if="item.Type == 1"
                v-model="controlSet[item.key]"
                :dic-id="item.DicId"
                :placeholder="item.Placeholder"
              />
              <dictItem
                v-else-if="item.Type == 2"
                v-model="controlSet[item.key]"
                :dic-id="item.DicId"
                :placeholder="item.Placeholder"
              />
              <el-switch v-else-if="item.Type == 3" v-model="controlSet[item.key]" />
              <el-select v-else-if="item.Type == 4" v-model="controlSet[item.key]">
                <el-option v-for="op in item.Items" :key="op.Key" :value="op.Key">{{ op.Value }}</el-option>
              </el-select>
              <span v-else-if="item.Type == 6">{{ controlSet[item.key] }}</span>
            </el-form-item>
          </el-form>
        </el-card>
      </el-col>
    </el-row>
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
      controlSet: {},
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
      control: {},
      colType: null
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
    intiControl() {
      this.colType = this.control.ColType
      const data = {}
      if (this.control.Config != null && this.control.Config.length !== 0) {
        const sour = this.control.ColType === this.column.ColType ? this.column.ControlSet : null
        this.control.Config.forEach(c => {
          if (c.Type === 6) {
            data[c.Key] = c.DefValue
          } else if (sour != null) {
            data[c.Key] = sour[c.Key]
          } else {
            data[c.Key] = c.DefValue
          }
        })
      }
      this.controlSet = data
    },
    async reset() {
      const res = await columnApi.Get(this.id)
      this.column = res
      this.intiControl()
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
