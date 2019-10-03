import Vue from 'vue'
import App from './app.vue'
import router from '@/router/panel'
import store from '@/store/panel'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
