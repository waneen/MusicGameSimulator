using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Skin
{
    public class File : Custom
    {
        private string Path { get; set; }
        private List<string> _filePathes = null;

        public override List<string> AllOptions => 
            _filePathes;

        public override string SelectedItem =>
            Path.Replace("*", AllOptions[SelectedIndex]);

        public File(string Name,string WildCardFilePath)
        {
            this.Name = Name;
            this.Path = WildCardFilePath;

            //Pathからファイル/フォルダ探索
        }
    }
}
