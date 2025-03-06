import axios from 'axios'
import { Message } from 'element-ui'
import store from '@/store'
import { apiUri } from '@/config/config'
// create an axios instance
const service = axios.create({
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 5000 // request timeout
})
const accreditError=[4714653491135125,509777198850181]

// request interceptor
service.interceptors.request.use(
  config => {
    var uri = config.url
    if (uri.indexOf('http:') ==-1 && uri.indexOf('https:') == -1) {
      config.url = getRequestUri(uri)
    }
    config.headers['content-Type'] = 'application/json'
    if (store.getters.token) {
      config.headers['AccreditId'] = store.getters.token
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)
export function getRequestUri(path) {
  const index = path.indexOf('/')
  if (index === 0) {
    path = path.substring(1)
    return getRequestUri(path)
  } else if (index === -1) {
    return null
  } else {
    const pres = path.substring(0, index)
    const uri = apiUri[pres]
    if (uri) {
      return uri + path
    }
    return apiUri['def'] + path
  }
}
// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data
    if (res.errorcode !== 0) {
      Message({
        message: res.errmsg,
        type: 'error',
        duration: 5 * 1000
      })
      if (accreditError.includes(res.errorcode)) {
        store.dispatch('user/resetToken').then(() => {
          location.reload()
        })
      }
      return Promise.reject(res)
    } else {
      return res.data
    }
  },
  error => {
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    console.log(error);
    return Promise.reject(error)
  }
)

export default service
