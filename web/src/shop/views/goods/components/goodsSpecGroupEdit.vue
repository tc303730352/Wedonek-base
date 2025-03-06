<template>
  <el-dialog
    title="编辑规格组信息"
    :visible="visible"
    width="600px"
    :before-close="close"
    :close-on-click-modal="false"
  >
    <el-form
      ref="specGroupEdit"
      :model="editData"
      label-width="130px"
      :rules="rules"
    >
      <el-form-item label="规格组名称" prop="GroupName">
        <el-input
          v-model="editData.GroupName"
          maxlength="50"
          placeholder="规格项名称"
        />
      </el-form-item>
      <el-row style="text-align: center; margin-top: 20px">
        <el-button
          :loading="loading"
          type="primary"
          @click="save"
        >确定</el-button>
        <el-button @click="reset">重置</el-button>
      </el-row>
    </el-form>
  </el-dialog>
</template>

<script>
import { SetGroup, AddGroup } from '@/shop/api/goodsSpec'
import imageUpBtn from '@/components/fileUp/imageUpBtn.vue'
export default {
  components: {
    imageUpBtn
  },
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    goodsId: {
      type: String,
      default: null
    },
    group: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      loading: false,
      rules: {
        GroupName: [
          {
            required: true,
            message: '规格组名不能为空！',
            trigger: 'blur'
          }
        ]
      },
      editData: {
        Id: null,
        GroupName: null
      }
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
      this.editData.GroupName = this.group.GroupName
      this.editData.Id = this.group.Id
    },
    save() {
      const that = this
      this.$refs['specGroupEdit'].validate((valid) => {
        if (valid) {
          that.loading = true
          if (that.editData.Id == null) {
            that.add()
          } else {
            that.set()
          }
        } else {
          return false
        }
      })
    },
    close() {
      this.$emit('close')
    },
    async add() {
      const data = {
        GroupName: this.editData.GroupName,
        GoodsId: this.goodsId
      }
      data.Id = await AddGroup(data)
      this.loading = false
      this.$emit('save', {
        type: 'add',
        data
      })
    },
    async set() {
      const data = {
        GroupName: this.editData.GroupName
      }
      const isSet = await SetGroup(this.editData.Id, this.editData.GroupName)
      this.loading = false
      this.$emit('save', {
        type: 'set',
        isUpdate: isSet,
        data: {
          GroupName: this.editData.GroupName,
          Id: this.editData.Id
        }
      })
    }
  }
}
</script>
  <style type="less" scoped>
</style>
