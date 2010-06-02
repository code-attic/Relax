namespace Relax.Impl.Model
{
    public interface IHandleJsonDocumentRevision
    {
        void UpdateRevFromJson(string jsonRev);
        string GetRevAsJson();
    }
}