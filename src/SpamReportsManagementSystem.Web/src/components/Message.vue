<template>
    <tr>
        <td class="align-middle">
            <span>{{data.timestamp}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.author.name}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.text}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.reportsCount}}</span>
        </td>
    </tr>
</template>

<script>
    const fields = {
      id: String,
      timestamp: String,
      author: Object,
      text: String,
      reportsCount: Number
    };

    export default {
        name: 'Message',
        props: fields,
        data: vm => ({
            data: vm.getData(),
            isProcessing: false,
            isLoading: false
        }),
        methods: {
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
        }
    };
</script>

<style scoped>
</style>

