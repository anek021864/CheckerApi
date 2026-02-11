
using Microsoft.AspNetCore.Mvc;
namespace JigNetApi;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails WithMeta(this ProblemDetails problem, string code, string traceId)
    {
        problem.Extensions["code"] = code;
        problem.Extensions["traceId"] = traceId;
        problem.Extensions["timestamp"] = DateTimeOffset.UtcNow;
        return problem;
    }
}
