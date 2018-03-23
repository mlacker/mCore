// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Resource from 'vue-resource'
import Oidc from 'oidc-client'

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>',
  created () {
    this.config()
  },
  methods: {
    config () {
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
              this.$root.$userManager.signinRedirect()
              //   this.$router.push('Login')
              break
          }
        }
      })

      this.$userManager = new Oidc.UserManager({
        authority: 'http://localhost:5000',
        client_id: 'js',
        redirect_uri: 'http://localhost:8080/callback',
        response_type: 'id_token token',
        scope: 'openid profile process',
        post_logout_redirect_uri: 'http://localhost:8080/'
      })

      this.$userManager.getUser().then(user => {
        if (user) {
          console.log('User logged in', user.profile)
        } else {
          console.log('User not logged in')
        }
      })
    }
  }
})
