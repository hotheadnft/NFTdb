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
            var DBRecord = new DB_Record();
            var nftTable = new DB_Record();
            var projectTable = new EyeBall();
            var collectionRecord = new EyeBall();
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
            // foreach (string nftFile in nftsToAdd)
            for (int i = 0; i < nftsToAdd.Count-1; i++)
            {
                string nftFile = nftsToAdd[i];
       
                nftTable = DBRecord.NFTBuildRecord(nftFile);
                DBRecord.AddRow(nftTable);
                projectTable = collectionRecord.CollectionBuildRecord(nftFile);
                collectionRecord.AddRow(projectTable, i);
            }
        }
        
        
    }
}