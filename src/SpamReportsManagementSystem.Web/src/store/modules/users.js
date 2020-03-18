import usersResource from '../../resources/users';
import { LOAD_USERS, LOCK_USER, WARN_USER, UNLOCK_USER } from '../actions-types';
import {
    REQUEST_USERS, RECEIVE_USERS,
    REQUEST_LOCK_USER, RECEIVE_LOCK_USER,
    REQUEST_WARN_USER, RECEIVE_WARN_USER,
    REQUEST_UNLOCK_USER, RECEIVE_UNLOCK_USER
} from '../mutations-types';

const state = {
  users: [],
  totalCount: null,
  isLoading: false
};

const getters = {
  users: (state) => state.users,
  totalCount: (state) => state.totalCount,
  isLoading: (state) => state.isLoading
};

function setProcessing(state, user) {
    const stateUser = state.users.find(localUser => localUser.id === user.id);
    if (stateUser) {
        stateUser.isProcessing = true;
    }
}

const mutations = {
    [REQUEST_USERS](state) {
        state.isLoading = true;
    },
    [RECEIVE_USERS](state, users) {
        if (users) {
            state.users = users;
        }
        state.isLoading = false;
    },

    [REQUEST_LOCK_USER](state, user) {
        setProcessing(state, user);
    },
    [RECEIVE_LOCK_USER](state, user) {
        if (user) {
            const index = state.users.findIndex(localUser => localUser.id === user.id);
            if (~index) {
                state.users.splice(index, 1, user);
            }
        }
    },

    [REQUEST_WARN_USER](state, user) {
        setProcessing(state, user);
    },
    [RECEIVE_WARN_USER](state, user) {
        if (user) {
            const index = state.users.findIndex(localUser => localUser.id === user.id);
            if (~index) {
                state.users.splice(index, 1, user);
            }
        }
    },

    [REQUEST_UNLOCK_USER](state, user) {
        setProcessing(state, user);
    },
    [RECEIVE_UNLOCK_USER](state, user) {
        if (user) {
            console.log(user.id);
            const index = state.users.findIndex(localUser => localUser.id === user.id);
            console.log(index);
            if (~index) {
                state.users.splice(index, 1, user);
            }
        }
    }
};

const defaultTake = 15;

const actions = {
    [LOAD_USERS]({ commit }, { skip, take, search } = {}) {
        commit(REQUEST_USERS);
        return usersResource.list({ take: take || defaultTake, skip: +skip || 0, search })
            .then(response => {
                commit(RECEIVE_USERS, response.body);
                return response;
            })
            .catch(error => {
                commit(RECEIVE_USERS);
                return error;
            });
    },
    [LOCK_USER]({ commit }, user) {
        commit(REQUEST_LOCK_USER, user);
        return usersResource.lock({ id: user.id }, user)
            .then(response => {
                commit(RECEIVE_LOCK_USER, response.body);
                return response;
            })
            .catch(error => {
                commit(RECEIVE_LOCK_USER);
                return error;
            });
    },
    [WARN_USER]({ commit }, user) {
        commit(REQUEST_WARN_USER, user);
        return usersResource.warn({ id: user.id }, user)
            .then(response => {
                commit(RECEIVE_WARN_USER, response.body);
                return response;
            })
            .catch(error => {
                commit(RECEIVE_WARN_USER);
                return error;
            });
    },
    [UNLOCK_USER]({ commit }, user) {
        commit(REQUEST_UNLOCK_USER, user);
        return usersResource.unlock({ id: user.id }, user)
            .then(response => {
                commit(RECEIVE_UNLOCK_USER, response.body);
                return response;
            })
            .catch(error => {
                commit(RECEIVE_UNLOCK_USER);
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
