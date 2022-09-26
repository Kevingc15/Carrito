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
            SelectedItems = new ObservableCollection<Item>();
            Item = new Item();
        }

        public ObservableCollection<Item> SelectedItems { get; set; }
        private Item item;
        public Item Item { get => item; set => SetProperty(ref item, value); }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("ListItems"))
            {
                foreach (Item Item in (ObservableCollection<Item>)parameters["ListItems"])
                {
                    SelectedItems.Add(Item);
                }
            }

        }

        public ICommand SumItemCommand => new DelegateCommand<Item>(SumItem);
        public ICommand RestItemCommand => new DelegateCommand<Item>(RestItem);

        public void SumItem(Item item)
        {
            item.Cantidad++;
        }

        public void RestItem(Item item)
        {
            if (item.Cantidad > 1)
            {
                item.Cantidad--;
            }
        }

        public ICommand DeleteItemCommand => new DelegateCommand<Item>(DeleteItem);
        public void DeleteItem(Item item)
        {
            SelectedItems?.Remove(item);
        }
    }
}
