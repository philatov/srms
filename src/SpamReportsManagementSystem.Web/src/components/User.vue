<template>
    <tr>
        <td class="align-middle">
            <span>{{name}}</span>
        </td>
        <td class="align-middle">
            <span>{{messagesCount}}</span>
        </td>
        <td class="align-middle">
            <span>{{reportsCount}}</span>
        </td>
        <td class="align-middle">
            <span>{{warningsCount}}</span>
        </td>
        <td class="align-middle">
            <input type="text" v-model="data.warnComment">
        </td>
        <td class="align-middle">
            <span><a class="button is-warning is-small is-outlined is-rounded" @click="warnUser(data)">Warn</a></span>
        </td>
        <td class="align-middle">
            <span v-if="isLockedOut">{{lockedOutTill}}</span>
        </td>
        <td class="align-middle">
            <input type="text" v-model="data.lockComment" v-bind:disabled="isLockDisabled">
        </td>
        <td class="align-middle">
            <span><a class="button is-danger is-small is-outlined is-rounded" v-bind:disabled="isLockDisabled" @click="lockUser(data)">Lock</a></span>
        </td>
        <td class="align-middle">
            <span><a class="button is-success is-small is-outlined is-rounded" v-bind:disabled="isUnlockDisabled" @click="unlockUser(data)">Unlock</a></span>
        </td>
    </tr>
</template>

<script>
  import { createNamespacedHelpers, mapActions as mapActionsRoot } from 'vuex';
  import { LOCK_USER, UNLOCK_USER, WARN_USER } from '../store/actions-types';
  const { mapActions } = createNamespacedHelpers('users');
  const fields = {
      id: String,
      name: String,   
      messagesCount: Number,
      reportsCount: Number,
      warningsCount: Number,
      lockedOutTill: String,
      isLockedOut: Boolean,
      lockComment: String,
      warnComment: String
    };

    export default {
        name: 'User',
        props: fields,
        data: vm => ({
            data: vm.getData(),
            isProcessing: false,
            isLoading: false            
        }),
        methods: {
          ...mapActions({
              lockUser: LOCK_USER,
              unlockUser: UNLOCK_USER,
              warnUser: WARN_USER
          }),
          getData() {
            const data = {};
            const ignoreList = ['id'];
            for (const key in fields) {
                data[key] = this.$props[key] !== undefined
                    ? this.$props[key]
                    : ignoreList.includes(key)
                        ? undefined
                        : null;
            }
            return JSON.parse(JSON.stringify(data));
          }
        },
        computed: {
            isUnlockDisabled: function () {
                return !this.isLockedOut;
            },
            isLockDisabled: function () {
                return this.isLockedOut;
            }
        }
    };
</script>

<style scoped>
</style>

