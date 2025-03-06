import Vue from 'vue'

import Cookies from 'js-cookie'

import 'normalize.css/normalize.css' // a modern alternative to CSS resets

import Element from 'element-ui'

import './styles/index.scss' // global css
import './styles/element-variables.scss'

import App from './App'
import store from './store'
import router from './router'

import './icons' // icon
import './permission' // permission control
import './utils/error-log' // error log

import enumItem from '@/components/base/enumDicItem'
import treeDicItem from '@/components/base/treeDicItem'
import ElementResizeDetectorMaker from 'element-resize-detector'
import dictItem from '@/components/base/dictItem'
import table from '@/components/table/tableList'
import leftRightSplit from '@/components/tools/leftRightSplit.vue'
import * as filters from './filters' // global filters
import moment from 'moment'
Vue.prototype.$moment = moment
Vue.prototype.$erd = ElementResizeDetectorMaker()
/**
 * If you don't want to use mock-server
 * you want to use MockJs for mock api
 * you can execute: mockXHR()
 *
 * Currently MockJs will be used in the production environment,
 * please remove it before going online ! ! !
 */
Vue.component('treeDicItem', treeDicItem)
Vue.component('leftRightSplit', leftRightSplit)
Vue.component('dictItem', dictItem)
Vue.component('enumItem', enumItem)
Vue.component('w-table', table)
Vue.use(Element, {
  size: Cookies.get('size') || 'medium' // set element-ui default size
})
Vue.filter('imageUri', function(src) {
  if (src == null) {
    return null
  }
  const token = store.getters.token
  if (token) {
    return src + '?accreditId=' + token
  }
  return src
})
// register global utility filters
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})
String.prototype.isNull = function() {
  return this === null || this === '' || this === undefined
}

Array.prototype.Max = function(attr) {
  let maxVal = Number.MIN_VALUE
  this.forEach(c => {
    const val = c[attr]
    if (val != null && val > maxVal) {
      maxVal = val
    }
  })
  if (maxVal == Number.MIN_VALUE) {
    return null
  }
  return maxVal
}

Array.prototype.Min = function(attr) {
  let minVal = Number.MAX_VALUE
  this.forEach(c => {
    const val = c[attr]
    if (val != null && val < minVal) {
      minVal = val
    }
  })
  if (minVal == Number.MAX_VALUE) {
    return null
  }
  return minVal
}
Array.prototype.OrderBy = function(attr) {
  this.sort(function(a, b) {
    let one = a[attr]
    let two = b[attr]
    if (Number.isInteger(one) || Number.isInteger(two)) {
      if (one == null) {
        one = Number.MAX_VALUE
      }
      if (two == null) {
        two = Number.MAX_VALUE
      }
      if (one == two) {
        return 0
      }
      return one > two ? 1 : -1
    } else {
      var x = a.type.toLowerCase()
      var y = b.type.toLowerCase()
      if (x < y) { return -1 }
      if (x > y) { return 1 }
      return 0
    }
  })
}
Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
