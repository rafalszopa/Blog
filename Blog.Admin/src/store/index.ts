import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';

import User from '../models/User';
import { ApplicationState } from '../models/ApplicationState';

import { getters } from './getters';
import { actions } from './actions';
import { mutations } from './mutations';


Vue.use(Vuex);

const store: StoreOptions<ApplicationState> = {
    state: {
        user: undefined,
        count: 99
    },
    getters,
    actions,
    mutations
};

export default new Vuex.Store<ApplicationState>(store);