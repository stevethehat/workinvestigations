<template>
  <div class="home">
    <p>hi</p>
    <img alt="Vue logo" src="../assets/logo.png">
    <formtest/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Formtest from '@/views/formtest.vue';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src

@Component({
  components: {
    HelloWorld,
    Formtest,
  },
})
export default class Home extends Vue {}
</script>
