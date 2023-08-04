using System.Diagnostics.CodeAnalysis;
using FappCommon.Exceptions.InfrastructureExceptions.Base;

namespace FappCommon.Exceptions.InfrastructureExceptions.ConfigurationExceptions.Base;

public class ConfigurationException : InfrastructureException
{
    public ConfigurationException(string message) : base(message)
    {}
    
    public class ValueNotFoundException : ConfigurationException
    {
        public ValueNotFoundException(string message) : base(message)
        {}

        public static ValueNotFoundException Instance => new ValueNotFoundException("The value was not found in the configuration file");

        public static ValueNotFoundException GenerateException([ConstantExpected] string propertyName)
        {
            return new ValueNotFoundException(
                $"""The value "${propertyName}" was not found in the configuration file""");
        }
        
    }
}