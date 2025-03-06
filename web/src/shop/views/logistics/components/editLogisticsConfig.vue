<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="700px"
    :before-close="closeForm"
    :close-on-click-modal="false"
  >
    <el-form
      ref="configEdit"
      :model="config"
      label-width="150px"
      :rules="rules"
    >
      <el-form-item label="地区" prop="AreaId">
        <areaSelect
          v-model="config.AreaId"
          placeholder="选择地区"
          @change="areaChange"
        />
      </el-form-item>
      <el-form-item label="计费方式" prop="ValuationMode">
        <el-select
          v-model="config.ValuationMode"
          placeholder="计费方式"
          class="el-input"
          @change="modeChange"
        >
          <el-option
            v-for="(i, index) in ValuationMode"
            :key="index"
            :value="i.value"
            :label="i.text"
          >{{ i.text }}</el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="物流费用" prop="LogisticsPrice">
        <el-input-number
          v-model="config.LogisticsPrice"
          :min="0"
          placeholder="物流费用"
        />
      </el-form-item>
      <template
        v-if="config.ValuationMode != 3 && config.ValuationMode != null"
      >
        <el-form-item :label="'免邮费' + unit" prop="FreeValue">
          <el-input-number
            v-model="config.FreeValue"
            :min="0"
            placeholder="免邮费值"
          />
        </el-form-item>
        <el-form-item :label="'起' + unit" prop="FirstVal">
          <el-input-number
            v-model="config.FirstVal"
            :min="0"
            placeholder="起始值"
          />
        </el-form-item>
        <el-form-item :label="'续' + unit" prop="ContinueVal">
          <el-input-number
            v-model="config.ContinueVal"
            :min="0"
            placeholder="续(重/体积)"
          />
        </el-form-item>
        <el-form-item :label="'续' + unit + '费用'" prop="ContinuePrice">
          <el-input-number
            v-model="config.ContinuePrice"
            :min="0"
            placeholder="续费用"
          />
        </el-form-item>
        <el-form-item
          v-if="config.ValuationMode == 2 || config.ValuationMode == 1"
          label="立方厘米比例值"
          prop="CcValue"
        >
          <el-input-number
            v-model="config.CcValue"
            :min="0"
            placeholder="立方厘米比例值"
          />
        </el-form-item>
      </template>
      <el-form-item label="免邮金额" prop="FreePrice">
        <el-input-number
          v-model="config.FreePrice"
          :min="0"
          placeholder="免邮金额"
        />
      </el-form-item>
      <el-form-item label="备注" prop="Remark">
        <el-input v-model="config.Remark" maxlength="255" placeholder="备注" />
      </el-form-item>
      <el-form-item label="是否启用" prop="IsEnable">
        <el-switch v-model="config.IsEnable" />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import * as logisticsApi from '@/shop/api/logisticsConfig'
import { ValuationMode } from '@/shop/config/shopConfig'
import areaSelect from '@/components/area/areaSelect.vue'
export default {
  components: {
    areaSelect
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    templateId: {
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
      ValuationMode,
      title: '新增物流明细',
      unit: '重',
      config: {
        AreaId: null,
        ValuationMode: null,
        Remark: null,
        IsEnable: false
      },
      loading: false,
      areaType: null,
      areaName: null,
      rules: {
        AreaId: [
          {
            required: true,
            message: '地区不能为空！',
            trigger: 'blur'
          }
        ],
        ValuationMode: [
          {
            required: true,
            message: '请选择计费方式!',
            trigger: 'blur'
          }
        ],
        ContinueVal: [
          {
            required: true,
            message: '续重/体积不能为空!',
            trigger: 'blur'
          }
        ],
        ContinuePrice: [
          {
            required: true,
            message: '续费用不能为空!',
            trigger: 'blur'
          }
        ],
        FreeValue: [
          {
            required: true,
            message: '免邮重/体积不能为空!',
            trigger: 'blur'
          }
        ],
        FirstVal: [
          {
            required: true,
            message: '起始值不能为空!',
            trigger: 'blur'
          }
        ],
        CcValue: [
          {
            required: true,
            message: '立方厘米比例值不能为空!',
            trigger: 'blur'
          }
        ],
        LogisticsPrice: [
          {
            required: true,
            message: '物流费用不能为空!',
            trigger: 'blur'
          }
        ],
        FreePrice: [
          {
            required: true,
            message: '免邮金额不能为空!',
            trigger: 'blur'
          }
        ]
      }
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
  methods: {
    areaChange(area) {
      if (area == null) {
        this.areaType = null
        this.areaName = null
      } else {
        this.areaType = area.AreaType
        this.areaName = area.Name
      }
    },
    modeChange(e) {
      if (this.config.ValuationMode == 0) {
        this.unit = '重(kg)'
      } else if (this.config.ValuationMode == 1) {
        this.unit = '体积重量'
      } else if (this.config.ValuationMode == 2) {
        this.unit = '体积'
      } else {
        this.unit = ''
      }
    },
    async reset() {
      if (this.id == null) {
        this.title = '新增物流明细'
        this.areaType = null
        this.areaName = null
        this.config = {
          AreaId: null,
          Remark: null,
          IsEnable: false,
          ValuationMode: null,
          FreePrice: 0,
          CcValue: 0,
          FreeValue: 0,
          ContinuePrice: 0,
          ContinueVal: 0,
          LogisticsPrice: 0,
          FirstVal: 0
        }
      } else {
        const res = await logisticsApi.Get(this.id)
        this.config = res
        this.title = '编辑物流明细'
      }
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['configEdit'].validate((valid) => {
        if (valid) {
          if (that.id == null) {
            that.add()
          } else {
            that.set()
          }
        } else {
          return false
        }
      })
    },
    async add() {
      this.loading = true
      this.config.AreaType = this.areaType
      this.config.AreaName = this.areaName
      this.config.TemplateId = this.templateId
      await logisticsApi.Add(this.config)
      this.$message({
        type: 'success',
        message: '添加成功!'
      })
      this.$emit('close', true)
    },
    async set() {
      this.loading = true
      this.config.AreaType = this.areaType
      this.config.AreaName = this.areaName
      const isSet = await logisticsApi.Set(this.id, this.config)
      this.$message({
        type: 'success',
        message: '保存成功!'
      })
      this.$emit('close', isSet)
    }
  }
}
</script>
