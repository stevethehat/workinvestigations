using System;
using Terminal.Gui;

namespace Curses{
    public class ViView: ScrollView{
        public ViView(Rect rect) : base(rect)
        {
        }
        public Func<KeyEvent, bool> KeyPressed;
        public Func<string, bool> Command;
        public override bool ProcessKey(KeyEvent kb){
            bool result = base.ProcessKey(kb);
            if(kb.KeyValue == 106){
                ScrollDown(1);
            }
            if(kb.KeyValue == 107){
                ScrollUp(1);
            }
            return result;
        }
    }
}