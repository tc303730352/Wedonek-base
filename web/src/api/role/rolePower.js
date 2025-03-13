import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/RolePower/' + name
}
export function Set(roleId, powerId, opId) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      RoleId: roleId,
      PowerId: powerId,
      OperateId: opId
    }
  })
}
