using System;
using Terminal.Gui;

namespace curses{
    public class ControlField: TextField{
        public ControlField(int left, int top, int width, string text) : base(left, top, width, text)
        {
        }
        public Func<KeyEvent, bool> KeyPressed;
        public Func<string, bool> Command;
        public override bool ProcessKey(KeyEvent kb){
            bool result = base.ProcessKey(kb);
            if(KeyPressed != null){
                result = KeyPressed(kb);
            }
            string possibleCommand = Text.ToString();
            if(possibleCommand.StartsWith(":") && Command != null && kb.Key == Key.Enter){
                result = Command(possibleCommand.ToString().Substring(1));
            }
            return result;
        }
    }
}