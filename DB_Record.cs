using System.Data.SQLite;

namespace NFTdb_init
{
    internal class DB_Record
    {
        private SQLiteConnection Connection { get; set; }

        private int _id;
        private string _name;
        private string _description;
        private decimal _price;
        private bool _sold;
        private int _max_Copies;
        private int _minted;
        private int _collectionID;
        

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public decimal Price { get => _price; set => _price = value; }
        public bool Sold { get => _sold; set => _sold = value; }
        public int Max_Copies { get => _max_Copies; set => _max_Copies = value; }
        public int Minted { get => _minted; set => _minted = value; }
        public int CollectionID { get => _collectionID; set => _collectionID = value; }

        public DB_Record()
        { }

        public void AddRecord(int id, string name, string description, decimal price, bool sold, int max_copies, int minted, int collectionid)
        {
        }

        public SQLiteConnection connection(string dbfile)
        {
            Connection = new SQLiteConnection(dbfile);
            return Connection;
        }
    }
}