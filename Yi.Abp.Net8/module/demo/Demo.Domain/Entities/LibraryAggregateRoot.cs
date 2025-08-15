using System;
using SqlSugar;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Demo.Domain.Entities
{
    /// <summary>
    ///     图书馆实体类
    /// </summary>
    [SugarTable("Library")]

    public class LibraryAggregateRoot : AggregateRoot<Guid>
    {
        public LibraryAggregateRoot()
        {
        }
        public LibraryAggregateRoot(Guid id, Guid bookId, string name, string location, int stock = 0)
        {
            Id = id;
            BookId = bookId;
            Name = name;
            Location = location;
            Stock = stock;
            ExtraProperties = new ExtraPropertyDictionary();
        }
        [SugarColumn(IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Stock { get; set; }
        
        [SugarColumn(IsIgnore = true)]
        public override ExtraPropertyDictionary ExtraProperties { get; protected set; }
        
        /// <summary>
        /// 更新关联的图书ID
        /// </summary>
        /// <param name="bookId">新的图书ID</param>
        public void UpdateBookId(Guid bookId)
        {
            BookId = bookId;
        }
        
        /// <summary>
        /// 更新图书馆名称
        /// </summary>
        /// <param name="name">新名称</param>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("图书馆名称不能为空", nameof(name));
            }
            
            Name = name;
        }
        
        /// <summary>
        /// 更新图书馆位置
        /// </summary>
        /// <param name="location">新位置</param>
        public void UpdateLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentException("图书馆位置不能为空", nameof(location));
            }
            
            Location = location;
        }
        
        /// <summary>
        /// 更新库存数量
        /// </summary>
        /// <param name="stock">新库存数量</param>
        public void UpdateStock(int stock)
        {
            if (stock < 0)
            {
                throw new ArgumentException("库存数量不能为负数", nameof(stock));
            }
            
            Stock = stock;
        }
    }
}