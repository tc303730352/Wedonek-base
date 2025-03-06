import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/shop/' + name
}

export function Login(companyId) {
    return request({
        url: formatRequestUri('Login'),
        method: 'get',
        params: {
            companyId
        }
    })
}
export function Get() {
    return request({
        url: formatRequestUri('Get'),
        method: 'get'
    })
}
export function Set(data) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data
    })
}