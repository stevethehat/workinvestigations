using System;
using Terminal.Gui;

namespace curses{
    public class App{
        public App(){
            //var windowManager = new WindowManager();
            Application.Init ();
            var top = Application.Top;

            // Creates the top-level window to show
            var win = new Window (new Rect (0, 1, top.Frame.Width, top.Frame.Height-1), "MyApp");
            top.Add (win);

            //var scrollView = new ScrollBarView(new Rect(0, 0, win.Frame.Width - 2, win.Frame.Height - 5), 500, 1, true);            
            var scrollView = new ViView(new Rect(0, 0, win.Frame.Width - 2, win.Frame.Height - 5));
            var command = new ControlField(1, win.Frame.Height - 3, win.Frame.Width -4, "");
            
            command.Command = (cmd => {
                if(cmd == "q"){
                    top.Running = false;   
                }
                if(cmd == "u"){
                    scrollView.ScrollUp(10);
                }
                if(cmd == "d"){
                    scrollView.ScrollDown(10);
                }

                return false;
            });
            var infoBar = new Label(1, win.Frame.Height - 4, "info bar to show filenames etc");

            win.Add(
                scrollView, infoBar, command
            );

            win.SetFocus(command);

            InfoView infoView = new InfoView(scrollView);
            infoView.Add("hello");
            //infoView.Display("/Users/stevelamb/Development/curses/test.json");

            Application.Run ();            
        }
    }
}