import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/RolePrower/' + name
}
export function Set(roleId, prowerId, opId) {
  return request({
    url: formatRequestUri('GetEnables'),
    method: 'get',
    params: {
      prowerId,
      roleId
    }
  })
}
