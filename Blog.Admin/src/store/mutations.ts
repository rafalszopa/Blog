import { MutationTree } from 'vuex';
import { ApplicationState } from '../models/ApplicationState';
import User from '../models/User';

export const mutations: MutationTree<ApplicationState> = {
    userLoaded(state: ApplicationState, payload: User) {
        state.user = payload;
    },
    userUnloaded(state: ApplicationState) {
        state.user = undefined;
        state.isAuthenticated = false;
    },
    incrementCounter(state: ApplicationState) {
        state.count++;
    },
    userAuthenticated(state: ApplicationState) {
        state.isAuthenticated = true;
    }
}