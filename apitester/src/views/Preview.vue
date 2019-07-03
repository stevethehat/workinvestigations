<template>
    <div class="pageContent">
        <p>{{StatusMessage}}</p>
        <editor v-model="data" @init="editorInit" lang="json" theme="monokai" width="800" height="600"></editor>
        <b-button-toolbar>
            <b-button-group size="sm">
                <b-button variant="primary" @click="back">&lt; Back</b-button>&nbsp;
                <b-button variant="primary" @click="go">Go</b-button>&nbsp;
                <b-button variant="primary" @click="reset">Reset preview</b-button>&nbsp;
                <b-button v-if="'' !== ActualSentData" variant="primary" @click="retry">Reload sent data</b-button>
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
    private ActualSentData: string        = '';

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
        self.ActualSentData = self.data;
        // http://localhost:8080/api/v1/manufacturer/agco/cpq

        apiTester.post('manufacturer/agco/cpq', JSON.parse(self.ActualSentData), function(result){
            if(false === result){
                
            } else {
                self.data = JSON.stringify(result, null, 2);
            }
            
        });
    }

    retry(){
        const self = this;

        self.data = self.ActualSentData;
        self.ActualSentData = '';
    }

    reset(){
        const self = this;

        self.data = JSON.stringify(apiTester.RequestData, null, 2);
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
