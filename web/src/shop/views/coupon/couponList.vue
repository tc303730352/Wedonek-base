<template>
  <el-tabs v-model="chiose" type="card">
    <el-tab-pane label="店铺卷" name="shop">
      <coupon :isLoad="chiose == 'shop'" :putInRange="0"></coupon>
    </el-tab-pane>
    <el-tab-pane label="商品卷" name="sku">
      <coupon :isLoad="chiose == 'sku'" :putInRange="1"></coupon>
    </el-tab-pane>
  </el-tabs>
</template>
          
    <script>
import coupon from "./components/coupon.vue";
export default {
  computed: {},
  data() {
    return {
      chiose: null,
    };
  },
  components: {
    coupon,
  },
  mounted() {
    this.chiose = "shop";
  },
  methods: {},
};
</script>