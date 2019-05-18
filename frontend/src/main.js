import Vue from 'vue'
import App from './App.vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faMeh,
} from '@fortawesome/free-regular-svg-icons'
import {
  faMapMarkedAlt,
  faPlus,
  faThumbsDown,
  faThumbsUp,
} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.component('icon', FontAwesomeIcon)

library.add(
  faMapMarkedAlt,
  faMeh,
  faPlus,
  faThumbsDown,
  faThumbsUp,
)

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
