<template>
    <b-container>
        <b-row>
            <b-col>
                <label>Host</label>
                <select type="text" v-model="Host">
                    <option v-for="host in Hosts" v-bind:key="host" :value="host" :v-model="Host">
                        {{host}}
                    </option>
                </select>
            </b-col>
            <b-col sm="8"></b-col>
        </b-row>
        <button @click="login">Login</button>
    </b-container>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { apiTester, TestHost }  from '@/util/ApiTester.ts';

import router                   from '@/router';

@Component({})
export default class Login extends Vue {
    public Hosts    : Array<string> = ['localhost', 'uat', 'staging', 'ibcdev'];
    public Host     : string        = 'localhost';

    login(){
        apiTester.login(this.Host, function(ok: boolean){
            if(ok){
                router.push('cpq');
            }
        });
        
    }
}
</script>
