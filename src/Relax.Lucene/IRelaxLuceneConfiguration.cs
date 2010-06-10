using System.Collections.Generic;

namespace Relax.Lucene
{
    public interface IRelaxLuceneConfiguration
    {
        HashSet<string> Databases { get; set; }
    }
}