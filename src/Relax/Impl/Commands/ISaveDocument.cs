namespace Relax.Impl.Commands
{
    public interface ISaveDocument
    {
        CommandResult Save<TModel>(TModel model);
        CommandResult Save(string databaseName, object model);
    }
}