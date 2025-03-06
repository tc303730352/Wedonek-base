import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/page/template/module/' + name
}

export function Add(data) {
  return request({
    url: formatRequestUri('Add'),
    method: 'post',
    data
  })
}

export function Gets(templateId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      templateId
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

export function SetSort(data) {
  return request({
    url: formatRequestUri('SetSort'),
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
