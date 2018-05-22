<template>
  <el-form ref="form" :model="form">
      <el-form-item label="流程名称">
          <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item label="所属分类">
          <el-select v-model="form.category" placeholder="请选择">
              <el-option v-for="item in options.categories"
                :key="item.key" :label="item.text" :value="item.key">
              </el-option>
          </el-select>
      </el-form-item>
      <el-form-item>
        <el-table :data="form.activities" @row-click="getActivity" size="mini" border>
          <el-table-column type="index"></el-table-column>
          <el-table-column prop="name" label="节点名称" align="center"></el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item label="节点名称">
        <el-input v-model="page.currentActivity.name"></el-input>
        <el-button @click="removeActivity">删除</el-button>
        <el-button @click="addActivity">添加</el-button>
      </el-form-item>
      <el-form-item>
        <el-table :data="form.transitions" @row-click="getTransition" size="mini" border>
          <el-table-column type="index"></el-table-column>
          <el-table-column prop="name" label="流转名称" align="center"></el-table-column>
        </el-table>
      </el-form-item>
      <el-form-item label="流转名称">
        <el-input v-model="page.currentTransition.name"></el-input>
        <el-button @click="removeTransition">删除</el-button>
        <el-button @click="addTransition">添加</el-button>
      </el-form-item>
      <el-form-item>
          <el-button @click="onCancel">取消</el-button>
          <el-button type="primay" @click="onSubmit">保存</el-button>
      </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'process-design',
  data () {
    return {
      form: {
        name: '',
        category: '',
        activities: [],
        transitions: []
      },
      page: {
        isEdit: false,
        currentActivity: {},
        currentTransition: {}
      },
      options: {
        categories: []
      }
    }
  },
  mounted () {
    this.init()
    this.getCategoryOptions()
  },
  methods: {
    init () {
      let id = this.$route.params.id
      if ((this.page.isEdit = !!id)) {
        this.getProcess(id)
      }
    },
    onSubmit () {
      if (!this.page.isEdit) {
        this.$http.post('/api/process', this.form)
      } else {
        this.$http.put(`/api/process/${this.form.id}`, this.form)
      }
    },
    onCancel () {
      this.$router.back()
    },
    getCategoryOptions () {
      this.$http.get('/api/process/categories').then(res => {
        this.options.categories = res.body
      })
    },
    getProcess (id) {
      this.$http.get(`/api/process/${id}`).then(res => {
        this.form = res.body
      })
    },
    getActivity (row) {
      this.page.currentActivity = row
    },
    addActivity () {
      this.form.activities.push({ name: `节点 ${this.form.activities.length + 1}` })
    },
    removeActivity () {
      let pos = this.form.activities.indexOf(this.page.currentActivity)
      this.form.activities.splice(pos, 1)
    },
    getTransition (row) {
      this.page.currentTransition = row
    },
    addTransition () {
      this.form.transitions.push({ name: `流转 ${this.form.transitions.length + 1}` })
    },
    removeTransition () {
      let pos = this.form.transitions.indexOf(this.page.currentTransition)
      this.form.transitions.splice(pos, 1)
    }
  }
}
</script>

<style>
.el-form { width: 520px; margin: auto; }
.el-input { width: 260px; }
</style>
