import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/goods/' + name
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
export function GetDetailed(id) {
  return request({
    url: formatRequestUri('GetDetailed'),
    method: 'get',
    params: {
      id
    }
  })
}
export function GetStocks(goodsId) {
  return request({
    url: formatRequestUri('GetStocks'),
    method: 'get',
    params: {
      goodsId
    }
  })
}
export function Public(id, stock) {
  return request({
    url: formatRequestUri('Public'),
    method: 'post',
    data: {
      Id: id,
      Value: stock
    }
  })
}

export function Offshelf(id) {
  return request({
    url: formatRequestUri('Offshelf'),
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

export function GetTops(data) {
  return request({
    url: formatRequestUri('GetTops'),
    method: 'post',
    data
  })
}
