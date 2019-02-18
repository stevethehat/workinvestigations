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
    getHover(): MarkdownString{
        var message 	= new vscode.MarkdownString();
        message.appendMarkdown(`### ${this.CSName} ###\n`);
        
        const declerationPosition = this.find(`${this.CSName} { get; set; }`);
        if (null !== declerationPosition) {
            message.appendText('___\n');
            const startPos = this.findPrevious(declerationPosition, new RegExp('^\\s*$'));
            const endPos = this.findNext(declerationPosition, new RegExp('^\\s*$'));

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