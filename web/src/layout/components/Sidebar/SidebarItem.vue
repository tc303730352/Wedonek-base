<template>
  <div>
    <el-submenu
      v-if="item.isEnd == false && item.isReadOnly == false"
      :index="item.key"
      popper-append-to-body
    >
      <template slot="title">
        <item :icon="item.icon" :title="item.title" />
      </template>
      <sidebar-item
        v-for="child in item.children"
        :key="child.key"
        :item="child"
        class="nest-menu"
      />
    </el-submenu>
    <el-menu-item-group v-else-if="item.isEnd == false && item.isReadOnly">
      <template slot="title">
        <item :icon="item.icon" :title="item.title" />
      </template>
      <sidebar-item
        v-for="child in item.children"
        :key="child.key"
        :item="child"
        class="nest-menu"
      />
    </el-menu-item-group>
    <el-menu-item v-else :index="item.key">
      <template slot="title">
        <item :icon="item.icon" :title="item.title" />
      </template>
    </el-menu-item>
  </div>
</template>

<script>
import Item from "./Item";
import FixiOSBug from "./FixiOSBug";

export default {
  name: "SidebarItem",
  components: { Item },
  mixins: [FixiOSBug],
  props: {
    item: {
      type: Object,
      required: true,
    }
  },
  data() {
    return {};
  },
  methods: {
   
  },
};
</script>
