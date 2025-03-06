import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/attr/template/' + name
}
export function GetTrees(id, isEnable) {
    return request({
        url: formatRequestUri('GetTrees'),
        method: 'post',
        data: {
            Id: id,
            Value: isEnable
        }
    })
}

export function SetSort(id, sort) {
    return request({
        url: formatRequestUri('SetSort'),
        method: 'post',
        data: {
            Id: id,
            Value: sort
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
export function Stop(id) {
    return request({
        url: formatRequestUri('Stop'),
        method: 'get',
        params: {
            id
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
export function Get(id) {
    return request({
        url: formatRequestUri('Get'),
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
            Value: data
        }
    })
}

export function GetAttrTrees(categoryId) {
    return request({
        url: formatRequestUri('GetAttrTrees'),
        method: 'get',
        params: {
            categoryId
        }
    })
}