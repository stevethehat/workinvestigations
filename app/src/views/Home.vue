<template>
  <div class="home">
    <money-flow-visualization></money-flow-visualization>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import MoneyFlowVisualization from '../components/moneyflowvisualization.vue'; // @ is an alias to /src

@Component({
  components: {
    MoneyFlowVisualization,
  },
})
export default class Home extends Vue {}
</script>
