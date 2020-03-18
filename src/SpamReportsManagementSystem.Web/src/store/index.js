import Vue from 'vue';
import Vuex from 'vuex';

import * as m from './modules';
import reports from './modules/reports';
import messages from './modules/messages';
import users from './modules/users';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production';

const getters = {
};

const actions = {
};

const store = new Vuex.Store({
  getters,
  actions,
  modules: {
      [m.reportsModule]: reports,
      [m.usersModule]: users,
      [m.messagesModule]: messages
  },
  strict: debug
});

if (module.hot) {
  module.hot.accept(['./modules/reports'], () => {
    const reportsModule = require('./modules/reports').default;
    store.hotUpdate({
      modules: {
        [m.reportsModule]: reportsModule
      }
    });
  });
}

export default store;
