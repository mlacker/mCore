import Vue from 'vue'
import Router from 'vue-router'

import Index from '@/components/Index'
import Claims from '@/components/account/Claims'
import Register from '@/components/account/Register'
import Callback from '@/components/account/Callback'
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
      path: '/account/claims',
      name: 'Claims',
      component: Claims
    },
    {
      path: '/account/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/callback',
      name: 'Callback',
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
