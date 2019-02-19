import * as vscode from 'vscode';
import { Base } from "./base";

export class Relation extends Base{
    getFileName(): string{
        return '';    
    }
    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0,0);
    }
}