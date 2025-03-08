import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/emp/' + name
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

export function queryEmp(query, paging) {
  return request({
    url: formatRequestUri('QueryEmp'),
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
export function getBasics(companyId, ids) {
  return request({
    url: formatRequestUri('GetBasics'),
    method: 'post',
    data: {
      Id: companyId,
      Value: ids
    }
  })
}
export function getBasic(empId, companyId) {
  return request({
    url: formatRequestUri('GetBasic'),
    method: 'get',
    params: {
      companyId,
      empId
    }
  })
}
export function add(data) {
  return request({
    url: formatRequestUri('Add'),
    method: 'post',
    data
  })
}
export function set(id, data) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      Id: id,
      Datum: data
    }
  })
}
export function get(id) {
  return request({
    url: formatRequestUri('Get'),
    method: 'get',
    params: {
      id
    }
  })
}
export function saveHead(empId, head, fileId) {
  return request({
    url: formatRequestUri('SaveHead'),
    method: 'post',
    data: {
      Id: empId,
      HeadUrl: head,
      FileId: fileId
    }
  })
}
export function checkRepeat(data) {
  return request({
    url: formatRequestUri('CheckIsRepeat'),
    method: 'post',
    data
  })
}
export function deleteEmp(id) {
  return request({
    url: formatRequestUri('Delete'),
    method: 'get',
    params: {
      id
    }
  })
}

export function setStatus(empId, status) {
  return request({
    url: formatRequestUri('SetStatus'),
    method: 'post',
    data: {
      Id: empId,
      Value: status
    }
  })
}
export function setEmpPost(empId, post) {
  return request({
    url: formatRequestUri('SetEmpPost'),
    method: 'post',
    data: {
      Id: empId,
      Value: post
    }
  })
}
export function setEmpEntry(empId, data) {
  return request({
    url: formatRequestUri('SetEmpEntry'),
    method: 'post',
    data: {
      Id: empId,
      Value: data
    }
  })
}
