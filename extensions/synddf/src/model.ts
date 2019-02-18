import * as vscode from 'vscode';
import * as _ from 'lodash';
import { Base } from './base';
let fs = require('fs');
let path = require('path');


export class Model extends Base{
    getFileName(): string {
        const root = this._config.get('modelRootFolder');
        const fileName = `${_.upperFirst(_.lowerCase(this.Name))}.cs`;
        return path.join(root, fileName);
    }
    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0,0);
    }
}