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
        [SugarColumn(IsPrimaryKey = true)]
        public override Guid Id { get; protected set; }
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Stock { get; set; }
        
        [SugarColumn(IsIgnore = true)]
        public override ExtraPropertyDictionary ExtraProperties { get; protected set; }
    }
}