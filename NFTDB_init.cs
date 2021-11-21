using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace NFTdb_init
{
    internal class NFTDB_init
    {
        private static void Main(string[] args)
        {
            var record = new DB_Record();
            var nftTable = new DB_Record();
            var projectTable = new Collection_Eyeball();
            string[] path_parts;

            string defaultFolder = @"G:\Projects\hashlips_art_engine-1.0.4\build";

            Directory.SetCurrentDirectory(defaultFolder);
            List<string> filestocheck = FileHelper.GetFilesRecursive("json");

            List<string> nftsToAdd = new List<string>();//list of .json files to work off of
            foreach (string name in filestocheck)
            {
                string line = name;
                path_parts = name.Split('.');
                if (path_parts[1].CompareTo("json") == 0)
                {
                    nftsToAdd.Add(line);
                }
            }
            foreach (string nftFile in nftsToAdd)
            {
                nftTable = record.buildRecord(nftFile);
                record.AddRow(nftTable);
            }
        }
        
        
    }
}