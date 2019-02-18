import * as vscode from 'vscode';
import * as _ from 'lodash';
import { Template } from './template';
import { Field } from './field';
let path = require('path');

export interface Token{
    Location                : vscode.Location;
    getHover()              : vscode.MarkdownString;
}

export type TokenOrNull     = Token | null;
export type LocationOfNull  = vscode.Location | null;


export class SynDDF{
    static modelFromFilename(filePath: string): string{
        let fileName        = filePath.substring(filePath.lastIndexOf(path.sep) + 1);
        fileName            = fileName.substring(0, fileName.indexOf('.'));
        return fileName;
    }

    static getTokenFromContext(document: vscode.TextDocument, position: vscode.Position): TokenOrNull {
        let result = null;
        const range 	        = document.getWordRangeAtPosition(position);
        const text              = document.getText(range);
        const line              = document.lineAt(position.line).text.replace(/ +/g, ' ');
        const lineElements      = line.split(' ');
        const textPos           = lineElements.indexOf(text);
        const previousElements  = _.slice(lineElements, 0, textPos);
        const context           = _.reverse(previousElements);
        
        switch (true) {
            case context[0] === 'Field':
                result = new Field(text, SynDDF.modelFromFilename(document.fileName));
                break;
            case (context[0] === 'Template' || context[0] === 'Parent'):
                result = new Template(text);
                break;
            case (lineElements[0] === 'Relation'):
                break;
        }

        return result;
    }
} 