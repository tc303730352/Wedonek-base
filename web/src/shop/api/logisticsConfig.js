import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/logistics/config/' + name
}

export function Gets(data) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'post',
    data
  })
}
export function SetIsEnable(id, enable) {
  return request({
    url: formatRequestUri('SetIsEnable'),
    method: 'post',
    data: {
      Id: id,
      Value: enable
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
      Id: id
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
export function Calculate(data) {
  return request({
    url: formatRequestUri('Calculate'),
    method: 'post',
    data: data
  })
}
