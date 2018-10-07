using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch
{
    public class MovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
