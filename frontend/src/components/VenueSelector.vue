<template>
  <div>
    <template v-if="error">{{ error.message }}</template>
    <template v-else>
      <template v-if="loading">Loading...</template>
      <template v-else>
        <template v-if="venues.length">
          <select v-model="selectedId" class="title">
            <option value="">New venue:</option>
            <option v-for="venue in venues"
                :key="venue.id"
                :value="venue.id">{{ venue.name }}</option>
          </select>
        </template>
        <form v-if="!selectedId" @submit.prevent="addVenue">
          <input v-model="newVenue.name" placeholder="Name">
          <button type="submit">
            <icon icon="plus"/>
          </button>
        </form>
      </template>
    </template>
    <hr>
  </div>
</template>

<script>
// json all the things 🍺
const headers = { 'accept': 'application/json', 'content-type': 'application/json' };

export default {
  name: 'VenueSelector',
  data() {
    return {
      error: null,
      loading: true,
      venues: [],
      selectedId: '',
      newVenue: {
        name: '',
        longitude: 0,
        latitude: 0,
      },
    }
  },
  computed: {
    selectedVenue() {
      return this.venues.find(venue => venue.id === this.selectedId);
    },
  },
  async mounted() {
    const res = await fetch('/api/venues');
    if (!res.ok) {
      return this.error = new Error(res.statusText);
    }
    this.loading = false;
    const venues = res.ok ? await res.json() : [];
    venues.forEach(venue => venue.created = new Date(venue.created));
    this.venues = venues;
  },
  methods: {
    async addVenue() {
      const body = JSON.stringify(this.newVenue);
      const res = await fetch('/api/venues', { method: 'POST', headers, body });
      if (!res.ok) {
        return this.error = new Error(res.statusText);
      }
      const venue = await res.json();
      venue.created = new Date(venue.created);
      this.venues.push(venue);
      this.selectedId = venue.id;
    }
  }
}
</script>

<style lang="scss" scoped>
.title {
  width: 256px;
  margin: 20px 0 10px 0;
  font-size: 22px;
  text-align-last: center;
  background: inherit;
  border: none;
  outline: none;
}
.title::-ms-expand {
  display: none;
}
form {
  display: flex;
  margin: 0 8px;
  & input {
    flex: 1;
  }
}
input {
  font-size: 18px;
  outline: none;
  text-align: center;
}
</style>
