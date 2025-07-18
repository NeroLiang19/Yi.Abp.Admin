using Yi.Abp.Tool.Domain.Shared.Enums;

namespace Yi.Abp.Tool.Domain.Shared.Dtos;

public class TemplateGenCreateDto
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     模块所属gitee分支
    /// </summary>
    public string GiteeRef { get; set; }

    /// <summary>
    ///     数据库提供者
    /// </summary>
    public DbmsEnum Dbms { get; set; }


    /// <summary>
    ///     需要替换的字符串内容
    /// </summary>
    public Dictionary<string, string> ReplaceStrData { get; set; }

    public void SetTemplateGiteeRef(string moduleType)
    {
        GiteeRef = moduleType.ToLower();
    }
}