using Carrito.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Carrito.Contracts
{
    public interface IItemsRepository
    {
        List<Item> GetItems();
    }
}
