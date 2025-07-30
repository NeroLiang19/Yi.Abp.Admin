using Demo.Domain.Entities;
using Demo.Domain.Events;
using SqlSugar;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.Application.EventHandlers
{
    /// <summary>
    ///     图书创建事件处理者
    /// </summary>
    public class BookCreatedEventHandler : ILocalEventHandler<BookCreatedEvent>, ITransientDependency
    {
        private readonly ISqlSugarRepository<LibraryAggregateRoot, Guid> _libraryRepository;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public BookCreatedEventHandler(ISqlSugarRepository<LibraryAggregateRoot, Guid> libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        /// <summary>
        ///     处理图书创建事件
        /// </summary>
        [UnitOfWork]
        public async Task HandleEventAsync(BookCreatedEvent eventData)
        {
            var existingBook = await _libraryRepository._DbQueryable.FirstAsync(x => x.Name == eventData.BookName);
            if (existingBook != null)
            {
                existingBook.Stock += 1;
                await _libraryRepository.UpdateAsync(existingBook);
            }
            else
            {
                var newLibraryEntry = new LibraryAggregateRoot
                {
                    BookId = eventData.BookId,
                    Name = eventData.BookName,
                    Location = "node",
                    Stock = 1
                };
                await _libraryRepository.InsertAsync(newLibraryEntry);
            }
        }
    }
}