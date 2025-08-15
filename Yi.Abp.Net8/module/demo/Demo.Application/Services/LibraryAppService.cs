using Demo.Application.Contracts.Dtos.Library;
using Demo.Application.Contracts.IServices;
using Demo.Domain.Entities;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.Application.Services
{
    /// <summary>
    /// 图书馆应用服务实现
    /// </summary>
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
        private readonly ISqlSugarRepository<BookAggregateRoot, Guid> _bookRepository;

        public LibraryAppService(
            ISqlSugarRepository<LibraryAggregateRoot, Guid> repository,
            ISqlSugarRepository<BookAggregateRoot, Guid> bookRepository)
            : base(repository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
        }

    }
}