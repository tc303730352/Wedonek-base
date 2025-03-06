<template>
  <div :class="{ 'has-logo': showLogo }">
    <logo v-if="showLogo" :collapse="isCollapse" />
    <el-scrollbar wrap-class="scrollbar-wrapper">
      <el-menu
        :default-active="activeMenu"
        :collapse="isCollapse"
        :background-color="variables.menuBg"
        :text-color="variables.menuText"
        :unique-opened="false"
        :active-text-color="variables.menuActiveText"
        :collapse-transition="false"
        @select="chioseMenu"
        mode="vertical"
      >
        <sidebar-item v-for="route in menus" :key="route.key" :item="route" />
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Logo from "./Logo";
import SidebarItem from "./SidebarItem";
import variables from "@/styles/variables.scss";

export default {
  data() {
    return {
      activeMenu: null,
      menus: [],
      routeDic: {},
      names: {},
    };
  },
  components: { SidebarItem, Logo },
  methods: {
    chioseMenu(key, keyPath) {
      if (key == this.activeMenu) {
        return;
      }
      this.activeMenu = key;
      const route = this.routeDic[key];
      if(route.params == null) {
        route.params = {}
      }
      this.$router.push({ name: route.name, params: route.params });
    },
    initMenu(route, menus) {
      if (
        route.hidden &&
        (route.children == null || route.children.length == 0)
      ) {
        return;
      } else if (route.hidden) {
        route.children.forEach((a) => {
          this.initMenu(a, menus);
        });
      } else {
        const data = {
          key: route.id.toString(),
          title: route.meta.title,
          name: route.name,
          icon: route.meta.icon,
          children: [],
          home: null,
          isReadOnly: false,
          isEnd: true,
        };
        if (route.children != null && route.children.length != 0) {
          route.children.forEach((a) => {
            this.initMenu(a, data.children);
          });
          data.home = data.children[0].key;
          data.isEnd = false;
        } else {
          data.home = data.key;
        }
        this.names[route.name] = data.key;
        this.routeDic[data.key] = {
          name: route.name,
          params: route.params,
        };
        menus.push(data);
      }
    },
    initRoute() {
      const datas = [];
      const routes = this.routes;
      this.names = {};
      this.routeDic = {};
      routes.forEach((c) => {
        this.initMenu(c, datas);
      });
      this.menus = datas;
      const route = this.$route;
      const key = this.names[route.name];
      if (key && key != this.activeMenu) {
       this.chioseMenu(key);
      }
    },
  },
  watch: {
    curSysId: {
      handler(val) {
        this.initRoute();
      },
      immediate: true,
    },
  },
  computed: {
    ...mapGetters(["routes", "sidebar", "curSysId"]),
    showLogo() {
      return this.$store.state.settings.sidebarLogo;
    },
    variables() {
      return variables;
    },
    isCollapse() {
      return !this.sidebar.opened;
    },
  },
};
</script>
