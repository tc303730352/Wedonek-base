<template>
  <div>
    <el-card>
      <div slot="header">
        <span>广告管理</span>
      </div>
      <el-tabs v-model="chiose" type="border-card">
        <el-tab-pane
          v-for="item in dataList"
          :key="item.Id"
          :name="item.Id.toString()"
          :label="item.PlaceTitle + '(' + item.AdvertNum + ')'"
        >
          <el-tabs v-model="item.Status" type="border-card" tab-position="left">
            <el-tab-pane label="投放中" name="ing">
              <advert
                :place-id="item.Id"
                :is-load="chiose == item.Id && item.Status == 'ing'"
                status="ing"
              />
            </el-tab-pane>
            <el-tab-pane label="已结束" name="end">
              <advert
                :place-id="item.Id"
                :is-load="chiose == item.Id && item.Status == 'end'"
                status="end"
              />
            </el-tab-pane>
            <el-tab-pane label="排期中" name="schedules">
              <advert
                :place-id="item.Id"
                :is-load="chiose == item.Id && item.Status == 'schedules'"
                status="schedules"
              />
            </el-tab-pane>
          </el-tabs>
        </el-tab-pane>
      </el-tabs>
    </el-card>
  </div>
</template>

<script>
import { Gets } from '../../api/advertPlace'
import advert from './components/advert.vue'
export default {
  components: {
    advert
  },
  data() {
    return {
      dataList: [],
      chiose: null
    }
  },
  computed: {},
  mounted() {
    this.load()
  },
  methods: {
    async load() {
      const res = await Gets(1)
      res.forEach(e => {
        e.Status = 'ing'
      })
      this.dataList = res
      if (res.length > 0) {
        this.chiose = res[0].Id.toString()
      }
    }
  }
}
</script>
