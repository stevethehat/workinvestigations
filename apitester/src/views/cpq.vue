<template>
    <div class="login">
        <h1>CPQ</h1>
        {{Host}}
        <b-card title="Wholegoods" 
            _img-src="~/assets/tractor2.png">
            <div v-for="wholegood in Wholegoods" v-bind:key="wholegood.StockNumber">
                <div>
                    <wholegood :wholegood="wholegood">
                    </wholegood>
                </div>
            </div>

            <b-button-toolbar>
                <b-button-group size="sm">
                    <b-button variant="primary" @click="addWholegood">Add</b-button>
                </b-button-group>
            </b-button-toolbar>
        </b-card>
        <b-card title="Trade Ins">
            <div v-for="tradeIn in TradeIns" v-bind:key="tradeIn.SerialNumber">
                <div>
                    <trade-in :tradein="tradeIn">
                    </trade-in>
                </div>
            </div>
            <b-button-toolbar>
                <b-button-group size="sm">
                    <b-button variant="primary" @click="addTradeIn">Add</b-button>
                </b-button-group>
            </b-button-toolbar>
        </b-card>
        <b-card>
            <b-button-toolbar>
                <b-button-group size="sm">
                    <b-button variant="primary" @click="go">Go</b-button>
                </b-button-group>
            </b-button-toolbar>
        </b-card>
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
        const requestData = {
            wholegoods: this.Wholegoods,
            tradins: this.TradeIns
        }
        alert(JSON.stringify(requestData, null, 2));
    }
}
</script>

