<template>
  <el-dialog
    title="编辑图片热区"
    :visible="visible"
    width="900px"
    :modal="false"
    :append-to-body="true"
    :before-close="handleClose"
  >
    <el-row :gutter="24">
      <el-col :span="14">
        <div style="width: 100%;text-align: center;">
          <VueHotImg
            v-model="zonesList"
            :image="img"
            style="width: 375px;display:inline-block;"
            @select-zone="onSelect"
          />
        </div>
      </el-col>
      <el-col :span="10">
        <el-card>
          <span slot="header">已选热点</span>
          <el-row v-for="(item,index) in zonesList" :key="index" class="row" :gutter="24">
            <el-col class="label" :span="2">{{ index + 1 }}，</el-col>
            <el-col :span="19">
              <span>{{ itemSet[item.extra.id] == null ?'未设置' : itemSet[item.extra.id].Title }}</span>
            </el-col>
            <el-col :span="3">
              <el-button icon="el-icon-setting" type="primary" circle size="small" @click="chiose(item)" />
            </el-col>
          </el-row>
        </el-card>
      </el-col>
    </el-row>
    <contentEdit :visible="editVisable" :module-id="moduleId" :page-id="pageId" :item-id="itemId" :config="config" @save="saveConfig" @close="editVisable=false" />
  </el-dialog>
</template>

<script>
import VueHotImg from 'vue-hot-img'
import contentEdit from './contentChioseEdit.vue'
export default {
  components: {
    VueHotImg,
    contentEdit
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    imgSrc: {
      type: String,
      default: null
    },
    pageId: {
      type: String,
      default: null
    },
    moduleId: {
      type: String,
      default: null
    },
    zones: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      zonesList: [],
      img: null,
      id: 1,
      loading: false,
      itemId: null,
      editVisable: null,
      itemSet: {},
      config: null
    }
  },
  computed: {},
  watch: {
    visible: {
      handler(val) {
        if (val) {
          this.reset()
        }
      },
      immediate: true
    }
  },
  methods: {
    reset() {
      this.img = this.imgSrc
      this.itemSet = {}
      this.zonesList = []
      this.id = 1
      if (this.zones && this.zones.length > 0) {
        const list = []
        let max = 0
        this.zones.forEach(c => {
          c.Id = parseInt(c.Id)
          let title = null
          if (c.JumpType === 0) {
            title = c.Sku.Title
          } else if (c.JumpType === 1) {
            title = c.Activity.ActivityTitle
          } else {
            title = c.Link.Title
          }
          this.itemSet[c.Id] = {
            JumpType: c.JumpType,
            Sku: c.Sku,
            Activity: c.Activity,
            Title: title,
            Link: c.Link
          }
          list.push({
            topPer: c.Y,
            leftPer: c.X,
            widthPer: c.Width,
            heightPer: c.Height,
            extra: {
              id: c.Id
            }
          })
          if (c.Id > max) {
            max = c.Id
          }
        })
        this.id = max + 1
        this.zonesList = list
      } else {
        this.zonesList = []
      }
    },
    chiose(item) {
      this.config = this.itemSet[item.extra.id]
      this.itemId = item.extra.id
      this.editVisable = true
    },
    saveConfig(id, config) {
      this.itemSet[id] = config
      this.editVisable = false
      this.save()
    },
    save() {
      if (this.zonesList.length === 0) {
        this.$emit('save', [])
        return
      }
      const items = this.zonesList.map(c => {
        const data = {
          Id: c.extra.id,
          X: c.leftPer,
          Y: c.topPer,
          Width: c.widthPer,
          Height: c.heightPer,
          Content: this.itemSet[c.extra.id]
        }
        const cont = this.itemSet[c.extra.id]
        if (cont != null) {
          data.Content = {
            JumpType: cont.JumpType,
            Sku: cont.Sku,
            Activity: cont.Activity,
            Link: cont.Link
          }
        }
        return data
      })
      this.$emit('save', items)
    },
    onSelect(e) {
      if (e && e.extra == null) {
        e.extra = {
          id: this.id
        }
        this.id = this.id + 1
      }
    },
    handleClose() {
      this.$emit('close')
    }
  }
}
</script>
<style scoped>
.row {
  line-height: 40px;
}
.label {
  color: #1890ff;
  font-size: 14px;
  font-weight: 400;
}
</style>
