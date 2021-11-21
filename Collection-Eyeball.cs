using System;
using System.Collections.Generic;
using System.Text;

namespace NFTdb_init
{
    class Collection_Eyeball
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
        public int ID { get => _id; set => _id = value; }
        public int Nftid { get => _nftid; set => _nftid = value; }
        public string Background { get => _background; set => _background = value; }
        public string Eyeball { get => _eyeball; set => _eyeball = value; }
        public string Eyecolor { get => _eyecolor; set => _eyecolor = value; }
        public string Iris { get => _iris; set => _iris = value; }
        public string Shine { get => _shine; set => _shine = value; }
        public string Bottom_lid { get => _bottom_lid; set => _bottom_lid = value; }
        public string Top_lid { get => _top_lid; set => _top_lid = value; }

    }
}
