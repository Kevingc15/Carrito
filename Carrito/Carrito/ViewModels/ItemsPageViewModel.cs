using Carrito.Constants;
using Carrito.Contracts;
using Carrito.Models;
using Carrito.Repositories;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Carrito.ViewModels
{
    public class ItemsPageViewModel : ViewModelBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsPageViewModel(INavigationService navigationService
            , IItemsRepository itemsRepository)
            : base(navigationService)
        {
            this.itemsRepository = itemsRepository;

            Item = new Item();
            Items = new ObservableCollection<Item>();
            SelectedItems = new ObservableCollection<Item>();
            GetItems();

            Total = 0;
        }

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Item> SelectedItems { get; set; }
        public Item Item { get; set; }

        public double Total { get; set; }

        public ICommand SumItemCommand => new DelegateCommand<Item>(SumItem);
        public ICommand RestItemCommand => new DelegateCommand<Item>(RestItem);

        public void SumItem(Item item)
        {
            item.Cantidad++;
        }

        public void RestItem(Item item)
        {
            if(item.Cantidad > 0)
            {
                item.Cantidad--;
            }
        }

        public ICommand AddCarritoCommand => new DelegateCommand<ObservableCollection<Item>>(AddCarrito);
        public void AddCarrito(ObservableCollection<Item> items)
        {
            SelectedItems.Clear();
            foreach(Item item in items)
            {
                if (item.Cantidad > 0) SelectedItems.Add(item);
                Total += item.Total();
            }
        }

        public ICommand GoToCarritoCommand => new DelegateCommand<ObservableCollection<Item>>(GoToCarrito);

        public async void GoToCarrito(ObservableCollection<Item> items)
        {
            if(items.Count > 0)
            {
                var parameters = new NavigationParameters();
                parameters.Add("ListItems", items);
                await NavigationService.NavigateAsync(NavigationConstants.CarritoPage, parameters);
            }
        }
        private void GetItems()
        {
            Items = itemsRepository.GetItems();
        }

    }
}
