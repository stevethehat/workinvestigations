// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';
import { TemplateParser } from './parsing';
let fs = require('fs');
let path = require('path');

// this method is called when your extension is activated
// your extension is activated the very first time the command is executed
export function activate(context: vscode.ExtensionContext) {

	// Use the console to output diagnostic information (console.log) and errors (console.error)
	// This line of code will only be executed once when your extension is activated
	console.log('Congratulations, your extension "synddf" is now active!');

	// The command has been defined in the package.json file
	// Now provide the implementation of the command with registerCommand
	// The commandId parameter must match the command field in package.json
	let disposable = vscode.commands.registerCommand('extension.synddf', () => {
		// The code you place here will be executed every time your command is executed

		// Display a message box to the user
		vscode.window.showInformationMessage('WOW Hello World!');
	});

	vscode.languages.registerHoverProvider('synddf', {
		provideHover(document: vscode.TextDocument, position: vscode.Position, token) {
			const range 	= document.getWordRangeAtPosition(position);
			const text 		= document.getText(range);

			var message 	= new vscode.MarkdownString();
			message.appendMarkdown(`=== Hover ${text} ===\n`);
			message.appendMarkdown(`${position.character}`)
			message.appendCodeblock('javascript', `var a = 'b'`);

			const result = new vscode.Hover(message, range);
			return result;
		}
	});

	vscode.languages.registerDeclarationProvider('synddf', {
		provideDeclaration(document: vscode.TextDocument, position: vscode.Position, provider) {
			const range 	    = document.getWordRangeAtPosition(position);
			const text 			= document.getText(range);
			const template 		= new TemplateParser(text);

			if (template.Exists) {
				return template.File;
			} else {
				return undefined;
			}
		}
	});

	context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
