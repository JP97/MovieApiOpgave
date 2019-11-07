using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApiOpgave.Models;


namespace MovieApiOpgave.Models
{
    public class MovieApiOpgaveContext : DbContext
    {
        public MovieApiOpgaveContext (DbContextOptions<MovieApiOpgaveContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
