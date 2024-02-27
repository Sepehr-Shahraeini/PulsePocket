using DevExpress.XtraRichEdit.SpellChecker;
using PulsePocket.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulsePocket.Services
{
    public class BookDatabase
    {
        SQLiteAsyncConnection Database;

        public BookDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Book>();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            await Init();
            return await Database.Table<Book>().ToListAsync();
        }


        public async Task<Book> GetBookAsync(string fileId, string bookId)
        {
            await Init();
            return await Database.Table<Book>().Where(i => i.FileId == fileId && i.BookId == bookId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveBookAsync(Book Book)
        {
            await Init();
            if (Book.Id != 0)
                return await Database.UpdateAsync(Book);
            else
                return await Database.InsertAsync(Book);
        }

        public async Task<int> DeleteBookAsync(Book Book)
        {
            await Init();
            return await Database.DeleteAsync(Book);
        }
    }
}
