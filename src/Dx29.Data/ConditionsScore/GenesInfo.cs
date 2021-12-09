using System;
using System.Collections.Generic;

namespace Dx29.Data
{
    public class GenesInfo {
        public string name { get; set; }
        public decimal score { get; set; }
        public List<string> diseases { get; set; }
        public decimal? combinedScore { get; set; }
    }
}
