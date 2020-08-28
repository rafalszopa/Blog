import Vue from 'vue';
import store from './store';
import axios from 'axios';
import router from './router';
import App from './App.vue';
import DefaultLayout from './layouts/Default.vue';
import BlankLayout from './layouts/Blank.vue';
import Router from 'vue-router'

Vue.config.productionTip = false;
Vue.prototype.$http = axios;
Vue.component('default-layout', DefaultLayout);
Vue.component('blank-layout', BlankLayout);

new Vue({
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');
