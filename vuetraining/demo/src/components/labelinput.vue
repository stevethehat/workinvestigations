<template>
    <div>
        <label>{{label}}</label>
        <input type="number" v-model="localValue"/>
    </div>
</template>

<script lang="ts">

import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';

@Component({})
export default class LabelInput extends Vue {
    @Prop({})
    public label    : string;
    @Prop({})
    public value    : number;

    private localValue: number = this.value;

    constructor() {
        super();
        this.label = '';
        this.value = 1;
    }

    @Watch('value')
    private valueChanged(newValue: number, oldValue: number) {
        if(newValue !== oldValue) {
            this.localValue = newValue;
        }
    }
    @Watch('localValue')
    private localValueChanged(newValue: number, oldValue: number) {
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
}
</script>

<style>

</style>
