using StructureMap;

namespace Relax.Impl
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