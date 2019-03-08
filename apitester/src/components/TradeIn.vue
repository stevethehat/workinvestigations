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
            <b-row>
                <b-col sm="12" class="col">
                    Description: <input type="text" style="width:max" v-model="internalTradeIn.Description"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col sm="12" class="col">
                    Condition: <input type="text" style="width:max" v-model="internalTradeIn.Condition"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    Compensation:
                    <input type="number" v-model="internalTradeIn.CompensationValue"/>
                </b-col>
                <b-col>
                    Book Price: 
                    <input type="number" v-model="internalTradeIn.BookValue"/>
                </b-col>
                <b-col>
                    <!-- Model:  -->
                    <!-- <input type="text" v-model="internalTradeIn.Model"/> -->
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
    public tradeIn!: CPQTradeIn;
    private internalTradeIn: CPQTradeIn = this.tradeIn;
    constructor(){
        super();
        this.internalTradeIn = this.tradeIn;
    }
    @Watch('internalTradeIn')
    internalTradeInChanged(newValue: CPQTradeIn, oldValue: CPQTradeIn){
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
    @Watch('wholegtradeinood')
    tradeInChanged(newValue: CPQTradeIn, oldValue: CPQTradeIn){
        if(newValue !== oldValue) {
            this.internalTradeIn = newValue;
        }
    }
}
</script>
