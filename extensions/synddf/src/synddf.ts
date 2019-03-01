import * as vscode      from 'vscode';
import * as _           from 'lodash';
import { Template }     from './template';
import { Field }        from './field';
import { Format }       from './format';
import { Dictionary } from 'lodash';
import { DDF } from './ddf';

let path                = require('path');

export interface Token{
    Location                : vscode.Location;
    getHover()              : vscode.MarkdownString;
}

export type TokenOrNull     = Token | null;
export type LocationOfNull  = vscode.Location | null;
export type PositionOrNull  = vscode.Position | null;

export class SynDDF{
    private _ddfs           : Dictionary<DDF>;
    protected _config       : vscode.WorkspaceConfiguration;
    protected _documentRoot: string;
    protected _templates: Dictionary<Template> = {};
        
    constructor() {
        this._config        = vscode.workspace.getConfiguration('synddf');
        const root          = this._config.get<string>('repositoryRootFolder');
        this._documentRoot  = '';
        if (undefined !== root) {
            this._documentRoot = root;            
        }
        this._ddfs = {};
    }

    
        
    documentOpened(document: vscode.TextDocument) {
        let documentType = '';
        if (true === document.fileName.startsWith(this._documentRoot) && false === document.fileName.endsWith('.git')) {
            const section = document.fileName.substr(this._documentRoot.length);
            vscode.window.showInformationMessage(`We opened a document ${section}`);
        }        
    }

    static modelFromFilename(filePath: string): string{
        let fileName        = filePath.substring(filePath.lastIndexOf(path.sep) + 1);
        fileName            = fileName.substring(0, fileName.indexOf('.'));
        return fileName;
    }

    getTemplate(name: string, andParse: boolean = true): Template{
        let result = null;
        if (undefined !== this._templates[name]) {
            result = this._templates[name];
        } else {
            result = new Template(name);
            if (andParse) {
                result.parse();
                //this.addSettings(settings);
            }
            this._templates[name] = result;
        }
        return result;
    }

    getTokenFromContext(document: vscode.TextDocument, position: vscode.Position): TokenOrNull {
        let   result            = null;
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
                result = this.getTemplate(text);

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