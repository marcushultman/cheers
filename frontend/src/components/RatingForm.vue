<template>
  <div>
    <template v-if="error">{{ error.message }}</template>
    <transition name="slide-fade">
      <h3 class="msg" v-if="showMsg">Ratings registered!</h3>
    </transition>
    <form @submit.prevent="sendRatings">
      <div v-for="{ category, text, rating } in categories" :key="category">
        <h4>{{ text }}</h4>
        <div class="icons">
          <icon v-for="{ icon, value } in choices" :key="value"
              class="icon"
              :class="{ selected: rating.score === value }"
              :icon="icon"
              @click="rating.score = rating.score == value ? null : value"/>
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
    venueId: String,
  },
  data() {
    return {
      error: null,
      showMsg: false,
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
        value: 0,
      }, {
        icon: ['far', 'meh'],
        value: 1,
      }, {
        icon: 'thumbs-up',
        value: 2,
      }],
    };
  },
  computed: {
    valid() {
      return !!this.venueId;
    },
  },
  methods: {
    async sendRatings() {
      const { venueId } = this;
      const ratings = this.categories
          .filter(({ rating: { score }}) => score !== null)
          .map(({ category, rating: { score }}) => ({ category, score }));
      const body = JSON.stringify({ venueId, ratings });
      const res = await fetch('/api/ratings', { method: 'POST', headers, body });
      if (!res.ok) {
        return this.error = new Error(res.statusText);
      }
      this.reset();
      this.showMsg = true;
      setTimeout(() => this.showMsg = false, 3000);
    },
    reset() {
      this.categories.forEach(({ rating }) => rating.score = null);
    },
  },
}
</script>

<style scoped lang="scss">
.msg {
  transition: all .3s ease;
}
.slide-fade-enter, .slide-fade-leave-to {
  transform: translateY(-10px);
  opacity: 0;
}
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
  margin: 24px 0;
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
