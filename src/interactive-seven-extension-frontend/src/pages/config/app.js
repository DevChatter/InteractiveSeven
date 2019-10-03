import Vue from 'vue'
import App from './app.vue'
import store from '@/store/config'
import vuetify from '@/plugins/vuetify'

Vue.config.productionTip = false

new Vue({
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
