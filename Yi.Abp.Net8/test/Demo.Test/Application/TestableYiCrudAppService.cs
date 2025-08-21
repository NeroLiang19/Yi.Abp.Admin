using Demo.Application.Services;
using Demo.Domain.Entities;
using Volo.Abp.EventBus.Local;
using Volo.Abp.ObjectMapping;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.Test.Application
{
    /// <summary>
    /// 通用的可测试AppService基类，解决单元测试中ObjectMapper为空的问题
    /// 使用这个基类可以避免在每个测试中都要配置复杂的ObjectMapper
    /// 
    /// 使用方法：
    /// 1. 继承这个基类并实现必要的映射方法
    /// 2. 在测试中设置 TestObjectMapper 属性
    /// 3. 配置必要的映射行为
    /// </summary>
    public abstract class TestableBookAppServiceBase : BookAppService
    {
        public IObjectMapper TestObjectMapper { get; set; }

        protected TestableBookAppServiceBase(
            ISqlSugarRepository<BookAggregateRoot, Guid> repository,
            ILocalEventBus localEventBus)
            : base(repository, localEventBus)
        {
        }

        protected IObjectMapper GetObjectMapper() => TestObjectMapper ?? ObjectMapper;
    }
}