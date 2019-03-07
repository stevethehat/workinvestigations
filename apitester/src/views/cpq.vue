<template>
    <div class="login">
        <h1>CPQ</h1>
        {{Host}}

        <div v-for="wholegood in Wholegoods" v-bind:key="wholegood">
            <b-card :title="wholegood.StockNumber">

            </b-card>
        </div>
        <button @click="go">Go</button>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue }     from 'vue-property-decorator';
import { apiTester }                from '@/util/ApiTester.ts';
import { CPQWholegood }             from '@/util/cpq.ts';

@Component({})
export default class CPQ extends Vue {
    public Host         : string = 'localhost';
    public Wholegoods   : Array<CPQWholegood> = [
        {
            StockNumber: '1234'
        }
    ];

    constructor(){
        super();
        this.Host = apiTester.Host;
    }

    go(){
        //alert(JSON.stringify(this.Wholegoods, null, 2));
        this.Wholegoods.push(
            {
                StockNumber: '5678'
            }
        );
    }
}
</script>

