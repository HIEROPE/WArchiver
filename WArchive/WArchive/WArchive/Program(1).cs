using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace WArchive
{
    class Program
    {
        static void Main(string[] args)
        {
            string n = ConfigurationManager.AppSettings["folders"];
            string[] folders = n.Split(';');
            foreach (string folder in folders)
            {
                Archiver arc = new Archiver();
                arc.to_dir = ConfigurationManager.AppSettings["output"];
                arc.from_dir = folder;
                arc.archive_files();
            }

            Environment.Exit(0);
        }
    }
}
