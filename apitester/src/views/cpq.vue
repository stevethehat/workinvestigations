<template>
    <div class="pageContent">
        {{StatusMessage}}
        <b-card title="Customer">
            <customer :customer="Customer" v-model="Customer"/>
        </b-card>
        <b-card title="Order type">
            <b-container>
                <b-row>
                    <b-col>
                        Order type:
                        <select type="text" v-model="OrderType">
                            <option value="DEALER">DEALER</option>
                            <option value="RETAIL">RETAIL</option>
                        </select>
                    </b-col>
                    <b-col>
                        FulFilment Type:
                        <select type="text" v-model="FulFilmentType">   
                            <option value="NEW">NEW</option>
                            <option value="DEALER_PIPELINE">DEALER PIPELINE</option>
                            <option value="DEALER_YARD">DEALER YARD</option>
                        </select>
                    </b-col>
                </b-row>
            </b-container>
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
                    <b-button variant="primary" @click="preview">Preview</b-button>&nbsp;
                    <b-button variant="primary" @click="clear">Clear</b-button>
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
    public OrderType        : string = 'DEALER';
    public FulFilmentType   : string = 'NEW';
    public Wholegoods       : Array<CPQWholegood> = [ ];
    public TradeIns         : Array<CPQTradeIn> = [  ];
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
    clear(){
        this.Wholegoods         = [];
        this.TradeIns           = [];
        this.AdditionalItems    = [];
    }

    createConfiguration(){
        const self = this;
        const configurationItems: any[]     = [];
        const tradeInItems: any[]           = [];
        const additionalItems: any[]        = [];

        this.Wholegoods.forEach(wholegood => {
            /*
            // if wholegood has warranty we add an additional item ending in "EW"!!
            if(0 !== wholegood.WarrantyCost){
                const warranty: CPQAdditionalItem = new CPQAdditionalItem();
                warranty.PartId = wholegood.StockNumber;
                warranty.Name = '11123EW';
                warranty.Description = wholegood.WarrantyDescription + 'EW';
                warranty.Price = wholegood.WarrantyCost;
                additionalItems.push(warranty.getCPQData());
            }
            */
            configurationItems.push(wholegood.getCPQData(this.OrderType, this.FulFilmentType))  ;
        });

        
        this.TradeIns.forEach(tradeIn => {
            if(tradeIn.Make != ""){
                tradeInItems.push(tradeIn.getCPQData());
            }
        });

        
        this.AdditionalItems.forEach(additionalItem => {
            additionalItems.push(additionalItem.getCPQData());
        });

        let date = new Date();
        date.setMonth(date.getMonth() + 1);

        return {
            quotation:{
                dealerCustomerId: this.Customer,
                depot: 1,
                //indicativeDeliveryDate: date.toString(),
                //indicativeDeliveryDate : 'Tue Jan 01 00:00:00 UTC 2019',
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

