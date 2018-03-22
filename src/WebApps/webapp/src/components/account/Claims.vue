<template>
    <table>
        <thead>
            <tr>
                <th>Type</th>
                <th>Value</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="claim in claims" :key="claim.type">
                <td class="claim-type">{{ claim.type }}</td>
                <td class="claim-value">{{ claim.value }}</td>
            </tr>
        </tbody>
    </table>
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
      this.$http.get('/api/account/claims').then(res => {
        this.claims = res.data
      })
    }
  }
}
</script>

<style scoped>
.claim-type {
  text-align: right;
  padding-right: 20px;
}
.claim-value {
  text-align: left;
}
</style>
