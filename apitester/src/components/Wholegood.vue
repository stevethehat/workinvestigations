<template>
    <div>
        <b-container>
            <b-row>
                <b-col>
                    Configuration Id:
                    <input type="text" v-model="internalWholegood.ConfigurationId"/>
                </b-col>
                <b-col>
                    Quantity:
                    <input type="number" v-model="internalWholegood.Quantity"/>
                </b-col>
                <b-col>
                    Series: 
                    <input type="text" v-model="internalWholegood.Series"/>
                </b-col>
                <b-col>
                    Model: 
                    <input type="text" v-model="internalWholegood.Model"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    Price pre VAT: 
                    <input type="number" v-model="internalWholegood.PriceBeforeVat"/>
                </b-col>
                <b-col>
                    Discount: 
                    <input type="number" v-model="internalWholegood.DealerDiscount"/>
                </b-col>
            </b-row>
            <!--
            <b-row>
                <b-col cols="9">
                    Warranty: 
                    <input type="string" v-model="internalWholegood.WarrantyDescription"/>
                </b-col>
                <b-col clas="3">
                    Cost: 
                    <input type="number" v-model="internalWholegood.WarrantyCost"/>
                </b-col>
            </b-row>
            -->
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
