<template>
  <div class="home">
      <h1>SignalR Test</h1>

        <p>this is a signalr test</p>

        <input/>
        <button v-on:click="test">click me</button>
        
        <table>
            <tr v-for="message in messages" v-bind:key="message.id">
                <td>{{message.user}}</td>
                <td>{{message.message}}</td>
            </tr>
        </table>

      
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import SignalR from '@/signalr';

export default {
    name: 'home',
    components: {
        HelloWorld
    },
    data: function(){
        return{
            messages: []
        }
    },
    mounted: function(){
        const self = this;
        const events = [
            {
                name: 'notification',
                event: function(args){
                    console.log(`event: ${args[0]} ${args[1]}`);
                    self.messages.push(
                        {
                            id: self.messages.length,
                            user: args[0],
                            message: args[1]
                        }   
                    );
                }
            }
        ];  
        //debugger;
        SignalR.init(events);
    },
    methods: {
        test: function(events){
            SignalR.send();
        }
    }
}
</script>
