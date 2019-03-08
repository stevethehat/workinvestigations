<template>
    <div class="pageContent">
        <h1>CPQ</h1>
        {{Host}}
        <b-card title="Wholegoods" 
            _img-src="~/assets/tractor2.png">
            <div v-for="wholegood in Wholegoods" v-bind:key="wholegood.Id">
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
            <div v-for="tradeIn in TradeIns" v-bind:key="tradeIn.Id">
                <div>
                    <trade-in :tradeIn="tradeIn">
                    </trade-in>
                </div>
            </div>
            <b-button-toolbar>
                <b-button-group size="sm">
                    <b-button variant="primary" @click="addTradeIn">Add</b-button>
                </b-button-group>
            </b-button-toolbar>
        </b-card>
        <b-card title="Additional Items">
            <div v-for="additionalItem in AdditionalItems" v-bind:key="additionalItem.Id">
                <div>
                    <additional-item :additionalItem="additionalItem">
                    </additional-item>
                </div>
            </div>
            <b-button-toolbar>
                <b-button-group size="sm">
                    <b-button variant="primary" @click="addAdditionalItem">Add</b-button>
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
import { CPQWholegood, CPQTradeIn, CPQAdditionalItem }             from '@/util/cpq.ts';

import Wholegood                    from '@/components/Wholegood.vue';
import TradeIn                      from '@/components/TradeIn.vue';
import AdditionalItem               from '@/components/AdditionalItem.vue';

import router                   from '@/router';

@Component({
    components:{
        Wholegood, TradeIn, AdditionalItem,
    }
})
export default class CPQ extends Vue {
    public Host         : string = 'localhost';
    public Wholegoods   : Array<CPQWholegood> = [ new CPQWholegood() ];
    public TradeIns   : Array<CPQTradeIn> = [ ];
    public AdditionalItems: Array<CPQAdditionalItem> = [];

    constructor(){
        super();
        if(!apiTester.LoggedIn){
            router.push('');    
        }
        this.Host = apiTester.Host;
    }

    addWholegood(){
        var wholegood = new CPQWholegood();
        wholegood.Id = this.Wholegoods.length;
        this.Wholegoods.push(wholegood);
    }
    addTradeIn(){
        var tradeIn = new CPQTradeIn();
        tradeIn.Id = this.TradeIns.length;
        this.TradeIns.push(tradeIn);
    }
    addAdditionalItem(){
        var additionalItem = new CPQAdditionalItem();
        additionalItem.Id = this.AdditionalItems.length;
        this.AdditionalItems.push(additionalItem);
    }
    go(){
        apiTester.RequestData = this.createConfiguration();
        router.push('preview');
    }

    createConfiguration(){
        const configurationItems: any[] = [];
        this.Wholegoods.forEach(wholegood => {
            configurationItems.push(wholegood.getCPQData());
        });

        const tradeInItems: any[] = [];
        this.TradeIns.forEach(tradeIn => {
            tradeInItems.push(tradeIn.getCPQData());
        });

        const additionalItems: any[] = [];
        this.AdditionalItems.forEach(additionalItem => {
            additionalItems.push(additionalItem.getCPQData());
        });

        return {
            quotation:{
                configuration: {
                    lineItems: configurationItems
                },
                tradein: {
                    lineItems: tradeInItems
                },
                additionalItems: {
                    lineItems: additionalItems
                }               
            }
        };
    }
}
</script>

