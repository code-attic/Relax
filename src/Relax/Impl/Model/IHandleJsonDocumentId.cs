namespace Relax.Impl.Model
{
    public interface IHandleJsonDocumentId
    {
        void UpdateKeyFromJson(string jsonKey);
        string GetIdAsJson();
    }
}