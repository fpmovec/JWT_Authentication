using JwtAuthorization.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthorization.Context
{
    public class BooksDbContext : IdentityDbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> opts) : base(opts)
        {
                
        }
        public DbSet<Book> Books { get; set; }
    }
}
