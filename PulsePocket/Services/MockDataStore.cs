//using Intents;
using DevExpress.XtraRichEdit.API.Native;
using PulsePocket.Models;

namespace PulsePocket.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        BookDatabase books = new BookDatabase();


        public MockDataStore()
        {
            DateTime baseDate = DateTime.Today;
            this.items = new List<Item>() {
                new Item { Id = "1",ParentId = null,BookId = "1" ,Version = 1,FileId = "1231" ,Text = "First item", Description="This is an item description.", StartTime = baseDate.AddHours(1), EndTime = baseDate.AddHours(2), Value=17.098, IsFile = false, FilesCount = 3, Url= null },
                new Item { Id = "2",ParentId = null,BookId = "2",Version = 0,FileId = "1232", Text = "Second item", Description="This is an item description.", StartTime = baseDate.AddHours(2), EndTime = baseDate.AddHours(4), Value=9.985, IsFile = false , FilesCount = 1,Url= null},
                new Item { Id = "3",ParentId = null,BookId = "3",Version = 0,FileId = "1233", Text = "Third item", Description="This is an item description.", StartTime = baseDate.AddHours(3), EndTime = baseDate.AddHours(5), Value=9.597, IsFile = false , FilesCount = 0,Url= null},
                new Item { Id = "4",ParentId = null,BookId = "4",Version = 0,FileId = "1234", Text = "Fourth item", Description="This is an item description.", StartTime = baseDate.AddHours(5), EndTime = baseDate.AddHours(6), Value=9.834, IsFile = false , FilesCount = 0, Url = null},
                new Item { Id = "5",ParentId = null,BookId = "5",Version = 0,FileId = "1235", Text = "Fifth item", Description="This is an item description.", StartTime = baseDate.AddHours(9), EndTime = baseDate.AddHours(12), Value=3.287 , IsFile = false , FilesCount = 0, Url = null},
                new Item { Id = "6",ParentId = null,BookId = "6",Version = 0,FileId = "1236", Text = "Sixth item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 0 , Url = null},
                new Item { Id = "7",ParentId = "1",BookId = "7",Version = 0,FileId = "1237", Text = "First Sub item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 1,Url= null},
                new Item { Id = "8",ParentId = "1",BookId = "8",Version = 0,FileId = "1238", Text = "First Sub item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 0, Url = null},
                new Item { Id = "9",ParentId = "1",BookId = "9",Version = 0,FileId = "1239", Text = "First Sub item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 0, Url = null},
                new Item { Id = "10",ParentId = "2",BookId = "10",Version = 0,FileId = "1241", Text = "Second Sub item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 0, Url = null},
                new Item { Id = "11",ParentId = "7",BookId = "11",Version = 0,FileId = "1242", Text = "Second Sub item", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = false , FilesCount = 2, Url = null},
                new Item { Id = "12",ParentId = "11",BookId = "12",Version = 1,FileId = "1243", Text = "First File", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = true , FilesCount = 0,  Url="202242510133234_0_36367.pdf"},
                new Item { Id = "13",ParentId = "11",BookId = "13",Version = 0,FileId = "1244", Text = "Seconde File", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = true , FilesCount = 0,Url= "202242510118245_0_41411.pdf"},
                new Item { Id = "14",ParentId = "10",BookId = "14",Version = 0,FileId = "1245", Text = "Seconde File", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = true , FilesCount = 0,Url= "2022425101335324_0_51308.pdf"},
                new Item { Id = "15",ParentId = "10",BookId = "15",Version = 0,FileId = "1246", Text = "Seconde File", Description="This is an item description.", StartTime = baseDate.AddHours(12), EndTime = baseDate.AddHours(15), Value=81.2 , IsFile = true , FilesCount = 0,Url= "2022425101417289_0_50734.pdf"},

            };
        }



        public async Task SaveBooks()
        {

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            this.items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = this.items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            this.items.Remove(oldItem);
            this.items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = this.items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            this.items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(this.items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<Item>> GetItemsList(string id) {
            return await Task.FromResult(this.items.Where(q => q.ParentId == id).ToList());
        }

        public async Task<List<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(this.items.Where(q => q.ParentId == null).ToList());
        }

        //public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    return await Task.FromResult(this.items.Where(q =>q.ParentId == null));
        //}
    }
}