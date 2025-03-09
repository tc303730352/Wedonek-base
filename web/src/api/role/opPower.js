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

export function SetIsEnable(id, isEnable) {
  return request({
    url: formatRequestUri('SetIsEnable'),
    method: 'post',
    data: {
      Id: id,
      Value: isEnable
    }
  })
}

export function Delete(id) {
  return request({
    url: formatRequestUri('Delete'),
    method: 'get',
    params: {
      id
    }
  })
}
