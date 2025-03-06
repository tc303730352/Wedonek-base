import request from '@/utils/request'

function formatRequestUri(name) {
    return 'hr/DeptChange/' + name
}
export function GetDept(deptId, isUnit) {
    return request({
        url: formatRequestUri('GetDept'),
        method: 'get',
        params: {
            deptId,
            isUnit
        }
    })
}
export function GetMergeEmp(query) {
    return request({
        url: formatRequestUri('GetMergeEmp'),
        method: 'post',
        data: query
    })
}
export function Merge(deptId, toDeptId) {
    return request({
        url: formatRequestUri('Merge'),
        method: 'get',
        params: {
            deptId,
            toDeptId
        }
    })
}
export function ToVoid(deptId) {
    return request({
        url: formatRequestUri('ToVoid'),
        method: 'get',
        params: {
            deptId
        }
    })
}
export function GetDisbandedEmps(query) {
    return request({
        url: formatRequestUri('GetDisbandedEmps'),
        method: 'post',
        data: query
    })
}