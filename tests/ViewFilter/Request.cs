using System;
using System.Text;
using Relax.Impl;

namespace Relax.Tests.ViewFilter
{
    public class Request : CouchDocument
    {
        public virtual string BusinessJustification { get; set; }
        public virtual decimal CapitalCostEstimate { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual DateTime? DateModified { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual decimal? EstimatedDuration { get; set; }
        public virtual int? Rank { get; set; }
        public virtual string Requestor { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual string Title { get; set; }

        public Request()
        {
        }
    }
}
