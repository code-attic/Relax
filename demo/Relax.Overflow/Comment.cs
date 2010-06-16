using System;

namespace Relax.Overflow
{
    public class Comment
    {
        public virtual long Id { get; set; }
        public virtual long PostId { get; set; }
        public virtual decimal Score { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual long UserId { get; set; }
    }
}