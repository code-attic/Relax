using System;
using System.Text;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Http
{
    public class CouchUri : ICloneable
    {
        private StringBuilder _builder = new StringBuilder();
        private bool _hasArguments = false;

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

        public CouchUri Attachment(string attachmentName)
        {
            _builder.AppendFormat("/{0}", attachmentName);
            return this;
        }

        public CouchUri BulkInsert()
        {
            _builder.Append("/_bulk_docs");
            return this;
        }

        public CouchUri ByRange<TKey>(TKey start, TKey end)
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

        public CouchUri Changes(Feed feed, int since)
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

        public CouchUri CleanupViews()
        {
            _builder.Append("/_view_cleanup");
            return this;
        }

        public CouchUri Compact()
        {
            _builder.Append("/_compact");
            return this;
        }

        public CouchUri CompactView(string designDocument)
        {
            _builder.Append("/_compact/{0}".AsFormat(designDocument));
            return this;
        }

        public CouchUri Descending()
        {
            _builder.AppendFormat("{0}descending=true",
                                  _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Design(string designDocumentName)
        {
            _builder.AppendFormat("/_design/{0}", designDocumentName);
            return this;
        }

        public CouchUri Format(string format)
        {
            _builder.AppendFormat("{0}format={1}",
                                  _hasArguments ? "&" : "?", format);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri IncludeDocuments()
        {
            _builder.AppendFormat("{0}include_docs=true", _hasArguments
                                                              ? "&"
                                                              : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri InclusiveEnd()
        {
            _builder.AppendFormat("{0}inclusive_end=true",
                _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Id<TKey>(TKey key)
        {
            _builder.AppendFormat("/{0}", key.ToString().TrimStart('"').TrimEnd('"'));

            return this;
        }

        public CouchUri IdAndRev<TKey, TRev>(TKey key, TRev rev)
        {
            if (!_hasArguments)
                _hasArguments = true;

            _builder.AppendFormat("/{0}?rev={1}",
                                  key.ToString().TrimStart('"').TrimEnd('"'),
                                  rev.ToString().TrimStart('"').TrimEnd('"'));
            return this;
        }

        public CouchUri Key<TKey>(TKey key)
        {
            var json = key.ToJson(false);
            _builder.AppendFormat("{0}key={1}", 
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri KeyAndRev<TKey, TRev>(TKey key, TRev rev)
        {
            var json = key.ToJson(false);
            _builder.AppendFormat("{0}key={1}&rev={2}",
                _hasArguments ? "&" : "?",
                json,
                rev);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Group()
        {
            return Group(1);
        }

        public CouchUri Group(int groupLevel)
        {
            _builder.AppendFormat("{0}group=true&group_level={1}",
                                  _hasArguments ? "&" : "?", groupLevel);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Limit(int limit)
        {
            _builder.AppendFormat("{0}limit={1}",
                                  _hasArguments ? "&" : "?", limit);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri List(string listName)
        {
            _builder.AppendFormat("/_list/{0}", listName);
            return this;
        }

        public CouchUri ListAll()
        {
            _builder.Append("/_all_docs");
            return this;
        }

        public CouchUri NoReduce()
        {
            _builder.AppendFormat("{0}reduce=false",
                                  _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Replicate()
        {
            _builder.Append("/_replicate");
            return this;
        }

        public CouchUri Revision<TRev>(TRev revision)
        {
            _builder.AppendFormat("{0}rev={1}",
                                  _hasArguments ? "&" : "?", revision.ToString().TrimStart('"').TrimEnd('"'));
            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri StartKey<TKey>(TKey start)
        {
            var json = start.ToJson(false);
            _builder.AppendFormat("{0}startkey={1}", 
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri EndKey<TKey>(TKey end)
        {
            var json = end.ToJson(false);
            _builder.AppendFormat("{0}endkey={1}",
                _hasArguments ? "&" : "?",
                json);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri Skip(int number)
        {
            _builder.AppendFormat("{0}skip={1}",
                                  _hasArguments ? "&" : "?", number);

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri StaleOk()
        {
            _builder.AppendFormat("{0}stale=ok",
                _hasArguments ? "&" : "?");

            if (!_hasArguments)
                _hasArguments = true;

            return this;
        }

        public CouchUri View(string viewName)
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
            _builder
                .AppendFormat(@"{0}://{1}:{2}@{3}:{4}", 
                    user,
                    password,
                    prefix, 
                    server, 
                    port);
        }

        public CouchUri(string user, string password, string prefix, string server, int port, string database)
        {
            DatabaseName = database;
            _builder
                .AppendFormat(@"{0}://{1}:{2}@{3}:{4}/{5}", 
                    user,
                    password,
                    prefix, 
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