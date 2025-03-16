import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/unit/' + name
}
export function getDeptTree(data) {
  return request({
    url: formatRequestUri('GetDeptTree'),
    method: 'post',
    data
  })
}

export function GetNameList(ids) {
  return request({
    url: formatRequestUri('GetNameList'),
    method: 'post',
    data: ids
  })
}

