import * as vscode from 'vscode';
let fs = require('fs');
let path = require('path');


export class Model {
    get File(): vscode.Location{
        return new vscode.Location(vscode.Uri.file(this.FileName), new vscode.Position(0, 1));
    }
    Exists      : boolean;
    FileName    : string;

    constructor(name: string) {
        const rootFolder 	= vscode.workspace.getConfiguration('synddf');
        this.FileName 		= path.join(rootFolder.get('repositoryRootFolder'), 'template', `${name}.DDF`);
    
        this.Exists         = fs.existsSync(this.FileName);
    }
}