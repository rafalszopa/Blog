import Vue from 'vue';
import VueRouter, { Route } from 'vue-router';
import LoginPage from '../views/LoginPage.vue';
import HomePage from '../views/HomePage.vue';
import PostsPage from '../views/PostsPage.vue';
import CommentsPage from '../views/CommentsPage.vue';
import UsersPage from '../views/UsersPage.vue';
import SettingsPage from '../views/SettingsPage.vue';
import Store from '../store';

Vue.use(VueRouter);

const router = new VueRouter({
    mode: 'hash',
    //base: process.env.BASE_URL,
    routes: [
        {
            path: '/',
            name: 'home',
            component: HomePage
        },
        {
            path: '/posts',
            component: PostsPage
        },
        {
            path: '/comments',
            component: CommentsPage
        },
        {
            path: '/users',
            component: UsersPage
        },
        {
            path: '/settings',
            component: SettingsPage
        },
        {
            path: '/login',
            component: LoginPage,
            meta: {
                public: true,
                layout: 'blank'
            }
        } 
    ]
});

router.beforeEach((to, from, next) => {
    const isPublic = to.matched.some(route => route.meta.public);
    const authenticated = Store.state.isAuthenticated;

    console.log('[router.beforeEach] Authenticated: ' + authenticated);
    console.log(to);
    if (to.path !== '/login' && !authenticated) {
        console.log('next("/login")');
        next({
            path: '/login',
            query: {
                redirect: to.fullPath
            }
        });
    } else {
        console.log('next()');
        next();
    }

    //if (isPublic) {
    //    console.log('public')
    //    next();
    //}
    //else if (!isPublic && !authenticated) {
    //    console.log('1');
    //    next({
    //        path: '/login',
    //        query: {
    //            redirect: to.fullPath
    //        }
    //    });
    //} else {
    //    console.log('2');
    //    next();
    //}
});

export default router;