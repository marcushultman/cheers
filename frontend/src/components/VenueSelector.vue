<template>
  <div>
    <h3>Venue</h3>
    <template v-if="loading">Loading...</template>
    <template v-else-if="!venues.length">
      <input v-model="newVenue.name">
    </template>
    <template v-else>
      <select>
        <option v-for="venue in venues" :key="venue.id">{{ venue.name }}</option>
      </select>
    </template>
  </div>
</template>

<script>
export default {
  name: 'VenueSelector',
  data() {
    return {
      loading: true,
      venues: [],
      newVenue: {
        name: ''
      },
    }
  },
  async mounted() {
    setTimeout(() => {
      this.loading = false
      this.venues = [{
        id: '0',
        name: 'mockplace1',
      }, {
        id: '1',
        name: 'mockplace2',
      }]
    }, 1000);
  },
}
</script>

<style scoped>
</style>
