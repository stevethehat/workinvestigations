import * as vscode from 'vscode';
//import * as _ from 'lodash';
import { Base } from './base';
let fs = require('fs');
let path = require('path');
import { synDDF } from './extension';
import { Chunker } from './template';

export class DDFCodeLenseProvider implements vscode.CodeLensProvider {
    /*
    onDidChangeCodeLenses?: vscode.Event<void> | undefined;    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): vscode.ProviderResult<vscode.CodeLens[]> {
        throw new Error("Method not implemented.");
    }
    */
    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): vscode.CodeLens[]{
        const result = new Array<vscode.CodeLens>();
        let position = 1;

        for(let lineNo = 0; lineNo < document.lineCount; lineNo++){
            const line = document.lineAt(lineNo);
            if (line.text.startsWith('Field')) {
                try {
                    const chunker = new Chunker([line.text]);
                    chunker.gotoElement('Template');
                    const templateName = chunker.getNextChunk();
                    const template = synDDF.getTemplate(templateName);
                    if (null !== template) {
                        if (null !== template.Settings) {
                            let size = Number(template.Settings['Size']);
                            if (NaN !== size) {
                                const codeLens = new vscode.CodeLens(line.range, {
                                    title: `Position ${position} - ${position + (size -1)} (${size})`, command: ''
                                });
                                result.push(codeLens);
                                position += (size);
                            }
                        }                        
                    }
                        
                } catch {

                }
            }
        }
        
        return result;
    }
    
    /*
    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): Promise<vscode.CodeLens[]> {
        let result = new Promise<vscode.CodeLens[]>(function (ok, err) {
            const result = new Array<vscode.CodeLens>();

            for (let lineNo = 0; lineNo < document.lineCount; lineNo++) {
                const line = document.lineAt(lineNo);
                if (line.text.startsWith('Field')) {
                    const chunker = new Chunker([line.text]);
                    chunker.gotoElement('Template');
                    const templateName = chunker.getNextChunk();
                    const template = synDDF.getTemplate(templateName);
                    const codeLens = new vscode.CodeLens(line.range, { title: 'Position 1 - 6', command: 'save' });
                    result.push(codeLens);
                }
            }

            ok(result);
        });
        return result;
    }

    resolveCodeLens?(codeLens: vscode.CodeLens, token: vscode.CancellationToken): vscode.ProviderResult<vscode.CodeLens>{
        if (undefined !== codeLens && undefined !== codeLens.command) {
            codeLens.command.title = 'this is my codelens';
        }
        return codeLens;
    }
    */

}