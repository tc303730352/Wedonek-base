import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/company/' + name
}
export function GetTree(data) {
  return request({
    url: formatRequestUri('GetTree'),
    method: 'post',
    data
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
      Dept: data
    }
  })
}
