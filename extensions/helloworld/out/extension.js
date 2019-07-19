"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// The module 'vscode' contains the VS Code extensibility API
// Import the module and reference it with the alias vscode in your code below
const vscode = require("vscode");
// this method is called when your extension is activated
// your extension is activated the very first time the command is executed
function activate(context) {
    // Use the console to output diagnostic information (console.log) and errors (console.error)
    // This line of code will only be executed once when your extension is activated
    console.log('Congratulations, your extension "helloworld" is now active!');
    // The command has been defined in the package.json file
    // Now provide the implementation of the command with registerCommand
    // The commandId parameter must match the command field in package.json
    let disposable = vscode.commands.registerCommand('extension.helloWorld', () => {
        // The code you place here will be executed every time your command is executed
        // Display a message box to the user
        vscode.window.showInformationMessage('WOW Hello World!');
    });
    let wanker = vscode.commands.registerCommand('extension.wanker', () => {
        // The code you place here will be executed every time your command is executed
        // Display a message box to the user
        vscode.window.showInformationMessage('You are such a wanker!!!');
    });
    vscode.languages.registerHoverProvider('synddf', {
        provideHover(document, position, token) {
            const range = document.getWordRangeAtPosition(position);
            const text = document.getText(range);
            var message = new vscode.MarkdownString();
            message.appendMarkdown(`=Hover ${text} **Content**=\n`);
            message.appendMarkdown(`this\n====`);
            message.appendMarkdown(`##Hover ${text} Content\n`);
            message.appendCodeblock('javascript', `var a = 'b'`);
            var result = new vscode.Hover(message, range);
            return result;
        }
    });
    vscode.languages.registerDeclarationProvider('synddf', {
        provideDeclaration(document, position, provider) {
            const range = document.getWordRangeAtPosition(position);
            const text = document.getText(range);
            const file = new vscode.Location(vscode.Uri.file(`/Users/stevelamb/Development/ibcos/investigations/DDF/repository/template/${text}.DDF`), new vscode.Position(0, 1));
            vscode.window.showInformationMessage(`we are gonna find a thing..,,${text} in ${document.uri}`);
            return file;
        }
    });
    context.subscriptions.push(disposable);
    context.subscriptions.push(wanker);
}
exports.activate = activate;
// this method is called when your extension is deactivated
function deactivate() { }
exports.deactivate = deactivate;
//# sourceMappingURL=extension.js.map