using System;

namespace Relax.Overflow
{
    public class Users
    {
        public virtual long Id { get; set; }
        public virtual decimal Reputation { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string EmailHash { get; set; }
        public virtual DateTime LastAccessDate { get; set; }
        public virtual string WebSiteUrl { get; set; }
        public virtual string Location { get; set; }
        public virtual long Age { get; set; }
        public virtual string AboutMe { get; set; }
        public virtual long Views { get; set; }
        public virtual long UpVotes { get; set; }
        public virtual long DownVotes { get; set; }
    }
}