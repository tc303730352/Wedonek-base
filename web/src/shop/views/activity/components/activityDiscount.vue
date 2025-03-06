<template>
  <div>
    <el-card>
      <div slot="header">
        <span>优惠门槛及内容</span>
      </div>
      <el-form
        ref="discountEdit"
        :model="discount"
        label-width="150px"
        :rules="rules"
      >
        <el-form-item label="促销方式" prop="Threshold">
          <enumItem
            v-model="discount.Threshold"
            :dic-key="EnumDic.threshold"
            placeholder="促销方式"
            :multiple="false"
            sys-head="shop"
            mode="radio-button"
            @change="thresholdChange"
          />
        </el-form-item>
        <el-card>
          <div slot="header">
            <span>优惠内容</span>
            <el-button
              type="success"
              style="float: right"
              @click="addItem"
            >新增优惠内容</el-button>
          </div>
          <div v-for="(item, index) in discount.Items" :key="index">
            <el-divider v-if="index != 0" />
            <el-form-item
              v-if="discount.Threshold != 2 && discount.Threshold != 3"
              label="门槛金额"
              :prop="'Items.' + index + '.ThresholdPrice'"
            >
              <el-input-number
                v-model="item.ThresholdPrice"
                :min="0"
                :precision="1"
              />
              <span style="padding-left: 10px">元</span>
            </el-form-item>
            <el-form-item
              v-else-if="discount.Threshold == 2"
              label="门槛值"
              :prop="'Items.' + index + '.ThresholdNum'"
            >
              <el-input-number
                v-model="item.ThresholdNum"
                :min="1"
                :precision="0"
              />
              <span style="padding-left: 10px">件</span>
            </el-form-item>
            <template v-if="discount.Threshold == 0">
              <el-form-item label="促销内容" :prop="'Items.' + index + '.Mode'">
                <enumItem
                  v-model="item.Mode"
                  :dic-key="EnumDic.disContent"
                  placeholder="促销内容"
                  :multiple="false"
                  sys-head="shop"
                  :filters="[6, 7, 8]"
                  mode="radio-button"
                />
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 1"
                label="减"
                :prop="'Items.' + index + '.ReducePrice'"
              >
                <el-input-number
                  v-model="item.ReducePrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 4"
                label="加价"
                :prop="'Items.' + index + '.AddPrice'"
              >
                <el-input-number
                  v-model="item.AddPrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 2 || item.Mode == 4"
                label="选择赠品"
                :rop="'Items.' + index + '.GiveSkuId'"
              >
                <el-input
                  v-model="item.GiveName"
                  placeholder="选择赠品"
                  style="width: 600px"
                  :readonly="true"
                >
                  <el-button
                    slot="append"
                    icon="el-icon-circle-plus-outline"
                    @click="chioseGive(item)"
                  />
                </el-input>
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 5 || item.Mode == 2 || item.Mode == 4"
                :label="item.Mode == 5 ? '任选' : '赠送'"
                :prop="'Items.' + index + '.GiveNum'"
              >
                <el-input-number
                  v-model="item.GiveNum"
                  :min="1"
                  :precision="0"
                />
                <span style="padding-left: 10px">件</span>
              </el-form-item>
            </template>
            <template v-else-if="discount.Threshold == 1">
              <el-form-item
                label="减"
                :prop="'Items.' + index + '.ReducePrice'"
              >
                <el-input-number
                  v-model="item.ReducePrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
              <el-form-item
                label="封顶价"
                :prop="'Items.' + index + '.ReduceTotalPrice'"
              >
                <el-input-number
                  v-model="item.ReduceTotalPrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
            </template>
            <template v-else-if="discount.Threshold == 2">
              <el-form-item label="促销内容" :prop="'Items.' + index + '.Mode'">
                <enumItem
                  v-model="item.Mode"
                  :dic-key="EnumDic.disContent"
                  placeholder="促销内容"
                  :multiple="false"
                  sys-head="shop"
                  :filters="[2, 3, 4, 5]"
                  mode="radio-button"
                />
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 1"
                label="减"
                :prop="'Items.' + index + '.ReducePrice'"
              >
                <el-input-number
                  v-model="item.ReducePrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
              <el-form-item
                v-else-if="item.Mode == 6"
                label="打"
                :prop="'Items.' + index + '.DiscountRatio'"
              >
                <el-input-number
                  v-model="item.DiscountRatio"
                  :min="0.1"
                  :max="9.9"
                  :precision="1"
                />
                <span style="padding-left: 10px">折</span>
              </el-form-item>
              <el-form-item
                v-else-if="item.Mode == 7"
                label="减"
                :prop="'Items.' + index + '.ReduceNum'"
              >
                <el-input-number
                  v-model="item.ReduceNum"
                  :min="1"
                  :precision="0"
                />
                <span style="padding-left: 10px">件</span>
              </el-form-item>
              <el-form-item
                v-else-if="item.Mode == 8"
                label="最低价商品享"
                :prop="'Items.' + index + '.DiscountPrice'"
              >
                <el-input-number
                  v-model="item.DiscountPrice"
                  :min="0.1"
                  :precision="1"
                />
                <span style="padding-left: 10px">元</span>
              </el-form-item>
            </template>
            <template v-else-if="discount.Threshold == 3">
              <el-form-item label="促销内容" :prop="'Items.' + index + '.Mode'">
                <enumItem
                  v-model="item.Mode"
                  :dic-key="EnumDic.disContent"
                  placeholder="促销内容"
                  :multiple="false"
                  sys-head="shop"
                  :filters="[1, 4, 5, 6, 7, 8]"
                  mode="radio-button"
                />
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 2"
                label="选择赠品"
                :rop="'Items.' + index + '.GiveSkuId'"
              >
                <el-input
                  v-model="item.GiveName"
                  placeholder="选择赠品"
                  style="width: 600px"
                  :readonly="true"
                >
                  <el-button
                    slot="append"
                    icon="el-icon-circle-plus-outline"
                    @click="chioseGive(item)"
                  />
                </el-input>
              </el-form-item>
              <el-form-item
                v-if="item.Mode == 2"
                label="赠送"
                :prop="'Items.' + index + '.GiveNum'"
              >
                <el-input-number
                  v-model="item.GiveNum"
                  :min="1"
                  :precision="0"
                />
                <span style="padding-left: 10px">件</span>
              </el-form-item>
            </template>
            <el-form-item label="包邮" :prop="'Items.' + index + '.IsFreeMail'">
              <el-switch
                v-model="item.IsFreeMail"
                active-text="包邮"
                inactive-text="不包邮"
              />
            </el-form-item>
            <el-form-item v-if="index != 0" label="">
              <el-button
                type="danger"
                @click="removeDrop(index)"
              >删除</el-button>
            </el-form-item>
          </div>
        </el-card>
      </el-form>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="success"
          @click="save"
        >保存并下一步</el-button>
        <el-button :loading="loading" @click="prevStep">上一步</el-button>
        <el-button type="warning" @click="reset">重置</el-button>
      </el-row>
    </el-card>
    <chioseSku
      :visible="visible"
      :sku-id="skuId"
      @save="saveSku"
      @close="visible = false"
    />
  </div>
</template>

<script>
import { EnumDic } from '@/shop/config/shopConfig'
import * as discountApi from '@/shop/api/activityDiscount'
import chioseSku from '@/shop/components/goods/chioseSku.vue'
export default {
  components: {
    chioseSku
  },
  props: {
    isLoad: {
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
      loading: false,
      visible: false,
      item: {},
      skuId: null,
      discount: {
        Threshold: 0,
        Items: []
      },
      rules: {
        ActivityTitle: [
          {
            required: true,
            message: '促销名不能为空!',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  computed: {},
  watch: {
    isLoad: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    thresholdChange() {
      this.discount.Items = []
      this.$nextTick(this.addItem)
    },
    removeDrop(index) {
      this.discount.Items.splice(index, 1)
    },
    save() {
      const that = this
      this.$refs['discountEdit'].validate((valid) => {
        if (valid) {
          that.set()
        } else {
          return false
        }
      })
    },
    format() {
      if (this.discount.Threshold == 2) {
        const data = Object.assign({}, this.discount)
        if (data.Items && data.Items.length > 0) {
          data.Items = data.Items.map((c) => {
            if (c.Mode == 6) {
              const i = Object.assign({}, c)
              i.DiscountRatio = i.DiscountRatio * 10
              return i
            }
            return c
          })
        }
        return data
      }
      return this.discount
    },
    async set() {
      this.loading = true
      const data = this.format()
      try {
        await discountApi.Set(this.id, data)
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        this.$emit('next')
      } catch {
        this.loading = false
      }
    },
    async reset() {
      const res = await discountApi.Get(this.id)
      if (res != null) {
        if (res.Threshold == 2) {
          if (res.Items && res.Items.length > 0) {
            res.Items.forEach((c) => {
              if (c.Mode == 6) {
                c.DiscountRatio = c.DiscountRatio / 10
              }
            })
          }
        }
        this.discount = res
      } else {
        this.discount = {
          Threshold: 0,
          Items: []
        }
        this.addItem()
      }
    },
    addItem() {
      const add = {
        ThresholdPrice: null,
        Mode: 0,
        ThresholdNum: null,
        ReducePrice: null,
        ReduceTotalPrice: null,
        AddPrice: null,
        GiveNum: null,
        CouponId: null,
        GiveSkuId: null,
        GiveName: null,
        DiscountRatio: null,
        DiscountPrice: null,
        ReduceTotalPrice: null
      }
      if (this.discount.Threshold == 1) {
        add.Mode = 1
      }
      this.discount.Items.push(add)
    },
    chioseGive(item) {
      this.item = item
      if (item.GiveSkuId) {
        this.skuId = [item.GiveSkuId]
      }
      this.visible = true
    },
    saveSku(e) {
      this.visible = false
      this.item.GiveSkuId = e.keys[0]
      const sku = e.skus[0]
      this.item.GiveName = sku.SkuName
    },
    prevStep() {
      this.$emit('prev')
    }
  }
}
</script>
<style scoped>
.el-radio-group {
  text-align: left;
}
.el-radio {
  width: 100%;
  line-height: 30px;
}
</style>
