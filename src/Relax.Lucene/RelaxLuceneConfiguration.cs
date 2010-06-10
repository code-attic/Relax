using System.Collections.Generic;

namespace Relax.Lucene
{
    public class RelaxLuceneConfiguration : IRelaxLuceneConfiguration
    {
        public HashSet<string> Databases { get; set; }

        public RelaxLuceneConfiguration()
        {
            Databases = new HashSet<string>();
        }
    }
}