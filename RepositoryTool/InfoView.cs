using System;
using System.IO;
using System.Collections.Generic;
using Terminal.Gui;
using Newtonsoft.Json;

namespace Curses
{
    public class InfoView
    {
        protected readonly ScrollView _parent;
        protected dynamic _data;
        public int Top { get; set; } = 1;
        public int ColSpacing { get; set; } = 2;
        public int RowSpacing { get; set; } = 2;



        protected Dictionary<int, int> _columnWidths;
        public InfoView(ScrollView parent)
        {
            _parent = parent;
            _columnWidths = new Dictionary<int, int>();
        }

        public void Display(string fileName)
        {
            string data = File.ReadAllText(fileName);
            _data = JsonConvert.DeserializeObject(data);
            PreProcess();
            foreach (var item in _data.items)
            {
                var cols = item.cols;
                int colIndex = 0;
                int left = 2;
                foreach(var col in cols){
                    Add(col.ToString(), left);
                    left += _columnWidths[colIndex];
                    colIndex++;
                }
                Top = Top + RowSpacing;
            }
        }

        public void PreProcess(){
            int rows = 0;
            foreach (var item in _data.items)
            {
                var cols = item.cols;
                int colIndex = 0;
                foreach(var col in cols){
                    int width = col.ToString().Length + ColSpacing;
                    if(_columnWidths.ContainsKey(colIndex)){
                        if(width > _columnWidths[colIndex]){
                            _columnWidths[colIndex] = width;
                        }
                    } else {
                        _columnWidths.Add(colIndex, width);
                    }
                    colIndex++;                
                }
                rows++;
            }
            _parent.ContentSize = new Size(_parent.Frame.Width, rows + (rows * RowSpacing));
        }

        public void Add(string value, int? left = null, int? top = null){
            int useLeft = 2;
            int useTop = Top;
            if(left != null){
                useLeft = left.Value;
            }
            if(top != null){
                useTop = top.Value;
            }
            _parent.Add(new Label(useLeft, useTop, value));
        }
    }
}