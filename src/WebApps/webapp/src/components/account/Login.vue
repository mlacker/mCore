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
    <button>Login</button>
  </div>
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
      }
    }
  },
  mounted () {
    this.user.ReturnUrl = this.$route.query['returnUrl']
  },
  methods: {
    submit () {
      this.$http.post('/api/account/login', this.user).then(res => {
        if (res.ok) {
          this.$router.push('Index')
        }
      })
    }
  }
}
</script>

<style scoped>

</style>
