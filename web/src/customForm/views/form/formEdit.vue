<template>
  <el-card>
    <div slot="header">
      <span>编辑表单-{{ title }}</span>
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
                  <div v-for="item in controls" :key="item.Id" class="item">
                    <div class="icon">
                      <icon :icon="item.Icon" />
                    </div>
                    <span>{{ item.Name }}</span>
                  </div>
                </transition-group>
              </draggable>
            </div>
          </div>
          <div class="card">
            <div class="header">
              <div class="title">
                <el-divider direction="vertical" />
                扩展控件
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
                  <div v-for="item in extendControl" :key="item.Id" class="item">
                    <div class="icon">
                      <icon :icon="item.Icon" />
                    </div>
                    <span>{{ item.Name }}</span>
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
                    v-if="item.TableType == 0"
                    :form-id="formId"
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
import * as formApi from '@/customForm/api/form'
import * as tableApi from '@/customForm/api/table'
import { GetItems } from '@/customForm/api/control'
import draggable from 'vuedraggable'
import formTable from './components/formTable.vue'
export default {
  components: {
    draggable,
    formTable
  },
  data() {
    return {
      title: null,
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
      extendControl: [],
      controls: [],
      tables: [],
      tableList: [],
      form: {},
      formId: null
    }
  },
  computed: {},
  mounted() {
    this.formId = this.$route.params.id
    if (this.formId == null) {
      return
    }
    this.initControl()
    this.initForm()
  },
  methods: {
    moment,
    async initForm() {
      const res = await formApi.GetBody(this.formId)
      this.title = res.FormName
      if (res.Tables != null) {
        this.tableList = res.Tables
        this.tables = res.Tables.map(c => {
          if (c.FormType === 0) {
            return {
              id: c.Id,
              type: 'form',
              Title: '表单',
              icon: 'form'
            }
          }
          return {
            id: c.Id,
            type: 'table',
            Title: '多行列表',
            icon: 'table'
          }
        })
      }
    },
    async initControl() {
      const list = await GetItems()
      this.extendControl = list.filter(a => a.IsBaseControl === false)
      this.controls = list.filter(a => a.IsBaseControl)
    },
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
    async addTable(e) {
      const index = e.newIndex
      const t = this.tables[index]
      const add = {
        FormId: this.formId,
        TableType: t.type === 'form' ? 0 : 1,
        Title: this.title + '-' + (index + 1),
        Sort: index,
        IsHidden: false,
        LabelWidth: 80,
        ColNum: 2
      }
      const id = await tableApi.Add(add)
      t.Id = id
      add.Columns = []
      add.Id = id
      this.tableList.push(add)
    },
    async endSort(e) {
      if (this.tableList.length <= 1 || e.newIndex === e.oldIndex) {
        return
      }
      const data = []
      const arr = this.tableList
      const t = arr[e.newIndex]
      const old = arr[e.oldIndex]
      data.push({
        Id: t.Id,
        Sort: e.oldIndex
      })
      data.push({
        Id: old.Id,
        Sort: e.newIndex
      })
      await tableApi.SetSort(data)
      arr[e.oldIndex].Sort = e.newIndex
      arr[e.newIndex] = old
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
