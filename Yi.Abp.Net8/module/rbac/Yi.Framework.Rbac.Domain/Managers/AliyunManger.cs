using System.Text.Json;
using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dysmsapi20170525;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Domain.Services;
using Yi.Framework.Rbac.Domain.Shared.Options;

namespace Yi.Framework.Rbac.Domain.Managers;

public class AliyunManger : DomainService, IAliyunManger
{
    private readonly ILogger<AliyunManger> _logger;

    public AliyunManger(ILogger<AliyunManger> logger, IOptions<AliyunOptions> options)
    {
        Options = options.Value;
        _logger = logger;
    }

    private AliyunOptions Options { get; }


    /// <summary>
    ///     发送短信
    /// </summary>
    /// <param name="phoneNumbers"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task SendSmsAsync(string phoneNumbers, string code)
    {
        try
        {
            var _aliyunClient = CreateClient();
            var sendSmsRequest = new SendSmsRequest
            {
                PhoneNumbers = phoneNumbers,
                SignName = Options.Sms.SignName,
                TemplateCode = Options.Sms.TemplateCode,
                TemplateParam = JsonSerializer.Serialize(new { code })
            };

            var response = await _aliyunClient.SendSmsAsync(sendSmsRequest);
        }

        catch (Exception _error)
        {
            _logger.LogError(_error, "阿里云短信发送错误:" + _error.Message);
            throw new UserFriendlyException("阿里云短信发送错误:" + _error.Message);
        }
    }

    private Client CreateClient()
    {
        var config = new Config
        {
            // 必填，您的 AccessKey ID
            AccessKeyId = Options.AccessKeyId,
            // 必填，您的 AccessKey Secret
            AccessKeySecret = Options.AccessKeySecret
        };
        // 访问的域名
        config.Endpoint = "dysmsapi.aliyuncs.com";
        return new Client(config);
    }
}

public interface IAliyunManger
{
    Task SendSmsAsync(string phoneNumbers, string code);
}