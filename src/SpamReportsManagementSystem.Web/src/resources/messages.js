import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
  list: {
    method: 'GET',
    url: 'api/messages{?take,skip,search}'
  }
};

export default Vue.resource('api/messages{/id}', {}, customActions);
