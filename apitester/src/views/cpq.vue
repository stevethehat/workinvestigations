<template>
    <div class="pageContent">
        {{StatusMessage}}
        <b-card title="Customer">
            <customer :customer="Customer"/>
        </b-card>
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
            <b-card-header>
                e.g. JJE687268132, 327HS380923, 277HH662223, TN75A-636552443
            </b-card-header>
            
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
                    <b-button variant="primary" @click="preview">Preview</b-button>
                </b-button-group>
            </b-button-toolbar>
        </b-card>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue }     from 'vue-property-decorator';

import { apiTester }                from '@/util/ApiTester.ts';
import { CPQWholegood, CPQTradeIn, CPQAdditionalItem }             from '@/util/cpq.ts';

import Customer                     from '@/components/Customer.vue';
import Wholegood                    from '@/components/Wholegood.vue';
import TradeIn                      from '@/components/TradeIn.vue';
import AdditionalItem               from '@/components/AdditionalItem.vue';

import router                   from '@/router';

@Component({
    components:{
        Wholegood, TradeIn, AdditionalItem, Customer, 
    }
})
export default class CPQ extends Vue {
    public Host             : string = 'localhost';
    public Customer         : string = '1800';
    public StatusMessage    : string = ''
    public Wholegoods       : Array<CPQWholegood> = [ new CPQWholegood() ];
    public TradeIns         : Array<CPQTradeIn> = [ ];
    public AdditionalItems  : Array<CPQAdditionalItem> = [];

    constructor(){
        super();
        this.StatusMessage = apiTester.UserDetails.StatusMessage;
        var title: HTMLElement | null = document.getElementById('pageTitle');
        if(null !== title){
            title.innerHTML = 'CPQ Configuration';
        }

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
    preview(){
        apiTester.RequestData = this.createConfiguration();
        router.push('preview');
    }

    createConfiguration(){
        const configurationItems: any[] = [];
        /*
        this.Wholegoods.forEach(wholegood => {
            configurationItems.push(wholegood.getCPQData());
        });
        */
       
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
                dealerCustomerId: this.Customer,
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

