namespace Extensions.ApiContext;


public class MessageModel<TEntity>
{
    public int code { get; set; } = 200;
    public bool success { get; set; } = false;
    public string msg { get; set; } = "";
    public string msgDev { get; set; } = "";
    public TEntity response { get; set; }
}


public static class MessageModel
{
    public static MessageModel<T> Succeed<T>(T response = default, string msg = "成功", string msgDev = "")
    {
        return new MessageModel<T>
        {
            code = 200,
            success = true,
            msg = msg,
            msgDev = msgDev,
            response = response
        };
    }

    public static MessageModel<T> Failed<T>(T response = default, int httpCode = 400, string msg = "失败", string msgDev = "")
    {
        return new MessageModel<T>
        {
            code = httpCode,
            success = false,
            msg = msg,
            msgDev = msgDev,
            response = response
        };
    }
}