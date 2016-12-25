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
            Archiver arc = new Archiver();
            arc.to_dir = ConfigurationManager.AppSettings["output"];
            foreach (string folder in folders)
            {
                arc.from_dir = folder;
                arc.archive_files();
            }

            Environment.Exit(0);
        }
    }
}
