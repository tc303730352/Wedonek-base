<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
    </div>
    <el-steps
      style="margin-bottom: 20px"
      :active="chiose"
      finish-status="success"
    >
      <el-step
        v-for="(step, index) in this.curSteps"
        :key="step.key"
        :title="step.title"
        @click.native="chioseStep(step.key, index)"
      />
    </el-steps>
    <activityDatum
      v-if="curKey == 'basic'"
      :id="activityId"
      :activity-type="activityType"
      :is-load="curKey == 'basic'"
      @load="loadActivity"
      @next="save"
    />
    <activitySkuSet
      v-else-if="curKey == 'disGoods'"
      :id="activityId"
      :activity-type="activityType"
      :is-load="curKey == 'disGoods'"
      @next="nextStep"
      @prev="prevStep"
    />
    <activityDiscount
      v-else-if="curKey == 'discount'"
      :id="activityId"
      :is-load="curKey == 'discount'"
      @next="nextStep"
      @prev="prevStep"
    />
    <activitySkuLimit
      v-else-if="curKey == 'goodsLimit'"
      :id="activityId"
      :is-load="curKey == 'goodsLimit'"
      @next="nextStep"
      @prev="prevStep"
    />
    <activityInfo
      v-else-if="curKey == 'activity'"
      :id="activityId"
      :is-load="curKey == 'activity'"
      @prev="prevStep"
    />
  </el-card>
</template>

<script>
import { ActivityType } from '@/shop/config/shopConfig'
import moment from 'moment'
import activityDatum from './components/activityDatum.vue'
import activitySkuSet from './components/activitySkuSet.vue'
import activityDiscount from './components/activityDiscount.vue'
import activitySkuLimit from './components/activitySkuLimit.vue'
import activityInfo from './components/activityInfo.vue'
export default {
  components: {
    activityDatum,
    activitySkuSet,
    activityDiscount,
    activitySkuLimit,
    activityInfo
  },
  data() {
    return {
      activityId: null,
      activityType: 0,
      chiose: -1,
      title: null,
      curSteps: [],
      curKey: null,
      steps: [
        {
          title: '设置基本规则',
          key: 'basic',
          type: 'all'
        },
        {
          title: '设置优惠门槛及内容',
          key: 'discount',
          type: 'shop'
        },
        {
          title: '选择商品',
          key: 'goodsLimit',
          type: 'shop'
        },
        {
          title: '设置商品优惠',
          key: 'disGoods',
          type: 'single'
        },
        {
          title: '活动详细',
          key: 'activity',
          type: 'all'
        }
      ]
    }
  },
  computed: {},
  mounted() {
    this.activityType = parseInt(this.$route.params.type)
    const id = this.$route.params.id
    if (id == null) {
      this.title = '新增' + ActivityType[this.activityType].text + '活动'
    } else {
      this.activityId = parseInt(id)
      this.title = '编辑' + ActivityType[this.activityType].text + '活动'
    }
    this.load()
  },
  methods: {
    moment,
    load() {
      if (this.activityType == 1) {
        this.curSteps = this.steps.filter(
          (c) => c.type == 'all' || c.type == 'shop'
        )
      } else {
        this.curSteps = this.steps.filter(
          (c) => c.type == 'all' || c.type == 'single'
        )
      }
      this.chiose = 0
      this.curKey = this.curSteps[0].key
    },
    nextStep() {
      if (this.chiose == (this.curSteps.length - 1)) {
        return
      }
      this.chiose = this.chiose + 1
      this.curKey = this.curSteps[this.chiose].key
    },
    loadActivity(data) {
      if (this.activityType == 1) {
        if (data.Range == 0) {
          this.curSteps = this.steps.filter(
            (c) =>
              c.type == 'all' || (c.type == 'shop' && c.key != 'goodsLimit')
          )
        } else {
          this.curSteps = this.steps.filter(
            (c) => c.type == 'all' || c.type == 'shop'
          )
        }
      }
    },
    save(data) {
      this.activityId = data.Id
      this.loadActivity(data)
      this.chiose = 1
      this.curKey = this.curSteps[1].key
    },
    prevStep() {
      if (this.chiose == 0) {
        return
      }
      this.chiose = this.chiose - 1
      this.curKey = this.curSteps[this.chiose].key
    },
    chioseStep(key, index) {
      if (index != 0 && this.activityId != null) {
        this.chiose = index
        this.curKey = this.curSteps[index].key
      } else if (index == 0 && this.chiose != 0) {
        this.chiose = index
        this.curKey = this.curSteps[index].key
      }
    }
  }
}
</script>
