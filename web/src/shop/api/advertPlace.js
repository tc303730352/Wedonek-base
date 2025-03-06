import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/advert/place/' + name
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
export function Gets(status) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      status
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
export function Enable(id) {
  return request({
    url: formatRequestUri('Enable'),
    method: 'get',
    params: {
      id
    }
  })
}
export function Stop(id) {
  return request({
    url: formatRequestUri('Stop'),
    method: 'get',
    params: {
      id
    }
  })
}
export function GetConfig(id) {
  return request({
    url: formatRequestUri('GetConfig'),
    method: 'get',
    params: {
      id
    }
  })
}

export function GetItems(query) {
  return request({
    url: formatRequestUri('GetItems'),
    method: 'post',
    data: query
  })
}

export function GetExampleImg(placeType) {
  return request({
    url: formatRequestUri('GetExampleImg'),
    method: 'post',
    data: placeType
  })
}
