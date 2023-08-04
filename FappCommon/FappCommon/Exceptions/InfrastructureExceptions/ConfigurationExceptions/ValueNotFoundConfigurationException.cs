using System.Diagnostics.CodeAnalysis;
using FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions.Base;

namespace FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions;

public class ValueNotFoundConfigurationException : ConfigurationException
{
    public ValueNotFoundConfigurationException(string message) : base(message)
    {
    }

    public static ValueNotFoundConfigurationException Instance =>
        new ValueNotFoundConfigurationException("The value was not found in the configuration file");

    public static ValueNotFoundConfigurationException GenerateException([ConstantExpected] string propertyName)
    {
        return new ValueNotFoundConfigurationException(
            $"""The value "${propertyName}" was not found in the configuration file""");
    }
}