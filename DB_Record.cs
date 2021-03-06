using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace NFTdb_init
{
    public class DB_Record
    {
        private SQLiteConnection Connection { get; set; }

        private int _id;
        private string _name;
        private string _description;
        private int _price;
        private int _sold;
        private int _max_Copies;
        private int _minted;
        private int _collectionID;
        private string _collectionname;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Price { get => _price; set => _price = value; }
        public int Sold { get => _sold; set => _sold = value; }
        public int Max_Copies { get => _max_Copies; set => _max_Copies = value; }
        public int Minted { get => _minted; set => _minted = value; }
        public string CollectionName { get => _collectionname; set => _collectionname = value; }
        public int CollectionID { get => _collectionID; set => _collectionID = value; }

        public DB_Record()
        { }
        public SQLiteConnection connection(string dbfile)
        {
            Connection = new SQLiteConnection(dbfile);
            return Connection;
        }
        public long AddRow(DB_Record record)
        {
            string name, description, collectionname;
            int id, max_copies, total_minted, sold;
            decimal price;
            long rowid;

            name = record.Name;
            // description = record.Description;
            description = "Hotheads-Eyeball9";
            id = record.ID;
            price = record.Price;
            max_copies = record.Max_Copies;
            total_minted = record.Minted;
            sold = record.Sold;

            string dbfile = "URI=file:NFTDB.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile);
            connection.Open();
            collectionname = "Eyeball9";

            string addNft = "insert into NFT (id,name,description,price,sold,max_copies,total_minted,collectionname)  VALUES (@id,@name,@description,@price,@sold,@max_copies,@total_minted,@collectionname);";
            //string addNft = "insert into NFT (name)  VALUES (@name);";
            SQLiteCommand command = new SQLiteCommand(addNft, connection);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@sold", sold);
            command.Parameters.AddWithValue("@max_copies", max_copies);
            command.Parameters.AddWithValue("@total_minted", total_minted);
            command.Parameters.AddWithValue("@collectionname", collectionname);
            command.ExecuteNonQuery();

            rowid = connection.LastInsertRowId;
            Debug.WriteLine(connection.LastInsertRowId + " after execute");
            connection.Close();
            return rowid;
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

        public DB_Record NFTBuildRecord(string nftJSONFile)
        {
            DB_Record currentRecord = new DB_Record();

            var NftMakerToConvert = File.ReadAllLines(nftJSONFile);
            currentRecord.Name = PrepJSONforDB(NftMakerToConvert[4]);
            currentRecord.Description = PrepJSONforDB(NftMakerToConvert[7]);
            currentRecord.Price = 100;
            currentRecord.Sold = 0;
            currentRecord.Max_Copies = 50;
            currentRecord.Minted = 0;
            // add collection fields (background, eyeball, eyecolor, etc) here as well

            return currentRecord;
        }
    }
}