using Machine.Specifications;

namespace Relax.Tests.Commands
{
    public abstract class with_get_doc_by_id_and_rev_setup : with_get_doc_by_id_setup
    {
        private Establish context = () =>
                                        {
                                            url = @"http://localhost:5984/testdoc/1?rev=1";
                                        };
    }
}