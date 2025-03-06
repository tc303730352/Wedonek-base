<template>
  <div>
    <el-form
      ref="couponEdit"
      :model="coupon"
      label-width="150px"
      :rules="rules"
    >
      <el-card>
        <div slot="header">
          <span>券基本信息</span>
        </div>
        <el-form-item label="优惠卷名" prop="CouponTitle">
          <el-input
            v-model="coupon.CouponTitle"
            maxlength="50"
            placeholder="优惠卷名"
          />
        </el-form-item>
        <el-form-item label="投放时段" prop="PutInTime">
          <el-date-picker
            v-model="coupon.PutInTime"
            type="datetimerange"
            :picker-options="pickerOptions"
            range-separator="至"
            start-placeholder="开始时间"
            end-placeholder="结束时间"
            format="yyyy-MM-dd HH:mm"
            @change="putInChange"
          />
        </el-form-item>
        <el-form-item
          label="使用时间"
          :prop="useMode == 0 ? 'UseTime' : 'ExpireDay'"
        >
          <el-date-picker
            v-if="useMode == 0"
            v-model="coupon.UseTime"
            :picker-options="pickerOptions"
            type="datetimerange"
            range-separator="至"
            start-placeholder="开始时间"
            end-placeholder="结束时间"
            format="yyyy-MM-dd HH:mm"
          />
          <template v-else>
            <span>领券领取后</span>
            <el-input-number
              v-model="coupon.ExpireDay"
              style="width: 150px; margin-left: 5px; margin-right: 5px"
            />
            <span>天内使用，逾期失效。</span>
          </template>
          <el-button
            style="margin-left: 10px"
            icon="el-icon-sort"
            :title="
              this.useMode == 0 ? '点击切换为输入天数' : '点击切换为选择时段'
            "
            @click="() => (useMode = useMode == 0 ? 1 : 0)"
          />
        </el-form-item>
        <el-form-item label="优惠卷类型" prop="CouponType">
          <enumItem
            v-model="coupon.CouponType"
            :dic-key="EnumDic.couponType"
            placeholder="优惠卷类型"
            :multiple="false"
            sys-head="shop"
            mode="radio"
            @change="typeChange"
          />
        </el-form-item>
        <el-form-item label="优惠信息" prop="Discount">
          <div
            v-for="(item, index) in dsicount"
            :key="index"
            style="margin-bottom: 5px"
          >
            <span>满</span>
            <el-input-number
              v-model="item.ThresholdPrice"
              style="width: 150px; margin-left: 5px; margin-right: 5px"
              :min="0.1"
              :precision="1"
            />
            <template v-if="coupon.CouponType == 1">
              <span>元，减</span>
              <el-input-number
                v-model="item.CouponVal"
                :min="0.1"
                :precision="1"
                style="width: 150px; margin-left: 5px; margin-right: 5px"
              />
              <span>元。</span>
            </template>
            <template v-else>
              <span>元，打</span>
              <el-input-number
                v-model="item.Discount"
                style="width: 150px; margin-left: 5px; margin-right: 5px"
                :precision="1"
                :min="0.1"
                :max="9.9"
              />
              <span>折。</span>
            </template>
            <el-button
              v-if="index == 0"
              icon="el-icon-plus"
              size="mini"
              circle
              @click="addItem"
            />
            <el-button
              v-else
              icon="el-icon-minus"
              size="mini"
              circle
              @click="removeItem(index)"
            />
          </div>
        </el-form-item>
        <el-form-item
          v-if="coupon.CouponType == 0"
          label="最高优惠"
          prop="MaxCouponVal"
        >
          <el-input-number
            v-model="coupon.MaxCouponVal"
            :min="0.1"
            :precision="1"
          />
        </el-form-item>
        <el-form-item label="投放量" prop="PutInNum">
          <el-input-number
            v-model="coupon.PutInNum"
            :min="1"
            :max="1000000"
            :precision="0"
          />
        </el-form-item>
      </el-card>
      <el-card style="margin-top: 10px">
        <div slot="header">
          <span>其它设置</span>
        </div>
        <el-form-item label="定向人群" prop="UserRange">
          <enumItem
            v-model="coupon.UserRange"
            :dic-key="EnumDic.userRange"
            placeholder="定向人群"
            :multiple="false"
            sys-head="shop"
            mode="radio-button"
          />
        </el-form-item>
        <el-form-item
          v-if="coupon.UserRange == 0"
          label="定向会员等级"
          prop="UserGrade"
        >
          <userGradeItem
            v-model="coupon.UserGrade"
            placeholder="定向会员等级"
            :multiple="false"
            mode="radio"
          />
        </el-form-item>
        <el-form-item label="限领规则" prop="ReceiveType">
          <enumItem
            v-model="coupon.ReceiveType"
            :dic-key="EnumDic.receiveType"
            placeholder="限领规则"
            :multiple="false"
            sys-head="shop"
            mode="radio"
          />
        </el-form-item>
        <el-form-item label="单次发放张数" prop="ReceiveNum">
          <el-input-number
            v-model="coupon.ReceiveNum"
            :min="1"
            :max="10"
            :precision="0"
          />
        </el-form-item>
        <el-form-item label="是否公开" prop="IsPublic">
          <el-radio-group v-model="coupon.IsPublic">
            <el-radio :label="true">公开</el-radio>
            <el-radio :label="false">不公开</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-card>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button :loading="loading" type="success" @click="save">{{
          this.putInRange == 1 ? "保存并下一步" : "保存"
        }}</el-button>
        <el-button type="warning" @click="reset">重置</el-button>

        <el-button @click="prevStep">返回</el-button>
      </el-row>
    </el-form>
  </div>
</template>

<script>
import { EnumDic } from '@/shop/config/shopConfig'
import moment from 'moment'
import userGradeItem from '@/shop/components/userGrade/userGradeItem.vue'
import * as couponApi from '@/shop/api/coupon'
export default {
  components: {
    userGradeItem
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    putInRange: {
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
      EnumDic,
      loading: false,
      coupon: {},
      dsicount: [],
      useMode: 0,
      pickerOptions: {
        disabledDate: function(time) {
          return time < moment().startOf('date')
        }
      },
      rules: {
        CouponTitle: [
          {
            required: true,
            message: '优惠卷标题不能为空!',
            trigger: 'blur'
          }
        ],
        PutInTime: [
          {
            required: true,
            message: '投放时段不能为空!',
            trigger: 'blur'
          }
        ],
        UseTimeRange: [
          {
            required: true,
            message: '使用时段不能为空!',
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
    moment,
    typeChange() {
      this.dsicount = []
      this.addItem()
    },
    putInChange() {
      if (this.coupon.UseTime == null || this.coupon.UseTime.length == 0) {
        this.coupon.UseTime = [
          this.coupon.PutInTime[0],
          this.coupon.PutInTime[1]
        ]
      }
    },
    addItem() {
      if (this.dsicount.length == 3) {
        this.$message({
          type: 'error',
          message: '最多支持三挡!'
        })
        return
      }
      const add = {
        ThresholdPrice: 1,
        CouponVal: null,
        Discount: null
      }
      if (this.coupon.CouponType == 0) {
        add.Discount = 9.9
      } else {
        add.CouponVal = 1
      }
      this.dsicount.push(add)
    },
    prevStep() {
      this.$emit('prev')
    },
    removeItem(index) {
      this.dsicount.splice(index, 1)
    },
    async reset() {
      this.loading = false
      if (this.id == null) {
        this.coupon = {
          CouponTitle: null,
          CouponType: 0,
          PutInTime: [],
          MaxCouponVal: 1,
          PutInNum: 1,
          UseTime: [],
          ExpireDay: 30,
          UserRange: 0,
          GradeId: null,
          ReceiveType: 0,
          ReceiveNum: 1,
          IsPublic: true
        }
        this.dsicount = []
        this.addItem()
      } else {
        const res = await couponApi.Get(this.id)
        res.PutInTime = [res.PutInBegin, res.PutInEnd]
        delete res.PutInBegin
        delete res.PutInEnd
        if (res.UseBegin && res.UseEnd) {
          res.UseTime = [res.UseBegin, res.UseEnd]
          this.useMode = 0
          delete res.UseBegin
          delete res.UseEnd
        } else {
          this.useMode = 1
        }
        this.coupon = res
        if (res.CouponType == 0) {
          res.Discount.forEach((c) => {
            c.Discount = c.Discount / 10
          })
        }
        this.dsicount = res.Discount
        this.$emit('load', res)
      }
    },
    save() {
      const that = this
      this.$refs['couponEdit'].validate((valid) => {
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
    format() {
      const data = Object.assign({}, this.coupon)
      data.PutInRange = this.putInRange
      data.PutInBegin = data.PutInTime[0]
      data.PutInEnd = data.PutInTime[1]
      if (this.coupon.CouponType == 0) {
        data.Discount = this.dsicount.map((c) => {
          const i = Object.assign({}, c)
          i.Discount = i.Discount * 10
          return i
        })
      } else {
        data.Discount = this.dsicount
      }
      if (this.useMode == 0) {
        data.UseBegin = data.UseTime[0]
        data.UseEnd = data.UseTime[1]
      }
      delete data.UseTime
      delete data.PutInTime
      delete data.Id
      return data
    },
    async add() {
      this.loading = true
      const data = this.format()
      try {
        const id = await couponApi.Add(data)
        this.$message({
          type: 'success',
          message: '添加成功!'
        })
        data.Id = id
        this.$emit('next', id)
      } catch {
        this.loading = false
      }
    },
    async set() {
      this.loading = true
      const data = this.format()
      try {
        await couponApi.Set(this.id, data)
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        data.Id = this.id
        this.$emit('next', this.id)
      } catch {
        this.loading = false
      }
    }
  }
}
</script>
