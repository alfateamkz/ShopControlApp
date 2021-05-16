using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopControlApp
{
    class DatabaseContext : DbContext

    {
        public string connectionString = @"Server = DESKTOP-CSPALBL\SQL369; Database=Shop;Trusted_Connection=True;";

        public DbSet<Check> Checks { get; set; }
        public DbSet<DiscontCard> DiscontCards { get; set; }
        public DbSet<Product> Goods { get; set; }
        public DbSet<GoodsAtWarehouses> GoodsAtWarehouse { get; set; }
        public DbSet<GoodsInCheck> GoodsInCheck { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        protected override void OnConfiguring (DbContextOptionsBuilder db)
        {
            db.UseSqlServer(connectionString);
            
        }
        protected override void OnModelCreating (ModelBuilder mb)
        {
            
        }

        public DatabaseContext()
        {
            Database.Migrate();
        }
    }
}
