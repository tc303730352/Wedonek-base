import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/goods/sku/' + name
}

export function Get(goodsId, specKey) {
    return request({
        url: formatRequestUri('Get'),
        method: 'post',
        data: {
            Id: goodsId,
            Value: specKey
        }
    })
}
export function Add(data) {
    return request({
        url: formatRequestUri('Add'),
        method: 'post',
        data
    })
}
export function Set(id, data) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data: {
            Id: id,
            Value: data
        }
    })
}

export function Query(query, paging) {
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
export function Gets(ids) {
    return request({
        url: formatRequestUri('Gets'),
        method: 'post',
        data: ids
    })
}