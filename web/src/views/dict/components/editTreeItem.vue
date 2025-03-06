<template>
  <el-dialog
    :title="title"
    :visible="visible"
    width="500px"
    :before-close="closeForm"
    :modal="false"
    :close-on-click-modal="false"
  >
    <el-form ref="dicEdit" :model="item" :rules="rules">
      <el-form-item label="父项" prop="ParentValue">
        <treeDicItem
          v-model="item.ParentValue"
          :dic-id="dicId"
          :disabled="id != null"
          :is-choice-end="false"
          placeholder="父级字典项"
          @change="prtChange"
        />
      </el-form-item>
      <el-form-item label="字典项文本" prop="DicText">
        <el-input
          v-model="item.DicText"
          maxlength="100"
          placeholder="文本说明"
        />
      </el-form-item>
      <el-form-item v-if="id == null" label="值自动生成">
        <el-switch v-model="item.IsAuto" />
      </el-form-item>
      <el-form-item
        v-if="item.IsAuto == false"
        label="字典项值"
        prop="DicValue"
      >
        <el-input
          v-model="item.DicValue"
          :disabled="id != null"
          maxlength="20"
          placeholder="字典项值"
        />
      </el-form-item>
    </el-form>
    <div slot="footer" style="text-align: center">
      <el-button type="primary" @click="save">保存</el-button>
      <el-button @click="resetForm">重置</el-button>
    </div>
  </el-dialog>
</template>

<script>
import * as itemApi from '@/api/base/treeDicItem'
export default {
  name: 'EditDicItem',
  components: {},
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    dicId: {
      type: String,
      default: null
    },
    parent: {
      type: Object,
      default: () => null
    },
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      title: '新增字典',
      item: {},
      source: null,
      rules: {
        DicText: [
          {
            required: true,
            message: '字典项文本不能为空！',
            trigger: 'blur'
          }
        ],
        DicValue: [
          {
            required: true,
            message: '字典值不能为空！',
            trigger: 'blur'
          }
        ]
      }
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
  methods: {
    prtChange(e) {
      if (e.item == null) {
        this.item.ParentId = null
      } else {
        this.item.ParentId = e.item[0].Id
      }
    },
    save() {
      const that = this
      this.$refs['dicEdit'].validate((valid) => {
        if (valid) {
          if (that.id != null) {
            that.set()
          } else {
            that.add()
          }
        } else {
          return false
        }
      })
    },
    async set() {
      await itemApi.Set(this.id, this.item)
      this.$message({
        message: '更新成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async add() {
      this.item.DicId = this.dicId
      await itemApi.Add(this.item)
      this.$message({
        message: '添加成功!',
        type: 'success'
      })
      this.$emit('close', true)
    },
    async reset() {
      if (this.id == null) {
        this.title = '新增字典项'
        this.source = null
        if (this.parent != null) {
          this.item = {
            IsAuto: true,
            ParentValue: this.parent.DicValue,
            ParentId: this.parent.Id
          }
        } else {
          this.item = {}
        }
      } else {
        const data = await itemApi.Get(this.id)
        this.source = data
        this.item = data
        this.item.IsAuto = false
        this.title = '编辑字典项-' + data.DicText
      }
    },
    closeForm() {
      this.$emit('close', false)
    },
    resetForm() {
      if (this.id == null) {
        this.dic = {}
      } else {
        this.dic = this.source
      }
    }
  }
}
</script>
