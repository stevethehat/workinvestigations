import * as vscode from 'vscode';
//import * as _ from 'lodash';
import { Base } from './base';
let fs = require('fs');
let path = require('path');

export class DDFCodeLenseProvider implements vscode.CodeLensProvider{
    /*
    onDidChangeCodeLenses?: vscode.Event<void> | undefined;    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): vscode.ProviderResult<vscode.CodeLens[]> {
        throw new Error("Method not implemented.");
    }
    */
    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): vscode.ProviderResult<vscode.CodeLens[]>{
        const result = new Array<vscode.CodeLens>();

        /*
        for(let lineNo = 0; lineNo < document.lineCount; lineNo++){
            const line = document.lineAt(lineNo);
            if(line.text.startsWith('Field')){
                const codeLens = new vscode.CodeLens(line.range, { title: 'Position 1 - 6', command: ''  } );
                result.push(codeLens);
            }
        }
        */
       
        return result;
    }

}