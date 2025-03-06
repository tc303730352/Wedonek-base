import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/activity/goods/' + name
}
export function Gets(activityId) {
    return request({
        url: formatRequestUri('Gets'),
        method: 'get',
        params: {
            activityId
        }
    })
}
export function Set(id, datas) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data: {
            Id: id,
            Value: datas
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

export function SetPrice(id, price, isMust) {
    return request({
        url: formatRequestUri('SetPrice'),
        method: 'post',
        data: {
            Id: id,
            Price: price,
            IsMust: isMust
        }
    })
}

export function BatchDelete(activityId, ids) {
    return request({
        url: formatRequestUri('BatchDelete'),
        method: 'post',
        data: {
            Id: activityId,
            Value: ids
        }
    })
}

export function BatchSetPrice(id, data) {
    return request({
        url: formatRequestUri('BatchSetPrice'),
        method: 'post',
        data: {
            Id: id,
            Value: data
        }
    })
}
