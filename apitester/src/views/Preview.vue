<template>
    <div class="preview">
        <h1>Preview</h1>

        <editor v-model="content" @init="editorInit" lang="html" theme="chrome" width="500" height="100"></editor>
        <!--
        <editor v-model="exsamplecontent"
                        v-bind:options="exsampleoptions">
        </editor>
        -->
    </div>
</template>

<script lang="ts">

import { Component, Vue, Watch } from 'vue-property-decorator';
var editor =  require('vue2-ace-editor');
//import Editor from '@/vue2-ace-editor';

@Component
export default class Preview extends Vue {
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
}
</script>
