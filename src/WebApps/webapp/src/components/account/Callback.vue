<template>
<div></div>
</template>

<script>
export default {
  name: 'Callback',
  data () {
    return {

    }
  },
  mounted () {
    let query = this.$route.fullPath.substring(this.$route.path.length + 1)
    this.$root.$userManager.signinRedirectCallback(query)
      .then(() => {
        this.$router.push('Index')
      }).catch((e) => {
        console.error(e)
      })
  }
}
</script>
