import Vue from 'vue';
import axios from 'axios';
import User from '../models/User';
import Router from '../router';

export default class AuthenticationService {
    async login(email: string, password: string): Promise<User> {

        return await axios
            .post<User>('/api/authentication/login', { email, password })
            .then((response) => {

                if (response.status !== 200) {
                    console.log(`Response status: ${response.status}`);
                    //Router.push('/');
                }
                return response.data;
            });
    }
    async logout() : Promise<any> {
        return await axios
            .post('/api/authentication/logout')
            .then((response) => {

                if (response.status !== 200) {
                    console.log(`Response status: ${response.status}`);
                    //Router.push('/');
                }
                return response.data;
            });
    }
}