var jq = require('jquery');
window.$ = jq;
window.jQuery = jq;
var signalr = require('signalr');

/*
var baseUrl = 'http://localhost:8080/api/v1/notifications'
var connection = $.hubConnection(baseUrl, { useDefaultPath: false, logging: true });

var notificationHubProxy = connection.createHubProxy('notificationHub');
var testHubProxy = connection.createHubProxy('testHub');

connection.start()
    .done(
        function(){
            console.log('connected');
        }
    )
    .fail(
        function(error){ 
            console.log('error');
            console.log(error);
            alert('Could not Connect!'); 
        }
    );
*/

const ibcdev = 'http://172.16.128.21:8080/api/v1/notifications';
const dev = 'http://localhost:8080/api/v1/notifications';

const SignalR = {
    baseUrl: ibcdev,
    init(events){
        var self = this;
        this.connection = $.hubConnection(this.baseUrl, { useDefaultPath: false, logging: true });
        this.notificationHubProxy = this.connection.createHubProxy('notificationHub');

        for(var eventIndex in events){
            const event = events[eventIndex];
            this.notificationHubProxy.on(event.name, 
                function(){
                    event.event(arguments);
                }
            );
        }

        this.connection.start()
            .done(
                function(){
                    console.log('connected');
                }
            )
            .fail(
                function(error){ 
                    console.log('error');
                    console.log(error);
                    alert('Could not Connect!'); 
                }
            );
    },
    gotNotification: function(){

    },
    send(){
        this.notificationHubProxy.invoke('notificationTest2', 'steve', 'hello')
        .done(
            function(){
                console.log('we sent a message');
            }
        )
    }
};
      
export default SignalR;
