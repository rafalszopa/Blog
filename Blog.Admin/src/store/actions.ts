import { ActionTree } from 'vuex';
import axios from 'axios';
import { ApplicationState } from '../models/ApplicationState';
import User from '../models/User';
import AuthenticationService from '../services/authentication.service';

export const actions: ActionTree<ApplicationState, any> = {
    login({ commit }, payload): any {

        let authenticationService = new AuthenticationService();
        let email = payload.email;
        let password = payload.password;

        authenticationService
            .login(email, password)
            .then(
                (response) => {
                    const payload: User = response;
                    commit('userLoaded', payload);
                },
                (error) => {
                    console.log(error);
                }
            )
    },
    logout({ commit }): any {

        let authenticationService = new AuthenticationService();
        authenticationService
            .logout()
            .finally(() => commit('userUnloaded'));
    }
}