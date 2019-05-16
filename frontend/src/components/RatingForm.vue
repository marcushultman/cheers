<template>
  <form>
    <h2>Rate!</h2>
    <div v-for="{ type, text } in categories" :key="type">
      <h4>{{ text }}</h4>
      <span v-for="{ icon, value } in ratings" :key="value">{{ icon }}&nbsp;</span>
    </div>
    <input type="submit" name="Send rating!">
  </form>
</template>

<script>
export default {
  name: 'RatingForm',
  data() {
    return {
      categories: [{
        type: 'beer',
        text: 'How as the beer?',
      }, {
        type: 'food',
        text: 'How was the food?',
      }, {
        type: 'overall',
        text: `What's the overall rating?`,
      }],
      ratings: [{
        icon: '-',
        value: -1
      }, {
        icon: '0',
        value: 0
      }, {
        icon: '+',
        value: 1
      }],
    };
  },
}
</script>

<style scoped>
</style>
