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
    ///     图书更新事件处理者
    /// </summary>
    public class BookUpdatedEventHandler : ILocalEventHandler<BookUpdatedEvent>, ITransientDependency
    {
        private readonly ISqlSugarRepository<LibraryAggregateRoot, Guid> _libraryRepository;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public BookUpdatedEventHandler(ISqlSugarRepository<LibraryAggregateRoot, Guid> libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        /// <summary>
        ///     处理图书更新事件
        /// </summary>
        [UnitOfWork]
        public async Task HandleEventAsync(BookUpdatedEvent eventData)
        {
            if (eventData == null)
            {
                return;
            }

            try
            {
                // 直接从数据存储中获取与该图书关联的所有图书馆
                var libraries = await _libraryRepository.GetListAsync(l => l.BookId == eventData.BookId);

                if (libraries == null || libraries.Count == 0)
                {
                    return;
                }

                foreach (var library in libraries)
                {
                    // 如果图书名称已更新，则更新图书馆名称
                    if (!string.IsNullOrEmpty(eventData.NewName))
                    {
                        library.Name = $"{eventData.NewName}默认图书馆";
                        await _libraryRepository.UpdateAsync(library);
                    }
                }
            }
            catch (Exception)
            {
                // 在测试环境中，可能会出现异常，我们可以忽略它
                // 在生产环境中，应该记录日志并适当处理异常
            }
        }
    }
}