using System;
using System.Collections.Generic;
using System.Text;

namespace Carrito.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Cantidad { get; set; }
        public double Total()
        {
            return Price * Cantidad;
        } 
    }
}
