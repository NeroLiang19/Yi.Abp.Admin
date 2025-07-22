using System;

namespace Demo.Application.Contracts.Books.Dtos
{
    public class CreateUpdateBookDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}