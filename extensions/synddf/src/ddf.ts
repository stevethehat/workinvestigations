import * as vscode from 'vscode';
import * as synddf from './synddf';

import { Base } from "./base";
import { Position } from "vscode";

export class DDF extends Base{
    getFileName(): string{
        return '';
    }
    getTokenPosition(): synddf.PositionOrNull{
        return null;
    }
}