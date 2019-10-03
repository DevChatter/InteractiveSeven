import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    colors: {
      topLeft: '#007300',
      topRight: '#004B00',
      bottomLeft: '#004F00',
      bottomRight: '#001600'
    }
  },
  getters: {
    topLeftStyle (state) {
      return {
        background: 'linear-gradient(to top left, rgba(0,0,176, 0) 50%, ' + state.colors.topLeft + ')'
      }
    },
    topRightStyle (state) {
      return {
        background: 'radial-gradient(farthest-side at top right, ' + state.colors.topRight + ', rgba(0, 0, 80, 0))'
      }
    },
    botLeftStyle (state) {
      return {
        background: 'radial-gradient(farthest-side at bottom left, ' + state.colors.bottomLeft + ', rgba(0, 0, 128, 0))'
      }
    },
    botRightStyle (state) {
      return {
        background: 'linear-gradient(to bottom right, rgba(0, 0, 32, 0) 50%, ' + state.colors.bottomRight + ')'
      }
    }
  },
  mutations: {

  },
  actions: {

  }
})
