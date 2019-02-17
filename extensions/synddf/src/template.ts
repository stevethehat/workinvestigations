import * as vscode from 'vscode';
import { Base } from './base';
let fs = require('fs');
let path = require('path');


export class Template extends Base {
    getFileName(): string {
        return path.join(this._config.get('repositoryRootFolder'), 'template', `${this.Name}.DDF`);
    }
    getHover(): vscode.MarkdownString{
        return new vscode.MarkdownString();
    }
}