<template>
    <b-container>
        <b-row>
            <b-col>
                <label>Customer: </label>
                <input type="text" v-model="internalCustomer"/>
            </b-col>
            <b-col sm="8"></b-col>
        </b-row>
    </b-container>
</template>

<script lang="ts">
import { Component, Prop, Watch, Vue } from 'vue-property-decorator';
import { apiTester, TestHost }  from '@/util/ApiTester.ts';


@Component({})
export default class Customer extends Vue {
    @Prop({})
    public customer!: string;
    
    private internalCustomer: string = this.customer;

    constructor(){
        super();
        this.internalCustomer = this.customer;
    }

    @Watch('internalCustomer')
    internalTradeInChanged(newValue: string, oldValue: string){
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
    @Watch('wholegtr')
    tradeInChanged(newValue: string, oldValue: string){
        if(newValue !== oldValue) {
            this.internalCustomer = newValue;
        }
    }

}
</script>
