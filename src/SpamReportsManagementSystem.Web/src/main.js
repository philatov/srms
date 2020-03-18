import Vue from 'vue';
import VueResource from 'vue-resource';
import store from './store';
import router from './router';
import App from './App.vue';

import './assets/app.scss';

Vue.use(VueResource);

Vue.config.productionTip = false;
Vue.http.options.root = 'http://localhost:5000';

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
