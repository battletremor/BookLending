using Microsoft.EntityFrameworkCore;
using DTO.Models;

namespace DTO.DBContexts
{
    public class DTOContext : DbContext
    {
        public DTOContext(DbContextOptions<DTOContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ExchangeRequest> ExchangeRequests { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TO-DO: add triggers and stored procedures to handle micros
        }

    }//class
}
