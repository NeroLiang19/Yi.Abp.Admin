using System.ComponentModel.DataAnnotations;
using Demo.Domain.Shared.Enums;

namespace Demo.Application.Contracts.Dtos.Library
{

    public class LibraryCreateUpdateDto
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public int Stock { get; set; }
    }
  
}
