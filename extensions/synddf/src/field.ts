import * as vscode from 'vscode';
import * as _ from 'lodash';

import { Base } from "./base";
import { Model } from "./model";
import { MarkdownString } from "vscode";
import { start } from 'repl';

export class Field extends Base{
    
    Model: Model;
    CSName: string;
    constructor(name: string, modelName: string) {
        super(name);
        this.Model = new Model(modelName);
        this.CSName = _.upperFirst(_.camelCase(name.substring(3).replace(/_/g, ' ')));
    }
    getTokenPosition(): vscode.Position | null{
        return this.find(`${this.CSName} { get; set; }`);
    }
    getHover(): MarkdownString{
        var message 	= new vscode.MarkdownString();
        message.appendMarkdown(`### ${this.CSName} ###\n`);
        const declarationPosition = this.getTokenPosition();
        
        if (null !== declarationPosition) {
            message.appendText('___\n');
            const isamFieldInfoPos = this.findPrevious(declarationPosition, new RegExp('[IsamField(\\d, \\d)]'));

            const startPos = this.findPrevious(declarationPosition, new RegExp('^\\s*$'));
            const endPos = this.findNext(declarationPosition, new RegExp('^\\s*$'));

            if (null !== startPos && null !== endPos) {
                const code = this.getLineRange(startPos.line +1, endPos.line);
                message.appendCodeblock(code, 'csharp');                    
            }
            message.appendText('\n\n');
        }

        return message;
    }
    getFileName(): string {
        return this.Model.FileName;   
    }
}