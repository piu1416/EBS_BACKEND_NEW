using ElectricityBillingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectricityBillingSystem.Models
{
    public class EBS_DbContext : DbContext
    {
        public EBS_DbContext(DbContextOptions<EBS_DbContext> options) : base(options)
        {
        }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MCSHB6J;Initial Catalog=EBS_BillDB;Integrated Security=True;");
        }
    }
}
