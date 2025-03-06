import request from '@/utils/request'

export function LoadArea() {
  return request({
    url: '/file/json/AreaMini.json',
    method: 'get'
  })
}
