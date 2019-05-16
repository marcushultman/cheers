<template>
  <div id="app">
    <VenueSelector/>
    <VenueRatings/>
    <RatingForm/>
  </div>
</template>

<script>
import VenueSelector from './components/VenueSelector.vue'
import VenueRatings from './components/VenueRatings.vue'
import RatingForm from './components/RatingForm.vue'

export default {
  name: 'app',
  components: {
    VenueSelector,
    VenueRatings,
    RatingForm,
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
