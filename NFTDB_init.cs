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
            long rowid, collectionid;

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
            for (int i = 0; i < nftsToAdd.Count - 1; i++)
            {
                string nftFile = nftsToAdd[i];

                nftTable = DBRecord.NFTBuildRecord(nftFile);
                rowid = DBRecord.AddRow(nftTable);
                projectTable = collectionRecord.CollectionBuildRecord(nftFile);
                collectionid = collectionRecord.AddRow(projectTable, i, rowid);

                //update NFT with collection id
                string dbfile = "URI=file:NFTDB.db";
                SQLiteConnection connection = new SQLiteConnection(dbfile);
                connection.Open();

                string addNft = $"update NFT set collectionid = {collectionid}  where id={rowid}";
                SQLiteCommand command = new SQLiteCommand(addNft, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}