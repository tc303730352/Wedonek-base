import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/TreeDic/' + name
}


export function getTrees(dicId) {
    return request({
        url: formatRequestUri('GetDicTree'),
        method: 'get',
        params: {
            dicId
        }
    })
}
export function getTree(query) {
    return request({
        url: formatRequestUri('GetTree'),
        method: 'post',
        data: query
    })
}

export function DeleteItem(id) {
    return request({
        url: formatRequestUri('Delete'),
        method: 'get',
        params: {
            id
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
            Datum: data
        }
    })
}
export function Stop(id) {
    return request({
        url: formatRequestUri('Stop'),
        method: 'get',
        params: {
            id
        }
    })
}
export function Enable(id) {
    return request({
        url: formatRequestUri('Enable'),
        method: 'get',
        params: {
            id
        }
    })
}
export function Get(id) {
    return request({
        url: formatRequestUri('Get'),
        method: 'get',
        params: {
            id
        }
    })
}
export function Move(id, toId) {
    return request({
        url: formatRequestUri('Move'),
        method: 'post',
        data: {
            FromId: id,
            ToId: toId
        }
    })
}