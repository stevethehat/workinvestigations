<template>
  <div class="moneyFlow">
    <h1>Test</h1>

    <canvas id="displayCanvas"> </canvas>
  </div>
</template>

<script>
import MoneyFlow from "./moneyflow"

export default {
    name: 'MoneyFlowVisualization',
    data () {
        return {
            msg: 'Welcome to Your Vue.js App',
            moneyFlow: null
        }
    },
    mounted: function(){
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

