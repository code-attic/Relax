namespace Relax
{
    public interface IHandleJsonDocumentId
    {
        void UpdateKeyFromJson(string jsonKey);
        string GetIdAsJson();
    }
}