using System;
using System.Data.SQLite;
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

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Price { get => _price; set => _price = value; }
        public int Sold { get => _sold; set => _sold = value; }
        public int Max_Copies { get => _max_Copies; set => _max_Copies = value; }
        public int Minted { get => _minted; set => _minted = value; }
        public int CollectionID { get => _collectionID; set => _collectionID = value; }

        public DB_Record()
        { }
        public SQLiteConnection connection(string dbfile)
        {
            Connection = new SQLiteConnection(dbfile);
            return Connection;
        }
        public void AddRow(DB_Record record)
        {
            string name, description;
            int id, max_copies, total_minted,sold;
            decimal price;
            
            name = record.Name;
            description = record.Description;
            id = record.ID;
            price = record.Price;
            max_copies = record.Max_Copies;
            total_minted = record.Minted;
            sold = record.Sold;

            string dbfile = "URI=file:NFTDB.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile);
            connection.Open();

            string addNft = "insert into NFT (name,description,price,sold,max_copies,total_minted)  VALUES ('name','description','price','sold','max_copies','total_minted');";
            SQLiteCommand command = new SQLiteCommand(addNft, connection);
            
            command.ExecuteNonQuery();
            connection.Close();

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

        public DB_Record buildRecord(string nft)
        {
            DB_Record currentRecord = new DB_Record();

            var NftMakerToConvert = File.ReadAllLines(nft);
            currentRecord.Name = PrepJSONforDB(NftMakerToConvert[4]);
            currentRecord.Description = PrepJSONforDB(NftMakerToConvert[7]);
            currentRecord.Price = 100;
            currentRecord.Sold = 0;
            currentRecord.Max_Copies = 50;
            currentRecord.Minted = 0;

            return currentRecord;
        }
    }
}