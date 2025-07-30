using Demo.Application.Contracts.Dtos.Library;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application.Contracts;

namespace Demo.Application.Contracts.IServices
{
    public interface ILibraryAppService :
      IYiCrudAppService< //Defines CRUD methods
          LibraryDto, //Used to show Librarys
          Guid, //Primary key of the Library entity
          PagedAndSortedResultRequestDto, //Used for paging/sorting
          LibraryCreateUpdateDto> //Used to create/update a Library
    {

    }
}