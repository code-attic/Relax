using Machine.Specifications;
using Relax.Impl.Cache;
using It = Machine.Specifications.It;

namespace Relax.Tests.Caching
{
    public class when_adding_cross_reference : with_couch_cache_provider
    {
        protected static ICouchCacheProvider couchCacheProvider;

        private Because of = () =>
                                 {
                                     keyAssociationManagerMock
                                         .Setup(x => x.AddKeyAssociation("item_1", "cache_key"));
                                     couchCacheProvider = CouchCacheProvider;
                                     couchCacheProvider.AddCrossReference("item_1", "cache_key");
                                 };

        private It calling_add_cross_reference_should_add_association_to_key_manager = () => 
                                                                                       keyAssociationManagerMock.Verify();
    }
}