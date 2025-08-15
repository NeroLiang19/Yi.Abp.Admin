using System;

namespace Demo.Domain.Events
{
    /// <summary>
    /// 图书删除事件
    /// </summary>
    public class BookDeletedEvent
    {
        /// <summary>
        /// 图书ID
        /// </summary>
        public Guid BookId { get; }
        
        /// <summary>
        /// 删除原因
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <param name="reason">删除原因</param>
        public BookDeletedEvent(Guid bookId, string reason = null)
        {
            BookId = bookId;
            Reason = reason ?? "未指定原因";
        }
    }
}