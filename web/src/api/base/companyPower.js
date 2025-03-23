import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/companyPower/' + name
}
export function Gets(companyId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      companyId
    }
  })
}

export function Sync(id, data) {
  return request({
    url: formatRequestUri('Sync'),
    method: 'post',
    data: {
      Id: id,
      Value: data
    }
  })
}
