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
      <el-form-item label="目的地" prop="AreaId">
        <areaSelect v-model="config.AreaId" placeholder="选择目的地" />
      </el-form-item>
      <el-form-item label="总价" prop="TotalPrice">
        <el-input-number
          v-model="config.TotalPrice"
          :min="0"
          placeholder="总价"
        />
      </el-form-item>
      <el-form-item label="总重(kg)" prop="Weight">
        <el-input-number v-model="config.Weight" placeholder="总重" />
      </el-form-item>
      <el-form-item label="体积" prop="Volume">
        <el-input-number v-model="config.Volume" placeholder="体积" />
      </el-form-item>
      <el-form-item label="费用">
        <span style="color: red"> {{ this.price }}元</span>
      </el-form-item>
      <el-form-item label="费用说明">
        <p style=" white-space: break-spaces;margin: 0;text-align: left;">{{ this.show }}</p>
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >计算</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import * as logisticsApi from '@/shop/api/logisticsConfig'
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
    param: {
      type: Object,
      default: () => {
        return {
          AreaId: null,
          TotalPrice: null,
          Weight: null,
          Volume: null
        }
      }
    }
  },
  data() {
    return {
      title: '计算物流费用',
      loading: false,
      price: null,
      show: null,
      config: {
        AreaId: null,
        TotalPrice: null,
        Weight: null,
        Volume: null
      },
      rules: {
        AreaId: [
          {
            required: true,
            message: '地区不能为空！',
            trigger: 'blur'
          }
        ],
        TotalPrice: [
          {
            required: true,
            message: '总价不能为空!',
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
    async reset() {
      this.price = null
      this.show = null
      this.config = this.param
      this.loading = false
    },
    closeForm() {
      this.$emit('close', false)
    },
    save() {
      const that = this
      this.$refs['configEdit'].validate((valid) => {
        if (valid) {
          that.calculate()
        } else {
          return false
        }
      })
    },
    async calculate() {
      this.loading = true
      this.config.TemplateId = this.templateId
      const res = await logisticsApi.Calculate(this.config)
      this.price = res.Price
      this.show = res.Show
      this.loading = false
    }
  }
}
</script>
