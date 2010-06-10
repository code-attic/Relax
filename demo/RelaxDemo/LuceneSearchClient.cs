using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Relax;
using Symbiote.Restfully;

namespace RelaxDemo
{
    public class LuceneSearchClient
    {
        protected IRemoteProxy<IRelaxQueryService> queryService;

        public string[] GetMatchesForQuery()
        {
            return queryService.Call(x => x.GetDocumentIdsForQuery("testdocument", "Message:Doc*"));
        }

        public LuceneSearchClient(IRemoteProxy<IRelaxQueryService> queryService)
        {
            this.queryService = queryService;
        }
    }
}
