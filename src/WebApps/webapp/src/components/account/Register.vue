<template>
  <form id="register-form" @submit.prevent="submit">
    <div>
      <label for="Username">Username</label>
      <input v-model="Username" />
    </div>
    <div>
      <label for="Name">Name</label>
      <input v-model="Name" />
    </div>
    <div>
      <label for="PhoneNumber">PhoneNumber</label>
      <input v-model="PhoneNumber" />
    </div>
    <div>
      <label for="Password">Password</label>
      <input v-model="Password" type="password" />
    </div>
    <div>
      <label for="ConfirmPassword">ConfirmPassword</label>
      <input v-model="ConfirmPassword" type="password" />
    </div>
    <div>
      <button>Register</button>
    </div>
  </form>
</template>

<script>
export default {
  name: 'Register',
  data () {
    return {
      Username: '',
      Name: '',
      PhoneNumber: '',
      Password: '',
      ConfirmPassword: ''
    }
  },
  methods: {
    submit () {
      this.$http.post('/api/account', this.$data)
    }
  }
}
</script>

<style scoped>
</style>
