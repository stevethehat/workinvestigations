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
        this.CSName = _.upperFirst(_.camelCase(name.substring(modelName.length -3).replace(/_/g, ' ')));
    }

    getTokenPosition(): vscode.Position | null{
        return this.find(`${this.CSName} { get; set; }`);
    }

    getHover(): vscode.MarkdownString{
        const message 	            = new vscode.MarkdownString();
        const declarationPosition   = this.getTokenPosition();
        
        message.appendMarkdown(`### ${this.CSName} ###\r`);
        if (null !== declarationPosition) {
            message.appendMarkdown('___\r');

            const isamFieldInfoPos = this.findPrevious(declarationPosition, /\[IsamField\(\d+, \d+\)\]/);
            if (null !== isamFieldInfoPos) {
                //message.appendText(`${Field.extractFieldPosition(this.TextLines[isamFieldInfoPos.line])}\r`);
                const fieldPosition = Field.extractFieldPosition(this.TextLines[isamFieldInfoPos.line]);
                message.appendText(`Position ${fieldPosition[0]} - ${fieldPosition[1]}\r`);
            }            
            message.appendMarkdown('___\r');
            message.appendMarkdown('   \r');

            const startPos  = this.findPrevious(declarationPosition, new RegExp('^\\s*$'));
            const endPos    = this.findNext(declarationPosition, new RegExp('^\\s*$'));

            if (null !== startPos && null !== endPos) {
                const code = `\r${this.getLineRange(startPos.line +1, endPos.line +1)}\r  \r`;
                message.appendCodeblock(code, 'csharp');                    
            }
            message.appendMarkdown('   \r');
        }

        return message;
    }

    static extractFieldPosition(line: string): [number, number] {
        line            = line.trimLeft();
        line            = line.replace('[IsamField(', '').replace(')]', '');
        const commaPos  = line.indexOf(',');
        const start     = Number(line.substr(0, line.indexOf(',')));
        const len = Number(line.substr(commaPos + 1));
        return [start, start + len - 1];
    }

    getFileName(): string {
        return this.Model.FileName;   
    }
}