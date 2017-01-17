using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;

namespace Controller
{
    public class MediaLibrary
    {
        public List<Movie> Movies { get; private set; }

        public MediaLibrary()
        {
            this.Movies = new List<Movie>();
        }

        public void AddMovies(string directory)
        {
            foreach (DirectoryInfo sub in new DirectoryInfo(directory).GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                this.Movies.Add(new Movie(sub.FullName));
            }
        }
    }
}
