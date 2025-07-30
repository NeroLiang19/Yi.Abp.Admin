using Demo.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Application.Contracts;

namespace Demo.Application.Contracts.Dtos.Book
{
    public class BookGetListInputVo : PagedAllResultRequestDto
    {
        public string? Name { get; set; }

        public BookTypeEnum? Type { get; set; }
    }
}
