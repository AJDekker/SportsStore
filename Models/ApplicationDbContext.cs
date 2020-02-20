using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /* The database context class is the bridge between the application and Entity Framework Core and provides access to the application’s data using model objects.  */
    /*  DbContext base class provides access to the Entity Framework Core’s underlying functionality */
    public class ApplicationDbContext : DbContext 
    {
        /* The ApplicationDbContext class is derived from DbContext and adds the properties that will be used to read and write the application’s data.  */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /* Products property will provide access to the Product objects in the database. */
        public DbSet<Product> Products { get; set; }
    }
}
