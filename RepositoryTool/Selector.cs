using System.Collections.Generic;
using Terminal.Gui;

namespace Curses{
    public class Selector{
        protected readonly Dialog _dialog;
        protected readonly Button _ok;
        public ListView _list;
        public Selector(List<string> items){
            _dialog = new Dialog($"Select", 60, 20);
            _ok = new Button($"Ok", true);
            _ok.Clicked += () => { Application.RequestStop(); };
            _dialog.AddButton(_ok);

            _list = new ListView(items)
            {
                X = 1,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 2
            };
            _dialog.Add(_list);

            Application.Run(_dialog);
        }
    }
}