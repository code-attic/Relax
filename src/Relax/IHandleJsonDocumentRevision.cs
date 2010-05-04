namespace Relax
{
    public interface IHandleJsonDocumentRevision
    {
        void UpdateRevFromJson(string jsonRev);
        string GetRevAsJson();
    }
}