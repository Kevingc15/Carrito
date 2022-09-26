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

            SelectedItems = new ObservableCollection<Item>();

            GetItems();

        }

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Item> SelectedItems { get; set; }

        private double total;
        public double Total { get => total; set => SetProperty(ref total, value); }

        public ICommand SumItemCommand => new DelegateCommand<Item>(SumItem);
        public ICommand RestItemCommand => new DelegateCommand<Item>(RestItem);

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

        public ICommand AddShoppingCarCommand => new DelegateCommand(AddShoppingCar);
        public void AddShoppingCar()
        {
            SelectedItems.Clear();
            Total = 0;
            foreach(Item item in Items)
            {
                if (item.Amount > 0) SelectedItems.Add(item);
                Total += item.Price * item.Amount;
            }
        }

        public ICommand GoToShoppingCarCommand => new DelegateCommand(GoToShopping);

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
