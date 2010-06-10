using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relax
{
    public interface IRelaxQueryService
    {
        string[] GetDocumentIdsForQuery(string database, string luceneQuery);
    }
}
