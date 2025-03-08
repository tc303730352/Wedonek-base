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
