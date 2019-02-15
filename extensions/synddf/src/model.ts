import * as vscode from 'vscode';
import * as _ from 'lodash';
import { Base } from './base';
let fs = require('fs');
let path = require('path');


export class Model extends Base{
    getFileName(): string {
        return path.join(this._config.get('modelRootFolder'), `${_.upperFirst(_.lowerCase(this.Name))}.cs`);
    }
}