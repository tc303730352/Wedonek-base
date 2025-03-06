import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/spec/template/' + name
}
export function Gets(templateId) {
    return request({
        url: formatRequestUri('Gets'),
        method: 'get',
        params: {
            templateId
        }
    })
}
export function GetItems(id, query) {
    return request({
        url: formatRequestUri('GetItems'),
        method: 'post',
        data: {
            Id: id,
            Value: query
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