<template>
  <div>
    <template v-if="error">{{ error.message }}</template>
    <template v-else>
      <template v-if="loading">Loading...</template>
      <template v-else>


        <div v-if="!error">
          <div class="find-input">
            <icon icon="list"/>
            <input autofocus v-model="venueSearch">
          </div>
          <div v-for="venue in filteredVenues">{{ venue.name }}</div>
          <div>kuken</div>
        </div>
        <template v-else>

          <h2 v-if="selectedVenue">
            <span>{{ selectedVenue }}</span>
          </h2>
          <div v-else class="find-label">Select todays venue...</div>

        </template>

        <!-- <select v-model="selectedId" class="title" :class="{ add: !selectedId }">
          <option value="">Add venue</option>
          <option v-for="venue in venues"
              :key="venue.id"
              :value="venue.id">{{ venue.name }}</option>
        </select>

        <form v-if="!selectedId" @submit.prevent="addVenue">
          <input v-model="newVenue.name" placeholder="Name">
          <button type="submit">
            <icon icon="plus"/>
          </button>
        </form> -->

        <a v-if="hasLocation" class="map" :href="mapLink">
          <icon icon="map-marked-alt"/>
        </a>

      </template>
    </template>
  </div>
</template>

<script>
// json all the things 🍺
const headers = { 'accept': 'application/json', 'content-type': 'application/json' };

function getLocation() {
  return new Promise((resolve, reject) => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(resolve);
    } else {
      reject(new Error('Geolocation is not supported by this browser.'));
    }
  })
}

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
      venueSearch: '',
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
      if (!id) {
        this.updateLocation();
      }
    }
  },
  computed: {
    selectedVenue() {
      return this.venues.find(venue => venue.id === this.selectedId);
    },
    hasLocation() {
      return this.selectedVenue && this.selectedVenue.longitude && this.selectedVenue.latitude;
    },
    mapLink() {
      return `https://maps.google.com/?q=${this.selectedVenue.latitude},${this.selectedVenue.longitude}`;
    },
    filteredVenues() {
      return this.venues.filter(venue => venue.name.toLowerCase().startsWith(this.venueSearch.toLowerCase()));
    },
  },
  async mounted() {
    await new Promise(r => setTimeout(r, 400));
    const res = await Promise.all([fetch('/api/venues'), fetch('/api/venues/current')]);
    if (!res.every(res => res.ok)) {
      return this.error = new Error(res.statusText);
    }
    const [resAll, resCurrent] = res;
    const venues = await resAll.json();
    venues.forEach(venue => venue.created = new Date(venue.created));
    this.venues = venues;
    this.loading = false;
    const currentVenue = null; // await resCurrent.json();
    if (currentVenue) {
      this.selectedId = currentVenue.id;
    } else {
      this.updateLocation();
    }
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
    },
    async updateLocation() {
      try {
        const location = await getLocation();
        this.newVenue.longitude = location.coords.longitude;
        this.newVenue.latitude = location.coords.latitude;
      } catch (e) {
        // ok, then we'll ignore it
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.title {
  margin: 20px 0 10px 0;
  font-size: 22px;
  text-align-last: center;
  background: inherit;
  border: none;
  outline: none;
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
.map {
  position: absolute;
  top: 24px;
  right: 24px;
  color: inherit;
  font-size: 24px;
}


.find-label {
  margin: 18px;
  color: gray;
  font-size: 20px;
  font-weight: normal;
  text-align: center;
}
.find-input {
  display: flex;
  align-items: center;
  margin: 12px 0;
  padding: 12px 8px;
  border-radius: 6px;
  background: #f4f4f4;
  & input {
    flex: 1;
    margin-left: 16px;
    color: #3e3e3e;
    background: inherit;
    border: none;
    outline: none;
    font-size: 16px;
  }
}

</style>
