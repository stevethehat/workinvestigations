<template>
    <div class="pageContent">
        {{StatusMessage}}
        <pre class="requestPreview">{{data}}</pre>

        <b-button-toolbar>
            <b-button-group size="sm">
                <b-button variant="primary" @click="go">Go</b-button>
            </b-button-group>
        </b-button-toolbar>
        
        <!--
        <editor v-model="content" @init="editorInit" lang="html" theme="chrome" width="500" height="100"></editor>
        <editor v-model="exsamplecontent"
                        v-bind:options="exsampleoptions">
        </editor>
        -->
    </div>
</template>

<script lang="ts">

import { Component, Vue, Watch } from 'vue-property-decorator';
import { apiTester }                from '@/util/ApiTester.ts';

@Component
export default class Preview extends Vue {
    public data: string = JSON.stringify(apiTester.RequestData, null, 2);
    public StatusMessage: string = ''

    constructor(){
        super();
        this.StatusMessage = apiTester.StatusMessage;
        var title: HTMLElement | null = document.getElementById('pageTitle');
        if(null !== title){
            title.innerHTML = 'Preview';
        }
    }

    go(){
        const self = this;
        // http://localhost:8080/api/v1/manufacturer/agco/cpq

        apiTester.post('manufacturer/agco/cpq', apiTester.RequestData, function(result){
            self.data = JSON.stringify(result, null, 2);
        });
    }
    /*
    //public 
    data(){
        return {
            content: 'hello'
        }
    }
    components =  {
        editor: editor
    }
        editorInit() {
            require('brace/ext/language_tools') //language extension prerequsite...
            require('brace/mode/html')                
            require('brace/mode/javascript')    //language
            require('brace/mode/less')
            require('brace/theme/chrome')
            require('brace/snippets/javascript') //snippet
    }
    */
}
</script>
