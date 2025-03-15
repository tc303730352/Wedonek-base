import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/DicItem/' + name
}

export function gets(dicId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      dicId
    }
  })
}
export function GetItemName(dicId) {
  return request({
    url: formatRequestUri('GetItemName'),
    method: 'get',
    params: {
      dicId
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
export function DeleteItem(id) {
  return request({
    url: formatRequestUri('Delete'),
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
export function Enable(id) {
  return request({
    url: formatRequestUri('Enable'),
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
export function Move(id, toId) {
  return request({
    url: formatRequestUri('Move'),
    method: 'get',
    params: {
      id,
      toId
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
      ItemSet: data
    }
  })
}

export function GetDicTextList(dicId, values) {
  return request({
    url: formatRequestUri('GetDicTextList'),
    method: 'post',
    data: {
      Id: dicId,
      Value: values
    }
  })
}
