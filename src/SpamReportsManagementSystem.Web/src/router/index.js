import Vue from 'vue';
import Router from 'vue-router';

import Reports from '@/components/Reports';
import Users from '@/components/Users';
import Messages from '@/components/Messages';

Vue.use(Router);

let router = new Router({
  routes: [
    {
        path: '/reports',
        name: 'Reports',
        component: Reports,
        props: true,
        meta: {
            requiresAuth: false
        }
    },
    {
        path: '/users',
        name: 'Users',
        component: Users,
        props: true,
        meta: {
            requiresAuth: false
        }
    },
    {
        path: '/messages',
        name: 'Messages',
        component: Messages,
        props: true,
        meta: {
            requiresAuth: false
        }
    }
  ]
});

export default router;