import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'
import BasicLayout from '@/layout/basicLayout'
const defLoadRoute = ['customForm']
const defRoute = []
export const getLayout = (name) => {
  if (name === 'BasicLayout') {
    return BasicLayout
  }
  return Layout
}
export const constantRoutes = [
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: BasicLayout,
    redirect: '/loading',
    children: [
      {
        path: '/loading',
        component: () => import('@/views/loading'),
        name: 'loading',
        hidden: true,
        meta: { title: '加载页', affix: false }
      },
      {
        path: '/base/index',
        component: () => import('@/views/index'),
        name: 'baseIndex',
        hidden: true,
        meta: { title: '基础系统登陆页', affix: false }
      }
    ]
  },
  {
    path: '/emp',
    component: Layout,
    redirect: '/emp',
    children: [
      {
        path: '/emp/add',
        component: () => import('@/views/emp/editEmp'),
        name: 'addEmp',
        hidden: true,
        meta: { title: '添加员工', affix: false }
      },
      {
        path: '/emp/edit/:id',
        component: () => import('@/views/emp/editEmp'),
        name: 'editEmp',
        hidden: true,
        meta: { title: '编辑员工资料', affix: false }
      },
      {
        path: '/emp/info/:id',
        component: () => import('@/views/emp/viewEmp'),
        name: 'showEmp',
        hidden: true,
        meta: { title: '查看员工资料', affix: false }
      }
    ]
  },
  {
    path: '/dept',
    component: Layout,
    redirect: '/dept',
    children: [
      {
        path: '/dept/change',
        component: () => import('@/views/dept/deptChange'),
        name: 'deptChange',
        hidden: true,
        meta: { title: '单位部门变动', affix: false }
      }
    ]
  }
]
/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  { path: '*', redirect: '/404', hidden: true }
]

const createRouter = (routers) => {
  if (defRoute.length === 0) {
    initDefRoute()
  }
  if (routers != null && routers.length !== 0) {
    const list = [].concat(defRoute)
    mergeRoute(list, routers)
    return new Router({
      mode: 'history',
      scrollBehavior: () => ({ y: 0 }),
      routes: list
    })
  } else {
    return new Router({
      mode: 'history',
      scrollBehavior: () => ({ y: 0 }),
      routes: defRoute
    })
  }
}

const router = createRouter(null)

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}
function initDefRoute() {
  if (defLoadRoute.length === 0) {
    defRoute.push(...constantRoutes)
  } else {
    const routes = [].concat(constantRoutes)
    defLoadRoute.forEach(c => {
      const router = require(`@/${c}/router`)
      if (router != null) {
        mergeRoute(routes, router.routes)
      }
    })
    defRoute.push(...routes)
  }
}
function mergeRoute(source, to) {
  to.forEach(c => {
    const route = source.find(a => a.path === c.path)
    if (route == null) {
      source.push(c)
    } else if (c.Children != null && c.Children.length > 0) {
      if (route.Children == null) {
        route.Children = []
      }
      mergeRoute(route.Children, c.Children)
    }
  })
}
export default router

export const formatDir = (dir, level) => {
  const route = {
    id: dir.Id,
    path: dir.RoutePath,
    component: getLayout(dir.Layout),
    hidden: false,
    redirect: dir.Children[0].RoutePath,
    meta: {
      title: dir.Name,
      icon: dir.Icon,
      permission: dir.Id,
      description: dir.Description
    }
  }
  if (dir.PageParam != null && dir.PageParam.hidden == true) {
    route.hidden = dir.PageParam.hidden
  }
  const lev = level + 1
  route.children = dir.Children.map(c => formatPage(c, lev))
  return route
}
export const formatRoutes = (routes, level) => {
  return routes.map(c => {
    if (c.PowerType === 1) {
      return formatDir(c, level)
    } else if (c.PowerType === 0 && level === 0) {
      return formatDir({
        Id: 'dir_' + c.Id,
        Children: [c],
        Layout: 'Layout',
        RoutePath: c.RoutePath,
        PageParam: { hidden: true }
      }, level)
    } else {
      return formatPage(c, level)
    }
  })
}
export const formatPage = (page, level) => {
  const route = {
    id: page.Id,
    path: page.RoutePath,
    hidden: false,
    name: page.RouteName,
    params: null,
    meta: {
      title: page.Name,
      icon: page.Icon,
      permission: page.Id,
      description: page.Description,
      affix: false,
      keepAlive: false
    }
  }
  if (page.PageParam != null) {
    route.params = {}
    for (const i in page.PageParam) {
      if (i === 'keepAlive') {
        route.meta.keepAlive = page.PageParam.keepAlive
      } else if (i === 'affix') {
        route.meta.affix = page.PageParam.affix
      } else if (i === 'hidden') {
        route.hidden = page.PageParam.hidden
      } else {
        route.params[i] = page.PageParam[i]
      }
    }
  }
  const path = page.PagePath
  console.log(path)
  route.component = resolve => require(['@/' + path], resolve)
  if (page.Children != null && page.Children.length > 0) {
    const lev = level + 1
    route.children = formatRoutes(page.Children, lev)
  }
  return route
}
