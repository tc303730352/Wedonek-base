import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/front/category/' + name
}

export function GetTree(param) {
    return request({
        url: formatRequestUri('GetTree'),
        method: 'post',
        data: param
    })
}
export function GetFullTree() {
    return request({
        url: formatRequestUri('GetFullTree'),
        method: 'get'
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
export function Disable(id) {
    return request({
        url: formatRequestUri('Disable'),
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
export function SetBind(id, categoryId) {
    return request({
        url: formatRequestUri('SetBind'),
        method: 'post',
        data: {
            Id: id,
            Value: categoryId
        }
    })
}