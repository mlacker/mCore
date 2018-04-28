import Vue from 'vue'
import Router from 'vue-router'

import Index from '@/components/Index'
import Claims from '@/components/account/Claims'
import Login from '@/components/account/Login'
import Register from '@/components/account/Register'
import Callback from '@/components/account/Callback'

Vue.use(Router)

export default new Router({
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
      path: '/account/login',
      name: 'Login',
      component: Login
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
    }
  ]
})
