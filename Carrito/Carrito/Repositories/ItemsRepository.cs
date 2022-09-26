using Carrito.Contracts;
using Carrito.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Carrito.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        public ObservableCollection<Item> GetItems()
        {
            Item item1 = new Item();
            item1.Id = 1;
            item1.Name = "Fuego y Sangre";
            item1.Price = 49.900;
            Item item2 = new Item();
            item2.Id = 2;
            item2.Name = "Choque de reyes";
            item2.Price = 45.000;
            Item item3 = new Item();
            item3.Id = 3;
            item3.Name = "Tormenta de espadas";
            item3.Price = 63.200;

            ObservableCollection<Item> items = new ObservableCollection<Item>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            return items;
        }
    }
}
