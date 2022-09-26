using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Carrito.Models
{
    public class Item : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        private int amount;
        public int Amount { get => amount; set => SetProperty(ref amount, value); }
    }
}
