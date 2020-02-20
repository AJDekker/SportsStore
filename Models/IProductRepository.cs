using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /* getting Product objects from a database */
    public interface IProductRepository
    {
        /* collection of objects that can be queried */
        IQueryable<Product> Products { get; }
    }
}
