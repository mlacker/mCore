<template>
  <el-container>
    <el-header>
      <el-form :inline="true">
        <el-form-item>
          <el-input v-model="page.filters.searchString" placeholder="流程名称、创建用户" clearable></el-input>
        </el-form-item>
        <el-form-item>
          <el-select v-model="page.filters.category" placeholder="所属分类" clearable>
            <el-option v-for="item in options.categories"
              :key="item.value" :label="item.text" :value="item.value">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button @click="onSearch">查询</el-button>
        </el-form-item>
      </el-form>
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
      <el-pagination :current-page.sync="page.index" :page-size="page.size" :total="page.total" layout="->,total,prev,pager,next"></el-pagination>
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
      this.options.categories = [
        { value: '1', text: '默认分类' }
      ]
    },
    edit (row) {
    },
    remove (row) {
    },
    onSearch () {
      this.getProcessList()
    },
    onPageChange (val) {
      this.getProcessList()
    }
  }
}
</script>
