import request from '@/utils/request'

function formatRequestUri(name) {
    return 'shop/user/grade/' + name
}

export function Gets() {
    return request({
        url: formatRequestUri('Gets'),
        method: 'get'
    })
}