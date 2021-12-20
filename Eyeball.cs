using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace NFTdb_init
{
    internal class EyeBall
    {
        private int _id;
        private int _nftid;
        private string _background;
        private string _eyeball;
        private string _eyecolor;
        private string _iris;
        private string _shine;
        private string _bottom_lid;
        private string _top_lid;
        private string _dna;
        private string _twitter;
        private string _web;

        public int ID { get => _id; set => _id = value; }
        public int Nftid { get => _nftid; set => _nftid = value; }
        public string Background { get => _background; set => _background = value; }
        public string Eyeball { get => _eyeball; set => _eyeball = value; }
        public string Eyecolor { get => _eyecolor; set => _eyecolor = value; }
        public string Iris { get => _iris; set => _iris = value; }
        public string Shine { get => _shine; set => _shine = value; }
        public string Bottom_lid { get => _bottom_lid; set => _bottom_lid = value; }
        public string Top_lid { get => _top_lid; set => _top_lid = value; }
        public string dna { get => _dna; set => _dna = value; }
        public string twitter { get => _twitter; set => _twitter = value; }
        public string web { get => _web; set => _web = value; }
        public EyeBall()
        { }

        public EyeBall CollectionBuildRecord(string nftJSONFile)
        {
            EyeBall currentCollection = new EyeBall();
            var NftMakerToConvert = File.ReadAllLines(nftJSONFile);
            currentCollection.ID = 0;
            currentCollection.Nftid = 1;
            currentCollection.Background = PrepJSONforDB(NftMakerToConvert[15]);
            currentCollection.Eyeball = PrepJSONforDB(NftMakerToConvert[16]);
            currentCollection.Eyecolor = PrepJSONforDB(NftMakerToConvert[17]);
            currentCollection.Iris = PrepJSONforDB(NftMakerToConvert[18]);
            currentCollection.Shine = PrepJSONforDB(NftMakerToConvert[19]);
            currentCollection.Bottom_lid = PrepJSONforDB(NftMakerToConvert[20]);
            currentCollection.Top_lid = PrepJSONforDB(NftMakerToConvert[21]);
            currentCollection.dna = PrepJSONforDB(NftMakerToConvert[22]);
            currentCollection.twitter = PrepJSONforDB(NftMakerToConvert[23]);
            currentCollection.web = PrepJSONforDB(NftMakerToConvert[24]);

            return currentCollection;
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

        public long AddRow(EyeBall collection, int loopCtr,long rowid)
        {
            string background, eyeball, eyecolor, iris, shine, bottom_lid, top_lid,dna,twitter,web;
            long collectionid;

            eyeball = collection.Eyeball;
            background = collection.Background;
            eyecolor = collection.Eyecolor;
            iris = collection.Iris;
            shine = collection.Shine;
            bottom_lid = collection.Bottom_lid;
            top_lid = collection.Top_lid;
            dna = collection.dna;
            twitter = collection.twitter;
            web = collection.web;
            string collectionname = "Eyeball9";
            string dbfile = "URI=file:NFTDB.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile);
            connection.Open();

            string addCollection = "insert into Eyeball9(id,NFTid,eyeball,eyecolor,iris,shine,bottom_lid,top_lid,background,dna,twitter,web,collectionname)" +
                "VALUES (@id,@NFTid,@eyeball,@eyecolor,@iris,@shine,@bottom_lid,@top_lid,@background,@dna,@twitter,@web,@collectionname);";
           
            SQLiteCommand command = new SQLiteCommand(addCollection, connection);
            
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@NFTID", rowid);
            command.Parameters.AddWithValue("@background", background);
            command.Parameters.AddWithValue("@eyeball", eyeball);
            command.Parameters.AddWithValue("@eyecolor", eyecolor);
            command.Parameters.AddWithValue("@iris", iris);
            command.Parameters.AddWithValue("@shine", shine);
            command.Parameters.AddWithValue("@bottom_lid", bottom_lid);
            command.Parameters.AddWithValue("@top_lid", top_lid);
            command.Parameters.AddWithValue("@dna", dna);
            command.Parameters.AddWithValue("@twitter", twitter);
            command.Parameters.AddWithValue("@web", web);            
            command.Parameters.AddWithValue("@collectionname", collectionname);

            command.ExecuteNonQuery();
            
            collectionid  = connection.LastInsertRowId;
            Debug.WriteLine(connection.LastInsertRowId + " after execute");
            connection.Close();

            return collectionid;
        }
    }
}