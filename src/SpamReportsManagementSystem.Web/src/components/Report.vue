<template>
    <tr>
        <td class="align-middle">
            <span>{{data.timestamp}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.about.name}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.from.name}}</span>
        </td>
        <td class="align-middle">
            <span>{{data.message.text}}</span>
        </td>
    </tr>
</template>

<script>
    const fields = {
      id: String,
      timestamp: String,
      from: Object,
      about: Object,      
      message: Object
    };

    export default {
        name: 'Report',
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

