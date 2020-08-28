import { ActionTree } from 'vuex';
import axios from 'axios';
import { ApplicationState } from '../models/ApplicationState';
import User from '../models/User';
import AuthenticationService from '../services/authentication.service';
//import Post from '../models/Post';

export const actions: ActionTree<ApplicationState, any> = {
    login({ commit }, payload): any {

        let authenticationService = new AuthenticationService();
        let email = payload.email;
        let password = payload.password;

        return authenticationService
            .login(email, password)
            .then(response => commit('userAuthenticated', true));
    },
    logout({ commit }): any {
        let authenticationService = new AuthenticationService();
        authenticationService
            .logout()
            .then(() => commit('userUnloaded'));
    },
    //getPosts({ commit }): Post {
    //    return new Post(99, 'Some title', 'Some content');
    //}
}