using System;
using System.Collections.Generic;

namespace Dx29.Data
{
    public class TermSearchResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class TermSearchResultSource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Source { get; set; }
        public string ItemCompared { get; set; }
    }

    public class DiseaseSearchResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IList<string> Synonyms { get; set; }
    }

    public class DiseaseSearchResultSource
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public List<string> Synonyms { get; set; }
        public string Source { get; set; }
        public string ItemCompared { get; set; }

    }
}
