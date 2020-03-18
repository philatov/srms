<template>
    <table class="table is-striped" ref="table">
        <thead>
            <tr>
                <th>Name</th>
                <th>Total Messages</th>
                <th>Total Reports</th>
                <th>Total Warnings</th>
                <th>Warn Comment</th>
                <th>Warn</th>
                <th>Locked Out Till</th>
                <th>Lock Comment</th>
                <th>Lock</th>
                <th>Unlock</th>
            </tr>
        </thead>
        <tbody>
            <user v-for="user in users" :key="user.id" v-bind="user"></user>
            <tr v-if="isLoading">
                <td class="p-3" colspan="3"><span v-if="isLoading">Loading Users</span><span v-else>No users {{search ? 'matched the search' : 'was found'}}.</span></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import { createNamespacedHelpers } from 'vuex';
    import { LOAD_USERS } from '../store/actions-types';
    import user from './User';

    const { mapActions, mapGetters } = createNamespacedHelpers('users');

    export default {
        name: 'Users',
        components: {
            user
        },
        data: vm => ({
            take: 15,
            current: null,
            search: null,
            resizer: null
        }),
        created() {
            this.loadUsers();
        },
        computed: {
            ...mapGetters(['users', 'totalCount', 'isLoading'])
        },
        methods: {
            ...mapActions({
                loadUsersInternal: LOAD_USERS
            }),
            loadUsers(...args) {
                this.loadUsersInternal(...args);
            },
            onPageChange(event) {
                this.loadUsers({ skip: event.first, search: this.search });
            }
        }
    };
</script>

<style lang="scss" scoped>
    td {
        padding: 2px 8px;
        overflow: auto;
        padding-left: 8px !important;
        padding-right: 8px !important;
        overflow: initial !important;
    }

    th {
        white-space: nowrap;
        text-overflow: ellipsis;
        overflow: hidden;
    }
</style>
