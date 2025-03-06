import request from '@/utils/request'

function formatRequestUri(name) {
  return 'shop/coupon/goods/' + name
}
export function Gets(couponId) {
  return request({
    url: formatRequestUri('Gets'),
    method: 'get',
    params: {
      couponId
    }
  })
}
export function Set(id, datas) {
  return request({
    url: formatRequestUri('Set'),
    method: 'post',
    data: {
      Id: id,
      Value: datas
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

export function BatchDelete(couponId, ids) {
  return request({
    url: formatRequestUri('BatchDelete'),
    method: 'post',
    data: {
      Id: couponId,
      Value: ids
    }
  })
}
