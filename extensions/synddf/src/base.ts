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

    abstract getFileName(): string;
}