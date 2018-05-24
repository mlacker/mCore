<template>
  <div></div>
</template>

<script>
export default {
  name: 'callback',
  data () {
    return {

    }
  },
  mounted () {
    this.$root.$userManager.signinRedirectCallback()
      .then((user) => {
        this.$router.push('Index')
      }).catch((e) => {
        console.error(e)
      })
  }
}
</script>
