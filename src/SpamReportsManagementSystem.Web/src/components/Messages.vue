<template>
    <table class="table is-striped" ref="table">
        <thead>
            <tr>
                <th>Time</th>
                <th>Author</th>
                <th>Message</th>
                <th>Total Reports</th>
            </tr>
        </thead>
        <tbody>
            <message v-for="message in messages" :key="message.id" v-bind="message"></message>
            <tr v-if="isLoading">
                <td class="p-3" colspan="4"><span v-if="isLoading">Loading Messages</span><span v-else>No messages {{search ? 'matched the search' : 'was found'}}.</span></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import { createNamespacedHelpers } from 'vuex';
    import { LOAD_MESSAGES } from '../store/actions-types';
    import message from './Message';

    const { mapActions, mapGetters } = createNamespacedHelpers('messages');

    export default {
        name: 'Messages',
        components: {
            message
        },
        data: vm => ({
            take: 15,
            current: null,
            search: null,
            resizer: null
        }),
        created() {
            this.loadMessages();
        },
        computed: {
            ...mapGetters(['messages', 'totalCount', 'isLoading'])
        },
        methods: {
            ...mapActions({
                loadMessagesInternal: LOAD_MESSAGES
            }),
            loadMessages(...args) {
                this.loadMessagesInternal(...args);
            },
            onPageChange(event) {
                this.loadMessages({ skip: event.first, search: this.search });
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
