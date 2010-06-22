using System;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Http
{
    public class CouchUri : ICloneable
    {
        private StringBuilder _builder = new StringBuilder();
        private bool _hasArguments = false;
        private JsonUrlEncoder encoder = new JsonUrlEncoder();

        public string DatabaseName { get; set; }

        public static CouchUri Build(string prefix, string server, int port, string database)
        {
            return new CouchUri(prefix, server, port, database);
        }

        public static CouchUri Build(string user, string password, string prefix, string server, int port, string database)
        {
            return new CouchUri(user, password, prefix, server, port, database);
        }

        public static CouchUri Build(string prefix, string server, int port)
        {
            return new CouchUri(prefix, server, port);
        }

        public static CouchUri Build(string user, string password, string prefix, string server, int port)
        {
            return new CouchUri(user, password, prefix, server, port);
        }

        public virtual CouchUri Attachment(string attachmentName)
        {
            _builder.AppendFormat("/{0}", attachmentName);
            return this;
        }

        public virtual CouchUri BulkInsert()
        {
            _builder.Append("/_bulk_docs");
            return this;
        }

        public virtual CouchUri ByRange<TKey>(TKey start, TKey end)
        {
            var startKey = start.ToJson(false);
            var endKey = end.ToJson(false);

            _builder.AppendFormat("{0}startkey={1}&endkey={2}",
                                  _hasArguments ? "&" : "?",
                                  startKey,
                                  endKey);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Changes(Feed feed, int since)
        {
            _builder.Append("/_changes");

            if (feed != Feed.Simple)
            {
                _builder.AppendFormat("?feed={0}",
                                      feed == Feed.Continuous
                                          ? "continuous"
                                          : "longpoll");
            }
            _builder.AppendFormat("&since={0}", since);
            _hasArguments = true;
            return this;
        }

        public virtual CouchUri CleanupViews()
        {
            _builder.Append("/_view_cleanup");
            return this;
        }

        public virtual CouchUri Compact()
        {
            _builder.Append("/_compact");
            return this;
        }

        public virtual CouchUri CompactView(string designDocument)
        {
            _builder.Append("/_compact/{0}".AsFormat(designDocument));
            return this;
        }

        public virtual CouchUri Descending()
        {
            _builder.AppendFormat("{0}descending=true",
                                  _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Design(string designDocumentName)
        {
            _builder.AppendFormat("/_design/{0}", designDocumentName);
            return this;
        }

        public virtual CouchUri Format(string format)
        {
            _builder.AppendFormat("{0}format={1}",
                                  _hasArguments ? "&" : "?", format);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri IncludeDocuments()
        {
            _builder.AppendFormat("{0}include_docs=true", _hasArguments
                                                              ? "&"
                                                              : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri InclusiveEnd()
        {
            _builder.AppendFormat("{0}inclusive_end=true",
                _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Id<TKey>(TKey key)
        {
            _builder.AppendFormat("/{0}", key.ToString().TrimStart('"').TrimEnd('"'));

            return this;
        }

        public virtual CouchUri IdAndRev<TKey, TRev>(TKey key, TRev rev)
        {
            if (!_hasArguments)
                _hasArguments = true;

            _builder.AppendFormat("/{0}?rev={1}",
                                  key.ToString().TrimStart('"').TrimEnd('"'),
                                  rev.ToString().TrimStart('"').TrimEnd('"'));
            return this;
        }

        public virtual CouchUri Key<TKey>(TKey key)
        {
            var json = encoder.Encode(key.ToJson(false));

            _builder.AppendFormat("{0}key={1}", 
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri KeyAndRev<TKey, TRev>(TKey key, TRev rev)
        {
            var json = encoder.Encode(key.ToJson(false));
            _builder.AppendFormat("{0}key={1}&rev={2}",
                _hasArguments ? "&" : "?",
                json,
                rev);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Group()
        {
            return Group(1);
        }

        public virtual CouchUri Group(int groupLevel)
        {
            _builder.AppendFormat("{0}group=true&group_level={1}",
                                  _hasArguments ? "&" : "?", groupLevel);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Limit(int limit)
        {
            _builder.AppendFormat("{0}limit={1}",
                                  _hasArguments ? "&" : "?", limit);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri List(string listName)
        {
            _builder.AppendFormat("/_list/{0}", listName);
            return this;
        }

        public virtual CouchUri ListAll()
        {
            _builder.Append("/_all_docs");
            return this;
        }

        public virtual CouchUri NoReduce()
        {
            _builder.AppendFormat("{0}reduce=false",
                                  _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Replicate()
        {
            _builder.Append("/_replicate");
            return this;
        }

        public virtual CouchUri Revision<TRev>(TRev revision)
        {
            _builder.AppendFormat("{0}rev={1}",
                                  _hasArguments ? "&" : "?", revision.ToString().TrimStart('"').TrimEnd('"'));
            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri StartKey<TKey>(TKey start)
        {
            var json = encoder.Encode(start.ToJson(false));
            _builder.AppendFormat("{0}startkey={1}", 
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri EndKey<TKey>(TKey end)
        {
            var json = encoder.Encode(end.ToJson(false));
            _builder.AppendFormat("{0}endkey={1}",
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri Skip(int number)
        {
            _builder.AppendFormat("{0}skip={1}",
                                  _hasArguments ? "&" : "?", number);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri StaleOk()
        {
            _builder.AppendFormat("{0}stale=ok",
                _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public virtual CouchUri View(string viewName)
        {
            _builder.AppendFormat("/_view/{0}", viewName);
            return this;
        }

        public CouchUri(string prefix, string server, int port, string database)
        {
            DatabaseName = database;
            _builder
                .AppendFormat(@"{0}://{1}:{2}/{3}", prefix, server, port, database);
        }

        public CouchUri(string prefix, string server, int port)
        {
            _builder
                .AppendFormat(@"{0}://{1}:{2}", prefix, server, port);
        }

        public CouchUri(string user, string password, string prefix, string server, int port)
        {
            user = HttpUtility.UrlEncode(user);
            password = HttpUtility.UrlEncode(password);

            _builder
                .AppendFormat(@"{0}://{1}:{2}@{3}:{4}",
                    prefix,    
                    user,
                    password,
                    server, 
                    port);
        }

        public CouchUri(string user, string password, string prefix, string server, int port, string database)
        {
            user = HttpUtility.UrlEncode(user);
            password = HttpUtility.UrlEncode(password);

            DatabaseName = database;
            _builder
                .AppendFormat(@"{0}://{1}:{2}@{3}:{4}/{5}",
                    prefix,
                    user,
                    password, 
                    server, 
                    port, 
                    database);
        }

        protected CouchUri(string content, bool hasArgs)
        {
            _builder.Append(content);
            _hasArguments = hasArgs;
        }

        public object Clone()
        {
            return new CouchUri(ToString(), _hasArguments);
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}