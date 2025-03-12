import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/EmpRole/' + name
}
export function set(empId, roleId) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data: {
            EmpId: empId,
            RoleId: roleId
        }
    })
}
export function get(empId) {
    return request({
        url: formatRequestUri('Get'),
        method: 'get',
        params: {
            empId
        }
    })
}