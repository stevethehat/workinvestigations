import * as vscode from 'vscode';
import * as _ from 'lodash';
import { Base } from './base';
import { Field } from './field';
let fs = require('fs');
let path = require('path');


class FieldInfo{
    Name: string = '';
}

export class Model extends Base{

    getFieldAtPosition(position: number) {
        for (var lineNo in this.TextLines) {
            const line = this.TextLines[lineNo].trimLeft();
            if (line.startsWith('public')) {
                const isamFieldInfoPos = this.findPrevious(new vscode.Position(Number(lineNo), 0), /\[IsamField\(\d+, \d+\)\]/);
                if (null !== isamFieldInfoPos && 0 !== isamFieldInfoPos.line) {
                    const fieldPosition = Field.extractFieldPosition(this.TextLines[isamFieldInfoPos.line]);
                    if (position >= fieldPosition[0] && position <= fieldPosition[1]) {
                        vscode.window.showInformationMessage(line);                                            
                    }
                }
            }
        }
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