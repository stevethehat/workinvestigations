// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';
import { SynDDF, Token } from './synddf';
import { Template } from './template';
import { CodeLenseProvider } from './codelense';
import { Model } from './model';
let fs = require('fs');
let path = require('path');

// this method is called when your extension is activated
// your extension is activated the very first time the command is executed
export function activate(context: vscode.ExtensionContext) {

	// Use the console to output diagnostic information (console.log) and errors (console.error)
	// This line of code will only be executed once when your extension is activated
	console.log('Congratulations, your extension "synddf" is now active!');

	let synDDF = new SynDDF();

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
			message.appendMarkdown(`${position.character}`);
			message.appendCodeblock('javascript', `var a = 'b'`);

			const result = new vscode.Hover(message, range);
			return result;
		}
	});

	vscode.languages.registerDeclarationProvider('synddf', {
		provideDeclaration(document: vscode.TextDocument, position: vscode.Position, provider) {
			//const range 	    = document.getWordRangeAtPosition(position);
			//const text 			= document.getText(range);
			
			//const target 		= new Model(SynDDF.modelFromFilename(document.fileName));
			//const location = target.findLocation('Name');
			
			const declarationToken: Token | null = SynDDF.getTokenFromContext(document, position);

			if (null !== declarationToken) {
				return declarationToken.Location;
			} else {
				return undefined;
			}
		}
	});

	/*
	vscode.languages.registerCodeLensProvider('synddf', {
		provideCodeLenses(document: vscode.TextDocument, token): vscode.CodeLens[]{
			var lense = new CodeLenseProvider(
				new vscode.Range(
					new vscode.Position(1, 1),
					new vscode.Position(2, 1)
				));
			return [lense];

		}
	});
	*/
	//vscode.languages.registerCodeLensProvider('synddf', new CodeLenseProvider());

	context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
