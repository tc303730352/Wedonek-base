import { LoadArea } from "@/api/area";
import moment from "moment";
const areaKey = 'area';

export async function getArea() {
    if (window.AreaList != null) {
        return window.AreaList
    }
    let area = localStorage.getItem(areaKey)
    if (area != null) {
        const data = JSON.parse(area)
        if (moment(data.expire) > moment()) {
            window.AreaList = data.area
            return data.area
        }
    }
    area = await LoadArea()
    localStorage.setItem(areaKey, JSON.stringify({
        expire: moment().add(1, 'day'),
        area
    }))
    window.AreaList = area
    return area
}

export async function getAreaTree() {
    if (window.AreaTree != null) {
        return window.AreaTree
    }
    const area = await getArea()
    const tree = []
    area.forEach(a => {
        if (a.ParentId == 0) {
            const item = {
                Id: a.Id,
                Name: a.Name,
                AreaType: a.AreaType,
                Children: []
            }
            initArea(item, area)
            tree.push(item)
        }
    });
    window.AreaTree = tree
    return tree
}
function initArea(prt, list) {
    const items = list.filter(c => c.ParentId == prt.Id)
    if (items.length == 0) {
        return
    }
    items.forEach(a => {
        const item = {
            Id: a.Id,
            Name: a.Name,
            AreaType: a.AreaType,
            Children: []
        }
        initArea(item, list)
        prt.Children.push(item)
    });
}
