import request from '@/utils/request'

function formatRequestUri(name, sysHead) {
  return sysHead + '/enum/' + name
}
export function GetItems(key, sysHead) {
  return request({
    url: formatRequestUri('GetItems', sysHead),
    method: 'get',
    params: {
      name: key
    }
  })
}
export function Gets(keys, sysHead) {
  return request({
    url: formatRequestUri('Gets', sysHead),
    method: 'post',
    data: keys
  })
}