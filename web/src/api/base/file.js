import request from '@/utils/request'
import { apiUri } from '@/config/config'
function formatRequestUri(name) {
  return 'file/' + name
}
export function getUpConfig(key, bizpk, tag) {
  return request({
    url: formatRequestUri('config'),
    method: 'post',
    data: {
      DirKey: key,
      Tag: tag,
      LinkBizPk: bizpk
    }
  })
}

export function getfileUpUri(config) {
  return apiUri['file'] + formatRequestUri('up/' + config)
}

export function deleteFile(fileId) {
  return request({
    url: formatRequestUri('delete'),
    method: 'get',
    params: {
      fileId
    }
  })
}

export function saveSort(sort) {
  return request({
    url: formatRequestUri('sort'),
    method: 'post',
    data: {
      Sort: sort
    }
  })
}

export function preUpFile(data, dirKey) {
  return request({
    url: formatRequestUri('preup/' + dirKey),
    method: 'post',
    data
  })
}

export function blockUpFile(data) {
  return request({
    url: formatRequestUri('block/up'),
    method: 'post',
    data
  })
}

export function getUpState(taskId) {
  return request({
    url: formatRequestUri('block/up/state'),
    method: 'get',
    params: {
      taskId
    }
  })
}
