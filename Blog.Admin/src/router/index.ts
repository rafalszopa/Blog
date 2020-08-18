import Vue from 'vue';
import VueRouter from 'vue-router';
import Login from '../components/Login.vue';
import Contact from '../components/Contact.vue';
import HelloWorld from '../components/HelloWorld.vue';
import Store from '../store';

Vue.use(VueRouter);

const router = new VueRouter({
    mode: 'history',
    routes: [
        {
            path: '/',
            component: HelloWorld
        },
        {
            path: '/login',
            component: Login,
            meta: {
                public: true
            }
        },
        {
            path: '/contact',
            component: Contact
        }
    ]
});

router.beforeEach((to, from, next) => {
    const isPublic = to.matched.some(route => route.meta.public);
    const authenticated = Store.state.user !== undefined;

    if (!isPublic && !authenticated) {
        return next({
            path: '/login',
            query: {
                redirect: to.fullPath
            }
        });
    }

    next();
});

export default router;