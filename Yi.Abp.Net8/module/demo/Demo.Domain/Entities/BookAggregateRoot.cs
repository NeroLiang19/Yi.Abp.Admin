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
        [SugarColumn(IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public string Name { get; set; }

        public BookTypeEnum Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        [SugarColumn(IsIgnore = true)]
        public override ExtraPropertyDictionary ExtraProperties { get; protected set; }        
    }
}
