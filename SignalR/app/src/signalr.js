var jq = require('jquery');
window.$ = jq;
window.jQuery = jq;
var signalr = require('signalr');

var baseUrl = 'http://localhost:8080/api/v1/notifications'
$.connection.hub.url = baseUrl
debugger;
var connection = $.hubConnection('http://localhost:8080/api/v1/notifications', { useDefaultPath: false, withCredentials: false, logging: true });
connection.url = baseUrl
connection.withCredentials = false

var notificationHubProxy = connection.createHubProxy('notificationHub');

connection.start({withCredentials: false, logging: true, useDefaultPath: false})
    .done(
        function(){
            alert('connected');
        }
    )
    .fail(
        function(error){ 
            console.log('error');
            console.log(error);
            alert('Could not Connect!'); 
        }
    );

//var test = new signalr.HubConnectionBuilder();
//signalr
//import { HubConnectionBuilder, LogLevel } from 'signalr'
 
/*
const connection = new HubConnectionBuilder()
  .withUrl('http://localhost:8080/api/v1/notifications')
  .configureLogging(LogLevel.Information)
  .build()
 
connection.start()
*/