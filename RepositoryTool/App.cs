using System;
using System.Collections.Generic;
using Terminal.Gui;
using Repository;

namespace Curses{
    public class App{
        public App(){
            //var windowManager = new WindowManager();
            Application.Init ();
            //Application.Driver
            var top = Application.Top;

            // Creates the top-level window to show
            var win = new Window (new Rect (0, 1, top.Frame.Width, top.Frame.Height-1), "MyApp");
            top.Add (win);

            //var scrollView = new ScrollBarView(new Rect(0, 0, win.Frame.Width - 2, win.Frame.Height - 5), 500, 1, true);            
            var scrollView = new ViView(new Rect(0, 0, win.Frame.Width - 2, win.Frame.Height - 5));
            var command = new ControlField(1, win.Frame.Height - 3, win.Frame.Width -4, "");
            var infoBar = new Label(1, win.Frame.Height - 4, "info bar to show filenames etc");
            
            command.Command = ((cmd, remainder) => {
                if(cmd == "q"){
                    top.Running = false;   
                }
                if(cmd == "u"){
                    scrollView.ScrollUp(10);
                }
                if(cmd == "d"){
                    scrollView.ScrollDown(10);
                }
                if(cmd == "o"){
                    infoBar.Text = "show open dialog";
                    Dialog dialog = new Dialog("test dialog", 100, 20);
                    Button ok = new Button("Ok", true);
                    ok.Clicked += () => { Application.RequestStop(); };
                    dialog.AddButton(ok);
                    Application.Run(dialog);
                }
                if(cmd == "p"){
                    List<string> items = RepositoryInfo.GetIsamTypes(remainder);

                    Selector selector = new Selector(items);
                    string selected = items[selector._list.SelectedItem];
                    infoBar.Text = $"Parse '{selected}'...";

                    if(selected != ""){
                        DDFFile ddfFile = new DDFFile("structure", selected);
                        var structure = ddfFile.Parse();
                        ddfFile.Save(selected);
                        var mb = MessageBox.Query(100, 20, "Parse","Parse complete", new string[] {"OK"});
                        infoBar.Text = $"{selected} Done.";
                    }
                }

                return false;
            });

            win.Add(
                scrollView, infoBar, command
            );

            win.SetFocus(command);

            InfoView infoView = new InfoView(scrollView);
            infoView.Add("hello");
            infoView.Display("/Users/stevelamb/Development/curses/test.json");

            Application.Run ();            
        }
    }
}