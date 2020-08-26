<template>
    <div>
        <h1>Login page</h1>
        <p>Email:</p>
        <input v-model="login" type="text" />
        <p>Password:</p>
        <input v-model="password" type="password" />
        <button @click="signIn">Sign in</button>

        <pre>Login: {{ this.login }}</pre>
        <pre>Password: {{ this.password }}</pre>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import AuthenticationService from '../services/authentication.service';

    @Component
    export default class Login extends Vue {
        private login: string = '';
        private password: string = '';
        private service: AuthenticationService;

        constructor() {
            super();
            this.login = 'Login';
            this.password = 'Password';
            this.service = new AuthenticationService();
        }

        signIn() {
            //this.service.login(this.login, this.password);
            this.$store.dispatch('login', {
                email: this.login,
                password: this.password
            });
        }
    }
</script>