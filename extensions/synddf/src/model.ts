import * as vscode from 'vscode';
import * as _ from 'lodash';
import { Base } from './base';
import { Field } from './field';
let fs = require('fs');
let path = require('path');


class FieldInfo{
    Name: string = '';
    LineNo: number = 0;
}

export class Model extends Base{
    Prefix  : string;

    constructor(name: string) {
        super(name);
        this.Prefix = this.Name.substring(0, this.Name.length -3);
    }

    getFieldAtPosition(position: number): FieldInfo | null {
        let fieldInfo = null;
        for (var lineNo in this.TextLines) {
            const line = this.TextLines[lineNo].trimLeft();
            if (line.startsWith('public')) {
                const fieldName = line.split(' ')[2];
                const isamFieldInfoPos = this.findPrevious(new vscode.Position(Number(lineNo), 0), /\[IsamField\(\d+, \d+\)\]/);
                if (null !== isamFieldInfoPos && 0 !== isamFieldInfoPos.line) {
                    const [start, end] = Field.extractFieldPosition(this.TextLines[isamFieldInfoPos.line]);
                    if (position >= start && position <= end) {
                        fieldInfo = {
                            Name: Field.getDDFName(fieldName, this.Prefix),
                            LineNo: Number(lineNo)
                        };
                        
                        vscode.window.showInformationMessage(`${fieldInfo.Name}`);                                            
                    }
                }
            }
        }

        return fieldInfo;
    }
 
    getFileName(): string {
        const root = this._config.get('modelRootFolder');
        const fileName = `${_.upperFirst(_.lowerCase(this.Name))}.cs`;
        return path.join(root, fileName);
    }

    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0,0);
    }
}