using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /*  class that implements the IProductRepository interface and gets its data using Entity Framework Core. */
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        /*  the repository implementation just maps the Products property defined by the IProductRepository interface onto the
            Products property defined by the ApplicationDbContext class. The Products property in the context class
            returns a DbSet<Product> object, which implements the IQueryable<T> interface and makes it easy to
            implement the IProductRepository interface when using Entity Framework Core. This ensures that queries to the database will retrieve only the objects that are required 
        */
        public IQueryable<Product> Products => context.Products;
    }
}
