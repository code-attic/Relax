using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Symbiote.Core.Extensions;

namespace Relax.Lucene.Tests
{
    public abstract class with_domain_model : with_lucene
    {
        protected static List<Person> people;
        protected static List<string> personDocuments;

        private Establish context = () =>
                                        {
                                            people = DomainHelper.Create_Family_With_Two_Cars();
                                            personDocuments = people.Select(x => x.ToJson(false)).ToList<string>();
                                        };
    }
}