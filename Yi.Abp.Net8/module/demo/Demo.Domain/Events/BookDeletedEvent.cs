using System;
using Volo.Abp.Domain.Entities.Events;

namespace Demo.Domain.Events
{
    /// <summary>
    ///     图书删除事件
    /// </summary>
    public class BookDeletedEvent : DomainEventEntry
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        public BookDeletedEvent(Guid bookId, string reason)
            : base(bookId, reason, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
        }
    }
}
