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

            SelectedItems = new List<Item>();

            GetItems();

        }

        private List<Item> items;
        public List<Item> Items { get => items; set => SetProperty(ref items, value); }

        public List<Item> selectedItems;
        public List<Item> SelectedItems { get => selectedItems; set => SetProperty(ref selectedItems, value); }

        private double total;
        public double Total { get => total; set => SetProperty(ref total, value); }

        private int itemsAmount;
        public int ItemsAmount { get => itemsAmount; set => SetProperty(ref itemsAmount, value); }

        public ICommand SumItemCommand => new DelegateCommand<Item>(SumItem);
        public ICommand RestItemCommand => new DelegateCommand<Item>(RestItem);
        public ICommand AddShoppingCarCommand => new DelegateCommand(AddShoppingCar);
        public ICommand GoToShoppingCarCommand => new DelegateCommand(GoToShopping);

        public void SumItem(Item item)
        {
            item.Amount++;
        }

        public void RestItem(Item item)
        {
            if(item.Amount > 0)
            {
                item.Amount--;
            }
        }

        public void AddShoppingCar()
        {
            SelectedItems.Clear();
            Total = 0;
            itemsAmount = 0; 
            foreach (Item item in Items)
            {
                if (item.Amount > 0) SelectedItems.Add(item);
                Total += item.Price * item.Amount;
                ItemsAmount += item.Amount;
            }
        }


        public async void GoToShopping()
        {
            if(SelectedItems.Count > 0)
            {
                var parameters = new NavigationParameters();
                parameters.Add("ListItems", SelectedItems);
                await NavigationService.NavigateAsync(NavigationConstants.CarritoPage, parameters);
            }
        }
        private void GetItems()
        {
            Items = itemsRepository.GetItems();
        }

    }
}
