import * as vscode from 'vscode';
import { Base } from './base';
import { Dictionary } from "lodash";

let fs = require('fs');
let path = require('path');

class MarkdownSettingLine{
    Name        : string    = '';
    Value       : string | null    = null;
    Overridden: boolean = false;
    
    constructor(name: string, value: string | null) {
        this.Name = name;
        this.Value = value;
    }
}

export class Template extends Base {
    Ancestor: Template | null = null;
    Settings: Dictionary<string> | null = null;
    private _chunker: Chunker | null = null;

    getFileName(): string {
        return path.join(this._config.get('repositoryRootFolder'), 'template', `${this.Name}.DDF`);
    }
    getHover(): vscode.MarkdownString{
        const message =  new vscode.MarkdownString();
        message.appendMarkdown(`### ${this.Name} ###\r`);
        message.appendMarkdown(`___\r`);
        message.appendMarkdown(`   \r`);

        this.parse();
        const settings = new Array<MarkdownSettingLine>();
        this.addSettings(settings);

        settings.shift();

        const overridden: Dictionary<Boolean> = {};
        for (let i in settings) {
            const line = settings[i];

            if (overridden[line.Name] === true) {
                line.Overridden = true;
            }
            overridden[line.Name] = true;
        }

        for (let i in settings) {
            const line = settings[i];
            if (null === line.Value) {
                message.appendMarkdown(`   \r`);        
                message.appendMarkdown(`#### ${line.Name} ####\r`);
                message.appendMarkdown(`___\r`);
            } else {
                //  && this._config.get('showOverriddenSettings')
                if (true === line.Overridden) {
                    message.appendMarkdown(`~~${line.Name}: ${line.Value}~~\r\r`);
                } else {
                    message.appendMarkdown(`**${line.Name}:** ${line.Value}\r\r`);                    
                }
            }
        }
        return message;
    }
    getTokenPosition(): vscode.Position | null{
        return new vscode.Position(0, 0);
    }

    addSettings(settings: MarkdownSettingLine[]) {
        if (null !== this.Ancestor) {
            this.Ancestor.addSettings(settings);
        } 

        for (var setting in this.Settings) {
            settings.unshift(new MarkdownSettingLine(setting, this.Settings[setting]));    
        }

        settings.unshift(new MarkdownSettingLine(this.Name, null));

    }
    parse() {
        let previousChunk = "";
        this._chunker = new Chunker(this.TextLines);
        this.Settings = {};

        let chunk = this._chunker.getNextChunk();
        while ('' !== chunk) {
            // process
            switch (true) {
                case 'Parent'       === chunk:
                    this.Ancestor = new Template(this._chunker.getNextChunk());
                    this.Ancestor.parse();
                    break;
                
                case 'Size'         === chunk ||
                     'Dimension'    === chunk ||
                     'Description'  === chunk ||
                     'Precision'    === chunk ||
                     'Overlay'      === chunk ||
                     'Prompt'       === chunk:
                    this.Settings[chunk] = this._chunker.getNextChunk();
                    break;

                case 'Type'         === chunk:
                    this.Ancestor = new Template(this._chunker.getNextChunk());
                    break;
                
                case 'Position'     === chunk:
                    this.Settings[chunk] = `${this._chunker.getNextChunk()} ${this._chunker.getNextChunk()}`;
                    break;
                
                case 'Method'       === chunk:
                    this.Settings[`${previousChunk}_${chunk}`] = this._chunker.getNextChunk();
                    break;
            }
            previousChunk = chunk;
            chunk = this._chunker.getNextChunk();
        }
    }
}

class Chunker{
    private _block: string[];
    private _line: string | undefined = '';
    constructor(block: string[]){
        this._block = block;
        this.advanceLine();
    }

    advanceLine(){
        this._line = this._block.shift();
    }
    getNextChunk(): string{
        let chunk = '';
        let chunkEnd = 0;

        if(undefined !== this._line){
            this._line = this._line.trimLeft();

            if('"' === this._line.substr(0, 1)){
                chunkEnd = this._line.indexOf('"', 1) +1;
            } else {
                chunkEnd = this._line.indexOf(' ');
            }

            if(-1 !== chunkEnd){
                chunk = this._line.substr(0, chunkEnd);
                this._line = this._line.substr(chunkEnd);
            } else {
                if('' !== this._line){
                    chunk = this._line;
                    this.advanceLine();
                } else {
                    this.advanceLine();
                    chunk = this.getNextChunk();
                }
            }
        }
        return chunk;
    }
}
