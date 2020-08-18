<template>
    <div>
        <!--<div style="background-color: blue; color: white;">
            First name: {{ $store.user.firstName }}
            Last name: {{ $store.user.lastName }}
        </div>
        <hr />
        <h1>Contact</h1>
        <pre>{{ this.jsonResponse }}</pre>
        <hr />-->
        <div v-if="this.isAuthenticated" style="background: green;">
            Authenticated!
        </div>
        <div v-else style="background: red;">
            Not authenticated
        </div>
        <h3>Count: {{ this.state.count }}</h3>
        <button @click="increment">Increment</button>
    </div>
</template>

<script lang="ts">
    import axios from 'axios';
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { State, Getter, Action, Mutation } from 'vuex-class';
    import { ApplicationState } from '../models/ApplicationState';

    @Component
    export default class Contact extends Vue {
        @State(state => state) private state!: ApplicationState
        @Getter('isAuthenticated') private isAuthenticated!: boolean
        @Mutation incrementCounter: any;
        created() {
            this.state;
            this.isAuthenticated;
        }

        private jsonResponse: string = 'no response';

        private increment() {
            this.incrementCounter();
            //console.log('click');
            //this.$store.commit('change', this.$store.getters.count + 1);
        }

        private mounted() {
            axios
                .get<string>('https://api.coindesk.com/v1/bpi/currentprice.json')
                .then(response => this.jsonResponse = response.data)
                .catch(error => console.log(error));
        }
    }
</script>