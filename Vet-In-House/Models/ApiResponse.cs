namespace VetInHouse.Models;

public record ApiResponse<T>(bool Success, T? Data, string? Message)
{
    public static ApiResponse<T> Ok(T data) => new(true, data, null);
    public static ApiResponse<T> Fail(string message) => new(false, default, message);
}

public record ApiResponse(bool Success, string? Message)
{
    public static ApiResponse Ok(string? message = null) => new(true, message);
    public static ApiResponse Fail(string message) => new(false, message);
}
