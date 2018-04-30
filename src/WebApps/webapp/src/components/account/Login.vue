<template>
  <div id="auth">
    <button type="button" @click="login">Login</button>
    <button type="button" @click="logout">Logout</button>
    <button type="button" @click="api">Call API</button>
  </div>
</template>

<script>
export default {
  name: 'Login',
  methods: {
    login () {
      this.$root.$userManager.signinRedirect()
    },
    logout () {
      this.$root.$userManager.signoutRedirect()
    },
    api () {
      this.$root.$userManager.getUser().then((user) => {
        this.$http.get('http://localhost:5001/api/values', {
          headers: {'Authorization': 'Bearer ' + user.access_token}
        }).then((res) => {
          console.log(res.body)
        })
      })
    }
  }
}
</script>

<style>
</style>
