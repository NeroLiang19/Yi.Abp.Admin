namespace Yi.Framework.SqlSugarCore.Abstractions;

/// <summary>
///     默认租户表特性
///     标记此特性的实体类将在默认租户数据库中创建表
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DefaultTenantTableAttribute : Attribute
{
}