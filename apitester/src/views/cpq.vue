<template>
    <div class="login">
        <h1>CPQ</h1>
        {{Host}}
        <h2>Wholegoods</h2>
        <div v-for="wholegood in Wholegoods" v-bind:key="wholegood.StockNumber">
            <b-card :title="wholegood.StockNumber">
                <wholegood :wholegood="wholegood">
                </wholegood>
            </b-card>
        </div>
        <b-button @click="addWholegood">Add</b-button>
        <h2>Trade Ins</h2>
        <div v-for="tradeIn in TradeIns" v-bind:key="tradeIn.SerialNumber">
            <b-card :title="tradeIn.SerialNumber">
                <trade-in :tradein="tradeIn">
                </trade-in>
            </b-card>
        </div>
        <b-button @click="addTradeIn">Add</b-button>
        <div>
            <b-button @click="go">Go</b-button>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue }     from 'vue-property-decorator';

import { apiTester }                from '@/util/ApiTester.ts';
import { CPQWholegood, CPQTradeIn }             from '@/util/cpq.ts';

import Wholegood                    from '@/components/Wholegood.vue';
import TradeIn                      from '@/components/TradeIn.vue';

@Component({
    components:{
        Wholegood, TradeIn,
    }
})
export default class CPQ extends Vue {
    public Host         : string = 'localhost';
    public Wholegoods   : Array<CPQWholegood> = [
        {
            StockNumber: '',
            Make: '',
            Model: ''
        }
    ];
    public TradeIns   : Array<CPQTradeIn> = [
        {
            SerialNumber: '',
            Make: '',
            Model: ''
        }
    ];

    constructor(){
        super();
        this.Host = apiTester.Host;
    }

    addWholegood(){
        this.Wholegoods.push(new CPQWholegood());
    }
    addTradeIn(){
        this.TradeIns.push(new CPQTradeIn());
    }
    go(){
        alert(JSON.stringify(this.Wholegoods, null, 2));
    }
}
</script>

