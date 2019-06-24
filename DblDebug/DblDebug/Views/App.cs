using System;
using Terminal.Gui;

namespace DblDebug.Views
{
    public class App
    {
        public App()
        {
            Application.Init();

            var top = new Toplevel()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Close", "", () => {
                        Application.RequestStop ();
                    })
                }),
            });


            // nest a window for the editor
            var win = new Window("hello")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            var editor = new TextView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            editor.Text = "hello";
            win.Add(editor);

            // Add both menu and win in a single call
            TextField text = new TextField("hello")
            {
                X = 2,
                Y = Pos.Bottom(win) - 2,
                Width = win.Width - 2,
                Height = 1
            };

            win.Add(text);
            Console.WriteLine("here4");
            Application.Top.Add(menu, win);
            Application.Run(top);
        }
    }
}

