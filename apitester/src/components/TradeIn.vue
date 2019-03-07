<template>
    <div>
        <b-container>
            <b-row>
                <b-col>
                    Serial Number:
                    <input type="text" v-model="internalTradeIn.SerialNumber"/>
                </b-col>
                <b-col>
                    Make: 
                    <input type="text" v-model="internalTradeIn.Make"/>
                </b-col>
                <b-col>
                    Model: 
                    <input type="text" v-model="internalTradeIn.Model"/>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script lang="ts">

import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import { apiTester }  from '@/util/ApiTester.ts';
import { CPQTradeIn} from '@/util/cpq.ts';

import router                   from '@/router';


@Component({})
export default class TradeIn extends Vue{
    @Prop({})
    public tradein!: CPQTradeIn;
    private internalTradeIn: CPQTradeIn = this.tradein;
    constructor(){
        super();
        this.internalTradeIn = this.tradein;
    }
    @Watch('internalWholegood')
    internalWholegoodChanged(newValue: CPQTradeIn, oldValue: CPQTradeIn){
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
    @Watch('wholegood')
    wholegoodChanged(newValue: CPQTradeIn, oldValue: CPQTradeIn){
        if(newValue !== oldValue) {
            this.internalTradeIn = newValue;
        }
    }
}
</script>
