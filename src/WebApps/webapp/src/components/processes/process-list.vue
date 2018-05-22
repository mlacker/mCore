<template>
  <el-container id="process-list">
    <el-header>
      <el-row>
        <el-col :span="8">
          <el-button @click="add" icon="el-icon-plus">新建</el-button>
        </el-col>
        <el-col :span="16">
          <el-form :inline="true">
            <el-form-item>
              <el-input v-model="page.filters.searchString" placeholder="流程名称、创建用户" clearable></el-input>
            </el-form-item>
            <el-form-item>
              <el-select v-model="page.filters.category" placeholder="所属分类" clearable>
                <el-option v-for="item in options.categories"
                  :key="item.key" :label="item.text" :value="item.key">
                </el-option>
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button @click="onSearch" icon="el-icon-search">搜索</el-button>
            </el-form-item>
          </el-form>
        </el-col>
      </el-row>
    </el-header>

    <el-main>
      <el-table :data="list" :border="true">
          <el-table-column type="index"></el-table-column>
          <el-table-column label="流程名称" prop="name" min-width="200px" align="center"></el-table-column>
          <el-table-column label="所属分类" prop="category" width="200px" align="center"></el-table-column>
          <el-table-column label="当前版本" prop="currentVersion" width="200px" align="center"></el-table-column>
          <el-table-column label="创建用户" prop="createdUser" width="200px" align="center"></el-table-column>
          <el-table-column label="创建时间" prop="createdTime" width="200px" align="center"></el-table-column>
          <el-table-column label="操作" width="240px" align="center">
            <template slot-scope="scope">
              <el-button @click="edit(scope.row)" type="text">编辑</el-button>
              <el-button @click="remove(scope.row)" type="text">删除</el-button>
            </template>
          </el-table-column>
      </el-table>
    </el-main>

    <el-footer>
      <el-pagination :current-page="page.index" :page-size="page.size" :total="page.total" layout="->,total,prev,pager,next"></el-pagination>
    </el-footer>
  </el-container>
</template>

<script>
export default {
  name: 'process-list',
  data () {
    return {
      list: [],
      page: {
        filters: {},
        index: 1,
        size: 10,
        total: 0
      },
      options: {
        categories: []
      }
    }
  },
  mounted () {
    this.getProcessList()
    this.getCategoryOptions()
  },
  methods: {
    getProcessList () {
      this.$http.get('/api/process', { params: this.page }).then(res => {
        this.list = res.body.items
        this.page.total = res.body.total
      })
    },
    getCategoryOptions () {
      this.$http.get('/api/process/categories').then(res => {
        this.options.categories = res.body
      })
    },
    add () {
      this.$router.push('process-design')
    },
    edit (row) {
      this.$router.push({ name: 'process-design', params: { id: row.id } },
        () => {
          console.log('onComplete', arguments)
        },
        () => {
          console.log('onAbort', arguments)
        })
    },
    remove (row) {
      this.$confirm('此操作将永久删除该流程, 是否继续?', '提示', { type: 'warning' }).then(() => {
        return this.$http.delete(`/api/process/${row.id}`)
      }).then(() => {
        this.$message({ type: 'success', message: '删除成功!' })
      })
    },
    onSearch () {
      this.getProcessList()
    },
    onPageChange (val) {
      this.page.index = val
      this.getProcessList()
    }
  }
}
</script>

<style>
#process-list > header form.el-form--inline { text-align: right; }
</style>
