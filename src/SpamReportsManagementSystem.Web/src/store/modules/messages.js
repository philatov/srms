import messagesResource from '../../resources/messages';
import { LOAD_MESSAGES } from '../actions-types';
import { REQUEST_MESSAGES, RECEIVE_MESSAGES } from '../mutations-types';

const state = {
  messages: [],
  totalCount: null,
  isLoading: false
};

const getters = {
  messages: (state) => state.messages,
  totalCount: (state) => state.totalCount,
  isLoading: (state) => state.isLoading
};

const mutations = {
  [REQUEST_MESSAGES] (state) {
    state.isLoading = true;
  },
  [RECEIVE_MESSAGES] (state, messages) {
    if (messages) {
      //state.totalCount = messages.totalCount;
      state.messages = messages;
    }
    state.isLoading = false;
  }
};

const defaultTake = 15;

const actions = {
  [LOAD_MESSAGES] ({ commit }, { skip, take, search } = {}) {
    commit(REQUEST_MESSAGES);
    return messagesResource.list({ take: take || defaultTake, skip: +skip || 0, search })
      .then(response => {
        commit(RECEIVE_MESSAGES, response.body);
        return response;
      })
      .catch(error => {
        //console.error(error);        
        commit(RECEIVE_MESSAGES);
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
