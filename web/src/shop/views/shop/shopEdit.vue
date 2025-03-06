<template>
  <el-card>
    <div slot="header" class="clearfix">
      <span>编辑门店资料</span>
    </div>
    <el-form :model="shop" label-width="150px" ref="shopEdit" :rules="rules">
      <el-form-item label="店铺名称" prop="ShopName">
        <el-input
          v-model="shop.ShopName"
          maxlength="100"
          placeholder="店铺名称"
        />
      </el-form-item>
      <el-form-item label="图标" prop="ShopLogo">
        <imageUpBtn
          fileKey="ShopLogo"
          v-model="fileId"
          @change="upComplete"
          :linkBizPk="shopId"
        />
      </el-form-item>
      <el-form-item label="门店介绍" prop="Show">
        <el-input
          type="textarea"
          v-model="shop.Show"
          maxlength="255"
          placeholder="门店介绍"
        />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button type="primary" @click="save">保存</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-card>
</template>
   
<script>
import * as shopApi from "@/shop/api/shopApi";
import imageUpBtn from "@/components/fileUp/imageUpBtn";
export default {
  computed: {},
  data() {
    return {
      loading: false,
      logo: null,
      shopId: null,
      fileId: null,
      shop: {
        ShopName: null,
      },
      rules: {
        ShopName: [
          {
            required: true,
            message: "门店名不能为空！",
            trigger: "blur",
          },
          {
            min: 2,
            max: 50,
            message: "门店名长度在 2 到 50 个字之间",
            trigger: "blur",
          },
        ],
      },
    };
  },
  async mounted() {
    this.shopId = await this.$store.dispatch("shop/getShopId");
    if (this.shopId == null) {
      this.$router.replace({
        name: this.$store.getters.loadRoute,
      });
      return;
    }
    this.reset();
  },
  methods: {
    upComplete(fileId, files) {
      if (files != null && files.length > 0) {
        this.logo = files[0].FileUri;
      } else {
        this.logo = null;
      }
    },
    save() {
      const that = this;
      this.$refs["shopEdit"].validate((valid) => {
        if (valid) {
          that.setShop();
        } else {
          return false;
        }
      });
    },
    async setShop() {
      this.loading = true;
      this.shop.ShopLogo = this.logo
      await shopApi.Set(this.shop);
      this.$message({
        type: "success",
        message: "保存成功!",
      });
      this.loading = false
    },
    async reset() {
      const res = await shopApi.Get();
      this.shop = res;
      this.logo = res.ShopLogo;
      this.loading = false;
    },
  },
  components: {
    imageUpBtn,
  },
};
</script>