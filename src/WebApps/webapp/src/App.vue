<template>
  <div id="app">
    <router-link to="/">Index</router-link>
    <router-link to="/account/claims">Claims</router-link>
    <router-link to="/account/login">LogIn</router-link>
    <router-link to="/account/register">Register</router-link>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
