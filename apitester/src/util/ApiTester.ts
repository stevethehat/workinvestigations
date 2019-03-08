import { Dictionary } from 'vuex';

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
    public RequestData: object = {};
    get LoggedIn(): boolean{
        return '' !== this._token;
    }
    get Host(): string{
        return this._host
    }
    public StatusMessage: string = 'Not logged in.'

    private _token: string = '';
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

    constructor(){
        if(undefined !== window.loginDetails){
            this._host = window.loginDetails.host;
            this._token = window.loginDetails.host;
            this.StatusMessage = window.loginDetails.statusMessage;
        }
    }

    getUrl(url: string): string{
        return this._hosts[this._host].Host + `/api/v1/${url}`;
    }

    error(callback: IBoolCallback){
        alert('Api connection error.');
        callback(false);
    }
    
    get(url: string) {
        const xhr = new XMLHttpRequest();
        xhr.open('GET', url);
        xhr.setRequestHeader('Authorization', this._token);
        xhr.onload = function() {
            if (200 === xhr.status) {
            } else {
            }
        };
        xhr.send();    
    }


    post(url: string, data: any, callback: IDataCallback) {
        var self = this;

        const xhr = new XMLHttpRequest();
        const fullUrl = this.getUrl(url);
        console.log(`POST to ${fullUrl} ${this._token}`)
        xhr.open('POST', fullUrl);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.setRequestHeader('Authorization', this._token);
        xhr.onerror = () => this.error(callback); 
        xhr.onload = function() {
            if (200 === xhr.status) {
                callback(JSON.parse(xhr.responseText));
            } else {
                alert(`Error calling ${fullUrl}`);
                callback(JSON.parse(xhr.responseText));
            }
        };
        xhr.send(JSON.stringify(data));    
    }

    login(host: string, callback: IBoolCallback){
        const self = this;
        self._host = host;

        const xhr = new XMLHttpRequest();

        xhr.open('POST', this.getUrl('account/login'));
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onerror = () => this.error(callback); 
        xhr.onload = function() {
            if (200 === xhr.status) {
                const response = JSON.parse(xhr.responseText);
                self._token = response['Token'];
                self.StatusMessage = `Logged in: ${self._hosts[self._host].Username}@${self._host} [${self._token}].`;
                
                window.loginDetails = {
                    host: self._host,
                    token: self._token,
                    statusMessage: self.StatusMessage
                }

                callback(true);
            }
            else if (200 !== xhr.status) {
                alert(`Could not log in. ${xhr.statusText} [${xhr.status}]`);
                callback(false);
            }
        };
        xhr.send(encodeURI('Username=' + self._hosts[self._host].Username + '&Password=ibcos.1234&Applicationid=bc83bacc-483f-47aa-8f8c-eff98586b146'));    
    }
}

export var apiTester: ApiTester = new ApiTester();
