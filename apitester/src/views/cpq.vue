<template>
    <div class="login">
        <h1>CPQ</h1>
        {{Host}}

        <div v-for="wholegood in Wholegoods" v-bind:key="wholegood.StockNumber">
            <b-card :title="wholegood.StockNumber">
                <wholegood :wholegood="wholegood">
                </wholegood>
            </b-card>
        </div>
        <button @click="add">Add</button>
        <button @click="go">Go</button>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue }     from 'vue-property-decorator';

import { apiTester }                from '@/util/ApiTester.ts';
import { CPQWholegood }             from '@/util/cpq.ts';

import Wholegood                    from '@/components/Wholegood.vue';

@Component({
    components:{
        Wholegood,
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

    constructor(){
        super();
        this.Host = apiTester.Host;
    }

    add(){
        this.Wholegoods.push(new CPQWholegood());
    }
    go(){
        alert(JSON.stringify(this.Wholegoods, null, 2));
    }
}
</script>

