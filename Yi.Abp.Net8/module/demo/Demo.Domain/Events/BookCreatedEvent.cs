using Demo.Domain.Shared.Enums;
using Volo.Abp.Domain.Entities.Events;

namespace Demo.Domain.Events
{
    /// <summary>
    ///     图书创建事件
    /// </summary>
    public class BookCreatedEvent : DomainEventEntry
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        public BookCreatedEvent(Guid bookId, string bookName, BookTypeEnum bookType, DateTime createdTime)
            : base(bookId, new { Name = bookName, Type = bookType, CreatedTime = createdTime }, createdTime.Ticks)
        {
            BookId = bookId;
            BookName = bookName;
            BookType = bookType;
            CreatedTime = createdTime;
        }

        /// <summary>
        ///     图书ID
        /// </summary>
        public Guid BookId { get; }
        /// <summary>
        ///     图书名称
        /// </summary>
        public string BookName { get; }
        /// <summary>
        ///     图书类型
        /// </summary>
        public BookTypeEnum BookType { get; }
        /// <summary>
        ///     图书创建时间
        /// </summary>
        public DateTime CreatedTime { get; }
    }
}
