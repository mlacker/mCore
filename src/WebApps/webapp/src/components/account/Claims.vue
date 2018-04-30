<template>
  <div>
    <dl>
      <dt>Type</dt>
      <dd>Value</dd>
      <template v-for="(claim, index) in claims">
      <dt :key="claim.type">{{ claim.type }}</dt>
      <dd :key="index">{{ claim.value }}</dd>
      </template>
    </dl>
  </div>
</template>

<script>
export default {
  name: 'Claims',
  data () {
    return {
      claims: []
    }
  },
  mounted () {
    this.getClaims()
  },
  methods: {
    getClaims () {
      let claims = this.claims
      this.$root.$userManager.getUser().then((user) => {
        if (user) {
          claims.splice(0, claims.length)

          for (let key in user.profile) {
            claims.push({
              type: key,
              value: user.profile[key]
            })
          }
        }
      })
    }
  }
}
</script>

<style>
</style>
