import request from '@/utils/request'

function formatRequestUri(name) {
  return 'hr/LoginLog/' + name
}
export function query(query, paging) {
  return request({
    url: formatRequestUri('Query'),
    method: 'post',
    data: {
      Query: query,
      Index: paging.Index,
      Size: paging.Size,
      SortName: paging.SortName,
      IsDesc: paging.IsDesc
    }
  })
}
