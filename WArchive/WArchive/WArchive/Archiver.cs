using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace WArchive
{
    class Archiver
    {
        public string to_dir { get; set; }
        public string from_dir { get; set; }
        private int max_days { get; set; }
        private string file_ext { get; set; }
        private string rar_path =  ConfigurationManager.AppSettings["rar_path"];
        private String rar_cmd = " a -r {0}.rar {1}{2}";
        
        public Archiver()
        {
            this.max_days = Int32.Parse(ConfigurationManager.AppSettings["max_days"]);
                        
            if (ConfigurationManager.AppSettings["file_ext"] == ".*")
            {
                this.file_ext = "";
            }
            
            else
            {
                this.file_ext = ConfigurationManager.AppSettings["file_ext"];
            }
        }

        public void archive_files()
        {
            DateTime max_date = DateTime.Today.AddDays(-max_days);
            List<String> list = new List<String>();
            string[] d = System.IO.Directory.GetFileSystemEntries(from_dir);
            int count = 0;

            Console.WriteLine("\nWArchive will archive subfolders that wasn't changed since {0}", max_date);

            if (string.IsNullOrEmpty(this.to_dir))
            {
                //System.Console.WriteLine(this.from_dir + @"\" + this.from_dir.Substring(this.from_dir.IndexOf(@"\")+1));
                //this.to_dir = this.from_dir + "//"+ this.from_dir;
                this.to_dir = this.from_dir + @"\" + this.from_dir.Substring(this.from_dir.LastIndexOf(@"\") + 1);
            }

            foreach (string dir in d)
            {
                //Console.WriteLine(to_dir.Replace("\"", "") + ".rar");
                if (dir == to_dir.Replace("\"", "") + ".rar")
                {
                    continue;
                }
                
                System.IO.DirectoryInfo SubdirInfo = new System.IO.DirectoryInfo(dir);

                if (SubdirInfo.LastWriteTime < max_date)
                {
                    Console.WriteLine("Archiving folder {0}", dir);
                    if (this.zipper(String.Format(rar_cmd, '"'+to_dir+'"', '"' + dir, file_ext + '"')))
                    {
                        count += 1;
                        this.delete_files(dir);
                    }
                }
            }

            Console.WriteLine("WArchive archived {0} folders for the context {1}\n", count, to_dir.Replace("\"", ""));
        }

        private void delete_files(string dir)
        {
            //dir = "C:\\Users\\Iran\\Desktop\\Novo Documento de Tex.txt";
            if (File.Exists(dir))
            {
                FileSystem.DeleteFile(dir, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
            else
            {
                FileSystem.DeleteDirectory(dir, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
        }

        private bool zipper(string cmd)
        {
            try
            {
                Console.WriteLine(cmd);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "rar.exe";
                process.StartInfo.WorkingDirectory = this.rar_path;
                process.StartInfo.Arguments = cmd;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
