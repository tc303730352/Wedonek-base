import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/OperatePower/' + name
}
export function GetEnables(powerId, roleId) {
  return request({
    url: formatRequestUri('GetEnables'),
    method: 'get',
    params: {
      powerId,
      roleId
    }
  })
}

export function Gets(powerId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      powerId
    }
  })
}
