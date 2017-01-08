using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data
{
    class Movie
    {
        public Movie(string directoryPath)
        {
            this.Directory = new DirectoryInfo(directoryPath);
        }

        private DirectoryInfo Directory { get; set; }
        public string Name { get { return this.Directory.Name; } }
        public double Size { get { return this.Directory.GetFiles("*", SearchOption.AllDirectories).Sum(x => x.Length); } }
    }
}
