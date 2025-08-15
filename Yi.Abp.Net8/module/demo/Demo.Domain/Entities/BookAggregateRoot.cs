using System;
using Demo.Domain.Shared.Enums;
using SqlSugar;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Demo.Domain.Entities
{
    /// <summary>
    ///     图书实体类
    /// </summary>
    [SugarTable("Book")]
    public class BookAggregateRoot : AuditedAggregateRoot<Guid>
    {
        public BookAggregateRoot()
        {
        }

        public BookAggregateRoot(Guid id, string name, BookTypeEnum type = BookTypeEnum.Undefined, DateTime? publishDate = null, float? price = null)
        {
            Id = id;
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
            ExtraProperties = new ExtraPropertyDictionary();
        }

        [SugarColumn(IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public string Name { get; set; }

        public BookTypeEnum Type { get; set; }

        public DateTime? PublishDate { get; set; }

        public float? Price { get; set; }

        [SugarColumn(IsIgnore = true)]
        public override ExtraPropertyDictionary ExtraProperties { get; protected set; }
        
        /// <summary>
        /// 更新图书名称
        /// </summary>
        /// <param name="name">新名称</param>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("图书名称不能为空", nameof(name));
            }
            
            Name = name;
        }
        
        /// <summary>
        /// 更新图书类型
        /// </summary>
        /// <param name="type">新类型</param>
        public void UpdateType(BookTypeEnum type)
        {
            Type = type;
        }
        
        /// <summary>
        /// 更新出版日期
        /// </summary>
        /// <param name="publishDate">新出版日期</param>
        public void UpdatePublishDate(DateTime? publishDate)
        {
            PublishDate = publishDate;
        }
        
        /// <summary>
        /// 更新价格
        /// </summary>
        /// <param name="price">新价格</param>
        public void UpdatePrice(float? price)
        {
            if (price.HasValue && price.Value < 0)
            {
                throw new ArgumentException("价格不能为负数", nameof(price));
            }
            
            Price = price;
        }
    }
}