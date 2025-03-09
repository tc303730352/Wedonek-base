import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/Power/' + name
}
export function GetTrees(param) {
  return request({
    url: formatRequestUri('GetTrees'),
    method: 'post',
    data: param
  })
}

export function GetTree(param) {
  return request({
    url: formatRequestUri('GetTree'),
    method: 'post',
    data: param
  })
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

export function GetPowerTrees(query) {
  return request({
    url: formatRequestUri('GetPowerTrees'),
    method: 'post',
    data: query
  })
}

export function SetSort(id, sort) {
  return request({
    url: formatRequestUri('SetSort'),
    method: 'post',
    data: {
      Id: id,
      Value: sort
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

export function Get(id) {
  return request({
    url: formatRequestUri('Get'),
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
