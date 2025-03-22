import { login, loginOut, GetLoginDatum, CheckIsLogin } from '@/api/login/empLogin'
import { setToken, removeToken, isLogin, getToken } from '@/utils/auth'
import * as cache from '@/utils/localCache'
import settings from '@/settings'
import { resetRouter, formatRoutes } from '@/router'
import md5 from 'js-md5'
const state = {
  token: null,
  subSystem: null,
  home: {},
  loadRoute: {},
  curSysId: null,
  routes: null,
  company: null,
  curComId: null,
  datum: {
    empId: null,
    name: null,
    head: null,
    unitId: null,
    deptId: null,
    companyId: null
  },
  isLoadRoute: false
}

const mutations = {
  SET_TOKEN: (state, tokenId) => {
    state.token = tokenId
  },
  SET_CurSysId: (state, sysId) => {
    state.curSysId = sysId
    cache.setCurSysId(sysId)
  },
  SET_CurComId: (state, comId) => {
    state.curComId = comId
    cache.setCurComId(comId)
  },
  SET_IsLoadRoute(state, isLoad) {
    state.isLoadRoute = isLoad
  },
  Clear_State: (state) => {
    state.isLoadRoute = false
    state.token = null
  },
  SET_User: (state, data) => {
    state.subSystem = data.SubSystem
    state.routes = {}
    data.SubSystem.forEach(a => {
      state.routes[a.Id] = formatRoutes(data.Power[a.Id], 0)
      state.home[a.Id] = a.Home
      state.loadRoute[a.Id] = a.LoginRouteName
    })
    state.company = data.Company
    state.datum = {
      empId: data.Datum.EmpId,
      name: data.Datum.EmpName,
      head: data.Datum.HeadUri ?? '/image/defhead.png'
    }
  }
}
async function checkLoginState() {
  try {
    await CheckIsLogin()
    return true
  } catch {
    return false
  }
}
async function loadState(commit) {
  const tokenId = getToken()
  commit('SET_TOKEN', tokenId)
  if (cache.isCache(tokenId)) {
    const cahce = cache.getCache(tokenId)
    commit('SET_User', cahce)
    const sysId = cache.getCurSysId(tokenId)
    if (sysId == null) {
      commit('SET_CurSysId', cahce.CurSubSysId)
    } else {
      commit('SET_CurSysId', sysId)
    }
    const comId = cache.getCurComId(tokenId)
    if (comId == null) {
      commit('SET_CurComId', cahce.Datum.CompanyId)
    } else {
      commit('SET_CurComId', comId)
    }
    return state
  }
  const res = await GetLoginDatum()
  commit('SET_User', res)
  commit('SET_CurSysId', res.CurSubSysId)
  commit('SET_CurComId', res.Datum.CompanyId)
  cache.setCache(tokenId, res)
  return state
}
const actions = {
  // user login
  login({ commit }, data) {
    return new Promise((resolve, reject) => {
      data.Password = md5(data.Password + settings.pwdSign)
      login(data).then(res => {
        cache.clearCache()
        commit('SET_TOKEN', res.AccreditId)
        setToken(res.AccreditId)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  switchSubSys({ commit }, sysId) {
    commit('SET_CurSysId', sysId)
  },
  // user logout
  logout({ dispatch, commit }) {
    return new Promise((resolve, reject) => {
      loginOut().then(() => {
        removeToken()
        resetRouter()
        cache.clearCache()
        commit('Clear_State')
        dispatch('tagsView/delAllViews', null, { root: true })
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },
  async init({ commit }) {
    if (!isLogin()) {
      return false
    } else if (await checkLoginState() === false) {
      return false
    }
    const state = await loadState(commit)
    if (state.curSysId == null) {
      throw Error('无菜单，请联系管理员!')
    }
    return true
  },
  async loadRoute({ commit }, router) {
    if (!isLogin()) {
      return false
    }
    commit('SET_IsLoadRoute', true)
    const state = await loadState(commit)
    if (state.curSysId == null) {
      return false
    }
    state.subSystem.forEach(c => {
      router.addRoutes(state.routes[c.Id])
    })
    return true
  },
  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      removeToken()
      resetRouter()
      cache.clearCache()
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
