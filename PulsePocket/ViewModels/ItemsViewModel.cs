using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using DevExpress.Maui.Pdf;
using DevExpress.Utils.Url;
using DevExpress.XtraSpreadsheet.Model;
using PulsePocket.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.IO;
using Microsoft.Maui.Storage;
using DevExpress.DataAccess.Native.Web;
using PulsePocket.Services;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace PulsePocket.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        Item _selectedItem;

        public bool IsBackTapped = false;



        BookDatabase database = new BookDatabase();
        public ItemsViewModel()
        {
            Title = "";
            IsBackVisible = false;
            FileVisible = false;
            FileUrl = string.Empty;
            IsConnected = false;
            ExItems = null;
            ItemId = null;
            Items = new ObservableCollection<Item>();
            Files = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            BackTapped = new Command(BackButton);
            LoadFiles = new Command<Item>(GetFiles);
        }





        public ObservableCollection<Item> Items { get; }

        public ObservableCollection<Item> Files { get; }
        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }

        public Command<Item> ItemTapped { get; }

        public Command BackTapped { get; }

        public Command<Item> LoadFiles { get; }
        public Item SelectedItem
        {
            get => this._selectedItem;
            set
            {
                SetProperty(ref this._selectedItem, value);
                OnItemSelected(value);
            }
        }

        List<BackList> backList = new List<BackList>();



        public async Task<bool> ConnectionStatus()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://sbapi.apvaresh.com/api/online");
                    if (response.StatusCode == HttpStatusCode.OK)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            IsBackVisible = false;
            FileVisible = false;
            ExecuteLoadItemsCommand().Wait();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }




        //public ICommand PerformSearch => new Command<string>((string query) =>
        //{

        //    var searchList = new List<Item>();
        //    var exItems = Items.ToList();

        //    foreach (var i in Items)
        //    {
        //        if (i.Text.Contains(query))
        //            searchList.Add(i);
        //    }
        //    Items.Clear();
        //    for (var x = 0; x < searchList.Count; x++)
        //    {
        //        Items.Add(searchList[x]);
        //    }


        //});


        public async void PerformSearch(string query)
        {

            var searchList = new List<Item>();


            if (query == string.Empty)
            {
                //Items.Clear();
                //for (var x = 0; x < ExItems.Count; x++)
                //{
                //    Items.Add(ExItems[x]);
                //}
                //ExItems.Clear();
                Items.Clear();
                var items = await DataStore.GetItemsList(ItemId);
                foreach (var i in items)
                {
                    Items.Add(i);
                }
            }
            else
            {
                //if (query.Length - 1 == 0)
                //    ExItems = Items.ToList();

                Items.Clear();
                var items = await DataStore.GetItemsList(ItemId);
                foreach (var i in items)
                {
                    Items.Add(i);
                }

                foreach (var i in Items)
                {
                    
                    if (i.Text.ToLower().Contains(query.ToLower()))
                        searchList.Add(i);
                }
                Items.Clear();
                for (var x = 0; x < searchList.Count; x++)
                {
                    Items.Add(searchList[x]);
                }
            }

        }




        async void OnAddItem(object obj)
        {
            await Navigation.NavigateToAsync<NewItemViewModel>(null);
        }


        public class BackList
        {
            public string Id { get; set; }
            public string ParentId { get; set; }
        }



        void BackButton()
        {
            IsBackTapped = true;
            OnItemSelected(null);
        }

        async void GetFiles(Item item)
        {
            if (item == null)
                return;
            else
            {
                var files = await DataStore.GetItemsList(item.Id);
                foreach (var file in files)
                {
                    Files.Add(file);
                }

            }

        }

        async void OnItemSelected(Item item)
        {

            ItemId = null;

            if (item == null || item.IsFile == false)
            {
                if (IsBackTapped)
                {
                    var last = backList.LastOrDefault();

                    Items.Clear();
                    if (last.ParentId != null)
                    {
                        var items = await DataStore.GetItemsList(last.ParentId);
                        foreach (var i in items)
                        {
                            Items.Add(i);
                        }
                        backList.RemoveAt(backList.Count - 1);
                    }
                    else
                    {
                        IsBackVisible = false;
                        var items = await DataStore.GetItemsAsync(true);
                        foreach (var i in items)
                        {
                            Items.Add(i);
                        }

                    }

                    IsBackTapped = false;
                }
                else
                {

                    if (item == null)
                        return;
                    else
                    {
                        BackList entity = new BackList();
                        entity.Id = item.Id;
                        entity.ParentId = item.ParentId;
                        backList.Add(entity);
                        ItemId = item.Id;

                        Items.Clear();
                        var items = await DataStore.GetItemsList(item.Id);
                        foreach (var i in items)
                        {
                            Items.Add(i);
                        }
                        IsBackVisible = true;
                    }
                    IsBackTapped = false;

                }
            }
            else
            {

                bool test = await ConnectionStatus();
                var book = await database.GetBookAsync(item.FileId, item.BookId);
                var file = await DataStore.GetItemAsync(item.Id);


                if ((book == null || book.Version < file.Version) && test == true)
                {
                    FileUrl = "https://fleet.flypersia.aero/airpocket/upload/clientsfiles/" + item.Url;

                    var entity = new Book();
                    entity.Id = book == null ? 0 : book.Id;
                    entity.BookId = item.Id;
                    entity.FileId = item.FileId;
                    entity.ParentId = item.ParentId;
                    entity.Version = item.Version;
                    entity.Url = item.Url;


                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(FileUrl);
                        using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                        {
                            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), item.Url);
                            using (FileStream streamToWriteTo = File.Create(fileName))
                            {
                                await streamToReadFrom.CopyToAsync(streamToWriteTo);
                                await database.SaveBookAsync(entity);

                            }
                        }
                    }
                    FileVisible = true;
                }
                else if (book == null && test == false)
                {
                    await Application.Current.MainPage.DisplayAlert("Network Connection", "Please Check Your Network Connection.", "Ok");
                }
                else
                {
                    FileUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), book.Url);
                    FileVisible = true;
                }
            }


        }
    }
}