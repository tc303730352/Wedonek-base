import request from '@/utils/request'

function formatRequestUri(name) {
  return 'form/column/' + name
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

export function SetSort(data) {
  return request({
    url: formatRequestUri('SetSort'),
    method: 'post',
    data
  })
}
export function SetSpan(id, span) {
  return request({
    url: formatRequestUri('SetSpan'),
    method: 'post',
    data: {
      Id: id,
      Value: span
    }
  })
}
export function SaveSpan(data) {
  return request({
    url: formatRequestUri('SaveSpan'),
    method: 'post',
    data
  })
}
