using Microsoft.AspNetCore.SignalR;

namespace Library.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string BName { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }
    }
}
