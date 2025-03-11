import router from './router'
import store from './store'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import getPageTitle from '@/utils/get-page-title'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['/login', '/auth-redirect'] // no redirect whitelist

router.beforeEach(async(to, from, next) => {
  // start progress bar
  NProgress.start()

  // set page title
  let title = to.meta.title
  if (to.params && to.params.routeTitle) {
    title = to.params.routeTitle
  }
  document.title = getPageTitle(title)
  if (to.path === '/login') {
    next()
  } else if (store.getters.isLoadRoute) {
    next()
  } else {
    const isLogn = await store.dispatch('user/loadRoute', router)
    if (isLogn) {
      next({ ...to, replace: true })
    } else {
      if (to.path === '/loading') {
        next('/login')
      } else {
        next(`/login?redirect=${to.path}`)
      }
      NProgress.done()
    }
  }
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
