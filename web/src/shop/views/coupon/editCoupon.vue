<template>
  <el-card>
    <div slot="header">
      <span>{{ title }}</span>
    </div>
    <el-steps
      v-if="putInRange == 1"
      style="margin-bottom: 20px"
      :active="chiose"
      finish-status="success"
    >
      <el-step
        title="基本信息"
        key="base"
        @click.native="chioseStep(0)"
      ></el-step>
      <el-step
        title="优惠卷商品"
        key="sku"
        @click.native="chioseStep(1)"
      ></el-step>
    </el-steps>
    <couponDatum
      v-if="chiose == 0"
      @prev="prevStep"
      @next="nextStep"
      :isLoad="chiose == 0"
      :putInRange="putInRange"
      :id="id"
    ></couponDatum>
    <couponSkuLimit
      v-else
      @prev="prevStep"
      @next="nextStep"
      :isLoad="chiose == 1"
      :id="id"
    ></couponSkuLimit>
  </el-card>
</template>
 
<script>
import couponDatum from "./components/couponDatum.vue";
import couponSkuLimit from "./components/couponSkuLimit.vue";
export default {
  computed: {},
  data() {
    return {
      putInRange: 0,
      id: null,
      title: null,
      chiose: null,
    };
  },
  components: {
    couponDatum,
    couponSkuLimit,
  },
  mounted() {
    this.putInRange = parseInt(this.$route.params.range);
    const id = this.$route.params.id;
    if (id == null) {
      this.title = "新增" + (this.putInRange == 0 ? "店铺" : "商品") + "优惠卷";
    } else {
      this.title = "编辑" + (this.putInRange == 0 ? "店铺" : "商品") + "优惠卷";
      this.id = parseInt(id);
    }
    this.chiose = 0;
  },
  methods: {
    nextStep(id) {
      if (this.chiose == 1) {
        this.$router.back();
        return;
      } else if (this.putInRange == 1) {
        this.id = id
        this.chiose = 1;
      }
    },
    prevStep() {
      if (this.chiose == 0) {
        this.$router.back();
      } else {
        this.chiose = 0;
      }
    },
  },
};
</script>