<template>
  <div v-show="ratings.length">
    <div v-for="rating in ratings" :key="rating.id">
      <span>{{ rating.date }}</span>
      <span v-for="{ rating, type } in rating.categories" :key="type">
        {{ rating }}&nbsp;
      </span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'VenuRatings',
  data() {
    return {
      ratings: []
    };
  },
  async mounted() {
    setTimeout(() => {
      this.ratings = [{
        id: '0',
        date: new Date(),
        categories: [{
          type: 'beer',
          rating: 1,
        }, {
          type: 'food',
          rating: -1,
        }, {
          type: 'general',
          rating: 0,
        }]
      }]
    }, 1000);
  }
}
</script>

<style scoped>
</style>
