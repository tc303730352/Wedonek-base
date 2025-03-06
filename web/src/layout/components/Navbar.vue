<template>
  <div class="navbar">
    <hamburger
      id="hamburger-container"
      :is-active="sidebar.opened"
      class="hamburger-container"
      @toggleClick="toggleSideBar"
    />

    <breadcrumb id="breadcrumb-container" class="breadcrumb-container" />

    <div class="right-menu">
      <div
        class="right-menu-item"
        v-if="subSystem != null && subSystem.length > 0"
      >
        <el-dropdown
          trigger="click"
          @command="chioseSubSystem"
        >
        <el-button class="btn" title="切换系统" icon="el-icon-s-tools" circle></el-button>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item v-for="(item) in this.subSystem" :icon="item.icon" :command="item.Id" :key="item.Id" >{{ item.Name }}</el-dropdown-item>
          </el-dropdown-menu>
        </el-dropdown>
      </div>
      <template v-if="device !== 'mobile'">
        <screenfull id="screenfull" class="right-menu-item hover-effect" />
      </template>
      <el-dropdown
        class="avatar-container right-menu-item hover-effect"
        trigger="click"
      >
        <div class="avatar-wrapper">
          <img :src="user.head" class="user-avatar" />
          <i class="el-icon-caret-bottom" />
        </div>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item divided @click.native="logout">
            <span style="display: block">退出登陆</span>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Breadcrumb from "@/components/Breadcrumb";
import Hamburger from "@/components/Hamburger";
import Screenfull from "@/components/Screenfull";

export default {
  components: {
    Breadcrumb,
    Hamburger,
    Screenfull,
  },
  data() {
    return {
      regionId: null,
      region: null,
      user: {
        head: null,
        name: null,
      },
    };
  },
  computed: {
    ...mapGetters(["sidebar", "device", "subSystem","curSysId"]),
  },
  mounted() {
    this.user = this.$store.getters.user;
  },
  methods: {
    toggleSideBar() {
      this.$store.dispatch("app/toggleSideBar");
    },
    chioseSubSystem(value) {
      if(this.curSysId != value) {
        this.$store.dispatch("user/switchSubSys", value);
        this.$router.push({ name: this.$store.getters.loadRoute })
      }
    },
    async logout() {
      await this.$store.dispatch("user/logout");
      this.$router.push(`/login?redirect=${this.$route.fullPath}`);
    },
  },
};
</script>

<style lang="scss" scoped>
.navbar {
  height: 50px;
  overflow: hidden;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);

  .hamburger-container {
    line-height: 46px;
    height: 100%;
    float: left;
    cursor: pointer;
    transition: background 0.3s;
    -webkit-tap-highlight-color: transparent;

    &:hover {
      background: rgba(0, 0, 0, 0.025);
    }
  }

  .breadcrumb-container {
    float: left;
  }

  .errLog-container {
    display: inline-block;
    vertical-align: top;
  }

  .right-menu {
    float: right;
    height: 100%;
    line-height: 50px;
    &:focus {
      outline: none;
    }

    .btn {
      height: auto;
      width: auto;
    }
    .right-menu-item {
      display: inline-block;
      padding: 0 8px;
      height: 100%;
      font-size: 18px;
      color: #5a5e66;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background 0.3s;

        &:hover {
          background: rgba(0, 0, 0, 0.025);
        }
      }
    }

    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 5px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}
</style>
