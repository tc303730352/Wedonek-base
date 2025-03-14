import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/dept/' + name
}
export function gets(data) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'post',
    data
  })
}
export function enable(ids) {
  return request({
    url: formatRequestUri('Enable'),
    method: 'post',
    data: ids
  })
}
export function stop(id) {
  return request({
    url: formatRequestUri('Stop'),
    method: 'get',
    params: {
      deptId: id
    }
  })
}
export function get(id) {
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

export function add(data) {
  return request({
    url: formatRequestUri('Add'),
    method: 'post',
    data
  })
}
export function set(id, data) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      Id: id,
      Dept: data
    }
  })
}

export function setLeader(id, leaderId) {
  return request({
    url: formatRequestUri('SetLeader'),
    method: 'post',
    data: {
      Id: id,
      Value: leaderId
    }
  })
}

export function getTallyTrees(data) {
  return request({
    url: formatRequestUri('GetTallyTrees'),
    method: 'post',
    data
  })
}
