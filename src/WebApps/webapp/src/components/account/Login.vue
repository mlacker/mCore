<template>
<form id="login-form" @submit.prevent="submit">
  <input type="hidden" name="user.ReturnUrl" />
  <div>
    <label for="Username">Username</label>
    <input v-model="user.Username" />
  </div>
  <div>
    <label for="Password">Password</label>
    <input v-model="user.Password" type="password" />
  </div>
  <div>
    <label for="RememberMe">RememberMe</label>
    <input v-model="user.RememberMe" type="checkbox" />
  </div>
  <div>
    <button type="button" @click="login">Login</button>
    <button type="button" @click="logout">Logout</button>
  </div>
  <ul class="message">
    <li v-for="(item, key) in page.error" :key="key">
      {{ item }}
    </li>
  </ul>
</form>
</template>

<script>
export default {
  name: 'Login',
  data () {
    return {
      user: {
        Username: '',
        Password: '',
        RememberMe: false,
        ReturnUrl: ''
      },
      page: {
        error: []
      }
    }
  },
  mounted () {
    this.user.ReturnUrl = this.$route.query['returnUrl']
  },
  methods: {
    login () {
      this.$root.$userManager.signinRedirect()
      // this.$http.post('/api/account/login', this.user).then(res => {
      //   if (res.ok) {
      //     this.$router.push('Index')
      //   }
      // }, (res) => {
      //   if (res.status === 400) {
      //     this.page.error = res.body
      //   }
      // })
    },
    logout () {
      this.$root.$userManager.signoutRedirect()
    }
  }
}
</script>

<style scoped>

</style>
