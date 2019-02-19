import * as vscode  from 'vscode';
import * as _       from 'lodash';

import { Base }     from "./base";
import { Model }    from "./model";

export class Field extends Base{
    Model   : Model;
    CSName  : string;
    
    constructor(name: string, modelName: string) {
        super(name);
        this.Model  = new Model(modelName);
        this.CSName = _.upperFirst(_.camelCase(name.substring(3).replace(/_/g, ' ')));
    }

    getTokenPosition(): vscode.Position | null{
        return this.find(`${this.CSName} { get; set; }`);
    }

    getHover(): vscode.MarkdownString{
        const message 	            = new vscode.MarkdownString();
        const declarationPosition   = this.getTokenPosition();
        
        message.appendMarkdown(`### ${this.CSName} ###\n`);
        if (null !== declarationPosition) {
            message.appendText('___\n');

            const isamFieldInfoPos = this.findPrevious(declarationPosition, /\[IsamField\(\d+, \d+\)\]/);
            if (null !== isamFieldInfoPos) {
                message.appendText(`${this.TextLines[isamFieldInfoPos.line]}\n`);
            }            

            const startPos  = this.findPrevious(declarationPosition, new RegExp('^\\s*$'));
            const endPos    = this.findNext(declarationPosition, new RegExp('^\\s*$'));

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