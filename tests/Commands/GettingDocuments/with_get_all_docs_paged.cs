using Machine.Specifications;

namespace Relax.Tests.Commands.GettingDocuments
{
    public abstract class with_get_all_docs_paged : with_get_all_docs
    {
        private Establish context = () =>
                                        {
                                            url = @"http://localhost:5984/relax/_all_docs?include_docs=true&skip=4&limit=2";
                                        };
    }
}