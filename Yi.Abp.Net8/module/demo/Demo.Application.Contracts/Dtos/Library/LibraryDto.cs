using Demo.Domain.Shared.Enums;
using Volo.Abp.Application.Dtos;

namespace Demo.Application.Contracts.Dtos.Library
{
    public class LibraryDto : AuditedEntityDto<Guid>
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Stock { get; set; }
    }
}
