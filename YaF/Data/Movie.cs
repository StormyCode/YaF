﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Diagnostics;

namespace Data
{
    [DebuggerDisplay("{Name}")]
    public class Movie
    {
        public Movie(string directoryPath)
        {
            this.Directory = new DirectoryInfo(directoryPath);
            this.ReadNfoFiles();
        }

        private DirectoryInfo Directory { get; set; }
        public string Name { get { return this.Directory.Name; } }
        public double Size { get { return this.Directory.GetFiles("*", SearchOption.AllDirectories).Sum(x => x.Length); } }
        public string Id { get { return Regex.Replace(this.Name, @"[^A-z0-9]", "").ToLower(); } }
        public Dictionary<string, string> Nfo { get; private set; }
        private string[] ValidExtensions { get; set; } = new string[] { ".avi", ".mkv", "mp4" };
        public FileInfo MovieFile
        {
            get
            {
                try
                {
                    this.Directory.GetAccessControl();
                    var files = this.Directory.GetFiles("*", SearchOption.AllDirectories).ToList<FileInfo>();
                    return files.Where(x => this.ValidExtensions.Contains(x.Extension) && x.Name.ToLower().Contains("sample") == false).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;

                }
            }
        }
        public TimeSpan Duration
        {
            get
            {
                if (this.MovieFile != null)
                {
                    
                }
                return new TimeSpan();
            }

        }

        public void ReadNfoFiles()
        {
            this.Nfo = new Dictionary<string, string>();
            if (this.MovieFile != null)
            {
                foreach (FileInfo fi in this.Directory.GetFiles(string.Format("{0}.nfo", Path.GetFileNameWithoutExtension(this.MovieFile.Name)), SearchOption.TopDirectoryOnly))
                {
                    using (StreamReader sr = new StreamReader(fi.OpenRead()))
                    {
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains(":"))
                            {
                                string[] splitted = line.Split(':');
                                string key = splitted[0];
                                string value = string.Join(":", splitted.Skip(1));
                                key = Regex.Replace(key, @"[^\w]*", "").Trim();
                                value = Regex.Replace(value, @"^[^\w]*", "");
                                value = Regex.Replace(value, @"[^\w]*$", "");
                                if (this.Nfo.ContainsKey(key) == false)
                                    this.Nfo[key] = "";
                                this.Nfo[key] = value;
                            }
                        }
                    }
                }
            }
        }

    }
}
