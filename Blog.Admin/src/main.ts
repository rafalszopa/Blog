import Vue from 'vue';
import store from './store';
import axios from 'axios';
import router from './router';
import App from './App.vue';

Vue.config.productionTip = false;
Vue.prototype.$http = axios;

new Vue({
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');
