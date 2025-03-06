import request from '@/utils/request'

function formatRequestUri(name) {
    return '/shop/goods/spec/' + name
}

export function GetSpecGroup(goodsId, categoryId) {
    return request({
        url: formatRequestUri('GetSpecGroup'),
        method: 'get',
        params: {
            goodsId,
            categoryId
        }
    })
}
export function Sync(goodsId, categoryId) {
    return request({
        url: formatRequestUri('Sync'),
        method: 'get',
        params: {
            goodsId,
            categoryId
        }
    })
}
export function SetSpec(id, data) {
    return request({
        url: formatRequestUri('SetSpec'),
        method: 'post',
        data: {
            Id: id,
            Value: data
        }
    })
}
export function AddSpec(data) {
    return request({
        url: formatRequestUri('AddSpec'),
        method: 'post',
        data
    })
}
export function DeleteSpec(id) {
    return request({
        url: formatRequestUri('DeleteSpec'),
        method: 'get',
        params: {
            id
        }
    })
}
export function SetSpecSort(id, sort) {
    return request({
        url: formatRequestUri('SetSpecSort'),
        method: 'post',
        data: {
            Id: id,
            Value: sort
        }
    })
}
export function AddGroup(data) {
    return request({
        url: formatRequestUri('AddGroup'),
        method: 'post',
        data
    })
}
export function SetGroup(id, data) {
    return request({
        url: formatRequestUri('SetGroup'),
        method: 'post',
        data: {
            Id: id,
            Value: data
        }
    })
}
export function DeleteGroup(id) {
    return request({
        url: formatRequestUri('DeleteGroup'),
        method: 'get',
        params: {
            id
        }
    })
}
export function DeleteSkuSpec(goodsId,specId) {
    return request({
        url: formatRequestUri('DeleteSkuSpec'),
        method: 'post',
        data: {
            Id:goodsId,
            Value: specId
        }
    })
}
export function SetGroupSort(id, sort) {
    return request({
        url: formatRequestUri('SetGroupSort'),
        method: 'post',
        data: {
            Id: id,
            Value: sort
        }
    })
}


export function SyncSku(goodsId) {
    return request({
        url: formatRequestUri('SyncSku'),
        method: 'get',
        params: {
            goodsId
        }
    })
}
export function GetSpecSku(goodsId) {
    return request({
        url: formatRequestUri('GetSpecSku'),
        method: 'get',
        params: {
            goodsId
        }
    })
}
export function SetSkuState(data) {
    return request({
        url: formatRequestUri('SetSkuState'),
        method: 'post',
        data
    })
}