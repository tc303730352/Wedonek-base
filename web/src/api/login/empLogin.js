import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/emplogin/' + name
}
export function login(data) {
  return request({
    url: formatRequestUri('EmpPwdLogin'),
    method: 'post',
    data
  })
}
export function loginOut() {
  return request({
    url: formatRequestUri('LoginOut'),
    method: 'get'
  })
}

export function GetLoginDatum() {
  return request({
    url: formatRequestUri('GetLoginDatum'),
    method: 'get'
  })
}
export function CheckIsLogin() {
  return request({
    url: formatRequestUri('CheckIsLogin'),
    method: 'get'
  })
}

export function Switch(companyId) {
  return request({
    url: formatRequestUri('Switch'),
    method: 'get',
    params: {
      companyId
    }
  })
}
