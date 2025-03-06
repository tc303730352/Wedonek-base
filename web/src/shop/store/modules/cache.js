const shopKey = 'curent_Shop'

export function clearCache() {
    localStorage.removeItem(shopKey)
}
export function setShopId(shopId) {
    localStorage.setItem(shopKey, shopId)
}
export function getShopId() {
    return localStorage.getItem(shopKey)
}