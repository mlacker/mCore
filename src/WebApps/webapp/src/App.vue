<template>
  <el-container id="app">
    <el-header>
      <el-menu :default-active="page.activeIndex" mode="horizontal" :router="true">
        <template v-for="menu in page.menus">
          <el-menu-item v-if="!menu.children" :key="menu.id" :index="menu.id" :route="menu.link">{{menu.name}}</el-menu-item>
          <el-submenu v-else :key="menu.id" :index="menu.id">
            <template slot="title">{{menu.name}}</template>
            <template v-for="smenu in menu.children">
              <el-menu-item :key="smenu.id" :index="menu.id" :route="smenu.link">{{smenu.name}}</el-menu-item>
            </template>
          </el-submenu>
        </template>
      </el-menu>
    </el-header>

    <el-main>
      <router-view/>
    </el-main>
  </el-container>
</template>

<script>
export default {
  name: 'App',
  data () {
    return {
      page: {
        activeIndex: '1',
        menus: {}
      }
    }
  },
  mounted () {
    this.getMenus()
  },
  methods: {
    getMenus () {
      this.page.menus = [
        { id: '1', name: 'Index', link: '/' },
        { id: '2', name: 'Claims', link: '/account/claims' },
        { id: '3', name: 'LogIn', link: '/account/login' },
        { id: '4', name: 'Register', link: '/account/register' },
        {
          id: '5',
          name: '流程',
          children: [
            { id: '5-1', name: '流程定义', link: '/processes/process-list' }
          ]
        }
      ]
    }
  }
}
</script>

<style>
#app > header > ul.el-menu {
  padding-left: 160px;
}
</style>
