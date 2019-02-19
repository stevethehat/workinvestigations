import * as vscode      from 'vscode';
import * as _           from 'lodash';
import { Template }     from './template';
import { Field }        from './field';
import { Format }       from './format';

let path                = require('path');

export interface Token{
    Location                : vscode.Location;
    getHover()              : vscode.MarkdownString;
}

export type TokenOrNull     = Token | null;
export type LocationOfNull  = vscode.Location | null;
export type PositionOrNull  = vscode.Position | null;


export class SynDDF{
    static modelFromFilename(filePath: string): string{
        let fileName        = filePath.substring(filePath.lastIndexOf(path.sep) + 1);
        fileName            = fileName.substring(0, fileName.indexOf('.'));
        return fileName;
    }

    static getTokenFromContext(document: vscode.TextDocument, position: vscode.Position): TokenOrNull {
        let result              = null;
        const range 	        = document.getWordRangeAtPosition(position);
        const text              = document.getText(range);
        const line              = document.lineAt(position.line).text.replace(/ +/g, ' ');
        const lineElements      = line.split(' ');
        const textPos           = lineElements.indexOf(text);
        const previousElements  = _.slice(lineElements, 0, textPos);
        const context           = _.reverse(previousElements);
        
        switch (true) {
            case 'Field' === context[0]:
                result = new Field(text, SynDDF.modelFromFilename(document.fileName));
                break;
            case ('Template' === context[0] || 'Parent' === context[0]):
                result = new Template(text);
                break;
            case ('Format' === context[0]):
                result = new Format(text);
                break;
            case ('Relation' === lineElements[0]):
                break;
        }

        return result;
    }
} 