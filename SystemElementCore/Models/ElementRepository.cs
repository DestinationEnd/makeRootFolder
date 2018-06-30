using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemElementCore.Models
{
    public class ElementRepository : IElementRepository
    {
        private readonly ElementDBContext dBContext;
        private Boolean Disposed;
        public ElementRepository(ElementDBContext context)
        {
            dBContext = context;
        }
        public Element findNullParentId()
        {
            return dBContext.Elements.Where(u => u.ParentId == null).FirstOrDefault();
        }

        public Element findParentElementByPermalink(string permalink)
        {
            return dBContext.Elements.Where(u => u.Url == permalink).FirstOrDefault();
        }
        public IEnumerable<Element> findParentId(int parentID)
        {
            return dBContext.Elements.Where(u => u.ParentId == parentID);
        }
    }
}
