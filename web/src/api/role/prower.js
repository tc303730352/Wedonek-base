import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/Prower/' + name
}
export function GetTrees(param) {
  return request({
    url: formatRequestUri('GetTrees'),
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

export function GetProwerTrees(query) {
  return request({
    url: formatRequestUri('GetProwerTrees'),
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
