import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/logistics/template/' + name
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
export function SetIsEnable(id, enable) {
    return request({
        url: formatRequestUri('SetIsEnable'),
        method: 'post',
        data: {
            Id: id,
            Value: enable
        }
    })
}
export function SetIsDefault(id, isDef) {
    return request({
        url: formatRequestUri('SetIsDefault'),
        method: 'post',
        data: {
            Id: id,
            Value: isDef
        }
    })
}
export function Get(id) {
    return request({
        url: formatRequestUri('Get'),
        method: 'get',
        params: {
            Id: id
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
export function Delete(id) {
    return request({
        url: formatRequestUri('Delete'),
        method: 'get',
        params: {
            id
        }
    })
}
export function GetItems() {
    return request({
        url: formatRequestUri('GetItems'),
        method: 'get'
    })
}