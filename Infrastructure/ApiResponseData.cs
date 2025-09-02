public class ApiResponseData<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponseData(T data, string message = "Success")
    {
        Success = true;
        Message = message;
        Data = data;
    }

    public ApiResponseData(string message)
    {
        Success = false;
        Message = message;
        Data = default(T);
    }
}