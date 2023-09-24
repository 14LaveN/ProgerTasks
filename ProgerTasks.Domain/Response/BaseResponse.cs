using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public required string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}