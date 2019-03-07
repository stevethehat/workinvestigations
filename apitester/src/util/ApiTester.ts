import { Dictionary } from 'vuex';

export class TestHost{
    Host: string = '';
    Username: string = '';
    Password: string = '';
    Protocol: string = '';
}

export class ApiTester{
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

    post(url: string, data: any) {
        var self = this;

        const xhr = new XMLHttpRequest();
        xhr.open('POST', url);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.setRequestHeader('Authorization', this._token);
        xhr.onload = function() {
            if (200 === xhr.status) {
            } else {
            }
        };
        xhr.send(JSON.stringify(data));    
    }

    login(host: string){
        const self = this;
        self._host = host;

        const xhr = new XMLHttpRequest();

        xhr.open('POST', this._hosts[this._host].Host + '/api/v1/account/login');
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onload = function() {
            if (200 === xhr.status) {
                const response = JSON.parse(xhr.responseText);
                self._token = response['Token'];
            }
            else if (200 !== xhr.status) {
            }
        };
        xhr.send(encodeURI('Username=' + self._hosts[self._host].Username + '&Password=ibcos.1234&Applicationid=bc83bacc-483f-47aa-8f8c-eff98586b146'));    
    }
}

export var apiTester: ApiTester = new ApiTester();
