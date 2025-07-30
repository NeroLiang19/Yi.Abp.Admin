using System;
using Demo.Domain.Shared.Enums;
using Volo.Abp.Domain.Entities.Events;

namespace Demo.Domain.Events
{
    /// <summary>
    ///     图书状态改变事件
    /// </summary>
    public class BookStatusChangedEvent : DomainEventEntry
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        public BookStatusChangedEvent(Guid bookId, BookTypeEnum oldStatus, BookTypeEnum newStatus)
            : base(bookId, new { OldStatus = oldStatus, NewStatus = newStatus }, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
        }
    }
}
