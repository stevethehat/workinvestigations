import { Dictionary } from 'vuex';
//import store from '@/store.ts';
import Vue      from 'vue'
import Vuex     from 'vuex'

Vue.use(Vuex)

interface IUserDetails{
    Token           : string;
    Username        : string;
    StatusMessage   : string;
    Data : any;
}


export const store = new Vuex.Store({
    state: {
        LoggedIn: false,
        Details: {
            Token: '',
            Username: '',
            StatusMessage: '',
        },
        RequestData: {}
    },
    mutations: {
        login(state, details){
            state.LoggedIn = true;
            state.Details = details;
        },
        logout: state => state.LoggedIn = false,
    },
    getters: {
        userDetails: state => state.Details
    },
    actions: {

    }
});

interface IDataCallback{
    (data: object | boolean): void;
}

interface IBoolCallback{
    (ok: boolean): void;
}

export class TestHost{
    Host        : string = '';
    Username    : string = '';
    Password    : string = '';
    Protocol    : string = '';
}


export class ApiTester{
    get RequestData(): object{
        return store.state.RequestData;
    }
    set RequestData(value: object){
        store.state.RequestData = value;
    }
    get LoggedIn(): boolean{
        var result = false;
        if(undefined !== this.UserDetails.Token){
            result = true;
        }
        return result;
    }
    get Host(): string{
        return this._host
    }
    get UserDetails(){
        this.getUserDetails();
        return this._userDetails;
    }

    private _hosts: Dictionary<TestHost> = {
        'localhost':{
            Host        : 'http://localhost:8080',
            Username    : 'steve.lamb@ibcos.co.uk',
            Password    : 'ibcos.1234',
            Protocol    : 'http'
        },
        'uat': {
            Host        : 'https://agco-ecommerce.ibcos.gold',
            Username    : 'steve.lamb@ibcos.co.uk',
            Password    : 'ibcos.1234',
            Protocol    : 'https'
        },
        'staging': {
            Host        : 'http://ibpos-stage-master.ibcos.co.uk:8080',
            Username    : 'agco@ibcos.co.uk',
            Password    : 'ibcos.1234',
            Protocol    : 'http'
        },
        'ibcdev': {
            Host        : 'http://172.16.128.21:8080',
            Username    : 'steve.lamb@ibcos.co.uk',
            Password    : 'ibcos.1234',
            Protocol    : 'http'
        }
    }
    private _host: string = 'localhost';
    private _userDetails!: IUserDetails;

    constructor(){
        const test = store;
        
        this.getUserDetails();
    }
    
    get(url: string) {
        const xhr = new XMLHttpRequest();
        xhr.open('GET', url);
        xhr.setRequestHeader('Authorization', this.UserDetails.Token);
        xhr.onload = function() {
            if (200 === xhr.status) {
            } else {
            }
        };
        xhr.send();    
    }


    post(url: string, data: any, callback: IDataCallback) {
        var self = this;

        self.getUserDetails();        

        const xhr = new XMLHttpRequest();
        const fullUrl = this.getUrl(url);
        console.log(`POST to ${fullUrl} ${this.UserDetails.Token}`)
        xhr.open('POST', fullUrl);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.setRequestHeader('Authorization', this.UserDetails.Token);
        xhr.onerror = () => this.error(callback); 
        xhr.onload = function() {
            if (200 === xhr.status) {
                callback(JSON.parse(xhr.responseText));
            } else {
                //alert(`Error calling ${fullUrl}`);
                callback(JSON.parse(xhr.responseText));
            }
        };
        xhr.send(JSON.stringify(data));    
    }

    getUrl(url: string): string{
        return this._hosts[this._host].Host + `/api/v1/${url}`;
    }

    error(callback: IBoolCallback){
        alert('Api connection error.');
        callback(false);
    }

    getUserDetails(){
        if(undefined === this._userDetails){
            const test = store;
            //debugger;
            
            const userDetails = store.state.Details;
            if(undefined !== userDetails){
                this._userDetails = userDetails;
            }
        }
    }

    login(host: string, callback: IBoolCallback){
        const self = this;
        const userName = self._hosts[self._host].Username;

        self._host = host;

        const xhr = new XMLHttpRequest();

        xhr.open('POST', this.getUrl('account/login'));
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onerror = () => this.error(callback); 
        xhr.onload = function() {
            if (200 === xhr.status) {
                const response = JSON.parse(xhr.responseText);

                self._userDetails = {
                    Token: response['Token'],
                    Username: userName,
                    StatusMessage: `Logged in: ${userName}@${self._host} [${response['Token']}].`
                };

                store.commit('login', self._userDetails);

                callback(true);
            }
            else if (200 !== xhr.status) {
                alert(`Could not log in. ${xhr.statusText} [${xhr.status}]`);
                callback(false);
            }
        };
        xhr.send(encodeURI('Username=' + userName + '&Password=ibcos.1234&Applicationid=bc83bacc-483f-47aa-8f8c-eff98586b146'));    
    }
}

export var apiTester: ApiTester = new ApiTester();
