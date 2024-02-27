using CommunityToolkit.Mvvm.Input;
using PulsePocket.Models;
using PulsePocket.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PulsePocket.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        bool isBusy = false;
        string title = string.Empty;
        bool isBackVisible = false;
        bool fileVisible = false;
        string fileUrl = string.Empty;
        bool isConnected = true;
        string itemId = null;
        List<Item> exItems = new List<Item>();


        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public BookDatabase Database => DependencyService.Get<BookDatabase>();
        public INavigationService Navigation => DependencyService.Get<INavigationService>();

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { SetProperty(ref this.isBusy, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { SetProperty(ref this.title, value); }
        }

        public bool IsBackVisible
        {
            get { return this.isBackVisible; }
            set { SetProperty(ref this.isBackVisible, value); }
        }

        public string FileUrl
        {
            get { return this.fileUrl; }
            set { SetProperty(ref this.fileUrl, value); }
        }
        public bool FileVisible
        {
            get { return this.fileVisible; }
            set { SetProperty(ref this.fileVisible, value); }
        }

        public bool IsConnected
        {
            get { return this.isConnected; }
            set { SetProperty(ref this.isConnected, value); }
        }

        public string ItemId
        {
            get { return this.itemId; }
            set { SetProperty(ref this.itemId, value); }
        }

        public List<Item> ExItems { get {  return this.exItems; } set { SetProperty(ref this.exItems, value); } } 

        public event PropertyChangedEventHandler PropertyChanged;


        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
       



    }
}