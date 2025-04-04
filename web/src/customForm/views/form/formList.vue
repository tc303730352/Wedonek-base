<template>
  <el-card>
    <div slot="header">
      <span>表单管理</span>
    </div>
    <div class="formMainBody">
      <div class="body">
        <div class="controlList">
          <div class="card">
            <div class="header">
              <div class="title">
                <el-divider direction="vertical" />
                模版
              </div>
            </div>
            <div class="list">
              <draggable
                v-model="layout"
                :allback-on-body="true"
                :sort="false"
                :group="layoutGroup"
                :move="checkIsMove"
              >
                <transition-group>
                  <div v-for="item in layout" :key="item.type" class="item">
                    <div class="icon">
                      <icon :icon="item.icon" />
                    </div>
                    <span>{{ item.Title }}</span>
                  </div>
                </transition-group>
              </draggable>
            </div>
          </div>
          <div class="card">
            <div class="header">
              <div class="title">
                <el-divider direction="vertical" />
                基本控件
              </div>
            </div>
            <div class="list">
              <draggable
                v-model="controls"
                :allback-on-body="true"
                :sort="false"
                :group="controlGroup"
                :move="checkIsMove"
              >
                <transition-group>
                  <div v-for="item in controls" :key="item.key" class="item">
                    <div class="icon">
                      <icon :icon="item.icon" />
                    </div>
                    <span>{{ item.Title }}</span>
                  </div>
                </transition-group>
              </draggable>
            </div>
          </div>
        </div>
        <div class="formBody">
          <div class="content">
            <draggable
              v-model="tables"
              class="draggable"
              :sort="true"
              :group="tableListGroup"
              draggable=".item"
              @add="addTable"
              @end="endSort"
            >
              <transition-group class="draggable-group">
                <div
                  v-for="item in tableList"
                  :key="item.Id"
                  class="item"
                >
                  <formTable
                    v-if="item.type == 'form'"
                    :table="item"
                  />
                </div>
              </transition-group>
            </draggable>
          </div>
        </div>
        <div class="clear" />
      </div>
    </div>
  </el-card>
</template>

<script>
import moment from 'moment'
import draggable from 'vuedraggable'
import formTable from './components/formTable.vue'
export default {
  components: {
    draggable,
    formTable
  },
  data() {
    return {
      layoutGroup: {
        name: 'layout',
        pull: 'clone',
        put: false
      },
      controlGroup: {
        name: 'control',
        pull: 'clone',
        put: false
      },
      tableListGroup: {
        name: 'layout',
        pull: false,
        put: true
      },
      layout: [
        {
          type: 'form',
          Title: '表单',
          icon: 'form'
        },
        {
          type: 'table',
          Title: '多行列表',
          icon: 'table'
        }
      ],
      controls: [
        {
          key: '1',
          type: 'input',
          Title: '文本控件',
          icon: 'input'
        }
      ],
      tables: [],
      tableList: [],
      id: 1
    }
  },
  computed: {},
  mounted() {},
  methods: {
    moment,
    checkIsMove(e) {
      if (e.relatedContext == null) {
        return false
      }
      const group = e.draggedContext.element.type === 'form' || e.draggedContext.element.type === 'table' ? 'layout' : 'control'
      if (e.relatedContext.component.$attrs.group.name !== group) {
        return false
      }
      return true
    },
    addTable(e) {
      const index = e.newIndex
      const t = Object.assign({}, this.tables[index])
      t.Controls = []
      t.Id = this.id
      this.id = this.id + 1
      this.tableList.push(t)
    },
    endSort(e) {
      const arr = this.tableList
      const t = arr[e.newIndex]
      arr[e.newIndex] = arr[e.oldIndex]
      arr[e.oldIndex] = t
      this.tableList = [].concat(arr)
    }
  }
}
</script>
<style>
.formMainBody .draggable .draggable-group {
  width: 100%;
  min-height: 120px;
  display: block;
}
</style>
<style scoped>
.formMainBody {
  width: 100%;
  text-align: center;
  min-width: 1640px;
}
.draggable-group .item:last-child {
  margin-bottom: 100px;
}
.formMainBody .body {
  max-width: 1920px;
  min-width: 1640px;
  display: inline-block;
  text-align: left;
}

.formMainBody .header {
  width: 100%;
  padding-bottom: 2px;
  line-height: 50px;
  border-bottom: 1px solid rgb(0 0 0 / 18%);
  margin-bottom: 15px;
  text-align: left;
}
.formMainBody .header .opBtn {
  float: right;
}

.formMainBody .header .title {
  font-size: 1.1rem;
  display: inline-block;
}
.formMainBody .header .title .el-divider--vertical {
  width: 4px;
  margin: 0;
  height: 1.2em;
  background-color: #0095ff;
}
.card {
  margin-bottom: 10px;
}
.clear {
  clear: both;
}
.controlList {
  width: 260px;
  float: left;
  border: 1px solid rgba(0, 0, 0, 0.1);
  padding: 10px;
}
.controlList .list {
  width: 220px;
}
.controlList .list .item {
  width: 100px;
  padding: 10px;
  margin: 5px;
  display: inline-block;
  height: 100px;
  text-align: center;
  cursor: pointer;
}
.controlList .list .item .icon {
  font-size: 2.2rem;
  height: 55px;
  line-height: 55px;
}
.controlList .list .item span {
  width: 100%;
  line-height: 25px;
  display: inline-block;
  font-size: 1rem;
  font-weight: 500;
}
.formBody {
  min-width: 1380px;
  max-width: 1660px;
  text-align: center;
  float: right;
}
.formBody .content {
  width: 1280px;
  display: inline-block;
  min-height: 800px;
  border: 1px solid rgba(0, 0, 0, 0.1);
  padding: 10px;
}
</style>
