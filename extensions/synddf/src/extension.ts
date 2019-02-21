// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
import * as vscode from 'vscode';
import { SynDDF, Token, TokenOrNull } from './synddf';
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
	let disposable = vscode.commands.registerCommand('extension.synddf.fieldat', () => {
		// The code you place here will be executed every time your command is executed
		vscode.window.showInputBox({
			prompt: 'a test'
		}).then(
			function (position) {
				const modelName = SynDDF.modelFromFilename(vscode.window.activeTextEditor!.document.fileName);
				const model = new Model(modelName);
				if (model.Exists) {
					const field = model.getFieldAtPosition(Number(position!));
					vscode.window.showInformationMessage(`Got: ${position} in ${modelName}`);							
				}
			}
		);
		
		// Display a message box to the user
		//vscode.window.showInformationMessage('WOW Hello World!');
	});

	/*
	vscode.workspace.onDidOpenTextDocument(function (document: vscode.TextDocument) {
		const input = vscode.window.showInputBox({
			prompt: 'a test'
		})
		synDDF.documentOpened(document);
	});
	*/

	vscode.languages.registerHoverProvider('synddf', {
		provideHover(document: vscode.TextDocument, position: vscode.Position, token) {
			const range = document.getWordRangeAtPosition(position);
			const hoverToken: TokenOrNull = SynDDF.getTokenFromContext(document, position);

			if (null !== hoverToken) {
				return new vscode.Hover(hoverToken.getHover(), range);
			} else {
				return undefined;
			}
		}
	});

	vscode.languages.registerDeclarationProvider('synddf', {
		provideDeclaration(document: vscode.TextDocument, position: vscode.Position, provider) {			
			const declarationToken: TokenOrNull = SynDDF.getTokenFromContext(document, position);

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
