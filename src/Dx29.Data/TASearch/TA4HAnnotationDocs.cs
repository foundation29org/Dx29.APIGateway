using System;
using System.Collections.Generic;

namespace Dx29.Data
{
    public class TA4HAnnotationDocs
    {
        public IList<TAHAnnotationDoc> Documents { get; set; }
    }

    public class TAHAnnotationDoc
    {
        public string Id { get; set; }
        public IList<TAHAnnotation> Entities { get; set; }
    }

    public class TAHAnnotation
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }

        public string Category { get; set; }
        public double ConfidenceScore { get; set; }

        public bool IsNegated { get; set; }
        public IList<TAHAnnotationLink> Links { get; set; }
    }

    public class TAHAnnotationLink
    {
        public TAHAnnotationLink() { }
        public TAHAnnotationLink(string dataSource, string id)
        {
            DataSource = dataSource;
            Id = id;
        }

        public string DataSource { get; set; }
        public string Id { get; set; }
    }
}
