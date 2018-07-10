using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SystemElementCore.Models
{
    public interface IElementRepository
    {
        Element findParentElementByPermalink(string permalink);
        Element findNullParentId();
        IEnumerable<Element> findParentId(int parentID);
        int StoreElement(Element element);
        void TruncateElements();
    }
}
