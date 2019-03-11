import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        LoggedIn: false,
        Details: {}
    },
    mutations: {
        login(state, details){
            state.LoggedIn = true;
            state.Details = details;
        },
        logout: state => state.LoggedIn = false,
    },
    getters: {
        userDetails: state => state.Details
    },
    actions: {

    }
})
