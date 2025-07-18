namespace Yi.Framework.WeChat.MiniProgram;

public class WeChatMiniProgramException : Exception
{
    public WeChatMiniProgramException()
    {
    }

    public WeChatMiniProgramException(string message)
        : base(message)
    {
    }

    public WeChatMiniProgramException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public override string Message =>
        // 加上前缀
        "微信Api异常: " + base.Message;
}