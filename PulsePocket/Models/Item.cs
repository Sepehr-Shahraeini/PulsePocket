using DevExpress.Xpo.Helpers;

namespace PulsePocket.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Value { get; set; }
        public bool IsFile { get; set; }
        public int FilesCount { get; set; }

        public List<Item> Files { get; set; }

        public bool FileVisible { get; set; }
        public string Url { get; set; }

        public int Version { get; set; }
        public string FileId { get; set; }
        public string BookId { get; set; }

    }
}