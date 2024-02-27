using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulsePocket.Models
{
    public class Book
    {
        [property: PrimaryKey]
        [property: AutoIncrement]
        public int Id { get; set; } 

        public string BookId { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsFile { get; set; }
        public int FilesCount { get; set; }
        public bool FileVisible { get; set; }
        public string Url { get; set; }
        public int Version { get; set; }
        public string FileId { get; set; }
    }
}
