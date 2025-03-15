const getters = {
  sidebar: state => state.app.sidebar,
  size: state => state.app.size,
  device: state => state.app.device,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  token: state => state.user.token,
  isLoadRoute: state => state.user.isLoadRoute,
  home: state => state.user.home[state.user.curSysId],
  loadRoute: state => state.user.loadRoute[state.user.curSysId],
  company: state => state.user.company,
  curComId: state => state.user.curComId,
  user: state => state.user.datum,
  routes: state => state.user.routes[state.user.curSysId],
  curSysId: state => state.user.curSysId,
  errorLogs: state => state.errorLog.logs,
  subSystem: state => state.user.subSystem,
  mainHeight: state => state.app.mainHeight
}
export default getters
