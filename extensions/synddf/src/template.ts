import * as vscode from 'vscode';
import { Base } from './base';
import { Parse } from './parse';
let fs = require('fs');
let path = require('path');


export class Template extends Base {
    getFileName(): string {
        return path.join(this._config.get('repositoryRootFolder'), 'template', `${this.Name}.DDF`);
    }
    getHover(): vscode.MarkdownString{
        const message =  new vscode.MarkdownString();
        message.appendMarkdown(`### ${this.Name} ###\r`);
        message.appendMarkdown(`___\r`);
        message.appendMarkdown(`   \r`);

        const p = new Parse(this.TextLines);
        p.parseBlock();
        return message;
    }
    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0,0);
    }
}