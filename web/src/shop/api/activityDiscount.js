import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/activity/discount/' + name
}
export function Get(activityId) {
    return request({
        url: formatRequestUri('Get'),
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