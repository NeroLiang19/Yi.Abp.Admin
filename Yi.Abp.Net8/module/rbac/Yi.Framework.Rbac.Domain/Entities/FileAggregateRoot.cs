using SqlSugar;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Yi.Framework.Core.Enums;
using Yi.Framework.Core.Helper;

namespace Yi.Framework.Rbac.Domain.Entities;

[SugarTable("File")]
public class FileAggregateRoot : AggregateRoot<Guid>, IAuditedObject
{
    public FileAggregateRoot()
    {
    }

    /// <summary>
    ///     创建文件
    /// </summary>
    /// <param name="fileId">文件标识id</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileSize">文件大小</param>
    public FileAggregateRoot(Guid fileId, string fileName, decimal fileSize)
    {
        Id = fileId;
        FileSize = fileSize;
        FileName = fileName;

        var type = GetFileType();

        var savePath = GetSaveFilePath();
        var filePath = Path.Combine(savePath, FileName);
        FilePath = filePath;
    }

    /// <summary>
    ///     文件大小
    /// </summary>
    [SugarColumn(ColumnName = "FileSize")]
    public decimal FileSize { get; internal set; }

    /// <summary>
    ///     文件名
    /// </summary>
    [SugarColumn(ColumnName = "FileName")]
    public string FileName { get; internal set; }

    /// <summary>
    ///     文件路径
    /// </summary>
    [SugarColumn(ColumnName = "FilePath")]
    public string FilePath { get; internal set; }

    public DateTime CreationTime { get; set; }
    public Guid? CreatorId { get; set; }

    public Guid? LastModifierId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    ///     检测目录是否存在，不存在便创建
    /// </summary>
    public void CheckDirectoryOrCreate()
    {
        var savePath = GetSaveDirPath();
        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
    }

    /// <summary>
    ///     文件类型
    /// </summary>
    /// <returns></returns>
    public FileTypeEnum GetFileType()
    {
        return MimeHelper.GetFileType(FileName);
    }

    /// <summary>
    ///     获取文件mime
    /// </summary>
    /// <returns></returns>
    public string GetMimeMapping()
    {
        return MimeHelper.GetMimeMapping(FileName) ?? @"text/plain";
    }

    /// <summary>
    ///     落库目录路径
    /// </summary>
    /// <returns></returns>
    public string GetSaveDirPath()
    {
        return $"wwwroot/{GetFileType()}";
    }

    /// <summary>
    ///     落库文件路径
    /// </summary>
    /// <returns></returns>
    public string GetSaveFilePath()
    {
        var savefileName = GetSaveFileName();
        return Path.Combine(GetSaveDirPath(), savefileName);
    }

    /// <summary>
    ///     获取保存的文件名
    /// </summary>
    /// <returns></returns>
    public string GetSaveFileName()
    {
        return Id + Path.GetExtension(FileName);
    }

    /// <summary>
    ///     检测，并且返回缩略图的保存路径
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <returns></returns>
    public string GetAndCheakThumbnailSavePath(bool isCheak = false)
    {
        var thumbnailPath = $"wwwroot/{FileTypeEnum.thumbnail}";
        if (isCheak)
            if (!Directory.Exists(thumbnailPath))
                Directory.CreateDirectory(thumbnailPath);

        return Path.Combine(thumbnailPath, GetSaveFileName());
    }


    /// <summary>
    ///     获取查询的的文件路径
    /// </summary>
    /// <param name="file"></param>
    /// <param name="isThumbnail"></param>
    /// <returns></returns>
    public string? GetQueryFileSavePath(bool? isThumbnail)
    {
        string fileSavePath;
        //如果为缩略图，需要修改路径
        if (isThumbnail is true)
            fileSavePath = GetAndCheakThumbnailSavePath();
        else
            fileSavePath = GetSaveFilePath();
        return fileSavePath;
    }
}