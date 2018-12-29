using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGS.Skin
{
    public class Option : Custom
    {
        private Dictionary<string, int> _options = null;

        public override List<string> AllOptions =>
            _options.Select(op => op.Key).ToList();

        public override string SelectedItem =>
            AllOptions[SelectedIndex];

        public Option(string Name,Dictionary<string,int> Options)
        {
            this.Name = Name;
            this._options = Options;
        }
    }
}
