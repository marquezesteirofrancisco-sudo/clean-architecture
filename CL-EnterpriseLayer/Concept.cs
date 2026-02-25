using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_EnterpriseLayer
{
    public class Concept
    {
        public int Id { get; set; }
        public int IdBeer { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public Concept(int id, int quantity, int idbeer, decimal unitPrice) 
        { 
            Id = id;
            Quantity = quantity;
            IdBeer = idbeer;
            UnitPrice = unitPrice;
            Price = GetTotalPrice();
        }


        private decimal GetTotalPrice()
        {
            return Quantity * UnitPrice;
        }
    }
}
