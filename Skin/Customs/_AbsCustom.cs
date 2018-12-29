using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Skin
{
    public abstract class Custom
    {
        public string Name { get; set; } = "";
        public abstract List<string> AllOptions { get; }
        public abstract string SelectedItem { get; }

        protected int SelectedIndex { get; set; } = 0;
        public void Next() =>
            SelectedIndex = (SelectedIndex + 1) % AllOptions.Count;
        public void Prev() =>
            SelectedIndex = (SelectedIndex + 1) % AllOptions.Count;
    }
}
