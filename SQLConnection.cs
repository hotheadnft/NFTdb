using System.Data.SQLite;

namespace NFTdb_init
{
    internal class SQLConnection
    {
        private SQLiteConnection Connection { get; set; }

        public SQLiteConnection openDB(string dbfile)
        {
            Connection = new SQLiteConnection(dbfile);
            return Connection;
        }
    }
}