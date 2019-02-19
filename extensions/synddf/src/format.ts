import * as vscode from 'vscode';
import { Base } from './base';
let fs = require('fs');
let path = require('path');


export class Format extends Base {
    getFileName(): string {
        return path.join(this._config.get('repositoryRootFolder'), 'format', `${this.Name}.DDF`);
    }
    getHover(): vscode.MarkdownString{
        return new vscode.MarkdownString();
    }
    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0,0);
    }
}