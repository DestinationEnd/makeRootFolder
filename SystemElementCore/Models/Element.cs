using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemElementCore.Models
{
    public class Element
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? Sort { get; set; }
        public int? ParentId { get; set; }
        public Element Parent { get; set; }

        public ICollection<Element> Children { get; set; }
        public Element()
        {
            Children = new List<Element>();
        }
    }
}
