<template>
    <div>
        <p>do we see a form</p>
        <input type="text" v-model="value"/>
        <p>{{value}}</p>

        <labelinput label="the label" v-model="value"/>

        <labelinput label="the second label" v-model="value2"/>

        <objlabelinput label="the object label" v-model="objvalue"/>

        <button @click="submitForm">Click Me</button>

        <p>{{objvalue.val1}}</p>
    </div>
</template>


<script lang="ts">
import Vue from 'vue';
import Labelinput from '@/components/labelinput.vue';
import Objlabelinput from '@/components/objlabelinput.vue';

export default Vue.extend({
    name: 'formtest',
    components: {
        Labelinput,
        Objlabelinput,
    },
    data() {
        return {
            value: 10,
            value2: 22,
            objvalue: {
                val1: 1,
                val2: 222,
            },
        };
    },
    methods: {
        submitForm() {
            alert(`${this.value} ${this.value2} ${JSON.stringify(this.objvalue)}`);
        },
    },
});
</script>
