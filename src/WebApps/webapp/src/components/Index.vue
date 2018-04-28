<template>
  <div>
    <p>Hello world!</p>
    <button @click="testApi">test api</button>
  </div>
</template>

<script>
export default {
  name: 'Index',
  data () {
    return {

    }
  },
  methods: {
    testApi () {
      this.$http.get('/api/values', {
        headers: {'Authorization': 'Bearer'}
      })
    }
  }
}
</script>

<style scoped>

</style>
