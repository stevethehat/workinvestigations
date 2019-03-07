<template>
    <div class="login">
        <h1>API Tester</h1>
        <div>
            <label>Host</label>
            <select type="text" v-model="Host">
                <option v-for="host in Hosts" v-bind:key="host" :value="host" :v-model="Host">
                    {{host}}
                </option>
            </select>
        </div>
        <button @click="login">Login</button>
    </div>
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
        apiTester.login(this.Host);
        router.push('cpq');
    }
}
</script>
