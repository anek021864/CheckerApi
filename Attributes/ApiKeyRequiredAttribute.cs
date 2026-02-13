namespace JigNetApi;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class ApiKeyRequiredAttribute : Attribute { }
