using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public int StoreElement(Element element)
        {
            dBContext.Elements.Add(element);
            dBContext.SaveChanges();
            return element.Id;
        }

        public void TruncateElements()
        {
            dBContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Elements]");
        }


        //public void StoreElements(List<Element> elements)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Id", typeof(String));
        //    dt.Columns.Add("Url", typeof(String));
        //    dt.Columns.Add("Sort", typeof(String));
        //    dt.Columns.Add("ParentId", typeof(String));
        //    DataRow workRow;
        //    foreach (Element element in elements)
        //    {
        //        workRow = dt.NewRow();
        //        workRow["Id"] = element.Id;
        //        workRow["Url"] = element.Url;
        //        workRow["Sort"] = element.Sort;
        //        workRow["ParentId"] = element.ParentId;
        //        dt.Rows.Add(workRow);
        //    }
        //    using (dBContext)
        //    {
        //        dBContext.Database.ExecuteSqlCommand("exec InsertElements @dt ", dt);
        //    }
        //}
    }
}
