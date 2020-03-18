import Vue from 'vue';
import VueResource from 'vue-resource';

Vue.use(VueResource);

const customActions = {
  list: {
    method: 'GET',
    url: 'api/users{?take,skip,search}'
  },
  lock: {
    method: 'POST',
    url: 'api/users/{id}/lock'
  },
  unlock: {
    method: 'POST',
    url: 'api/users/{id}/unlock'
  },
  warn: {
    method: 'POST',
    url: 'api/users/{id}/warn'
  }
};

export default Vue.resource('api/users{/id}', {}, customActions);
