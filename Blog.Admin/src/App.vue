<template>
    <div id="app">
        {{ $store.getters.count }}
        <!--<img alt="Vue logo" src="./assets/logo.png">-->
        <div>
            <pre>Put component here!</pre>
        </div>
        <!--<HelloWorld msg="Welcome to Your Vue.js + TypeScript App hosted with ASP.NET"/>-->
        <div>
            <p>Menu</p>
            <ul>
                <li>
                    <router-link to="/">Go to home</router-link>
                </li>
                <li>
                    <router-link to="/login">Login</router-link>
                </li>
                <li>
                    <router-link to="/contact">Contact</router-link>
                </li>
            </ul>
        </div>
        <router-view></router-view>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import HelloWorld from './components/HelloWorld.vue';
    import AuthenticationService from './services/authentication.service';

@Component({
  components: {
    //HelloWorld,
  },
})
export default class App extends Vue {}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
