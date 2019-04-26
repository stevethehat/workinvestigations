<template>
    <div class="pageContent">
        {{StatusMessage}}
        <editor v-model="data" @init="editorInit" lang="json" theme="monokai" width="800" height="600"></editor>
        <b-button-toolbar>
            <b-button-group size="sm">
                <b-button variant="primary" @click="back">&lt; Back</b-button>&nbsp;
                <b-button variant="primary" @click="go">Go</b-button>&nbsp;
                <b-button v-if="'' !== DataSent" variant="primary" @click="retry">Retry</b-button>
            </b-button-group>
        </b-button-toolbar>
    </div>
</template>

<script lang="ts">

import { Component, Vue, Watch }    from 'vue-property-decorator';
import { apiTester }                from '@/util/ApiTester.ts';

import router                       from '@/router';
const editor                        = require('vue2-ace-editor');

@Component(
    {
        components: {
            editor
        }
    }
)
export default class Preview extends Vue {
    public data: string             = JSON.stringify(apiTester.RequestData, null, 2);
    public StatusMessage: string    = '';
    private DataSent: string        = '';

    constructor(){
        super();
        this.StatusMessage              = apiTester.UserDetails.StatusMessage;
        var title: HTMLElement | null   = document.getElementById('pageTitle');
        if(null !== title){
            title.innerHTML             = 'Preview';
        }
    }

    go(){
        const self = this;
        self.DataSent = self.data;
        // http://localhost:8080/api/v1/manufacturer/agco/cpq

        //apiTester.post('manufacturer/agco/cpq', apiTester.RequestData, function(result){
        apiTester.post('manufacturer/agco/cpq', self.DataSent, function(result){
            if(false === result){
                
            } else {
                self.data = JSON.stringify(result, null, 2);
            }
            
        });
    }

    retry(){
        const self = this;

        self.data = self.DataSent;
        self.DataSent = '';
    }

    back(){
        router.push('cpq');
    }

    editorInit() {
        require('brace/ext/language_tools') //language extension prerequsite...
        require('brace/mode/json')    //language
        require('brace/mode/less')
        require('brace/theme/monokai')
        require('brace/snippets/json') //snippet
    }
}
</script>
