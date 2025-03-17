import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/LoginUser/' + name
}
export function open(comId, empId) {
  return request({
    url: formatRequestUri('OpenAccount'),
    method: 'post',
    data: {
      Id: comId,
      Value: empId
    }
  })
}

export function query(query, paging) {
  return request({
    url: formatRequestUri('Query'),
    method: 'post',
    data: {
      Query: query,
      Index: paging.Index,
      Size: paging.Size,
      SortName: paging.SortName,
      IsDesc: paging.IsDesc
    }
  })
}

export function deleteUser(empId) {
  return request({
    url: formatRequestUri('DeleteAccount'),
    method: 'get',
    params: {
      empId
    }
  })
}
export function resetPwd(empId) {
  return request({
    url: formatRequestUri('ResetPwd'),
    method: 'get',
    params: {
      empId
    }
  })
}
export function setPwd(old, newPwd) {
  return request({
    url: formatRequestUri('UpdatePwd'),
    method: 'post',
    data: {
      Pwd: old,
      NewPwd: newPwd
    }
  })
}
