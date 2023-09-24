using ProgerTasks.Domain.Enum;

namespace ProgerTasks.Domain.Response;

public interface IBaseResponse<T>
{
    string Description { get; }
    
    StatusCode StatusCode { get; }
    
    T Data { get; }
}