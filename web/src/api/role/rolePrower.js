import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/RolePrower/' + name
}
export function Set(roleId, prowerId, opId) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      RoleId: roleId,
      ProwerId: prowerId,
      OperateId: opId
    }
  })
}
