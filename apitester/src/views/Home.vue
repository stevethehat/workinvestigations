<template>
    <div class="pageContent">
        <b-card>
            <login>

            </login>
        </b-card>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Login from '@/components/Login.vue'; // @ is an alias to /src

@Component({
  components: {
    Login,
  },
})
export default class Home extends Vue {
    constructor(){
        super();
        var title: HTMLElement | null = document.getElementById('pageTitle');
        if(null !== title){
            title.innerHTML = 'Preview';
        }

    }
}
</script>
