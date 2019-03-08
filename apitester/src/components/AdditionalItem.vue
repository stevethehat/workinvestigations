<template>
    <div>
        <b-container>
            <b-row>
                <b-col sm="12" class="col">
                    Name: <input type="text" style="width:max" v-model="internalAdditionalItem.Name"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col sm="12" class="col">
                    Description: <input type="text" style="width:max" v-model="internalAdditionalItem.Description"/>
                </b-col>
            </b-row>
            <b-row>
                <b-col>
                    Part Id: 
                    <input type="text" v-model="internalAdditionalItem.PartId"/>
                </b-col>
                <b-col>
                    Price: 
                    <input type="number" v-model="internalAdditionalItem.Price"/>
                </b-col>
            </b-row>
        </b-container>
    </div>
</template>

<script lang="ts">

import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
import { apiTester }  from '@/util/ApiTester.ts';
import { CPQAdditionalItem } from '@/util/cpq.ts';

import router                   from '@/router';


@Component({})
export default class AdditionalItem extends Vue{
    @Prop({})
    public additionalItem!: CPQAdditionalItem;
    private internalAdditionalItem: CPQAdditionalItem = this.additionalItem;
    constructor(){
        super();
        this.internalAdditionalItem= this.additionalItem;
    }
    @Watch('internalAdditionalItem')
    internalAdditionalItemChanged(newValue: CPQAdditionalItem, oldValue: CPQAdditionalItem){
        if(newValue !== oldValue) {
            this.$emit('input', newValue);
        }
    }
    @Watch('additionalItem')
    additionalItemChanged(newValue: CPQAdditionalItem, oldValue: CPQAdditionalItem){
        if(newValue !== oldValue) {
            this.internalAdditionalItem = newValue;
        }
    }
}
</script>
