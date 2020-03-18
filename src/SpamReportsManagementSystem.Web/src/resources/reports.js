import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
  list: {
    method: 'GET',
    url: 'api/reports{?take,skip,search}'
  }
};

export default Vue.resource('api/reports{/id}', {}, customActions);
