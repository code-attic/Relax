using StructureMap;

namespace Symbiote.Relax.Impl
{
    public class CouchCommandFactory : ICouchCommandFactory
    {
        public ICouchCommand GetCommand()
        {
            return ObjectFactory.GetInstance<ICouchCommand>();
        }

        public CouchCommandFactory()
        {
        }
    }
}