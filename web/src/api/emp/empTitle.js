import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/EmpTitle/' + name
}
export function gets(empId, companyId) {
    return request({
        url: formatRequestUri('GetList'),
        method: 'get',
        params: {
            companyId,
            empId
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
export function deleteTitle(id) {
    return request({
        url: formatRequestUri('Delete'),
        method: 'get',
        params: {
            id
        }
    })
}