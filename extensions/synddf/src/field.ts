import * as vscode  from 'vscode';
import * as _       from 'lodash';

import { Base }     from "./base";
import { Model }    from "./model";

export class Field extends Base{
    Model   : Model;
    CSName  : string;
    Prefix  : string;
    
    constructor(name: string, modelName: string) {
        super(name);
        this.Model  = new Model(modelName);
        this.Prefix = this.Name.substring(0, modelName.length -3);
        this.CSName = Field.getCSName(name, this.Prefix);
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
                const [start, end] = Field.extractFieldPosition(this.TextLines[isamFieldInfoPos.line]);
                message.appendText(`Position ${start} - ${end}\r`);
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

    static getCSName(name: string, prefix: string): string{
        return _.upperFirst(_.camelCase(name.substring(prefix.length).replace(/_/g, ' ')));
    }

    static getDDFName(name: string, prefix: string){
        let ddfName = `${prefix}`;

        for(let i = 0;i < name.length;i++){
            let char = name.substr(i, 1);
            if(char.toUpperCase() === char && isNaN(Number(char))){
                ddfName = `${ddfName}_${char}`;
            } else{
                ddfName = `${ddfName}${char}`;
            }
        }

        return ddfName.toUpperCase();
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