import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/OperatePrower/' + name
}
export function GetEnables(prowerId, roleId) {
  return request({
    url: formatRequestUri('GetEnables'),
    method: 'get',
    params: {
      prowerId,
      roleId
    }
  })
}

export function Gets(prowerId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      prowerId
    }
  })
}
