using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Relax.Overflow
{
    [XmlRoot("row")]
    public class Post : CouchDocument
    {
        [XmlAttribute("Id")]
        public virtual long Id { get; set; }
        [XmlAttribute("PostTypeId")]
        public virtual long PostTypeId { get; set; }
        [XmlAttribute("ParentID")]
        public virtual long ParentID { get; set; }
        [XmlAttribute("CreationDate")]
        public virtual DateTime CreationDate { get; set; }
        [XmlAttribute("Score")]
        public virtual decimal Score { get; set; }
        [XmlAttribute("ViewCount")]
        public virtual long ViewCount { get; set; }
        [XmlAttribute("Body")]
        public virtual string Body { get; set; }
        [XmlAttribute("OwnerUserId")]
        public virtual string OwnerUserId { get; set; }
        [XmlAttribute("LastEditorUserId")]
        public virtual string LastEditorUserId { get; set; }
        [XmlAttribute("LastEditorDisplayName")]
        public virtual string LastEditorDisplayName { get; set; }
        [XmlAttribute("LastEditDate")]
        public virtual DateTime LastEditDate { get; set; }
        [XmlAttribute("LastActivityDate")]
        public virtual DateTime LastActivityDate { get; set; }
        [XmlAttribute("Title")]
        public virtual string Title { get; set; }
        [XmlAttribute("Tags")]
        public virtual string Tags { get; set; }
        [XmlAttribute("AnswerCount")]
        public virtual long AnswerCount { get; set; }
        [XmlAttribute("FavoriteCount")]
        public virtual long FavoriteCount { get; set; }
        [XmlAttribute("AcceptedAnswerId")]
        public virtual long AcceptedAnswerId { get; set; }
    }
}
