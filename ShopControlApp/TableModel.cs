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
    public class Check
    {
        [Key]
        public int ID { get; set; }
        [Required][ConcurrencyCheck]
        public int SellerID { get; set; }
        public Seller Seller { get; set; }

        [ConcurrencyCheck]
        public int DiscontID { get; set; }

        public DiscontCard Discont { get; set; }
        [Required]
        [ConcurrencyCheck]
    //    [Column(TypeName="Date")]
        public DateTime SellDate { get; set; }
        [ConcurrencyCheck]
        public string ListOfProducts { get; set; }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
    public class DiscontCard
    { 
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public double Percentage { get; set; }
        [Required]
        [ConcurrencyCheck]
        public double Sum { get; set; }

        [Phone]
        [Required]
        [ConcurrencyCheck]
        public string Phone { get; set; }

        public override string ToString()
        {
            return ID.ToString() + " телефон : "+Phone;
        }
    }
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int ManufacturerID { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Title { get; set; }
        [Required]
        [ConcurrencyCheck]
        public double Price { get; set; }
        [ConcurrencyCheck]
        public string Warranty { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
    public class GoodsAtWarehouses
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int GoodsID { get; set; }
        public Product Goods { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int WarehouseID { get; set; }
        public Warehouse Warehouse { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int Quantity { get; set; }



    }
    public class GoodsInCheck
    {
        [ConcurrencyCheck]
        [Key]
        public int ID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int GoodsWarehouseID { get; set; }
        public GoodsAtWarehouses GoodsWarehouse { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int CheckID { get; set; }
        public Check Check { get; set; }
    }

    public class Manufacturer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Title { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Country { get; set; }
        [ConcurrencyCheck]
        public string City { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
    public class Position
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
    public class Seller
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Name { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Surname { get; set; }
        [ConcurrencyCheck]
        public string Patronymic { get; set; }
        [Required]
        [ConcurrencyCheck]
        public DateTime Birthday { get; set; }
        [Required]
        [ConcurrencyCheck]
        public DateTime EmploymentDay { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Username { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Password { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int PositionID { get; set; }
        public Position Position { get; set; }
        public override string ToString()
        {
            return Name + " "+Surname;
        }
    }

    public class Warehouse
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ConcurrencyCheck]
        public string Address { get; set; }
        public override string ToString()
        {
            return Address;
        }

    }
}

