<template>
    <div>
        <b-container>
            <b-row>
                <b-col>
                    Stock Number:
                    <input type="text" v-model="internalWholegood.StockNumber"/>
                </b-col>
                <b-col>
                    Make: 
                    <input type="text" v-model="internalWholegood.Make"/>
                </b-col>
                <b-col>
                    Model: 
                    <input type="text" v-model="internalWholegood.Model"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    Price:
                    <input type="number" v-model="internalWholegood.Price"/>
                </b-col>
                <b-col>
                    List Price: 
                    <input type="number" v-model="internalWholegood.ListPrice"/>
                </b-col>
                <b-col>
                    Discount: 
                    <input type="number" v-model="internalWholegood.DealerDiscount"/>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script lang="ts">

import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import { apiTester }  from '@/util/ApiTester.ts';
import { CPQWholegood } from '@/util/cpq.ts';

import router                   from '@/router';


@Component({})
export default class Wholegood extends Vue{
    @Prop({})
    public wholegood!: CPQWholegood;
    private internalWholegood: CPQWholegood = this.wholegood;
    constructor(){
        super();
        this.internalWholegood = this.wholegood;
    }
    @Watch('internalWholegood')
    internalWholegoodChanged(newValue: CPQWholegood, oldValue: CPQWholegood){
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
    @Watch('wholegood')
    wholegoodChanged(newValue: CPQWholegood, oldValue: CPQWholegood){
        if(newValue !== oldValue) {
            this.internalWholegood = newValue;
        }
    }
}
</script>
