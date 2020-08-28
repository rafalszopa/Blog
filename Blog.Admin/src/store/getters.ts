import { GetterTree } from 'vuex';
import { ApplicationState } from '../models/ApplicationState';

export const getters: GetterTree<ApplicationState, any> = {
    isAuthenticated(state): boolean {
        const { isAuthenticated } = state;
        return isAuthenticated;
    }
};