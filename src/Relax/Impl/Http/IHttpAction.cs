using System;

namespace Relax.Impl.Http
{
    public interface IHttpAction
    {
        string GetResponse(CouchUri uri, string method, string body);
        Tuple<string, byte[]> GetAttachment(CouchUri uri);
        string SaveAttachment(CouchUri uri, string type, byte[] content);
        void GetContinuousResponse(CouchUri uri, int since, Action<string, ChangeRecord> callback);
        void StopContinousResponse();
        string Post(CouchUri uri);
        string Post(CouchUri uri, string body);
        string Put(CouchUri uri);
        string Put(CouchUri uri, string body);
        string Get(CouchUri uri);
        string Delete(CouchUri uri);
    }
}