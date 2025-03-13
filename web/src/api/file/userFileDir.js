import request from '@/utils/request'
function formatRequestUri(name) {
  return 'file/userfiledir/' + name
}
export function GetDirs() {
  return request({
    url: formatRequestUri('GetDirs'),
    method: 'get'
  })
}
