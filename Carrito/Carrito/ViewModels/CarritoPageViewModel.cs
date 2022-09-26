using Carrito.Contracts;
using Carrito.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Carrito.ViewModels
{
    public class CarritoPageViewModel : ViewModelBase
    {
        public CarritoPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            SelectedItems = new List<Item>();
        }

        public List<Item> selectedItems;
        public List<Item> SelectedItems { get => selectedItems; set => SetProperty(ref selectedItems, value); }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("ListItems"))
            {
                foreach (Item Item in (List<Item>)parameters["ListItems"])
                {
                    SelectedItems.Add(Item);
                }
            }

        }

        public ICommand SumItemCommand => new DelegateCommand<Item>(SumItem);
        public ICommand RestItemCommand => new DelegateCommand<Item>(RestItem);

        public void SumItem(Item item)
        {
            item.Amount++;
        }

        public void RestItem(Item item)
        {
            if (item.Amount > 1)
            {
                item.Amount--;
            }
        }

        public ICommand DeleteItemCommand => new DelegateCommand<Item>(DeleteItem);
        public void DeleteItem(Item item)
        {
            SelectedItems.Remove(item);
        }
    }
}