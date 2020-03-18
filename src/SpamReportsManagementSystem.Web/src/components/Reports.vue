<template>
    <table class="table is-striped" ref="table">
        <thead>
            <tr>
                <th>Time</th>
                <th>From</th>
                <th>About</th>
                <th>Message</th>
            </tr>
        </thead>
        <tbody>
            <report v-for="report in reports" :key="report.id" v-bind="report"></report>
            <tr v-if="isLoading">
                <td class="p-3" colspan="4"><span v-if="isLoading">Loading Reports</span><span v-else>No reports {{search ? 'matched the search' : 'was found'}}.</span></td>
            </tr>
        </tbody>
    </table>
</template>

<script>
    import { createNamespacedHelpers } from 'vuex';
    import { LOAD_REPORTS } from '../store/actions-types';
    import report from './Report';

    const { mapActions, mapGetters } = createNamespacedHelpers('reports');

    export default {
        name: 'Reports',
        components: {
            report
        },
        data: vm => ({
            take: 15,
            current: null,
            search: null,
            resizer: null
        }),
        created() {
            this.loadReports();
        },
        computed: {
            ...mapGetters(['reports', 'totalCount', 'isLoading'])
        },
        methods: {
            ...mapActions({
                loadReportsInternal: LOAD_REPORTS
            }),
            loadReports(...args) {
                this.loadReportsInternal(...args);
            },
            onPageChange(event) {
                this.loadReports({ skip: event.first, search: this.search });
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
