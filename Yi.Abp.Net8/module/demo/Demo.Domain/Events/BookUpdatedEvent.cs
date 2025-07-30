using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events;

namespace Demo.Domain.Events
{
    /// <summary>
    ///     图书更新事件
    /// </summary>
    public class BookUpdatedEvent : DomainEventEntry
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        public BookUpdatedEvent(Guid bookId, Dictionary<string, (object OldValue, object NewValue)> changes)
            : base(bookId, changes, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
        {
        }
    }
}
