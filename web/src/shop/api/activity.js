import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/activity/' + name
}
export function Query(query, paging) {
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
export function Get(id) {
  return request({
    url: formatRequestUri('Get'),
    method: 'get',
    params: {
      id
    }
  })
}

export function Add(data) {
  return request({
    url: formatRequestUri('Add'),
    method: 'post',
    data
  })
}
export function Set(id, data) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      Id: id,
      Value: data
    }
  })
}

export function GetDetailed(id) {
  return request({
    url: formatRequestUri('GetDetailed'),
    method: 'get',
    params: {
      id
    }
  })
}
export function SubmitAudit(id) {
  return request({
    url: formatRequestUri('SubmitAudit'),
    method: 'get',
    params: {
      id
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

export function AuditPass(id) {
  return request({
    url: formatRequestUri('AuditPass'),
    method: 'get',
    params: {
      id
    }
  })
}
export function AuditNoPass(id, show) {
  return request({
    url: formatRequestUri('AuditNoPass'),
    method: 'post',
    data: {
      Id: id,
      Value: show
    }
  })
}

export function OffShelf(id) {
  return request({
    url: formatRequestUri('OffShelf'),
    method: 'get',
    params: {
      id
    }
  })
}

export function Gets(ids) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'post',
    data: ids
  })
}
