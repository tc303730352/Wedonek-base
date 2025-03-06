import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/stock/' + name
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
export function Add(data) {
    return request({
        url: formatRequestUri('Add'),
        method: 'post',
        data
    })
}
export function GetStockNum(skuId) {
    return request({
        url: formatRequestUri('GetStockNum'),
        method: 'get',
        params: {
            skuId
        }
    })
}