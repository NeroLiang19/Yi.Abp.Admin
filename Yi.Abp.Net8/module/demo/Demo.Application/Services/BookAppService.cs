using Demo.Application.Contracts.Dtos.Book;
using Demo.Application.Contracts.IServices;
using Demo.Domain.Entities;
using Demo.Domain.Events;
using Volo.Abp.EventBus.Local;
using SqlSugar;
using Volo.Abp.Application.Dtos;
using Yi.Framework.Ddd.Application;
using Yi.Framework.SqlSugarCore.Abstractions;
using Volo.Abp.ObjectMapping;
using Demo.Application.Contracts.Dtos.Library;

namespace Demo.Application.Services
{
    public class BookAppService :
           YiCrudAppService<
               BookAggregateRoot,
               BookDto,
               Guid,
               BookGetListInputVo,
               BookCreateUpdateDto>,
           IBookAppService
    {
        private readonly ISqlSugarRepository<BookAggregateRoot, Guid> _repository;
        private readonly ILocalEventBus _localEventBus;

        public BookAppService(
            ISqlSugarRepository<BookAggregateRoot, Guid> repository,
            ILocalEventBus localEventBus)
            : base(repository)
        {
            _repository = repository;
            _localEventBus = localEventBus;            
        }

      

        /// <summary>
        ///     获取列表
        /// </summary>
        public override async Task<PagedResultDto<BookDto>> GetListAsync(BookGetListInputVo input)
        {
            RefAsync<int> total = 0;
            var entities = await _repository._DbQueryable
                .WhereIF(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name!))
                .WhereIF(input.Type is not null, u => u.Type == input.Type)
                .OrderBy(u => u.PublishDate)
                .ToListAsync();
            return new PagedResultDto<BookDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        /// <summary>
        ///     获取分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BookDto>> GetPageAsync(BookGetListInputVo input)
        {
            RefAsync<int> total = 0;
            var entities = await _repository._DbQueryable
                .WhereIF(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name!))
                .WhereIF(input.Type is not null, u => u.Type == input.Type)
                .OrderBy(u => u.PublishDate)
                .ToPageListAsync(input.SkipCount, input.MaxResultCount, total);
            return new PagedResultDto<BookDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        /// <summary>
        ///     创建
        /// </summary>
        public override async Task<BookDto> CreateAsync(BookCreateUpdateDto input)
        {
            var book = await base.CreateAsync(input);
            // 新书入库
            await _localEventBus.PublishAsync(new Demo.Domain.Events.BookCreatedEvent(book.Id, book.Name, book.Type, DateTime.Now));
            return book;
        }

        /// <summary>
        ///     更新
        /// </summary>
        public override async Task<BookDto> UpdateAsync(Guid id, BookCreateUpdateDto input)
        {
            var oldBook = await _repository.GetAsync(id);
            var updatedBook = await base.UpdateAsync(id, input);
            // 可补充字段变更对比逻辑
            var changes = new System.Collections.Generic.Dictionary<string, (object, object)>
            {
                { "Name", (oldBook.Name, input.Name) },
                { "Type", (oldBook.Type, input.Type) },
                { "Price", (oldBook.Price, input.Price) }
            };

            //ExtraPropertyDictionary 是 ABP 框架提供的一个用于动态扩展实体属性的字典类型。
            // 以下是赋值和读取扩展属性示例：默认出版社
            var defaultBook = new BookAggregateRoot();
            defaultBook.ExtraProperties["Publisher"] = "人民文学出版社";
            // 存在该扩展属性
            if (oldBook.ExtraProperties.ContainsKey("Publisher"))
            {
                //读取扩展属性
                changes.Add("Publisher", (oldBook.ExtraProperties["Publisher"]?.ToString(), defaultBook.ExtraProperties["Publisher"]?.ToString()));
            }

            // 图书信息更新
            await _localEventBus.PublishAsync(new Demo.Domain.Events.BookUpdatedEvent(id, changes));
            return updatedBook;
        }

        /// <summary>
        ///     删除
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
            // 图书下架
            await _localEventBus.PublishAsync(new Demo.Domain.Events.BookDeletedEvent(id, "下架操作"));
        }

        /// <summary>
        ///     改变图书状态
        /// </summary>
        public async Task ChangeBookStatusAsync(Guid id, Demo.Domain.Shared.Enums.BookTypeEnum newStatus)
        {
            var book = await _repository.GetAsync(id);
            var oldStatus = book.Type;
            book.Type = newStatus;
            await _repository.UpdateAsync(book);
            // 图书状态变更
            await _localEventBus.PublishAsync(new Demo.Domain.Events.BookStatusChangedEvent(id, oldStatus, newStatus));
        }
    }
}
