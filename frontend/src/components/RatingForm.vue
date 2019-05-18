<template>
  <div>
    <template v-if="error">{{ error.message }}</template>
    <form v-else @submit.prevent="sendRatings">
      <div v-for="{ category, text, rating } in categories" :key="category">
        <h4>{{ text }}</h4>
        <div class="icons">
          <icon v-for="{ icon, value } in choices" :key="value"
              class="icon"
              :class="{ selected: rating.score === value }"
              :icon="icon"
              @click="rating.score = value"/>
        </div>
      </div>
      <button type="submit" :disabled="!valid">Send</button>
    </form>
  </div>
</template>

<script>
// json all the things 🍺
const headers = { 'accept': 'application/json', 'content-type': 'application/json' };

export default {
  name: 'RatingForm',
  props: {
    venueId: {
      type: String,
      required: true
    },
  },
  data() {
    return {
      error: null,
      categories: [{
        category: 'beer',
        text: 'How was the beer?',
        rating: { score: null },
      }, {
        category: 'food',
        text: 'How was the food?',
        rating: { score: null },
      }, {
        category: 'overall',
        text: `What's the overall rating?`,
        rating: { score: null },
      }],
      choices: [{
        icon: 'thumbs-down',
        value: -1,
      }, {
        icon: ['far', 'meh'],
        value: 0,
      }, {
        icon: 'thumbs-up',
        value: 1,
      }],
    };
  },
  computed: {
    valid() {
      return this.venueId && this.categories.every(({ rating }) => rating.score !== null);
    },
  },
  methods: {
    async sendRatings() {
      const { venueId } = this;
      const ratings = this.categories.map(({ category, rating: { score }}) => {
        return { category, score };
      });
      const body = JSON.stringify({ venueId, ratings });
      const res = await fetch('/api/ratings', { method: 'POST', headers, body });
      if (!res.ok) {
        return this.error = new Error(res.statusText);
      }
    },
  },
}
</script>

<style scoped lang="scss">
.icons {
  display: flex;
}
.icon {
  flex: 1;
  font-size: 32px;
  padding: 0 8px;
  &.selected { color: #ffc92b; }
}
button {
  margin: 16px 0;
  font-size: 22px;
  background: #ffc92b;
  color: white;
  border: none;
  border-radius: 32px;
  outline: none;
  padding: 8px 32px;
  &[disabled] { background: lightgray; }
}
</style>
