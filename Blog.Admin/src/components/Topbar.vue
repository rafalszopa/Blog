<template>
    <div>
        <h1>Topbar</h1>
    </div>
</template>

<script lang="ts">
    import axios from 'axios';
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { State, Getter, Action, Mutation } from 'vuex-class';
    import { ApplicationState } from '../models/ApplicationState';

    @Component
    export default class Contact extends Vue {
        created() {
            console.log('Topbar');
        }
    }
</script>

<style scoped>
</style>