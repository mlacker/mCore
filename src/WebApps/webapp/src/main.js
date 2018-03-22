// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Resource from 'vue-resource'

Vue.use(Resource)

Vue.config.productionTip = false
Vue.http.interceptors.push(function (request) {
  // return response callback
  return function (response) {
    switch (response.status) {
      case 400:
        console.error(response.body)
        break
      case 401:
        this.$router.push(response.headers.get('location'))
        break
    }
  }
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
