<template>
  <div>
    <template v-if="error">{{ error.message }}</template>
    <template v-else>
      <template v-if="loading">Loading...</template>
      <template v-else>
        <template v-if="venues.length">
          <select v-model="selectedId" class="title" :class="{ add: !selectedId }">
            <option value="">Add venue</option>
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
  </div>
</template>

<script>
// json all the things 🍺
const headers = { 'accept': 'application/json', 'content-type': 'application/json' };

export default {
  name: 'VenueSelector',
  props: {
    venueId: String,
  },
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
  watch: {
    selectedId(id) {
      this.$emit('update:venueId', String(id));
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
  & input {
    flex: 1;
    margin: 8px 0;
    padding: 0;
    font-size: 20px;
    text-align: center;
    border: none;
    outline: none;
  }
  & button {
    position: absolute;
    right: 16px;
    margin: 8px 0;
    padding: 0;
    line-height: 20px;
    background: none;
    border: none;
  }
}
.add {
  font-size: 14px;
}
</style>
