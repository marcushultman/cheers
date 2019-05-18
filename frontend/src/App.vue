<template>
  <div id="app">
    <VenueSelector :venueId.sync="venueId"/>
    <RatingForm :venueId="venueId"/>
  </div>
</template>

<script>
import VenueSelector from './components/VenueSelector.vue'
import RatingForm from './components/RatingForm.vue'

export default {
  name: 'app',
  components: {
    VenueSelector,
    RatingForm,
  },
  data() {
    return {
      venueId: '',
    };
  },
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
