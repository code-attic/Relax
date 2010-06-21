using System.Linq;
using Machine.Specifications;
using Relax.Impl.Cache;

namespace Relax.Tests.Caching
{
    public class when_associating_keys
    {
        protected static IKeyAssociationManager manager;
        protected static string[] expected = new string[] { "Mithrandir", "Greyhame", "Storm Crow" };
        protected static string[] actual;
        protected static string[] keyList;
        private Establish context = () => manager = new DefaultKeyAssociationManager();

        private Because of = () =>
                                 {
                                     manager.AddKeyAssociation("Gandalf", "Storm Crow");
                                     manager.AddKeyAssociation("Gandalf", "Greyhame");
                                     manager.AddKeyAssociation("Gandalf", "Mithrandir");
                                     actual = manager.GetAssociations("Gandalf").ToArray();
                                     keyList = manager.GetAllKeys().ToArray();
                                 };

        private It should_have_key_list = () => keyList.SequenceEqual(new [] {"Gandalf"});
        private It should_have_all_keys_previously_associated = () => 
                                                                actual.SequenceEqual(expected).ShouldBeTrue();
    }
}