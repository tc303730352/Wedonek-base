import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/user/' + name
}

export function Get() {
    return request({
        url: formatRequestUri('Get'),
        method: 'get'
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

export function Enable(id) {
    return request({
        url: formatRequestUri('Enable'),
        method: 'get',
        params: {
            userId: id
        }
    })
}
export function Disable(id) {
    return request({
        url: formatRequestUri('Disable'),
        method: 'get',
        params: {
            userId: id
        }
    })
}