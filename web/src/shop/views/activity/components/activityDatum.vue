<template>
  <div>
    <el-card>
      <div slot="header">
        <h4 style="color: #1890ff">{{ title }}</h4>
      </div>
      <el-form
        ref="activityEdit"
        :model="activity"
        label-width="150px"
        :rules="rules"
      >
        <el-card>
          <div slot="header">
            <span>基本规则</span>
          </div>
          <el-form-item label="促销名" prop="ActivityTitle">
            <el-input
              v-model="activity.ActivityTitle"
              maxlength="50"
              placeholder="活动名"
            />
          </el-form-item>
          <el-form-item label="促销时段" prop="ActivityTime">
            <el-date-picker
              v-model="activity.ActivityTime"
              type="datetimerange"
              :picker-options="pickerOptions"
              range-separator="至"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              format="yyyy-MM-dd HH:mm"
            />
          </el-form-item>
          <el-form-item label="活动简介" prop="ActivityShow">
            <el-input
              v-model="activity.ActivityShow"
              maxlength="100"
              placeholder="活动简介"
            />
          </el-form-item>
        </el-card>
        <el-card style="margin-top: 10px">
          <div slot="header">
            <span>其它设置</span>
          </div>
          <el-form-item label="定向人群" prop="UserRange">
            <enumItem
              v-model="activity.UserRange"
              :dic-key="EnumDic.userRange"
              placeholder="定向人群"
              :multiple="false"
              sys-head="shop"
              mode="radio-button"
            />
          </el-form-item>
          <el-form-item
            v-if="activity.UserRange == 0"
            label="定向会员等级"
            prop="UserGrade"
          >
            <userGradeItem
              v-model="activity.UserGrade"
              placeholder="定向会员等级"
              :multiple="false"
              mode="radio"
            />
          </el-form-item>
          <el-form-item
            v-if="activityType == 1"
            label="优惠范围"
            prop="UserRange"
          >
            <enumItem
              v-model="activity.Range"
              :dic-key="EnumDic.range"
              placeholder="优惠范围"
              :multiple="false"
              sys-head="shop"
              mode="radio-button"
            />
          </el-form-item>
          <el-form-item label="每单限购" prop="IsLimitBuyNum">
            <el-radio-group
              v-model="activity.IsLimitBuyNum"
              :disabled="activityType == 3"
            >
              <p>
                <el-radio :label="false">不限购</el-radio>
              </p>
              <p>
                <el-radio :label="true">
                  <span>每单每个SKU最少</span>
                  <el-input-number
                    v-model="activity.MinBuyNum"
                    :disabled="
                      activity.IsLimitBuyNum == false || activityType == 3
                    "
                    :min="1"
                    style="width: 120px"
                  />
                  <span
                    style="padding-left: 2px; padding-right: 2px"
                  >个，最多</span>
                  <el-input-number
                    v-model="activity.MaxBuyNum"
                    :disabled="
                      activity.IsLimitBuyNum == false || activityType == 3
                    "
                    :min="1"
                    style="width: 120px"
                  />
                  <span>个。</span>
                </el-radio>
              </p>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="是否限购一单" prop="IsLimitBuy">
            <el-switch
              v-model="activity.IsLimitBuy"
              :disabled="activityType == 3"
              active-text="是"
              inactive-text="否"
            />
          </el-form-item>
        </el-card>
        <el-row style="text-align: center; margin-top: 20px">
          <el-button
            :loading="loading"
            type="success"
            @click="save"
          >保存并下一步</el-button>
          <el-button @click="reset">重置</el-button>
        </el-row>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ActivityType, EnumDic } from '@/shop/config/shopConfig'
import moment from 'moment'
import userGradeItem from '@/shop/components/userGrade/userGradeItem.vue'
import * as activityApi from '@/shop/api/activity'
export default {
  components: {
    userGradeItem
  },
  props: {
    isLoad: {
      type: Boolean,
      default: false
    },
    activityType: {
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
      activity: {},
      title: null,
      pickerOptions: {
        disabledDate: function(time) {
          return time < moment().startOf('date')
        }
      },
      rules: {
        ActivityTitle: [
          {
            required: true,
            message: '促销名不能为空!',
            trigger: 'blur'
          }
        ],
        ActivityTime: [
          {
            required: true,
            message: '促销时段不能为空!',
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
    async reset() {
      this.loading = false
      this.title = ActivityType[this.activityType].text
      if (this.id == null) {
        this.activity = {
          ActivityTitle: null,
          ActivityShow: null,
          ActivityTime: [],
          UserRange: 0,
          MinBuyNum: 1,
          MaxBuyNum: 1,
          UserGrade: null,
          Range: 0,
          IsLimitBuy: false,
          IsLimitBuyNum: false
        }
        if (this.activityType == 3) {
          this.activity.IsLimitBuy = true
          this.activity.IsLimitBuyNum = true
        }
      } else {
        const res = await activityApi.Get(this.id)
        res.ActivityTime = [res.BeginTime, res.EndTime]
        delete res.BeginTime
        delete res.EndTime
        this.activity = res
        this.$emit('load', res)
      }
    },
    save() {
      const that = this
      this.$refs['activityEdit'].validate((valid) => {
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
      const data = Object.assign({}, this.activity)
      data.ActivityType = this.activityType
      data.BeginTime = data.ActivityTime[0]
      data.EndTime = data.ActivityTime[1]
      delete data.ActivityTime
      delete data.Id
      return data
    },
    async add() {
      this.loading = true
      const data = this.format()
      try {
        const id = await activityApi.Add(data)
        this.$message({
          type: 'success',
          message: '添加成功!'
        })
        data.Id = id
        this.$emit('next', data)
      } catch {
        this.loading = false
      }
    },
    async set() {
      this.loading = true
      const data = this.format()
      try {
        await activityApi.Set(this.id, data)
        this.$message({
          type: 'success',
          message: '保存成功!'
        })
        data.Id = this.id
        this.$emit('next', data)
      } catch {
        this.loading = false
      }
    }
  }
}
</script>
