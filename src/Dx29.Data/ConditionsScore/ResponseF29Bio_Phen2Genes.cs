using System;
using System.Collections.Generic;

namespace Dx29.Data
{
    public class ResponseF29Bio_Phen2Genes
    {
        public object query { get; set; }
        public Dictionary<string,Data_Phen2Genes> response { get; set; }
    }
    public class Data_Phen2Genes
    {
        public int rank { get; set;}
        public int id { get; set; }
        public decimal score { get; set; }
        public string status { get; set; }
    }

    public class ResponseF29Bio_DiseasesFromGenes
    {
        public Dictionary<string,object> diseases { get; set; }
        public string id { get; set; }
        public string label { get; set; }
    }
}
