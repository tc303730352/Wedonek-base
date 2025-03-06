import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/page/module/' + name
}

export function Gets(range) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      range
    }
  })
}
