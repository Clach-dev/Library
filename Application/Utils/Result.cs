using System.Net;

namespace Application.Utils;

public record Result<T>(bool IsSuccess, HttpStatusCode StatusCode, T? Value, string[] Errors);