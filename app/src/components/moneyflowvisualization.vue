<template>
  <div class="moneyFlow">
    <canvas id="displayCanvas"> </canvas>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import MoneyFlow from './moneyflow';

@Component
export default class MoneyFlowVisualization extends Vue {
    public moneyFlow!: MoneyFlow;
    public data() {
        return {
            moneyFlow: null,
        };
    }

    public mounted() {
        //
        this.moneyFlow = new MoneyFlow();
    }
}
</script>

<style>
    #displayCanvas{
        width: 1200px;
        height: 1000px;
        background-color: antiquewhite;
    }
</style>

