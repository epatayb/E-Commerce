namespace dotnet_store.Services;

public class ServiceResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public string Type { get; set; } = "success";
}