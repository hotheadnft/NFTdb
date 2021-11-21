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
            string dbfile = "URI=file:NFTDB.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile);
            connection.Open();
            DB_Record record = new DB_Record();
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

            foreach (string nft in nftsToAdd)
            {
                Console.WriteLine(nft);
                var NftMakerToConvert = File.ReadAllLines(nft);
                record.Name = PrepJSONforDB(NftMakerToConvert[4]);
                record.Description = PrepJSONforDB(NftMakerToConvert[7]);
                record.Price = 100;
                record.Sold = false;
                record.Max_Copies = 50;
                record.Minted = 0;
            }
        }

        private static string PrepJSONforDB(string fieldToClean)
        {
            string jsonBuffer;
            string[] json_parts;
            json_parts = fieldToClean.Split(':');
            jsonBuffer = json_parts[1];
            jsonBuffer = jsonBuffer.Replace(",", String.Empty);
            jsonBuffer = jsonBuffer.Replace("\"", String.Empty);
            return jsonBuffer;
        }
    }
}