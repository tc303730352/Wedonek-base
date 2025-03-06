import * as cacheApi from './modules/cache'
const state = {
    shopId: null
}

const mutations = {
    SET_ShopId: (state, shopId) => {
        state.shopId = shopId
        cacheApi.setShopId(shopId)
    }
}
const actions = {
    setShopId({ commit }, shopId) {
        commit('SET_ShopId', shopId)
    },
    getShopId({ state }) {
        if (state.shopId == null) {
            state.shopId = cacheApi.getShopId()
        }
        return state.shopId
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}