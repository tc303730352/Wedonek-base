const cacheKey = 'user_datum'
const existKey = 'user_Key'
const chioseSysId = 'chiose_SysId'
const chioseComId = 'chiose_comId'

export function clearCache() {
  localStorage.removeItem(existKey)
  localStorage.removeItem(cacheKey)
  localStorage.removeItem(chioseSysId)
  localStorage.removeItem(chioseComId)
}
export function isCache(accreditId) {
  return localStorage.getItem(existKey) === accreditId
}
export function setCache(accreditId, data) {
  localStorage.setItem(cacheKey, JSON.stringify(data))
  localStorage.setItem(existKey, accreditId)
}
export function getCache(accreditId) {
  if (localStorage.getItem(existKey) === accreditId) {
    const str = localStorage.getItem(cacheKey)
    return JSON.parse(str)
  }
  return null
}
export function getCurSysId(accreditId) {
  if (localStorage.getItem(existKey) === accreditId) {
    return localStorage.getItem(chioseSysId)
  }
  return null
}

export function setCurSysId(curSysId) {
  localStorage.setItem(chioseSysId, curSysId)
}

export function getCurComId(accreditId) {
  if (localStorage.getItem(existKey) === accreditId) {
    return localStorage.getItem(chioseComId)
  }
  return null
}

export function setCurComId(comId) {
  localStorage.setItem(chioseComId, comId)
}
