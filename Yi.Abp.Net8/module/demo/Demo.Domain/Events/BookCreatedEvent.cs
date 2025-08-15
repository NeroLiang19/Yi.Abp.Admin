using System;
using Demo.Domain.Shared.Enums;

namespace Demo.Domain.Events
{
    /// <summary>
    /// 图书创建事件
    /// </summary>
    public class BookCreatedEvent
    {
        /// <summary>
        /// 图书ID
        /// </summary>
        public Guid BookId { get; }
        
        /// <summary>
        /// 图书名称
        /// </summary>
        public string BookName { get; }
        
        /// <summary>
        /// 图书类型
        /// </summary>
        public BookTypeEnum BookType { get; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <param name="bookName">图书名称</param>
        /// <param name="bookType">图书类型</param>
        /// <param name="creationTime">创建时间</param>
        public BookCreatedEvent(Guid bookId, string bookName, BookTypeEnum bookType, DateTime? creationTime = null)
        {
            BookId = bookId;
            BookName = bookName;
            BookType = bookType;
            CreationTime = creationTime ?? DateTime.Now;
        }
    }
}