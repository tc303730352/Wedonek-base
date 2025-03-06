import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/page/module/goods/' + name
}
export function Gets(tModuleId, tag) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      tModuleId,
      tag
    }
  })
}
export function Sync(data) {
  return request({
    url: formatRequestUri('Sync'),
    method: 'post',
    data
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

export function BatchDelete(ids) {
  return request({
    url: formatRequestUri('BatchDelete'),
    method: 'post',
    data: ids
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

export function GetTops(data) {
  return request({
    url: formatRequestUri('GetTops'),
    method: 'post',
    data
  })
}
