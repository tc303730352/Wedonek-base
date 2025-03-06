import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/Role/' + name
}
export function get(id) {
    return request({
        url: formatRequestUri('GetDetailed'),
        method: 'get',
        params: {
            id
        }
    })
}
export function getSelect() {
    return request({
        url: formatRequestUri('GetSelect'),
        method: 'get'
    })
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
export function setIsEnable(id, isEnable) {
    return request({
        url: formatRequestUri('SetIsEnable'),
        method: 'post',
        data: {
            Id: id,
            Value: isEnable
        }
    })
}
export function setIsDefRole(id) {
    return request({
        url: formatRequestUri('SetIsDefRole'),
        method: 'get',
        params: {
            id
        }
    })
}
export function deleteRole(id) {
    return request({
        url: formatRequestUri('Delete'),
        method: 'get',
        params: {
            id
        }
    })
}

export function add(data) {
    return request({
        url: formatRequestUri('Add'),
        method: 'post',
        data
    })
}
export function set(id, data) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data: {
            Id: id,
            Datum: data
        }
    })
}
export function setIsAdmin(id, isAdmin) {
    return request({
        url: formatRequestUri('setIsAdmin'),
        method: 'post',
        data: {
            Id: id,
            Value: isAdmin
        }
    })
}