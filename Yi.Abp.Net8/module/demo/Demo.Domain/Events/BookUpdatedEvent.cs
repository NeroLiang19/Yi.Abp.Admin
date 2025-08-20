using System;
using System.Collections.Generic;
using Demo.Domain.Shared.Enums;

namespace Demo.Domain.Events
{
    /// <summary>
    /// 图书更新事件
    /// </summary>
    public class BookUpdatedEvent
    {
        /// <summary>
        /// 图书ID
        /// </summary>
        public Guid BookId { get; }
        
        /// <summary>
        /// 新的图书名称
        /// </summary>
        public string NewName { get; }
        
        /// <summary>
        /// 变更字段集合
        /// </summary>
        public Dictionary<string, (object OldValue, object NewValue)> Changes { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <param name="changes">变更字段集合</param>
        public BookUpdatedEvent(Guid bookId, Dictionary<string, (object, object)> changes)
        {
            BookId = bookId;
            Changes = changes;
            
            // 尝试从变更集合中获取名称变更
            if (changes.TryGetValue("Name", out var nameChange))
            {
                NewName = nameChange.Item2?.ToString();
            }
            else
            {
                NewName = null;
            }
        }
        
        /// <summary>
        /// 构造函数 - 仅更新名称
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <param name="newName">新的图书名称</param>
        public BookUpdatedEvent(Guid bookId, string newName)
        {
            BookId = bookId;
            NewName = newName;
            Changes = new Dictionary<string, (object, object)>
            {
                { "Name", (null, newName) }
            };
        }
    }
}