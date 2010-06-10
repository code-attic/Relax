using System;
using System.IO;
using System.Net;
using System.Text;
using Relax.Config;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Http
{
    public class HttpAction : IHttpAction
    {
        protected ICouchConfiguration configuration { get; set; }
        protected bool _pollForChanges = false;

        public virtual string GetResponse(CouchUri uri, string method, string body)
        {
            var request = WebRequest.Create(uri.ToString());
            request.Method = method;
            request.Timeout = configuration.TimeOut;
            //request.PreAuthenticate = configuration.Preauthorize;

            if (!string.IsNullOrEmpty(body))
            {
                var bytes = UTF8Encoding.UTF8.GetBytes(body);
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = bytes.Length;

                var writer = request.GetRequestStream();
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }

            var result = "";
            var response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
                response.Close();
            }

            return result;
        }

        public virtual void GetContinuousResponse(CouchUri uri, int since, Action<string, ChangeRecord> callback)
        {
            var baseUri = uri.Clone() as CouchUri;
            uri = uri.Changes(Feed.Continuous, since);
            var request = WebRequest.Create(uri.ToString());
            request.Method = "GET";
            request.Timeout = int.MaxValue;
            request.PreAuthenticate = configuration.Preauthorize;
            var result = "";
            _pollForChanges = true;

            try
            {
                var response = request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while (_pollForChanges)
                    {
                        result = reader.ReadLine();
                        if (!string.IsNullOrEmpty(result))
                        {
                            var changeUri = baseUri.Clone() as CouchUri;
                            var change = result.FromJson<ChangeRecord>();
                            change.Document = GetResponse(changeUri.Id(change.Id), "GET", "");
                            callback.BeginInvoke(uri.DatabaseName, change, null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                "An exception occurred while receiving the change stream from {0}. \r\n\t {1}"
                    .ToError<IDocumentRepository>(uri.ToString(), ex);
                throw;
            }
            finally
            {
                callback = null;
            }
        }

        public virtual Tuple<string, byte[]> GetAttachment(CouchUri uri)
        {
            var request = WebRequest.Create(uri.ToString());
            request.Method = "GET";
            request.Timeout = configuration.TimeOut;

            var response = request.GetResponse();
            int bytesRead = 0;
            var memoryStream = new MemoryStream();
            
            using(var stream = response.GetResponseStream())
            {
                stream.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;
            var result =
                Tuple.Create(
                    response.ContentType, 
                    memoryStream.ToArray()
                    );

            response.Close();

            return result;
        }

        public virtual string SaveAttachment(CouchUri uri, string type, byte[] content)
        {
            using(var client = new WebClient())
            {
                client.Headers.Add("Content-Type", type);
                var bytes = client.UploadData(uri.ToString(), "PUT", content);
                return UTF8Encoding.UTF8.GetString(bytes);
            }
        }

        public virtual void StopContinousResponse()
        {
            _pollForChanges = false;
        }

        public virtual string Post(CouchUri uri)
        {
            return GetResponse(uri, "POST", "");
        }

        public virtual string Post(CouchUri uri, string body)
        {
            return GetResponse(uri, "POST", body);
        }

        public virtual string Put(CouchUri uri)
        {
            return GetResponse(uri, "PUT", "");
        }

        public virtual string Put(CouchUri uri, string body)
        {
            return GetResponse(uri, "PUT", body);
        }

        public virtual string Get(CouchUri uri)
        {
            return GetResponse(uri, "GET", "");
        }

        public virtual string Delete(CouchUri uri)
        {
            return GetResponse(uri, "DELETE", "");
        }

        public HttpAction(ICouchConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }


}