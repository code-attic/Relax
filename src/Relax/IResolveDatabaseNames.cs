namespace Relax
{
    public interface IResolveDatabaseNames
    {
        string GetDatabaseNameFor<TModel>();
    }
}