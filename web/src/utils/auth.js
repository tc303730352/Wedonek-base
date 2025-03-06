const tokenKey = 'AccreditId'

export function getToken() {
  return localStorage.getItem(tokenKey)
}

export function isLogin() {
  return localStorage.getItem(tokenKey) != null
}
export function setToken(token) {
  localStorage.setItem(tokenKey, token)
}

export function removeToken() {
  localStorage.removeItem(tokenKey)
}
