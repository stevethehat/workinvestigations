import * as vscode from 'vscode';
import * as _ from 'lodash';

let fs = require('fs');
let path = require('path');

export abstract class Base {
    get Location(): vscode.Location{
        return new vscode.Location(vscode.Uri.file(this.FileName), new vscode.Position(0, 1));
    }
    get FileName(): string{
        return this.getFileName();
    }
    get Exists(): boolean{
        return fs.existsSync(this.FileName);
    }
    get TextLines(): string[]{
        if (null === this._textLines) {
            this._textLines = this.getText().split('\n');
        }
        return this._textLines;
    }
    private _textLines  : string[] | null = null;
    
    public  Name        : string;
    protected _config   : vscode.WorkspaceConfiguration;

    constructor(name: string) {
        this._config    = vscode.workspace.getConfiguration('synddf');
        this.Name       = name;
    }

    findLocation(text: string): vscode.Location | null{
        let result      = null;
        const start     = this.find(text);

        if (null !== start) {
            result = new vscode.Location(vscode.Uri.file(this.FileName), start);
        }

        return result;
    }

    getLineRange(start: number, end: number): string{
        return _.slice(this.TextLines, start, end).join('\n');
    }

    findPrevious(position: vscode.Position, regex: RegExp): vscode.Position | null{
        var result = null;
        for (var i = position.line; i >= 0; i--){
            const line = this.TextLines[i];
            if (regex.test(line)) {
                result = new vscode.Position(i, 0);   
                break;
            }
        }
        if (null === result) {
            result = new vscode.Position(0, 0);
        }
        return result;
    }

    findNext(position: vscode.Position, regex: RegExp): vscode.Position | null{
        var result = null;
        for (var i = position.line; i <= this.TextLines.length; i++){
            if (regex.test(this.TextLines[i])) {
                result = new vscode.Position(i, 0);    
                break;
            }
        }
        if (null === result) {
            result = new vscode.Position(this.TextLines.length, 0);
        }
        return result;
    }

    find(text: string): vscode.Position | null{
        var found       = false;
        var foundLine   = 0;

        for (var lineIndex in this.TextLines) {
            const line = this.TextLines[lineIndex];
            if(line.indexOf(text) !== -1){
                found = true;
                break;
            }            
            foundLine++;
        }
        
        if(true === found){
            const foundPosition = this.TextLines[foundLine].indexOf(text);
            const start = new vscode.Position(foundLine, foundPosition);

            return start;
        } else {
            return null;
        }
    }

    protected getText(): string{
        const contents = fs.readFileSync(this.FileName);
        return contents.toString();
    }


    abstract getFileName(): string;
}