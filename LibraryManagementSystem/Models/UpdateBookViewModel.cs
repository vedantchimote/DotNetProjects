namespace Library.Models
{
    public class UpdateBookViewModel
    {

        public Guid Id { get; set; }
        public string BName { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }

    }
}
