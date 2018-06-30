using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemElementCore.Models
{
    public class ElementDBContext : DbContext
    {
        public ElementDBContext(DbContextOptions<ElementDBContext> options) : base(options)
        {

        }
        public DbSet<Element> Elements { get; set; }
    }
}
