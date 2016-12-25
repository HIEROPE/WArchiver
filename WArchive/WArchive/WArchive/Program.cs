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
            Console.WriteLine("Listing the folders to archive");
            string[] folders = n.Split(';');
            Console.WriteLine("WArchive will verify {0} folders \n", folders.Length);
            foreach (string folder in folders)
            {
                Console.WriteLine("Creating context for the folder {0}", folder);
                Archiver arc = new Archiver();
                arc.to_dir = ConfigurationManager.AppSettings["output"];
                arc.from_dir = folder;
                arc.archive_files();
                Console.WriteLine("Folder {0} was processed successfully", folder);
            }

            Environment.Exit(0);
        }
    }
}
