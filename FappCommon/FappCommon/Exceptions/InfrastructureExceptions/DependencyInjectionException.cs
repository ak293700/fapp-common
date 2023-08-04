using System.Diagnostics.CodeAnalysis;
using FappCommon.Exceptions.InfrastructureExceptions.Base;

namespace FappCommon.Exceptions.InfrastructureExceptions;

public class DependencyInjectionException : InfrastructureException
{
    public DependencyInjectionException(string message) : base(message)
    {
    }
    
    public static DependencyInjectionException Instance => new DependencyInjectionException("Unable to resolve the dependency");

    public static DependencyInjectionException GenerateException([ConstantExpected] string dependencyName)
    {
        return new DependencyInjectionException(
            $"""Unable to resolve the dependency ${dependencyName}""");
    }
}