import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/onlineuser/' + name
}
export function query(paging) {
  return request({
    url: formatRequestUri('Query'),
    method: 'post',
    data: {
      Index: paging.Index,
      Size: paging.Size,
      SortName: paging.SortName,
      IsDesc: paging.IsDesc
    }
  })
}
export function KickOut(accreditId) {
  return request({
    url: formatRequestUri('KickOut'),
    method: 'get',
    params: {
      accreditId
    }
  })
}
