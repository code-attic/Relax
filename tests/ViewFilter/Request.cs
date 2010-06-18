using System;
using System.Text;
using Relax.Impl;

namespace Relax.Tests.ViewFilter
{
    public class Request : CouchDocument
    {
        public string Message { get; set; }

        public Request()
        {
        }
    }
}
