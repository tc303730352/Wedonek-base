import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/DeptPrower/' + name
}
export function set(empId, companyId, deptId) {
    return request({
        url: formatRequestUri('Set'),
        method: 'post',
        data: {
            EmpId: empId,
            CompanyId: companyId,
            DeptId: deptId
        }
    })
}
export function get(empId, companyId) {
    return request({
        url: formatRequestUri('Get'),
        method: 'get',
        params: {
            empId,
            companyId
        }
    })
}