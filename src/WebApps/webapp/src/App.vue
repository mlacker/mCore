<template>
  <div id="app">
    <header>
      <nav>
        <router-link to="/">Index</router-link>
        <router-link to="/account/claims">Claims</router-link>
        <router-link to="/account/register">Register</router-link>
      </nav>
      <Login></Login>
    </header>
    <main>
      <router-view/>
    </main>
  </div>
</template>

<script>
import Login from '@/components/account/Login'

export default {
  name: 'App',
  components: {
    Login
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
