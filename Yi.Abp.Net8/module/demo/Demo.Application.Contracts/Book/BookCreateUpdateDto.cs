using System.ComponentModel.DataAnnotations;
using Demo.Domain.Shared.Enums;

namespace Demo.Application.Contracts.Dtos.Book
{

    public class BookCreateUpdateDto
        {
            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public BookTypeEnum Type { get; set; } = BookTypeEnum.Undefined;

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;

            [Required]
            public float Price { get; set; }
        }
  
}
