import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/Prower/' + name
}
export function getTreeBySystem() {
    return request({
        url: formatRequestUri('GetTreeBySystem'),
        method: 'get'
    })
}