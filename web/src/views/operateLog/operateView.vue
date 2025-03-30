<template>
  <el-dialog
    title="操作日志详细"
    :visible="visible"
    min-width="1280px"
    width="80%"
    :before-close="closeForm"
    :modal-append-to-body="false"
    :append-to-body="true"
    :close-on-click-modal="false"
  >
    <el-form label-width="120px">
      <el-row>
        <el-col :span="6">
          <el-form-item label="模块标题">
            {{ log.Title }}
          </el-form-item>
          <el-form-item label="人员名称">
            {{ log.EmpName }}
          </el-form-item>
          <el-form-item label="部门名称">
            {{ log.DeptName }}
          </el-form-item>
          <el-form-item label="业务类型">
            {{ busType[log.BusType] }}
          </el-form-item>
          <el-form-item label="用户类型">
            <span v-if="log.UserType"> {{ HrLoginUserType[log.UserType].text }}</span>
          </el-form-item>
          <el-form-item label="请求路径">
            {{ log.Uri }}
          </el-form-item>
          <el-form-item label="Web路径">
            {{ log.Referer }}
          </el-form-item>
          <el-form-item label="Ip">
            {{ log.Ip }}
          </el-form-item>
          <el-form-item label="访问地址">
            {{ log.Address }}
          </el-form-item>
          <el-form-item label="是否成功">
            <span :style="{ color: log.IsSuccess ? '#43AF2B' : '#999' }">{{
              log.IsSuccess ? "成功" : "失败"
            }}</span>
          </el-form-item>
          <el-form-item v-if="log.IsSuccess == false" label="失败原因">
            {{ log.FailShow == null ? log.ErrorCode : log.FailShow }}
          </el-form-item>
          <el-form-item label="耗时">
            {{ log.Duration }} 毫秒
          </el-form-item>
          <el-form-item label="添加时间">
            {{ moment(log.AddTime).format('YYYY-MM-DD HH:mm:ss') }}
          </el-form-item>
        </el-col>
        <el-col :span="18">
          <el-form-item label="请求参数">
            <el-input
              :readonly="true"
              :value="log.ReqParam"
              type="textarea"
              autosize
              placeholder="请求参数"
            />
          </el-form-item>
          <el-form-item label="结果">
            <el-input
              :readonly="true"
              :value="log.Result"
              autosize
              type="textarea"
              placeholder="结果"
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import { get } from '@/api/operateLog'
import { HrLoginUserType } from '@/config/publicDic'
import { GetItemName } from '@/api/base/dictItem'
import moment from 'moment'
export default {
  components: {
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      HrLoginUserType,
      log: {}
    }
  },
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
  mounted() {
    this.loadItem()
  },
  methods: {
    moment,
    async loadItem() {
      this.busType = await GetItemName('6')
    },
    async reset() {
      const data = await get(this.id)
      data.Result = JSON.parse(data.Result)
      data.Result = JSON.stringify(data.Result, null, 2)
      if (data.ReqParam != null) {
        data.ReqParam = JSON.parse(data.ReqParam)
        data.ReqParam = JSON.stringify(data.ReqParam, null, 2)
      }
      this.log = data
    },
    closeForm() {
      this.$emit('update:visible', false)
    }
  }
}
</script>
