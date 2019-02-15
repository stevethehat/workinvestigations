import * as vscode from 'vscode';
let fs = require('fs');
let path = require('path');


export abstract class Base {
    get Location(): vscode.Location{
        return new vscode.Location(vscode.Uri.file(this.FileName), new vscode.Position(0, 1));
    }
    public  Exists      : boolean;
    public  FileName    : string;
    public  Name        : string;
    protected _config     : vscode.WorkspaceConfiguration;

    constructor(name: string) {
        this._config    = vscode.workspace.getConfiguration('synddf');
        this.Name       = name;
        this.FileName   = this.getFileName();
            
        this.Exists     = fs.existsSync(this.FileName);
    }

    find(text: string): vscode.Range | null{
        const textLines = this.getText().split('\n');
        var found       = false;
        var foundLine   = 0;

        for(var line in textLines ){
            if(line.indexOf(text) != -1){
                found = true;
                break;
            }            
            foundLine++;
        }
        
        if(true === found){
            const foundPosition = textLines[foundLine].indexOf(text);
            const start = new vscode.Position(foundLine, foundPosition);
            const end = new vscode.Position(foundLine, foundPosition + text.length);    

            return new vscode.Range(start, end);
        } else {
            return null;
        }

        var start = new vscode.Position(1, 1);
        var end = new vscode.Position(1, 10);
        var result = new vscode.Range(start, end);

        return result;
    }

    protected getText(): string{
        return fs.readFileSync(this.FileName);
    }


    abstract getFileName(): string;
}