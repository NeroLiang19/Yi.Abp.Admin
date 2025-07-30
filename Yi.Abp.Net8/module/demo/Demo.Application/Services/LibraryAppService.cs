using Demo.Application.Contracts.Dtos.Library;
using Demo.Application.Contracts.IServices;
using Demo.Domain.Entities;
using Demo.Domain.Events;
using Volo.Abp.EventBus.Local;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;


namespace Demo.Application.Services
{
    public class LibraryAppService :
           YiCrudAppService<
               LibraryAggregateRoot,
               LibraryDto,
               Guid,
               PagedAndSortedResultRequestDto,
               LibraryCreateUpdateDto>,
           ILibraryAppService
    {
        private readonly ISqlSugarRepository<LibraryAggregateRoot, Guid> _repository;

        public LibraryAppService(ISqlSugarRepository<LibraryAggregateRoot, Guid> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}
