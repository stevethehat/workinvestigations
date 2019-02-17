import * as vscode from 'vscode';
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
    //public  Exists      : boolean;
    //public  FileName    : string;
    public  Name        : string;
    protected _config     : vscode.WorkspaceConfiguration;

    constructor(name: string) {
        this._config    = vscode.workspace.getConfiguration('synddf');
        this.Name       = name;
    }

    findLocation(text: string): vscode.Location | null{
        let result = null;
        const start = this.find(text);

        if (null !== start) {
            result = new vscode.Location(vscode.Uri.file(this.FileName), start);
        }

        return result;
    }

    find(text: string): vscode.Position | null{
        const textLines = this.getText().split('\n');
        var found       = false;
        var foundLine   = 0;

        for (var lineIndex in textLines) {
            const line = textLines[lineIndex];
            if(line.indexOf(text) !== -1){
                found = true;
                break;
            }            
            foundLine++;
        }
        
        if(true === found){
            const foundPosition = textLines[foundLine].indexOf(text);
            const start = new vscode.Position(foundLine, foundPosition);
            const end = new vscode.Position(foundLine, foundPosition + text.length);    

            return start;
        } else {
            return null;
        }

        var start = new vscode.Position(1, 1);
        var end = new vscode.Position(1, 10);
        var result = new vscode.Range(start, end);

        //return result;
    }

    protected getText(): string{
        const contents = fs.readFileSync(this.FileName);
        return contents.toString();
    }


    abstract getFileName(): string;
}