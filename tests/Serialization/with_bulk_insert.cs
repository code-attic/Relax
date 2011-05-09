using System;
using Machine.Specifications;
using Relax.Impl.Json;
using Relax.Tests.Document;

namespace Relax.Tests.Serialization
{
    public abstract class with_bulk_insert
    {
        protected static BulkPersist bulkAction;

        private Establish context = () =>
                                        {
                                            //Assimilate.Core();
                                            bulkAction = new BulkPersist(true, false, 
                                                    new object[] 
                                                    {
                                                        new Person()
                                                            {
                                                                DateOfBirth = DateTime.Now,
                                                                FirstName = "Roy",
                                                                LastName = "Orbison",
                                                                Social = "Wouldn't You like to know : p"
                                                            }
                                                    }
                                                );
                                        };
    }
}
