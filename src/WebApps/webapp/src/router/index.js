import Vue from 'vue'
import Router from 'vue-router'

import Index from '@/components/index'
import Login from '@/components/account/login'
import Callback from '@/components/account/callback'
import ProcessList from '@/components/processes/process-list'
import ProcessDesign from '@/components/processes/process-design'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Index',
      component: Index
    },
    {
      path: '/account/login',
      name: 'login',
      component: Login
    },
    {
      path: '/account/callback',
      name: 'callback',
      component: Callback
    },
    {
      path: '/processes/process-list',
      name: 'process-list',
      component: ProcessList
    },
    {
      path: '/processes/process-design/:id?',
      name: 'process-design',
      component: ProcessDesign
    }
  ]
})
