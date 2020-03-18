import reportsResource from '../../resources/reports';
import { LOAD_REPORTS } from '../actions-types';
import { REQUEST_REPORTS, RECEIVE_REPORTS } from '../mutations-types';

const state = {
  reports: [],
  totalCount: null,
  isLoading: false
};

const getters = {
  reports: (state) => state.reports,
  totalCount: (state) => state.totalCount,
  isLoading: (state) => state.isLoading
};

const mutations = {
  [REQUEST_REPORTS] (state) {
    state.isLoading = true;
  },
  [RECEIVE_REPORTS] (state, reports) {
    if (reports) {
      //state.totalCount = reports.totalCount;
      state.reports = reports;
    }
    state.isLoading = false;
  }
};

const defaultTake = 15;

const actions = {
  [LOAD_REPORTS] ({ commit }, { skip, take, search } = {}) {
    commit(REQUEST_REPORTS);
    return reportsResource.list({ take: take || defaultTake, skip: +skip || 0, search })
      .then(response => {
        commit(RECEIVE_REPORTS, response.body);
        return response;
      })
      .catch(error => {
        //console.error(error);        
        commit(RECEIVE_REPORTS);
        return error;
      });
  }
};

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions
};
