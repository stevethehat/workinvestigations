<template>
    <div>
        <label>{{label}}</label>
        [obj label]<input type="number" v-model="localValue.val1"/>
    </div>
</template>

<script>

import { StringOrNumber } from './types';

export default {
    name: 'objlabelinput',
    props: {
        label: {
            type: StringOrNumber,
        },
        value: {
            type: Object,
        },
    },
    data() {
        return {
            localValue: this.value,
        };
    },
    watch: {
        value(newValue, oldValue) {
            this.localValue = newValue;
        },
        localValue: {
            handler(newValue, oldValue) {
                this.$emit('input', newValue);
            },
            deep: true,
        },
    },
};
</script>

<style>

</style>
